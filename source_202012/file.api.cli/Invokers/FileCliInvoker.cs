using AutoMapper;
using FileapiCli.CommandHandlers;
using FileapiCli.Commands;
using FileapiCli.Commands.MassPayment;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;

namespace FileapiCli
{
    public class FileCliInvoker
    {
        #region fields
        private readonly ILogger<FileCliInvoker> _logger;
        private readonly IUserInfo _userInfo;
        //private readonly IConfigurationRoot configurationRoot;
        private readonly DefaultOptions _defaultOptions;
        private readonly IMapper _mapper;

        private readonly ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> _initiateUploadHandler;
        private readonly ICommandHandler<UploadFileCmd, UploadFileCmdResult> _uploadFileHandler;
        private readonly ICommandHandler<AddUserTagsCmd, AddUserTagsCmdResult> _addUserTagHandler;
        private readonly ICommandHandler<RemoveUserTagsCmd, RemoveUserTagsCmdResult> _removUserGateHandler;
        private readonly ICommandHandler<SendFileToEthnoFilesCmd, SendFileToEthnoFilesCmdResult2> _sentToEthnoFilesHandler;
        private readonly ICommandHandler<DownloadFileCmd, DownloadFileCmdResult> _downloadFileHandler;
        private readonly ICommandHandler<SampleMassPaymentCmd, SampleMassPaymentResult> _sampleMassPaymentHandler;
        private readonly ICommandHandler<MassPaymentOutcomeCmd, MassPaymentOutcomeResult> _massPaymentOutcomeHandler;
        private readonly ICommandHandler<MassPaymentCmd, MassPaymentResult> _massPaymentHandler;
        private readonly ICommandHandler<RequestStatusCmd, RequestStatusResult> _requestStatusHandler;
        private readonly ICommandHandler<RetrieveStatusCmd, RetrieveStatusResult> _retrieveStatusHandler;
        private readonly ICommandHandler<SetPasswordCmd, SetPasswordResult> _setPasswordHandler;
        #endregion

        #region ctor
        public FileCliInvoker(IUserInfo userInfo,
                              ILoggerFactory loggerFactory,
                              DefaultOptions defaultOptions,
                              IMapper mapper,
                              ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> initiateUploadHandler,
                              ICommandHandler<UploadFileCmd, UploadFileCmdResult> uploadFileHandler,
                              ICommandHandler<DownloadFileCmd, DownloadFileCmdResult> downloadFileHandler,
                              ICommandHandler<AddUserTagsCmd, AddUserTagsCmdResult> addUserTagHandler,
                              ICommandHandler<RemoveUserTagsCmd, RemoveUserTagsCmdResult> removeTagHandler,
                              ICommandHandler<SendFileToEthnoFilesCmd, SendFileToEthnoFilesCmdResult2> sendtoEthnoFilersHandler,
                              ICommandHandler<SampleMassPaymentCmd, SampleMassPaymentResult> sampleMassPaymentHandler,
                              ICommandHandler<MassPaymentOutcomeCmd, MassPaymentOutcomeResult> massPaymentOutcomeHandler,
                              ICommandHandler<MassPaymentCmd, MassPaymentResult> massPaymentHandler,
                              ICommandHandler<RequestStatusCmd, RequestStatusResult> requestStatusHandler,
                              ICommandHandler<RetrieveStatusCmd, RetrieveStatusResult> retrieveStatusHandler,
                              ICommandHandler<SetPasswordCmd, SetPasswordResult> setPasswordHandler)
        #endregion

        {
             #region assigment
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            _logger = loggerFactory.CreateLogger<FileCliInvoker>();
            _userInfo = userInfo;
          
            this._defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _initiateUploadHandler = initiateUploadHandler ?? throw new ArgumentNullException(nameof(initiateUploadHandler));
            _uploadFileHandler = uploadFileHandler ?? throw new ArgumentNullException(nameof(uploadFileHandler));
            _downloadFileHandler = downloadFileHandler ?? throw new ArgumentNullException(nameof(downloadFileHandler));
            _addUserTagHandler = addUserTagHandler ?? throw new ArgumentNullException(nameof(addUserTagHandler));
            _removUserGateHandler = removeTagHandler ?? throw new ArgumentNullException(nameof(removeTagHandler));
            _sentToEthnoFilesHandler = sendtoEthnoFilersHandler ?? throw new ArgumentNullException(nameof(sendtoEthnoFilersHandler));
            _sampleMassPaymentHandler = sampleMassPaymentHandler ?? throw new ArgumentNullException(nameof(sampleMassPaymentHandler));
            this._massPaymentOutcomeHandler = massPaymentOutcomeHandler ?? throw new ArgumentNullException(nameof(sampleMassPaymentHandler));
            this._massPaymentHandler = massPaymentHandler ?? throw new ArgumentNullException(nameof(massPaymentHandler));
            this._requestStatusHandler = requestStatusHandler ?? throw new ArgumentNullException(nameof(requestStatusHandler));
            this._retrieveStatusHandler = retrieveStatusHandler ?? throw new ArgumentNullException(nameof(retrieveStatusHandler));
            this._setPasswordHandler = setPasswordHandler ?? throw new ArgumentNullException(nameof(setPasswordHandler));


            #endregion
        }

