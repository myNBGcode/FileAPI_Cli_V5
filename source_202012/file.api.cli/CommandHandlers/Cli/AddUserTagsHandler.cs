using AutoMapper;
using file.types;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;

namespace FileapiCli.CommandHandlers
{
    public class AddUserTagsHandler : CliCommandHandler<AddUserTagsCmd, AddUserTagsCmdResult>
    {
        private readonly IMapper _mapper;

        public AddUserTagsHandler(ILoggerFactory loggerFactory, ICliService cliService, IMapper mapper, ICommandValidator<AddUserTagsCmd> validator ) 
            : base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
        }
      
        protected override AddUserTagsCmdResult DoHandle(AddUserTagsCmd command)
        {
            var result = new AddUserTagsCmdResult();

            var updateFileRequest = _mapper.Map<AddUserTagsCmd, UpdateFileRequest>(command);
            
            _cliService.AddUserTags(Guid.Parse(command.FileId), updateFileRequest);

            return result;
        }
    }
}
