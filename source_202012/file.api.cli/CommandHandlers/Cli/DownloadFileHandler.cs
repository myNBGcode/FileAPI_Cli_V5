using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;

namespace FileapiCli.CommandServices
{
    public class DownloadFileHandler : CliCommandHandler<DownloadFileCmd, DownloadFileCmdResult>
    {
        public DownloadFileHandler(ILoggerFactory loggerFactory,ICliService cliService, ICommandValidator<DownloadFileCmd> validator) 
            : base(loggerFactory,  cliService, validator)
        {
        }
        protected override DownloadFileCmdResult DoHandle(DownloadFileCmd command)
        {
            var result = new DownloadFileCmdResult();
            string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";
            _cliService.DownloadFile(Guid.Parse(command.FileId), command.DownloadFolder, requester, subject);
            _logger.LogInformation($"File Downloaded at: {command.DownloadFolder}");
            return result;
        }
    }
}
