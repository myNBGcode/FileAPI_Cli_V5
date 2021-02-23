using ethnofiles.types;
using ethnofiles.validations.types;
using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace FileapiCli.CommandHandlers
{
    public class SendToEthnoFilesHandler : CliCommandHandler<SendFileToEthnoFilesCmd, SendFileToEthnoFilesCmdResult2>
    {
        public SendToEthnoFilesHandler(ILoggerFactory loggerFactory, ICliService cliService, ICommandValidator<SendFileToEthnoFilesCmd> validator)
            : base(loggerFactory, cliService, validator)
        {
        }
        protected override SendFileToEthnoFilesCmdResult2 DoHandle(SendFileToEthnoFilesCmd command)
        {
            //Populate File Type
            var result = new SendFileToEthnoFilesCmdResult2();
            var selectedFileType = _cliService.PopulateFileTypes(new PopulateFiletypesRequest { UserId = command.UserInfo.UserName }, command.Idfield);
            if (selectedFileType == null) return result;

            
            //Create Request
            var sendFileToEthnofilesRequest = CreateSendFileToEthnofilesRequest(command, selectedFileType);

            //Validate
            ValidateFileName(command);
            ValidateFileContent(command, sendFileToEthnofilesRequest);

            //Send
            _cliService.SendToEthnofiles(sendFileToEthnofilesRequest);

            return result;
        }

        private void ValidateFileName(SendFileToEthnoFilesCmd command)
        {
            string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";
            var fileDetails = _cliService.GetFile(Guid.Parse(command.FileId), requester, subject);

            int.TryParse(command.Idfield, out int fileTypeId);
            var request = new ValidateFilenameRequest { UserId = command.UserInfo.UserName, FileTypeId = fileTypeId, FileName = fileDetails.FileName };
            _cliService.ValidateEthnofilesFilename(request);
        }

        private void ValidateFileContent(SendFileToEthnoFilesCmd command, SendFileToEthnofilesRequest sendFileToEthnofilesRequest)
        {

            var request = new ValidateFileRequest { 
                UserId = sendFileToEthnofilesRequest.UserID,
                FileTypeId = int.Parse(sendFileToEthnofilesRequest.FileTypeId),
                TotalAmount = (decimal?)sendFileToEthnofilesRequest.TotalSum,
                TotalRecords = sendFileToEthnofilesRequest.RowCount,
                SepaFileId = sendFileToEthnofilesRequest.FileId,
                ConvId = sendFileToEthnofilesRequest.ConvId,
                FileApiFileId = sendFileToEthnofilesRequest.FileApiFileId

            };

            _cliService.ValidateEthnofilesFile(request);
        }

        private SendFileToEthnofilesRequestCLI CreateSendFileToEthnofilesRequest(SendFileToEthnoFilesCmd command, EthnofilesFileTypesResponse selectedFileType)
        {
            var request = new SendFileToEthnofilesRequestCLI()
            {
                Filename = Path.GetFileName(command.InputFile),
                FileApiFileId = Guid.Parse(command.FileId),
                UserID = command.UserInfo.UserName,
                FileTypeId = command.Idfield,
                TanNumber = command.TanNumber,
                Locale = command.Locale,
                InboxId = command.InboxId,
                XactionId = command.XactionId,
                IsSmsOtp = command.IsSmsOtp,
                ConvId = selectedFileType.ConvId,
                XmlFileField = selectedFileType.XmlFileField,
                DebtorName = command.DebtorName,
                AcceptTerms = command.AcceptTerms,
                AcceptTrnTerms = command.AcceptTrnTerms        
            };

            var convertToXmlFileId = PrepareFile(command, selectedFileType);
            if (!String.IsNullOrEmpty(convertToXmlFileId)) request.FileId = convertToXmlFileId;

            HandleRowNum(request, selectedFileType, command);
            HandleTotalSum(request, selectedFileType, command);
            HandleAccountInput(request, selectedFileType, command);
            HandleTanNumber(request);

            return request;
        }

        private string PrepareFile(SendFileToEthnoFilesCmd command, EthnofilesFileTypesResponse selectedFileType)
        {
            if (String.IsNullOrEmpty(selectedFileType.ConvId)) return null;

            // call prepare file and get the sepa file id
            var prepareFileRequest = new PrepareFileRequest
            {
                FileApiFileId = Guid.Parse(command.FileId),
                UserID = command.UserInfo.UserName,
                FileTypeId = command.Idfield,
                ConvId = selectedFileType.ConvId,
                XmlFileField = selectedFileType.XmlFileField,
                DebtorName = command.DebtorName
            };

            if (HasAccountInput(selectedFileType)) prepareFileRequest.DebtorIBAN = command.DebtorIban;

            var convertToXmlResponse = _cliService.PrepareFile(prepareFileRequest);
            return convertToXmlResponse?.FileId;
        }


        private void HandleRowNum(SendFileToEthnofilesRequest request, EthnofilesFileTypesResponse selectedFileType, SendFileToEthnoFilesCmd command)
        {
            if (!SendRowNum(selectedFileType)) return;
            
            if (!String.IsNullOrEmpty(command.RowCountFromPainXml) && command.RowCountFromPainXml == "001" && !String.IsNullOrEmpty(command.InputFile))
            {
                var sepaPainFile = DeserializeXmlSEPA001ISO20022(command.InputFile);
                request.RowCount = sepaPainFile?.Document?.CstmrCdtTrfInitn?.GrpHdr?.NbOfTxs;
            }
            else if (command.DataFromFileName && !String.IsNullOrEmpty(command.InputFile)) request.RowCount = GetRowCountFromFileName(command.InputFile);
            else request.RowCount = command.RowCount;
        
        }

        private void HandleTotalSum(SendFileToEthnofilesRequest request, EthnofilesFileTypesResponse selectedFileType, SendFileToEthnoFilesCmd command) 
        {
            if (!SendTotalSum(selectedFileType)) return;
            
            if (!String.IsNullOrEmpty(command.TotalSumFromPainXml) && command.RowCountFromPainXml == "001" && !String.IsNullOrEmpty(command.InputFile))
            {
                var sepaPainFile = DeserializeXmlSEPA001ISO20022(command.InputFile);
                var ctrlSum = sepaPainFile?.Document?.CstmrCdtTrfInitn?.GrpHdr?.CtrlSum;

                if (ctrlSum != null) request.TotalSum = decimal.ToDouble((decimal)ctrlSum);
            }
            else if (command.DataFromFileName && !String.IsNullOrEmpty(command.InputFile))  request.TotalSum = GetTotalSumFromFileName(command.InputFile);
            else request.TotalSum = command.TotalSum;
        }

        private void HandleAccountInput(SendFileToEthnofilesRequest request, EthnofilesFileTypesResponse selectedFileType, SendFileToEthnoFilesCmd command) 
        {
            if (HasAccountInput(selectedFileType)) request.DebtorIBAN = command.DebtorIban;
        }

        private void HandleTanNumber(SendFileToEthnofilesRequest request) 
        {
            if (!request.IsSmsOtp == null || request.IsSmsOtp == false)
                request.TanNumber = "bypass";

        }

        /// <summary>
        /// filename example : CUS2BANK171400_20200127_09_00003_160989.68-.FTI.XML
        /// return value example : 00003
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static int GetRowCountFromFileName(string fileName)
        {
            string rowCountString = String.Empty;
            var fileNameWithoutFullPath = Path.GetFileName(fileName);
            var parts = fileNameWithoutFullPath.Split('_', '-');
            if (parts.Length > 3) rowCountString = parts[3];
            int.TryParse(rowCountString, out int rowCount);

            return rowCount;
        }

        /// <summary>
        /// filename example : CUS2BANK171400_20200127_09_00003_160989.68-.FTI.XML
        /// return value example : 160989.68
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static double GetTotalSumFromFileName(string fileName)
        {
            string totalSumString = String.Empty;
            var fileNameWithoutFullPath = Path.GetFileName(fileName);
            var parts = fileNameWithoutFullPath.Split('_', '-');
            if (parts.Length > 4)
            {
                // string fourthPart = parts[4];
                //  totalSumString = fourthPart.Substring(0, fourthPart.IndexOf('-'));
                totalSumString = parts[4];
            }
            double.TryParse(totalSumString, NumberStyles.Any, CultureInfo.InvariantCulture, out double totalSum);

            return totalSum;
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

        private bool SendRowNum(EthnofilesFileTypesResponse selectedFileType)
        {
            return selectedFileType.ValidationTypeField != null && selectedFileType.ValidationTypeField != "none";
        }

        private bool SendTotalSum(EthnofilesFileTypesResponse selectedFileType)
        {
            return selectedFileType.ValidationTypeField == "countAndSum";
        }

        private bool HasAccountInput(EthnofilesFileTypesResponse selectedFileType)
        {
            return selectedFileType.ConvId != null && selectedFileType.ConvId.Length > 0 && (selectedFileType.ConvId.Contains("Payroll") || selectedFileType.ConvId.Contains("SHP"));
        }
    }
}
