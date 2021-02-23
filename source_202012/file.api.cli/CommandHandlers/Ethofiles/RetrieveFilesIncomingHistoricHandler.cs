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
    internal class RetrieveFilesIncomingHistoricHandler : CliCommandHandler<RetrieveFilesIncomingHistoricCmd, RetrieveFilesIncomingHistoricResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveFilesIncomingHistoricHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveFilesIncomingHistoricCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveFilesIncomingHistoricResult DoHandle(RetrieveFilesIncomingHistoricCmd command)
        {
            //Retrieve Incoming Historic Files
            var result = new RetrieveFilesIncomingHistoricResult();
            var customerApplicationsIncomingHistoricResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = command.IsHistorical
            });


            if (customerApplicationsIncomingHistoricResponse.CustomerApplicationFiles != null && customerApplicationsIncomingHistoricResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsIncomingHistoricResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Historic Incoming Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"IncomingHistoricFilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Historic Incoming File list is empty");
            }
            return result;
        }
    }
}
