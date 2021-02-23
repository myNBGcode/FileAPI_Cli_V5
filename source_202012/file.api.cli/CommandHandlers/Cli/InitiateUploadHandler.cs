using AutoMapper;
using file.types;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;

namespace FileapiCli.CommandHandlers
{
    public class InitiateUploadHandler : CliCommandHandler<InitiateUploadCmd, InitiateUploadCmdResult>
    {
        private readonly IMapper _mapper;

        public InitiateUploadHandler(ILoggerFactory loggerFactory, ICliService cliService, IMapper mapper,ICommandValidator<InitiateUploadCmd> validator )
            : base(loggerFactory,  cliService, validator)
        {
            _mapper = mapper;
        }
        protected override InitiateUploadCmdResult DoHandle(InitiateUploadCmd command)
        {
            var result = new InitiateUploadCmdResult();

            var uploadInitRequest = _mapper.Map<InitiateUploadCmd, UploadInitiationRequest>(command);

            var uploadInitiationResponse = _cliService.InitiateUpload(uploadInitRequest);

            result.FileId = uploadInitiationResponse.FileId?.ToString();
            result.ChuckSize = uploadInitiationResponse.FileChunkSize * 1024 * 1024;
            result.TotalChucks = uploadInitiationResponse.FileChunks;

            _logger.LogInformation($"Initiate Operation ended. File Name: {command.FileName} with File Id: {result.FileId}");
            return result;
        }
    }
}
