using System;
using System.Linq;
using AutoMapper;
using FileapiCli.Commands.MassTransfers;
using FileapiCli.Core;
using FileapiCli.FileapiCli;
using Microsoft.Extensions.Logging;
using proxy.types;
using Serilog;

namespace FileapiCli.CommandHandlers
{
    public class MassTransfersCreditHandler : CliCommandHandler<MassTransfersCreditCmd, MassTransfersCreditResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MassTransfersCreditHandler(ILoggerFactory loggerFactory,
            ICliService cliService, IMapper mapper,
            ICommandValidator<MassTransfersCreditCmd> validator, 
            IFileService fileService) 
            : base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileService;
        }

        protected override MassTransfersCreditResult DoHandle(MassTransfersCreditCmd command)
        {
            var result = new MassTransfersCreditResult();

            var verifyFileRequest = new VerifyFileCreditRequest
            {
                UserID = command.UserInfo.UserName,
                FileType = FindFileType(command.InputFile),
                Account = command.DebitAccount,
                IsPayroll = command.IsPayroll,
                FileId = command.FileId
            };

            FileCreditVerifyResponse verifyResponse = _fileService.MassiveTransfersVerifyFileCredit(verifyFileRequest);
            if (verifyResponse.HasErrors)
            {
                //Το αρχείο έχει λάθη. Για να ολοκληρώσετε την συναλλαγή πρέπει να υποβάλλετε ξανά το αρχείο διορθωμένο.
                Log.Information($"Cannot validate {verifyResponse.FileName} File.");
                foreach (var record in verifyResponse.Records)
                {
                    if (!string.IsNullOrEmpty(record.Error))
                    {
                        Log.Information($"[Debit account: {record.DebitAccount} " +
                                        $"- Amount: {record.Amount} " +
                                        $"- Reason: {record.Reason}" +
                                        $"- Error: {record.Error}]");
                    }
                }
                throw new Exception($"File {verifyResponse.FileName} rejected. Total records: {verifyResponse.TotalRecords} - Total valid records: {verifyResponse.TotalSuccRecords}");
            }

            var payFileRequest = new PayFileCreditRequest
            {
                DebitAccount = command.DebitAccount,
                FileName = verifyResponse.FileName,
                FileType = FindFileType(verifyResponse.FileName),
                TotalAmount = verifyResponse.SumAmClear.GetValueOrDefault(),
                TotalRecords = verifyResponse.TotalRecords,
                IsPayroll = command.IsPayroll,
                FileId = command.FileId,
                //TanNumber = command.TanNumber,
                UserID = command.UserInfo.UserName
            };
            PayFileResponse payFileResponse;
            try
            {
                payFileResponse = _fileService.MassiveTransfersPayFileCredit(payFileRequest);
            }
            catch (SMSOTPException smsotpex)
            {
                var otp = string.Empty;
                while (string.IsNullOrEmpty(otp))
                {
                    Console.Write("Please Enter One Time Password (OTP):");
                    otp = Console.ReadLine()?.Trim();
                }

                //payFileRequest.TanNumber = otp;
                payFileResponse = _fileService.MassiveTransfersPayFileCredit(payFileRequest);
            }

            if (payFileResponse.RequiresApproval)
            {
                Log.Information("Mass Transfers File Requires Approval.");
                Log.Information($"*** THE MASSIVE TRANSFERS FILE ID IS : {command.FileId} ***");
                return result;
            }

            Log.Information("Massive Transfers File Received Successfully.");
            Log.Information($"*** THE MASSIVE TRANSFERS FILE ID IS : {command.FileId} ***");
            Log.Information($"Total File Amount: {payFileResponse.TotalFileAmount} " +
                            $"- Total Amount: {payFileResponse.TotalAmount} " +
                            $"- Total Commission: {payFileResponse.TotalCommission} " +
                            $"- Total Records: {payFileResponse.TotalRecords} " +
                            $"- Total Valid Records: {payFileResponse.TotalSuccRecords}");
            return result;
        }

        private static string FindFileType(string fileName)
        {
            var extension = new string(fileName.SkipWhile(c => c != '.').Skip(1).ToArray());

            return extension;
        }
    }
}
