using ethnofiles.validations.types;
using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using proxy.types;
using System;
using System.IO;
using System.Xml;

namespace FileapiCli.CommandHandlers
{
    public class SendEthnofilesCommandHandler : CliCommandHandler<SendEthnofilesCmd, SendEthnofilesCmdResult>
    {
        public SendEthnofilesCommandHandler(ILoggerFactory loggerFactory, ICliService cliService, ICommandValidator<SendEthnofilesCmd> validator)
        : base(loggerFactory, cliService, validator)
        {
        }

        protected override SendEthnofilesCmdResult DoHandle(SendEthnofilesCmd command)
        {
            var result = new SendEthnofilesCmdResult();

            //Retrieve incoming customer applications
            var retrieveCustomerApplicationsRequest = new RetrieveCustomerApplicationsRequest()
            {
                UserId = command.UserInfo.UserName,
                FileDirection = 0 //incoming
            };

            // get selected customer application
            var selectedCustomerApplication = _cliService.RetrieveCustomerApplication(retrieveCustomerApplicationsRequest, command.CustomerApplicationId);
            if (selectedCustomerApplication == null) return result;

            // create send to ethnofiles request
            var sendFileRequest = new SendFileRequest() {
                Filename = Path.GetFileName(command.InputFile),
                UserId = command.UserInfo.UserName,
                CustomerApplicationId = command.CustomerApplicationId,
                IsSmsOtp = command.IsSmsOtp,
                AcceptTerms = command.AcceptTerms,
                AcceptTrnTerms = command.AcceptTrnTerms,
                DebtorName = command.DebtorName,
                DebtorIBAN = HandleDebtorIban(command, selectedCustomerApplication),
                TotalRecords = HandleRowNum(command, selectedCustomerApplication),
                TotalAmount = HandleTotalSum(command, selectedCustomerApplication),
                TanNumber = HandleTanNumber(command),
                FileId = command.FileId
            };

            //handle xml conversion from sepa. if the file is converted sent the converted file's id from the Files db.
            var sepaConvertResponse = HandleSepaConvert(command, selectedCustomerApplication);
            if (sepaConvertResponse != null) sendFileRequest.FileId = sepaConvertResponse.FileId;

            //Validate ethnofiles file
            ValidateFileName(command);
            ValidateFileContent(command, sendFileRequest);

            //Send to ethnofiles
            var sendFileResponse = _cliService.SendFile(sendFileRequest);
            _logger.LogInformation($"File sent to ethnofiles succesfully, and received customer application file id : {sendFileResponse.CustomerApplicationFileId}");

            //handle sepa status
            if (sepaConvertResponse != null) 
                HandleSepaFileStatus(command.UserInfo.UserName, selectedCustomerApplication.ConversionId, sepaConvertResponse.SepaFileId); 

            return result;
        }

        private string HandleTanNumber(SendEthnofilesCmd command)
        {
            if (!command.IsSmsOtp == null || command.IsSmsOtp == false)
                return "bypass";

            return command.TanNumber;
        }

        private string HandleDebtorIban(SendEthnofilesCmd command, CustomerApplication selectedCustomerApplication)
        {
            if (HasAccountInput(selectedCustomerApplication)) return command.DebtorIban;

            return null;
        }

        private decimal? HandleTotalSum(SendEthnofilesCmd command, CustomerApplication selectedCustomerApplication)
        {
            if (selectedCustomerApplication.ValidationType != "countAndSum") return null;

            if (!String.IsNullOrEmpty(command.TotalSumFromPainXml) && command.TotalSumFromPainXml == "001" && !String.IsNullOrEmpty(command.InputFile))
            {
                var sepaPainFile = DeserializeXmlSEPA001ISO20022(command.InputFile);
                var ctrlSum = sepaPainFile?.Document?.CstmrCdtTrfInitn?.GrpHdr?.CtrlSum;
                return ctrlSum;
            }
            else return command.TotalAmount;
        }

        private int? HandleRowNum(SendEthnofilesCmd command, CustomerApplication selectedCustomerApplication)
        {
            if (selectedCustomerApplication.ValidationType == "none") return null;

            if (!String.IsNullOrEmpty(command.RowCountFromPainXml) && command.RowCountFromPainXml == "001" && !String.IsNullOrEmpty(command.InputFile))
            {
                var sepaPainFile = DeserializeXmlSEPA001ISO20022(command.InputFile);
                return sepaPainFile?.Document?.CstmrCdtTrfInitn?.GrpHdr?.NbOfTxs;
            }
            else return command.TotalRecords;
        }

        private SepaConvertResponse HandleSepaConvert(SendEthnofilesCmd command, CustomerApplication selectedCustomerApplication)
        {
            if (String.IsNullOrEmpty(selectedCustomerApplication.ConversionId)) return null;

            var request = new SepaConvertRequest() {
                UserId = command.UserInfo.UserName,
                UnconvertedFileId = command.FileId,
                CustomerApplicationId = int.Parse(selectedCustomerApplication.Id),
                IsXml = false,
                ConversionId = selectedCustomerApplication.ConversionId,
                DebtorName = command.DebtorName,
                DebtorIBAN = HandleDebtorIban(command, selectedCustomerApplication)
            };
            var result = _cliService.SepaConvert(request);
            return result; 
        }

        private void HandleSepaFileStatus(string username, string conversionId, string sepaFileId)
        {
            if (String.IsNullOrEmpty(conversionId)) return;

            var request = new SepaSetFileStatusAsSentRequest() {
                UserId = username,
                ConvId = conversionId,
                FileId = sepaFileId
            };
            var result = _cliService.SepaSetFileStatusAsSent(request);

        }

        private bool HasAccountInput(CustomerApplication selectedCustomerApplication)
        {
            return selectedCustomerApplication.ConversionId != null && selectedCustomerApplication.ConversionId.Length > 0 && (selectedCustomerApplication.ConversionId.Contains("Payroll") || selectedCustomerApplication.ConversionId.Contains("SHP"));
        }

        private SepaPainFile DeserializeXmlSEPA001ISO20022(string inputFile)
        {
            try
            {
                string xml = File.ReadAllText(inputFile);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                string jsonText = JsonConvert.SerializeXmlNode(doc);
                SepaPainFile sepaPainFile = JsonConvert.DeserializeObject<SepaPainFile>(jsonText);

                return sepaPainFile;
            }
            catch (Exception)
            {
                Console.WriteLine(Environment.NewLine + "An Error occured while deserializing the pain Xml file:" + Environment.NewLine);
                throw;
            }
        }

        private void ValidateFileName(SendEthnofilesCmd command)
        {
            string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";
            var fileDetails = _cliService.GetFile(command.FileId, requester, subject);

            var request = new ValidateFilenameRequest { 
                UserId = command.UserInfo.UserName, 
                FileTypeId = command.CustomerApplicationId, 
                FileName = fileDetails.FileName 
            };
            _cliService.ValidateEthnofilesFilename(request);
        }

        private void ValidateFileContent(SendEthnofilesCmd command, SendFileRequest sendFileRequest)
        {

            var request = new ValidateFileRequest
            {
                UserId = sendFileRequest.UserId,
                FileTypeId = sendFileRequest.CustomerApplicationId,
                TotalAmount = sendFileRequest.TotalAmount,
                TotalRecords = sendFileRequest.TotalRecords,
                FileApiFileId = sendFileRequest.FileId,
                ConvId = null, //this is empty so the validation api will not try to get the content from the sepa db
                SepaFileId = null
            };

            _cliService.ValidateEthnofilesFile(request);
        }
    }
}
