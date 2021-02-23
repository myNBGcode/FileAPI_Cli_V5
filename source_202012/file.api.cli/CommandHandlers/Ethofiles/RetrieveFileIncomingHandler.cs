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
    internal class RetrieveFileIncomingHandler : CliCommandHandler<RetrieveFileIncomingCmd, RetrieveFileIncomingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFileIncomingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFileIncomingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFileIncomingResult DoHandle(RetrieveFileIncomingCmd command)
        {
            //Incoming File Retrieve
            var result = new RetrieveFileIncomingResult();
            var fileIncomingResponse = _ethnofilesService.RetrieveFile(new RetrieveFileRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                IsHistorical = command.IsHistorical,
                CustomerApplicationFileId =command.CustomerApplicationFileId
            });

            if (fileIncomingResponse != null)
            {
                var json = JsonConvert.SerializeObject(fileIncomingResponse);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"Incoming File Id:{Environment.NewLine} { prettyJson}");
                                
                string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
                string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

                _cliService.DownloadFile(fileIncomingResponse.FileId, command.DownloadFolder + @"\", requester, subject);
            }
            else
            {
                _logger.LogInformation("No corresponding Incoming File found");
            }
            return result;
        }
    }
}
