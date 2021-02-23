using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using proxy.types;
using System;
using System.IO;
using System.Linq;

namespace FileapiCli.CommandHandlers
{
    internal class RetrieveFilesOutgoingHandler : CliCommandHandler<RetrieveFilesOutgoingCmd, RetrieveFilesOutgoingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFilesOutgoingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFilesOutgoingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFilesOutgoingResult DoHandle(RetrieveFilesOutgoingCmd command)
        {
            //Retrieve Outgoing Files
            var result = new RetrieveFilesOutgoingResult();
            var customerApplicationsOutgoingResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = command.IsHistorical
            });

            if (customerApplicationsOutgoingResponse.CustomerApplicationFiles != null && customerApplicationsOutgoingResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsOutgoingResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Outgoing Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"OutgoingFilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Outgoing File list is empty");
            }
            return result;
        }
    }
}
