using AutoMapper;
using file.types;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileapiCli.CommandHandlers
{
    public class UploadFileHandler : CliCommandHandler<UploadFileCmd, UploadFileCmdResult>
    {
        private readonly IMapper _mapper;

        public UploadFileHandler(ILoggerFactory loggerFactory, ICliService cliService, IFileService fileApiRestRequest, IMapper mapper, ICommandValidator<UploadFileCmd> validator)
              : base(loggerFactory,  cliService, validator)
        {
            _mapper = mapper;
        }

        protected override UploadFileCmdResult DoHandle(UploadFileCmd uploadFileCmd)
        {
            var result = new UploadFileCmdResult();
            var uploadRequest = _mapper.Map<UploadFileCmd, UploadRequest>(uploadFileCmd);

           _cliService.UploadFile(uploadFileCmd.InputFile, uploadRequest, uploadFileCmd.FileId, uploadFileCmd.ChuckSize, uploadFileCmd.TotalChucks);

            return result;
        }
    
    }
}
