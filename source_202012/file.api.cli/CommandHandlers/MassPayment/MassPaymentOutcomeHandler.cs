using System;
using AutoMapper;
using FileapiCli.Commands.MassPayment;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using tpp.types;

namespace FileapiCli.CommandHandlers
{
    public class MassPaymentOutcomeHandler : CliCommandHandler<MassPaymentOutcomeCmd, MassPaymentOutcomeResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MassPaymentOutcomeHandler(ILoggerFactory loggerFactory,
                ICliService cliService, IMapper mapper, 
                ICommandValidator<MassPaymentOutcomeCmd> validator,
                IFileService fileService) 
            : base(loggerFactory,  cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileService;
        }
        protected override MassPaymentOutcomeResult DoHandle(MassPaymentOutcomeCmd command)
        {
            var result = new MassPaymentOutcomeResult();

            var req = new RetrieveMassPaymentsOutcomeWithFileBpRequest
            {
                FileID = command.FileId,
                UserID = command.UserInfo.UserName
            };
            RetrieveMassPaymentsWithFileOutcomeResponse resp = _fileService.RetrieveMassPaymentsOutcome(req);

            if (!Guid.TryParse(resp.FileID, out Guid returnId))
            {
                string err = $"Id:{resp.FileID} received is not a valid guid";
                _logger.LogError(err);
                 throw new ArgumentException(err);
            }

            string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

            _cliService.DownloadFile(returnId, command.DownloadFolder, requester, subject);

          
            _logger.LogInformation($"Mass Payment Outcome File downloaded: {command.DownloadFolder} ");

            return result;
        }
    }
}
