using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using System;
using tpp.types;

namespace FileapiCli.CommandHandlers
{
    public class RetrieveStatusHandler : CliCommandHandler<RetrieveStatusCmd, RetrieveStatusResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public RetrieveStatusHandler(ILoggerFactory loggerFactory, ICliService cliService, IMapper mapper,ICommandValidator<RetrieveStatusCmd> validator, IFileService fileService ) 
            : base(loggerFactory,cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileService;
        }
        protected override RetrieveStatusResult DoHandle(RetrieveStatusCmd command)
        {
            var result = new RetrieveStatusResult();

            var req = new RetrieveIndividualPaymentsStatusWithFileBpRequest {FileID = command.FileId, UserID = command.UserInfo.UserName};

            RetrieveIndividualPaymentsStatusWithFileResponse resp = _fileService.RetrievePaymentStatus(req);

            if (!Guid.TryParse(resp.FileID, out Guid returnId))
            {
                string err = $"Id:{resp?.FileID} received is not a valid guid";
                _logger.LogError(err);
                throw new ArgumentException(err);
            }

            //string requester = $"{command.RequestEntity.Registry}:{command.RequestEntity.Id}";
            //string subject = $"{command.Subject.Registry}:{command.Subject.Id}";
            string requester = $"{command.UserInfo.Registry}:{command.UserInfo.UserName}";
            string subject = $"{command.UserInfo.SubjectRegistry}:{command.UserInfo.SubjectUser}";

            _cliService.DownloadFile(returnId, command.DownloadFolder, requester, subject); 

            _logger.LogInformation($"Retrieve Payment Status completed at:{command.DownloadFolder}");
        
            return result;
        }
    }
}
