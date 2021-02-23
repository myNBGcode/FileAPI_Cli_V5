using System;
using System.IO;
using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using proxy.types;

namespace FileapiCli.CommandHandlers
{
    class SampleMassTransfersHandler : CliCommandHandler<SampleMassTransfersCmd, SampleMassTransfersResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public SampleMassTransfersHandler(ILoggerFactory loggerFactory,
            ICliService cliService,
            IMapper mapper, ICommandValidator<SampleMassTransfersCmd> validator,
            IFileService fileservice) : base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileservice;
        }


        protected override SampleMassTransfersResult DoHandle(SampleMassTransfersCmd command)
        {
            var result = new SampleMassTransfersResult();
            var request = new MassTransfersSampleRequest()
            {
                FileFormat = command.FileFormat,
                UserID = command.UserInfo.UserName
            };

            MassTransfersSampleResponse response = _fileService.GenerateMassTransfersCreditSample(request);

            // var fileName = Path.GetFileName(command.DownloadFolder);
            string massTransfersFileName = $"masstransferssample_{DateTime.Now:yyyyMMddHHmmss}.{command.FileFormat.ToLower()}";


            string downloadPath = command.DownloadFolder + @"\" + massTransfersFileName;

            File.WriteAllBytes(downloadPath, response.Content);

            _logger.LogInformation($"$Sample file downloaded at {downloadPath}");
            return result;
        }
    }
}
