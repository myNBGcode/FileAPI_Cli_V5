using ethnofiles.types;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using FileapiCli.Commands;

namespace FileapiCli.CommandHandlers
{
    internal class PopulateFileTypesHandler : CliCommandHandler<PopulateFileTypesCmd, PopulateFileTypesResult>
    {
        public PopulateFileTypesHandler(ILoggerFactory loggerFactory, ICliService cliService, ICommandValidator<PopulateFileTypesCmd> validator)
          : base(loggerFactory, cliService, validator)
        {
        }
        protected override PopulateFileTypesResult DoHandle(PopulateFileTypesCmd command)
        {
            //Populate File Type
            var result = new PopulateFileTypesResult();
            var fileTypeList = _cliService.PopulateFileTypes(new PopulateFiletypesRequest { UserId = command.UserInfo.UserName });
            if (fileTypeList.Any())
            {
                var json = JsonConvert.SerializeObject(fileTypeList);
                var prettyJson = JValue.Parse(json).ToString(Formatting.Indented);
                _logger.LogInformation($"List of File Types:{Environment.NewLine} { prettyJson}");
            }
            else
            {
                _logger.LogInformation("File Types list is empty");
            }
            return result;
        }
    }
}
