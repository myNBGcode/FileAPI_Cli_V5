using AutoMapper;
using FileapiCli.Commands;
using FileapiCli.Commands.MassPayment;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using FileapiCli.CommandHandlers;

namespace FileapiCli
{
    public class MassPaymentInvoker
    {
        #region fields
        private readonly ILogger<FileCliInvoker> _logger;
        private readonly IUserInfo _userInfo;
        private readonly DefaultOptions _defaultOptions;
        private readonly IMapper _mapper;

        private readonly ICommandHandler<SampleMassPaymentCmd, SampleMassPaymentResult> _sampleMassPaymentHandler;
        private readonly ICommandHandler<MassPaymentOutcomeCmd, MassPaymentOutcomeResult> _massPaymentOutcomeHandler;
        private readonly ICommandHandler<MassPaymentCmd, MassPaymentResult> _massPaymentHandler;
        private readonly ICommandHandler<RequestStatusCmd, RequestStatusResult> _requestStatusHandler;
        private readonly ICommandHandler<RetrieveStatusCmd, RetrieveStatusResult> _retrieveStatusHandler;
        private ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> _initiateUploadHandler;
        private readonly ICommandHandler<UploadFileCmd, UploadFileCmdResult> _uploadFileHandler;

        #endregion

        #region ctor
        public MassPaymentInvoker(IUserInfo userInfo,
                              ILoggerFactory loggerFactory,
                              DefaultOptions defaultOptions,
                              IMapper mapper,
                              ICommandHandler<SampleMassPaymentCmd, SampleMassPaymentResult> sampleMassPaymentHandler,
                              ICommandHandler<MassPaymentOutcomeCmd, MassPaymentOutcomeResult> massPaymentOutcomeHandler,
                              ICommandHandler<MassPaymentCmd, MassPaymentResult> massPaymentHandler,
                              ICommandHandler<RequestStatusCmd, RequestStatusResult> requestStatusHandler,
                              ICommandHandler<RetrieveStatusCmd, RetrieveStatusResult> retrieveStatusHandler,
                              ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> initiateUploadHandler,
                              ICommandHandler<UploadFileCmd, UploadFileCmdResult> uploadFileHandler)
        #endregion

        {
             #region assigment
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            _logger = loggerFactory.CreateLogger<FileCliInvoker>();
            _userInfo = userInfo;
          
            _defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sampleMassPaymentHandler = sampleMassPaymentHandler ?? throw new ArgumentNullException(nameof(sampleMassPaymentHandler));
            _massPaymentOutcomeHandler = massPaymentOutcomeHandler ?? throw new ArgumentNullException(nameof(sampleMassPaymentHandler));
            _massPaymentHandler = massPaymentHandler ?? throw new ArgumentNullException(nameof(massPaymentHandler));
            _requestStatusHandler = requestStatusHandler ?? throw new ArgumentNullException(nameof(requestStatusHandler));
            _retrieveStatusHandler = retrieveStatusHandler ?? throw new ArgumentNullException(nameof(retrieveStatusHandler));
            _initiateUploadHandler = initiateUploadHandler ?? throw new ArgumentNullException(nameof(ArgumentNullException));
            _uploadFileHandler = uploadFileHandler ?? throw new ArgumentException(nameof(uploadFileHandler));

            #endregion
        }
        
        internal void Run(object parserResult)
        {
            switch (parserResult)
            {
                case SampleMassPaymentOption sampleMassPaymentOption:
                {
                    ExecuteSampleMassPaymentFile(sampleMassPaymentOption);
                    break;
                }
                case MassPaymentOption massPaymentOption:
                {
                    ExecuteMassPayment(massPaymentOption);
                    break;
                }
                case MassPaymentOutcomeOption massPaymentOutcomeOption:
                {
                    ExecuteMassPaymentOutcome(massPaymentOutcomeOption);
                    break;
                }
                case RequestPaymentStatusOption requestPaymentStatusOption:
                {
                    ExecuteRequestPaymentStatus(requestPaymentStatusOption);
                    break;
                }
                case RetrievePaymentStatusOption retrievePaymentStatus:
                {
                    ExecuteRetrievePaymentStatus(retrievePaymentStatus);
                    break;
                }

                default:
                    Console.WriteLine($"A instance of type {parserResult.GetType().Name}");
                    break;
            }
        }

