using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using tpp.types;

namespace FileapiCli.CommandHandlers
{
    public class RequestStatusHandler : CliCommandHandler<RequestStatusCmd, RequestStatusResult>
    {
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public RequestStatusHandler(ILoggerFactory loggerFactory,  ICliService cliService, 
            IMapper mapper,ICommandValidator<RequestStatusCmd> validator,
            IFileService fileService) 
            : base(loggerFactory, cliService, validator)
        {
            _mapper = mapper;
            _fileService = fileService;
        }

        protected override RequestStatusResult DoHandle(RequestStatusCmd command)
        {
            var result = new RequestStatusResult();
            var req = new RequestIndividualPaymentsStatusWithFileBpRequest() 
                
                {UserID = command.UserInfo.UserName, FileID = command.FileId};
            
            var resp = _fileService.RequestPaymentStatus(req);

            _logger.LogInformation(!resp.ReceivedSuccessfully
                ? "Request for Payment status failed."
                : "Request for Payment status received successfully.");

            return result;
        }
    }
}
