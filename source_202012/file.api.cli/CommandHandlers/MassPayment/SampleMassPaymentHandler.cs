using System;
using System.IO;
using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using tpp.types;

namespace FileapiCli.CommandHandlers
{
    // TODO: vags
    public class SampleMassPaymentHandler : CliCommandHandler<SampleMassPaymentCmd, SampleMassPaymentResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public SampleMassPaymentHandler(ILoggerFactory loggerFactory,
                                        ICliService cliService, 
                                        IMapper mapper,ICommandValidator<SampleMassPaymentCmd> validator,
                                        IFileService fileservice): base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileservice;
        }
        protected override SampleMassPaymentResult DoHandle(SampleMassPaymentCmd command)
        {
            var result = new SampleMassPaymentResult();
            var request = new GenerateMassPaymentsSampleFileBpRequest()
            {
                FileFormat = command.FileFormat.ToLower() == "json" ? MassPaymentsFileFormat.JSON
                 : command.FileFormat.ToLower() == "xml" ? MassPaymentsFileFormat.XML
                 : command.FileFormat.ToLower() == "csv" ? MassPaymentsFileFormat.CSV
                 : throw new ArgumentException("FileFormat not supported."),
                UserID = command.UserInfo.UserName
            };
       
            GenerateMassPaymentsSampleFileResponse response = _fileService.GenerateMassPaymentSampleFile(request);

     
           // var fileName = Path.GetFileName(command.DownloadFolder);
            string massPaymentFilenName = $"masspaymentsample_{DateTime.Now:yyyyMMddHHmmss}.{command.FileFormat.ToLower()}";

            
            string downloadPath = command.DownloadFolder + @"\" + massPaymentFilenName;
             
            File.WriteAllBytes(downloadPath, response.Content);
            
            _logger.LogInformation($"$Sample file downloaded at {downloadPath}");
            return result;
        }
    }
}
