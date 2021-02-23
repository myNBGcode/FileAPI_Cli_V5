using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using proxy.types;
using System;
using System.Linq;

namespace FileapiCli.CommandHandlers
{
    internal class RetrieveFileOutgoingHandler : CliCommandHandler<RetrieveFileOutgoingCmd, RetrieveFileOutgoingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFileOutgoingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFileOutgoingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFileOutgoingResult DoHandle(RetrieveFileOutgoingCmd command)
        {
            //Outgoing File Retrieve
            var result = new RetrieveFileOutgoingResult();
            var fileOutgoingResponse = _ethnofilesService.RetrieveFile(new RetrieveFileRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                IsHistorical = command.IsHistorical,
                CustomerApplicationFileId =command.CustomerApplicationFileId
            });

            if (fileOutgoingResponse != null)
            {
                var json = JsonConvert.SerializeObject(fileOutgoingResponse);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"Outgoing File Id:{Environment.NewLine} { prettyJson}");

                string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
                string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

                _cliService.DownloadFile(fileOutgoingResponse.FileId, command.DownloadFolder + @"\", requester, subject);
            }
            else
            {
                _logger.LogInformation("No corresponding Outgoing File found");
            }
            return result;
        }
    }
}
