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
    internal class RetrieveFilesOutgoingHistoricHandler : CliCommandHandler<RetrieveFilesOutgoingHistoricCmd, RetrieveFilesOutgoingHistoricResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFilesOutgoingHistoricHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFilesOutgoingHistoricCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFilesOutgoingHistoricResult DoHandle(RetrieveFilesOutgoingHistoricCmd command)
        {
            //Retrieve Outgoing Historic Files
            var result = new RetrieveFilesOutgoingHistoricResult();
            var customerApplicationsOutgoingHistoricResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = command.IsHistorical
            });

            if (customerApplicationsOutgoingHistoricResponse.CustomerApplicationFiles != null && customerApplicationsOutgoingHistoricResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsOutgoingHistoricResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Historic Outgoing Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"OutgoingHistoricFilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Historic Outgoing File list is empty");
            }
            return result;
        }
    }
}
