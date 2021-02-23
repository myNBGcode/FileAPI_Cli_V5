using AutoMapper;
using file.types;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;

namespace FileapiCli.CommandHandlers
{
    public class RemoveUserTagsHandler : CliCommandHandler<RemoveUserTagsCmd, RemoveUserTagsCmdResult>
    {
        private readonly IMapper _mapper;
        public RemoveUserTagsHandler(ILoggerFactory loggerFactory, ICliService cliService, IMapper mapper,ICommandValidator<RemoveUserTagsCmd> validator ) 
            : base(loggerFactory,  cliService, validator)
        {
            _mapper = mapper;
        }
        protected override RemoveUserTagsCmdResult DoHandle(RemoveUserTagsCmd command)
        {
            var result = new RemoveUserTagsCmdResult();
            var updateFileRequest = _mapper.Map<RemoveUserTagsCmd, UpdateFileRequest>(command);

            _cliService.RemoveUserTags(Guid.Parse(command.RemoveUserTagsFileId), updateFileRequest, command.UserTags);
            return result;
        }
    }
}