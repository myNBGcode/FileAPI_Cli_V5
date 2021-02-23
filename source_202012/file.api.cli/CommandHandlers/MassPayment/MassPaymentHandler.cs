using System;
using System.IO;
using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using FileapiCli.FileapiCli;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using tpp.types;

namespace FileapiCli.CommandHandlers
{
    public class MassPaymentHandler : CliCommandHandler<MassPaymentCmd, MassPaymentResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MassPaymentHandler( ILoggerFactory loggerFactory,
                                  
                                   ICliService cliService, 
                                   IMapper mapper, 
                                   ICommandValidator<MassPaymentCmd> validator,
                                   IFileService fileService)
                                 : base(loggerFactory,  cliService, validator)
        {
            _mapper = mapper;
            this._fileService = fileService;
        }
        protected override MassPaymentResult DoHandle(MassPaymentCmd command)
        {
            var result = new MassPaymentResult();

            ValidateMassPaymentsFileBpRequest valRequest = new ValidateMassPaymentsFileBpRequest
            {
                UserID=command.UserInfo.UserName,
                FileID=command.FileId
            };
            ValidateMassPaymentsFileResponse resp = _fileService.ValidateMassPaymentFile(valRequest);

            if (!resp.AreAllValid)
            {
                //export validation failed response to file
                var validateMassPaymentFileRespExport = new ValidateMassPaymentsFileResponseExport(resp, command.FileId);
                WriteMassPaymentResponseToFile(validateMassPaymentFileRespExport, command.Filename, command.DownloadFolder);

                throw new Exception($"FileRejected. Cannot Validate Mass Payment File: Error(s): {string.Join(",", resp.ValidationErrors)}");
            }

            MassPaymentsWithFileBpRequest request = new MassPaymentsWithFileBpRequest
            {
                UserID=command.UserInfo.UserName,
                DebtorΙΒΑΝ = command.DebtorIBAN,
                DebtorTelephone = command.DebtorTelephone,
                Ccy = command.Ccy,
                DebtorName = command.DebtorName,
                AcceptDuplicate = command.AcceptDuplicate,
                FileID =command.FileId
            };
            MassPaymentsWithFileResponse massPaymentResp=null;
            try
            {
                massPaymentResp = _fileService.MassPaymentWithFile(request, command.RequestId);
            }
            catch (SMSOTPException smsotpex)
            {
                var token = string.Empty;
                while (string.IsNullOrEmpty(token))
                {
                    Console.Write("Please Enter One Time Password (OTP):");
                    token = Console.ReadLine().Trim();
                }

                request.TanNumber = token;
                massPaymentResp = _fileService.MassPaymentWithFile(request, command.RequestId);
            }

            //export response to file
            var massPaymentRespExport = new MassPaymentsWithFileResponseExport(massPaymentResp, command.FileId);
            WriteMassPaymentResponseToFile(massPaymentRespExport, command.Filename, command.DownloadFolder);

            if (massPaymentResp.ReceivedSuccessfully)
            {
                Log.Information("MassPayment File Received Successfully.");
                Log.Information($"*** THE MASS PAYMENT FILE ID IS : {command.FileId} ***");
                return result;
            }
            var errStr = string.Join(",", massPaymentResp.ErrorMessages);
            throw new Exception($"Errors Occured when accepting Payment {errStr} )");
           
        }


        private void WriteMassPaymentResponseToFile<T>(T response, string filename, string downloadFolder)
        {
            var json = JsonConvert.SerializeObject(response);
            var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
            string massPaymentRespFilename = $"MassPaymentResponse_{filename}_({DateTime.Now:yyyyMMddHHmm}).json";
            string downloadPath = downloadFolder + @"\" + massPaymentRespFilename;
            File.WriteAllText(downloadPath, prettyJson);
        }
    }
}
