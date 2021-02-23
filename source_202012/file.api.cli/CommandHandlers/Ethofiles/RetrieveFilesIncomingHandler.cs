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
    internal class RetrieveFilesIncomingHandler : CliCommandHandler<RetrieveFilesIncomingCmd, RetrieveFilesIncomingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFilesIncomingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFilesIncomingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFilesIncomingResult DoHandle(RetrieveFilesIncomingCmd command)
        {
            //Retrieve Incoming Files
            var result = new RetrieveFilesIncomingResult();
            var customerApplicationsIncomingResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = command.IsHistorical
            });

            if (customerApplicationsIncomingResponse.CustomerApplicationFiles != null && customerApplicationsIncomingResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsIncomingResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Incoming Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"IncomingFilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Incoming File list is empty");
            }
            return result;
        }
    }
}