        private void ExecuteRetrievePaymentStatus(RetrievePaymentStatusOption retrievePaymentStatusOption)
        {
            retrievePaymentStatusOption = _mapper.Map(_defaultOptions.RetrievePaymentStatusOption, retrievePaymentStatusOption);
            DisplayCommandInfo(retrievePaymentStatusOption);
            var cmd = RetrieveStatusCmd.Create(retrievePaymentStatusOption, _userInfo);
            var result = _retrieveStatusHandler.Handle(cmd);
        }

        private void ExecuteRequestPaymentStatus(RequestPaymentStatusOption requestPaymentStatusOption)
        {
            requestPaymentStatusOption = _mapper.Map(_defaultOptions.RequestPaymentStatusOption, requestPaymentStatusOption);
            DisplayCommandInfo(requestPaymentStatusOption);

            var cmd = RequestStatusCmd.Create(requestPaymentStatusOption, _userInfo);
            var result = _requestStatusHandler.Handle(cmd);
        }

        private void ExecuteMassPaymentOutcome(MassPaymentOutcomeOption massPaymentOutcomeOption)
        {
            massPaymentOutcomeOption = _mapper.Map(_defaultOptions.massPaymentOutcomeOption, massPaymentOutcomeOption);
             DisplayCommandInfo(massPaymentOutcomeOption);
            var massCmd = MassPaymentOutcomeCmd.Create(massPaymentOutcomeOption, _userInfo);
            var massResult = this._massPaymentOutcomeHandler.Handle(massCmd);
        }

        private void ExecuteMassPayment(MassPaymentOption massPaymentOption)
        {
            //merge any arguments from appsettings.json defaultOptions.
            massPaymentOption = _mapper.Map(_defaultOptions.massPaymentOption, massPaymentOption);
            DisplayCommandInfo(massPaymentOption, "Upload");

            //get upload options from masspaymentOptions
            var uploadOptions = _mapper.Map<MassPaymentOption, UploadOptions>(massPaymentOption);

            _logger.LogDebug($"Upload options:{JsonConvert.SerializeObject(uploadOptions)}");
            var cmd = InitiateUploadCmd.CreateCommand(uploadOptions, _userInfo);
            var result = this._initiateUploadHandler.Handle(cmd);


            var uploadCmd = UploadFileCmd.Create(uploadOptions, cmd.InputFile, Guid.Parse(result.FileId), result.ChuckSize, result.TotalChucks, _mapper, _userInfo);
            var uploadResult = this._uploadFileHandler.Handle(uploadCmd);

            //set the fileid to the upload commmand fileid, generate from the initiateupload.
            massPaymentOption.FileId = uploadCmd.FileId.ToString();
            massPaymentOption.Filename = cmd.FileName;

            DisplayCommandInfo(massPaymentOption);
            var massCmd = MassPaymentCmd.Create(massPaymentOption, _userInfo);
            var massResult = this._massPaymentHandler.Handle(massCmd);
        }

        private void ExecuteSampleMassPaymentFile(SampleMassPaymentOption sampleMassPaymentOption)
        {
            sampleMassPaymentOption = _mapper.Map(_defaultOptions.sampleMassPaymentOption, sampleMassPaymentOption);
            DisplayCommandInfo(sampleMassPaymentOption);
            var cmd = SampleMassPaymentCmd.Create(sampleMassPaymentOption, _userInfo);
            var result = this._sampleMassPaymentHandler.Handle(cmd);
        }

        private void DisplayCommandInfo(IOptions options)
        {
            _logger.LogInformation($"Executing: {options.GetType().Name.Replace("Option", ". Arguments:")}:{JsonConvert.SerializeObject(options)}");
        }
        private void DisplayCommandInfo(IOptions options, string operationName)
        {
            _logger.LogInformation($"Executing: {operationName}:{JsonConvert.SerializeObject(options)}");
        }
    }
}

