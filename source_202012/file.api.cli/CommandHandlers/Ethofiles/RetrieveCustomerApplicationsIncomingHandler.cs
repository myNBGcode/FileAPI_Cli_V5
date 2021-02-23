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
    internal class RetrieveCustomerApplicationsIncomingHandler : CliCommandHandler<RetrieveCustomerApplicationsIncomingCmd, RetrieveCustomerApplicationsIncomingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveCustomerApplicationsIncomingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveCustomerApplicationsIncomingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveCustomerApplicationsIncomingResult DoHandle(RetrieveCustomerApplicationsIncomingCmd command)
        {
            //Incoming Retrieve Customer Applications
            var result = new RetrieveCustomerApplicationsIncomingResult();
            var customerApplicationsIncomingResponse = _ethnofilesService.RetrieveCustomerApplications(new RetrieveCustomerApplicationsRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection
            });

            if (customerApplicationsIncomingResponse.CustomerApplications != null && customerApplicationsIncomingResponse.CustomerApplications.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsIncomingResponse.CustomerApplications);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Incoming Customer Applications:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"CustomerApplicationsIncoming_({DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Incoming Customer Applications list is empty");
            }
            return result;
        }
    }
}
