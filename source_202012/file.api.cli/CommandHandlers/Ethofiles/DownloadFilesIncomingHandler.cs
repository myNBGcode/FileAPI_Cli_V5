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
    internal class DownloadFilesIncomingHandler : CliCommandHandler<DownloadFilesIncomingCmd, DownloadFilesIncomingResult>
    {
        private readonly IFileService _ethnofilesService;

        public DownloadFilesIncomingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<DownloadFilesIncomingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override DownloadFilesIncomingResult DoHandle(DownloadFilesIncomingCmd command)
        {
            var result = new DownloadFilesIncomingResult();
            if (command.IsHistorical == null)
            {
                DownloadFileIncoming(command, false);
                DownloadFileIncoming(command, true);
            }
            else
                DownloadFileIncoming(command, (bool)command.IsHistorical);

            return result;
        }

        private void DownloadFileIncoming(DownloadFilesIncomingCmd command, bool isHistorical) 
        {
            string historic = isHistorical ? "Historic" : String.Empty;
            _logger.LogInformation($"Download of {historic} incoming files started. ");

            var customerApplicationsIncomingResponse = _ethnofilesService.RetrieveFileList(new RetrieveFileListRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection,
                DateFrom = command.DateFrom,
                DateTo = command.DateTo,
                IsHistorical = isHistorical
            });

            if (customerApplicationsIncomingResponse.CustomerApplicationFiles != null && customerApplicationsIncomingResponse.CustomerApplicationFiles.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsIncomingResponse.CustomerApplicationFiles);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                 _logger.LogInformation($"List of {historic} Incoming Files:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"Incoming{historic}FilesList_({command.DateFrom:yyyyMMdd}-{command.DateTo ?? DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);

                var limitedCollection = customerApplicationsIncomingResponse.CustomerApplicationFiles.Take(command.MaxItems);
                foreach (var customerApplicationFile in limitedCollection)
                {
                    var fileIncomingResponse = _ethnofilesService.RetrieveFile(new RetrieveFileRequest
                    {
                        UserId = command.UserInfo.UserName,
                        FileDirection = command.FileDirection,
                        CustomerApplicationFileId = customerApplicationFile.Id,
                        IsHistorical = isHistorical
                    });

                    if (fileIncomingResponse != null)
                    {
                        var jsonFile = JsonConvert.SerializeObject(fileIncomingResponse);
                        var prettyJsonFile = JValue.Parse(jsonFile).ToString(Formatting.Indented);
                        _logger.LogInformation($"Incoming File Id:{Environment.NewLine} { prettyJsonFile}");

                        string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
                        string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

                        // use customer application file id as prefix to distinguish files with the same filename
                        _cliService.DownloadFile(fileIncomingResponse.FileId, command.DownloadFolder + @"\", requester, subject, $"{customerApplicationFile.Id}_");
                    }
                    else
                    {
                        _logger.LogInformation("No corresponding Incoming File found");
                    }
                }

            }
            else
            {
                _logger.LogInformation($"{historic} Incoming File list is empty");
            }
        }
    }
}
