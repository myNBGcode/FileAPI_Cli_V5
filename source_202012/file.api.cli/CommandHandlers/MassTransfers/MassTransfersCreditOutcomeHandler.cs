using System;
using AutoMapper;
using FileapiCli.Commands.MassTransfers;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using proxy.types;

namespace FileapiCli.CommandHandlers
{
    public class MassTransfersCreditOutcomeHandler : CliCommandHandler<MassTransfersCreditOutcomeCmd, MassTransfersCreditOutcomeResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MassTransfersCreditOutcomeHandler(ILoggerFactory loggerFactory,
            ICliService cliService, IMapper mapper,
            ICommandValidator<MassTransfersCreditOutcomeCmd> validator,
            IFileService fileService)
            : base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileService;
        }

        protected override MassTransfersCreditOutcomeResult DoHandle(MassTransfersCreditOutcomeCmd command)
        {
            var result = new MassTransfersCreditOutcomeResult();
            var req = new ResultPayCreditWithFileRequest
            {
                FileId = command.FileId,
                UserID = command.UserInfo.UserName
            };

            ResultPayCreditWithFileResponse resp = _fileService.RetrieveMassTransfersCreditOutcome(req);
            if (!Guid.TryParse(resp.FileId, out var returnId))
            {
                var err = $"Id:{resp.FileId} received is not a valid guid";
                _logger.LogError(err);
                throw new ArgumentException(err);
            }

            var requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            var subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

            _cliService.DownloadFile(returnId, command.DownloadFolder, requester, subject);
            _logger.LogInformation($"Mass Credit Transfers Outcome File downloaded: {command.DownloadFolder} ");

            return result;
        }
    }
}
