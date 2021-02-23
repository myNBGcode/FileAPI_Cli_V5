using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using proxy.types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileapiCli.CommandHandlers
{
    internal class DownloadFilesOutgoingHandler : CliCommandHandler<DownloadFilesOutgoingCmd, DownloadFilesOutgoingResult>
    {
        private readonly IFileService _ethnofilesService;

        public DownloadFilesOutgoingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<DownloadFilesOutgoingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override DownloadFilesOutgoingResult DoHandle(DownloadFilesOutgoingCmd command)
        {
            var result = new DownloadFilesOutgoingResult();
            if (command.IsHistorical == null)
            {
                DownloadFileOutgoing(command, false);
                DownloadFileOutgoing(command, true);
            }
            else
                DownloadFileOutgoing(command, (bool)command.IsHistorical);

            return result;
        }

        private void DownloadFileOutgoing(DownloadFilesOutgoingCmd command, bool isHistorical) 
        {
            string historic = isHistorical ? "Historic" : String.Empty;
            _logger.LogInformation($"Download of {historic} outgoing files started. ");

            var customerApplicationsOutgoingResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = isHistorical
            });

            if (customerApplicationsOutgoingResponse.CustomerApplicationFiles != null && customerApplicationsOutgoingResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsOutgoingResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                 _logger.LogInformation($"List of {historic} Outgoing Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"Outgoing{historic}FilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);

                var limitedCollection = customerApplicationsOutgoingResponse.CustomerApplicationFiles.Take(command.MaxItems);
                foreach (var customerApplicationFile in limitedCollection)
                {
                    var fileOutgoingResponse = _ethnofilesService.RetrieveFile(new RetrieveFileRequest
                    {
                        UserId = command.UserInfo.UserName,
                        FileDirection = command.FileDirection,
                        CustomerApplicationFileId = customerApplicationFile.Id,
                        IsHistorical = isHistorical
                    });

                    if (fileOutgoingResponse != null)
                    {
                        var jsonFile = JsonConvert.SerializeObject(fileOutgoingResponse);
                        var prettyJsonFile = JValue.Parse(jsonFile).ToString(Formatting.Indented);
                        _logger.LogInformation($"Outgoing File Id:{Environment.NewLine} { prettyJsonFile}");

                        string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
                        string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

                        // use customer application file id as prefix to distinguish files with the same filename
                        _cliService.DownloadFile(fileOutgoingResponse.FileId, command.DownloadFolder + @"\", requester, subject, $"{customerApplicationFile.Id}_");
                    }
                    else
                    {
                        _logger.LogInformation("No corresponding Outgoing File found");
                    }
                }

            }
            else
            {
                _logger.LogInformation($"{historic} Outgoing File list is empty");
            }
        }
    }
}