        internal void Run(object parserResult)
        {
            switch (parserResult)
            {
                case SetPasswordOptions passwordOptions:
                {
                    ExecuteSetPassword(passwordOptions);
                    break;
                }
                case UploadOptions uploadOptions:
                {
                    ExecuteUpload(uploadOptions);
                    break;
                }
                case DownloadOptions downloadOptions:
                {
                    ExecuteDownload(downloadOptions);
                    break;
                }
                case AddUserTagsOptions addUserTagsOptions:
                {
                    ExecuteAddUserTags(addUserTagsOptions);
                    break;
                }
                case RemoveUserTagOptions removeTagOptions:
                {
                    ExecuteRemoveUserTags(removeTagOptions);
                    break;
                }
                case SendToEthnoFilesOptions ethnoOptions:
                {
                    ExecuteSendToEthnoFiles(ethnoOptions); 
                    break;
                }
                case ProcessEnthofilesFileOptions processFileOptions:
                {
                    ExecuteProcessFile(processFileOptions);
                    break;
                }
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
                case RequestPaymentStatusOption RequestPaymentStatusOption:
                {
                    ExecuteRequestPaymentStatus(RequestPaymentStatusOption);
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

        private void ExecuteSetPassword(SetPasswordOptions setPasswordOptions)
        {
            var cmd = SetPasswordCmd.Create();
            DisplayCommandInfo(setPasswordOptions);
            var result = _setPasswordHandler.Handle(cmd);
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
            massPaymentOption = _mapper.Map(_defaultOptions.massPaymentOption, massPaymentOption);
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

        private void ExecuteProcessFile(ProcessEnthofilesFileOptions processEnthofilesFileOptions )
        {
            processEnthofilesFileOptions = _mapper.Map(_defaultOptions.ProcessEnthofilesFileOptions, processEnthofilesFileOptions);
            DisplayCommandInfo(processEnthofilesFileOptions);

            var uploadOptions = _mapper.Map<ProcessEnthofilesFileOptions, UploadOptions>(processEnthofilesFileOptions);

            _logger.LogDebug($"Upload options:{JsonConvert.SerializeObject(processEnthofilesFileOptions)}");
            var cmd = InitiateUploadCmd.CreateCommand(uploadOptions, _userInfo);
            var result = this._initiateUploadHandler.Handle(cmd);


            var uploadCmd = UploadFileCmd.Create(uploadOptions, cmd.InputFile, Guid.Parse(result.FileId), result.ChuckSize, result.TotalChucks, _mapper, _userInfo);
            var uploadResult = this._uploadFileHandler.Handle(uploadCmd);

            var ethnoOptions = _mapper.Map<ProcessEnthofilesFileOptions, SendToEthnoFilesOptions>(processEnthofilesFileOptions);
            ethnoOptions.FileId = uploadCmd.FileId.ToString();

            Log.Debug($"Ethno options:{JsonConvert.SerializeObject(processEnthofilesFileOptions)}");
            var ethnoCmd2 = SendFileToEthnoFilesCmd.Create(ethnoOptions, _mapper, userInfo: _userInfo);
            var resultEthno = _sentToEthnoFilesHandler.Handle(ethnoCmd2);
        }

        private void ExecuteSendToEthnoFiles(SendToEthnoFilesOptions ethnoOptions)
        {
            ethnoOptions = _mapper.Map(_defaultOptions.sendToEthnoFilesOptions, ethnoOptions);
            DisplayCommandInfo(ethnoOptions);
            var ethnoCmd = SendFileToEthnoFilesCmd.Create(ethnoOptions, _mapper, _userInfo);
            var resultentho = this._sentToEthnoFilesHandler.Handle(ethnoCmd);
        }

        private void ExecuteRemoveUserTags(RemoveUserTagOptions removeTagOptions)
        {
            removeTagOptions = _mapper.Map(_defaultOptions.RemoveUserTagsOptions, removeTagOptions);
            DisplayCommandInfo(removeTagOptions);
            var removeTagsCmd = RemoveUserTagsCmd.Create(removeTagOptions, _userInfo);
            var removeTagsResult = this._removUserGateHandler.Handle(removeTagsCmd);
        }

        private void ExecuteAddUserTags(AddUserTagsOptions addUserTagsOptions)
        {
            addUserTagsOptions = _mapper.Map(_defaultOptions.addUserTagsOptions, addUserTagsOptions);
            DisplayCommandInfo(addUserTagsOptions);
            var addUserTagsCmd = AddUserTagsCmd.Create(addUserTagsOptions, userInfo: _userInfo);
            var addUserTagsResult = this._addUserTagHandler.Handle(addUserTagsCmd);
        }

        private void ExecuteDownload(DownloadOptions downloadOptions)
        {
            downloadOptions = _mapper.Map(_defaultOptions.downloadOptions, downloadOptions);
            DisplayCommandInfo(downloadOptions);
            var downloadCmd = DownloadFileCmd.Create(downloadOptions, _userInfo);
            var downloadResult = this._downloadFileHandler.Handle(downloadCmd);
        }

        private void ExecuteUpload(UploadOptions uploadOptions)
        {
            uploadOptions = _mapper.Map(_defaultOptions.uploadOptions, uploadOptions);

            DisplayCommandInfo(uploadOptions);

            var cmd = InitiateUploadCmd.CreateCommand(uploadOptions, _userInfo);
            var result = this._initiateUploadHandler.Handle(cmd);

            var uploadCmd = UploadFileCmd.Create(uploadOptions, cmd.InputFile, Guid.Parse(result.FileId), result.ChuckSize, result.TotalChucks, _mapper, _userInfo);
            var uploadResult = this._uploadFileHandler.Handle(uploadCmd);
        }

        private void DisplayCommandInfo(IOptions options)
        {
            _logger.LogInformation($"Executing: {options.GetType().Name.Replace("Options",". Arguments:")}:{JsonConvert.SerializeObject(options)}");
        }
    }
}

