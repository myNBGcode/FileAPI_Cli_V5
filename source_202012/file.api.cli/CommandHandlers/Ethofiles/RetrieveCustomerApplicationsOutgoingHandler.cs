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
    internal class RetrieveCustomerApplicationsOutgoingHandler : CliCommandHandler<RetrieveCustomerApplicationsOutgoingCmd, RetrieveCustomerApplicationsOutgoingResult>
    {
        private readonly IFileService _ethnofilesService;

        public RetrieveCustomerApplicationsOutgoingHandler(ILoggerFactory loggerFactory, IFileService ethnofilesService, ICliService cliService, ICommandValidator<RetrieveCustomerApplicationsOutgoingCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
            _ethnofilesService = ethnofilesService;
        }
        protected override RetrieveCustomerApplicationsOutgoingResult DoHandle(RetrieveCustomerApplicationsOutgoingCmd command)
        {
            //Outgoing Retrieve Customer Applications
            var result = new RetrieveCustomerApplicationsOutgoingResult();
            var customerApplicationsOutgoingResponse = _ethnofilesService.RetrieveCustomerApplications(new RetrieveCustomerApplicationsRequest
            {
                UserId = command.UserInfo.UserName,
                FileDirection = command.FileDirection
            });

            if (customerApplicationsOutgoingResponse.CustomerApplications != null && customerApplicationsOutgoingResponse.CustomerApplications.Any())
            {
                var json = JsonConvert.SerializeObject(customerApplicationsOutgoingResponse.CustomerApplications);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of Outgoing Customer Applications:{Environment.NewLine} { prettyJson}");

                string cusAppsFileName = $"CustomerApplicationsOutgoing_({DateTime.Now:yyyyMMdd}).json";
                string downloadPath = command.DownloadFolder + @"\" + cusAppsFileName;
                File.WriteAllText(downloadPath, prettyJson);
            }
            else
            {
                _logger.LogInformation("Outgoing Customer Applications list is empty");
            }
            return result;
        }
    }
}
