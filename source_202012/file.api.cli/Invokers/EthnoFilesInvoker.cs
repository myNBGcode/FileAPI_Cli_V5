using AutoMapper;
using FileapiCli.CommandHandlers;
using FileapiCli.Commands;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using proxy.types;
using Serilog;
using System;

namespace FileapiCli
{
    public class EthnoFilesInvoker
    {
        #region fields
        private readonly ILogger<EthnoFilesInvoker> _logger;
        private readonly IUserInfo _userInfo;
        private readonly DefaultOptions _defaultOptions;
        private readonly IMapper _mapper;

        private readonly ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> _initiateUploadHandler;
        private readonly ICommandHandler<UploadFileCmd, UploadFileCmdResult> _uploadFileHandler;
        private readonly ICommandHandler<SendFileToEthnoFilesCmd, SendFileToEthnoFilesCmdResult2> _sentToEthnoFilesHandler;
        private readonly ICommandHandler<PopulateFileTypesCmd, PopulateFileTypesResult> _populateFileTypesHandler;
        private readonly ICommandHandler<RetrieveCustomerApplicationsOutgoingCmd, RetrieveCustomerApplicationsOutgoingResult> _retrieveCustomerApplicationsOutgoingHandler;
        private readonly ICommandHandler<RetrieveCustomerApplicationsIncomingCmd, RetrieveCustomerApplicationsIncomingResult> _retrieveCustomerApplicationsIncomingHandler;
        private readonly ICommandHandler<RetrieveFileOutgoingCmd, RetrieveFileOutgoingResult> _retrieveFileOutgoingHandler;
        private readonly ICommandHandler<RetrieveFileIncomingCmd, RetrieveFileIncomingResult> _retrieveFileIncomingHandler;
        private readonly ICommandHandler<RetrieveFilesIncomingHistoricCmd, RetrieveFilesIncomingHistoricResult> _retrieveFilesIncomingHistoricHandler;
        private readonly ICommandHandler<RetrieveFilesIncomingCmd, RetrieveFilesIncomingResult> _retrieveFilesIncomingHandler;
        private readonly ICommandHandler<RetrieveFilesOutgoingHistoricCmd, RetrieveFilesOutgoingHistoricResult> _retrieveFilesOutgoingHistoricHandler;
        private readonly ICommandHandler<RetrieveFilesOutgoingCmd, RetrieveFilesOutgoingResult> _retrieveFilesOutgoingHandler;
        private readonly ICommandHandler<DownloadFilesOutgoingCmd, DownloadFilesOutgoingResult> _downloadFilesOutgoingHandler;
        private readonly ICommandHandler<DownloadFilesIncomingCmd, DownloadFilesIncomingResult> _downloadFilesIncomingHandler;
        private readonly ICommandHandler<SendEthnofilesCmd, SendEthnofilesCmdResult> _sendEthnofilesHandler;
        
        #endregion

        #region ctor
        public EthnoFilesInvoker(IUserInfo userInfo,
                                ILoggerFactory loggerFactory,
                                DefaultOptions defaultOptions,
                                IMapper mapper,
                                ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> initiateUploadHandler,
                                ICommandHandler<UploadFileCmd, UploadFileCmdResult> uploadFileHandler,
                                ICommandHandler<SendFileToEthnoFilesCmd, SendFileToEthnoFilesCmdResult2> sendtoEthnoFilersHandler,
                                ICommandHandler<PopulateFileTypesCmd, PopulateFileTypesResult> populateFileTypesHandler,
                                ICommandHandler<RetrieveCustomerApplicationsOutgoingCmd, RetrieveCustomerApplicationsOutgoingResult> retrieveCustomerApplicationsOutgoingHandler,
                                ICommandHandler<RetrieveCustomerApplicationsIncomingCmd, RetrieveCustomerApplicationsIncomingResult> retrieveCustomerApplicationsIncomingHandler,
                                ICommandHandler<RetrieveFileOutgoingCmd, RetrieveFileOutgoingResult> retrieveFileOutgoingHandler,
                                ICommandHandler<RetrieveFileIncomingCmd, RetrieveFileIncomingResult> retrieveFileIncomingHandler,
                                ICommandHandler<RetrieveFilesIncomingHistoricCmd, RetrieveFilesIncomingHistoricResult> retrieveFilesIncomingHistoricHandler,
                                ICommandHandler<RetrieveFilesIncomingCmd, RetrieveFilesIncomingResult> retrieveFilesIncomingHandler,
                                ICommandHandler<RetrieveFilesOutgoingHistoricCmd, RetrieveFilesOutgoingHistoricResult> retrieveFilesOutgoingHistoricHandler,
                                ICommandHandler<RetrieveFilesOutgoingCmd, RetrieveFilesOutgoingResult> retrieveFilesOutgoingHandler,
                                ICommandHandler<DownloadFilesOutgoingCmd, DownloadFilesOutgoingResult> downloadFilesOutgoingHandler,
                                ICommandHandler<DownloadFilesIncomingCmd, DownloadFilesIncomingResult> downloadFilesIncomingHandler,
                                ICommandHandler<SendEthnofilesCmd, SendEthnofilesCmdResult> sendEthnofilesHandler
            )

        #endregion

        {
            #region assigment
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            _logger = loggerFactory.CreateLogger<EthnoFilesInvoker>();
            _userInfo = userInfo;
            _defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _initiateUploadHandler = initiateUploadHandler;
            _uploadFileHandler = uploadFileHandler;
            _sentToEthnoFilesHandler = sendtoEthnoFilersHandler ?? throw new ArgumentNullException(nameof(sendtoEthnoFilersHandler));
            this._populateFileTypesHandler = populateFileTypesHandler ?? throw new ArgumentNullException(nameof(populateFileTypesHandler));
            _retrieveCustomerApplicationsOutgoingHandler = retrieveCustomerApplicationsOutgoingHandler ?? throw new ArgumentNullException(nameof(retrieveCustomerApplicationsOutgoingHandler));
            _retrieveCustomerApplicationsIncomingHandler = retrieveCustomerApplicationsIncomingHandler ?? throw new ArgumentNullException(nameof(retrieveCustomerApplicationsIncomingHandler));
            _retrieveFileOutgoingHandler = retrieveFileOutgoingHandler ?? throw new ArgumentNullException(nameof(retrieveFileOutgoingHandler));
            _retrieveFileIncomingHandler = retrieveFileIncomingHandler ?? throw new ArgumentNullException(nameof(retrieveFileIncomingHandler));
            _retrieveFilesIncomingHistoricHandler = retrieveFilesIncomingHistoricHandler ?? throw new ArgumentNullException(nameof(retrieveFilesIncomingHistoricHandler));
            _retrieveFilesIncomingHandler = retrieveFilesIncomingHandler ?? throw new ArgumentNullException(nameof(retrieveFilesIncomingHandler));
            _retrieveFilesOutgoingHistoricHandler = retrieveFilesOutgoingHistoricHandler ?? throw new ArgumentNullException(nameof(retrieveFilesOutgoingHistoricHandler));
            _retrieveFilesOutgoingHandler = retrieveFilesOutgoingHandler ?? throw new ArgumentNullException(nameof(retrieveFilesOutgoingHandler));
            _downloadFilesOutgoingHandler = downloadFilesOutgoingHandler ?? throw new ArgumentNullException(nameof(downloadFilesOutgoingHandler));
            _downloadFilesIncomingHandler = downloadFilesIncomingHandler ?? throw new ArgumentNullException(nameof(downloadFilesIncomingHandler));
            _sendEthnofilesHandler = sendEthnofilesHandler ?? throw new ArgumentNullException(nameof(sendEthnofilesHandler));
            #endregion
        }

        internal void Run(object parserResult)
        {
            switch (parserResult)
            {
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
                case PopulateFileTypesOptions populateFileTypesOptions:
                    ExecutePopulateFileTypes(populateFileTypesOptions);
                    break;
                case RetrieveCustomerApplicationsOutgoingOptions retrieveCustomerApplicationsOutgoingOptions:
                    ExecuteRetrieveCustomerApplicationsOutgoing(retrieveCustomerApplicationsOutgoingOptions);
                    break;
                case RetrieveCustomerApplicationsIncomingOptions retrieveCustomerApplicationsIncomingOptions:
                    ExecuteRetrieveCustomerApplicationsIncoming(retrieveCustomerApplicationsIncomingOptions);
                    break;
                case RetrieveFileOutgoingOptions retrieveFileOutgoingOptions:
                    ExecuteRetrieveFileOutgoing(retrieveFileOutgoingOptions);
                    break;
                case RetrieveFileIncomingOptions retrieveFileIncomingOptions:
                    ExecuteRetrieveFileIncoming(retrieveFileIncomingOptions);
                    break;
                case RetrieveFilesIncomingHistoricOptions retrieveFilesIncomingHistoricOptions:
                    ExecuteRetrieveFilesIncomingHistoric(retrieveFilesIncomingHistoricOptions);
                    break;
                case RetrieveFilesIncomingOptions retrieveFilesIncomingOptions:
                    ExecuteRetrieveFilesIncoming(retrieveFilesIncomingOptions);
                    break;
                case RetrieveFilesOutgoingHistoricOptions retrieveFilesOutgoingHistoricOptions:
                    ExecuteRetrieveFilesOutgoingHistoric(retrieveFilesOutgoingHistoricOptions);
                    break;
                case RetrieveFilesOutgoingOptions retrieveFilesOutgoingOptions:
                    ExecuteRetrieveFilesOutgoing(retrieveFilesOutgoingOptions);
                    break;
                case DownloadFilesOutgoingOptions downloadFilesOutgoingOptions:
                    ExecuteDownloadFilesOutgoing(downloadFilesOutgoingOptions);
                    break;
                case DownloadFilesIncomingOptions downloadFilesIncomingOptions:
                    ExecuteDownloadFilesIncoming(downloadFilesIncomingOptions);
                    break;
                case SendEthnofilesOptions sendEthnofilesOptions:
                    ExecuteSendEthnofiles(sendEthnofilesOptions);
                    break;
                default:
                    Console.WriteLine($"A instance of type {parserResult.GetType().Name}");
                    break;
            }
        }

        private void ExecuteSendEthnofiles(SendEthnofilesOptions sendEthnofilesOptions)
        {
            sendEthnofilesOptions = _mapper.Map(_defaultOptions.SendEthnofilesOptions, sendEthnofilesOptions);
            DisplayCommandInfo(sendEthnofilesOptions);

            //initiate file upload
            var uploadOptions = _mapper.Map<SendEthnofilesOptions, UploadOptions>(sendEthnofilesOptions);
            _logger.LogDebug($"Upload options:{JsonConvert.SerializeObject(uploadOptions)}");
            var cmd = InitiateUploadCmd.CreateCommand(uploadOptions, _userInfo);
            var result = this._initiateUploadHandler.Handle(cmd);

            //upload file
            var uploadCmd = UploadFileCmd.Create(uploadOptions, cmd.InputFile, Guid.Parse(result.FileId), result.ChuckSize, result.TotalChucks, _mapper, _userInfo);
            var uploadResult = this._uploadFileHandler.Handle(uploadCmd);

            //send to ethnofiles
            var ethnoCmd = SendEthnofilesCmd.Create(sendEthnofilesOptions, uploadCmd.FileId, userInfo: _userInfo);
            var resultEthno = _sendEthnofilesHandler.Handle(ethnoCmd);
        }

        private void ExecuteProcessFile(ProcessEnthofilesFileOptions processEnthofilesFileOptions)
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

        private void ExecutePopulateFileTypes(PopulateFileTypesOptions populateFileTypesOptions)
        {
            var populateFileHandlerCmd = PopulateFileTypesCmd.Create(populateFileTypesOptions, _userInfo);
            var result = this._populateFileTypesHandler.Handle(populateFileHandlerCmd);
        }
        private void ExecuteRetrieveCustomerApplicationsOutgoing(RetrieveCustomerApplicationsOutgoingOptions retrieveCustomerApplicationsOutgoingOptions)
        {
            retrieveCustomerApplicationsOutgoingOptions = _mapper.Map(_defaultOptions.RetrieveCustomerApplicationsOutgoingOptions, retrieveCustomerApplicationsOutgoingOptions);
            DisplayCommandInfo(retrieveCustomerApplicationsOutgoingOptions);
            var retrieveCustomerApplicationsOutgoingCmd = RetrieveCustomerApplicationsOutgoingCmd.Create(retrieveCustomerApplicationsOutgoingOptions, _userInfo);
            var result = this._retrieveCustomerApplicationsOutgoingHandler.Handle(retrieveCustomerApplicationsOutgoingCmd);
        }
        private void ExecuteRetrieveCustomerApplicationsIncoming(RetrieveCustomerApplicationsIncomingOptions retrieveCustomerApplicationsIncomingOptions)
        {
            retrieveCustomerApplicationsIncomingOptions = _mapper.Map(_defaultOptions.RetrieveCustomerApplicationsIncomingOptions, retrieveCustomerApplicationsIncomingOptions);
            DisplayCommandInfo(retrieveCustomerApplicationsIncomingOptions);
            var retrieveCustomerApplicationsIncomingCmd = RetrieveCustomerApplicationsIncomingCmd.Create(retrieveCustomerApplicationsIncomingOptions, _userInfo);
            var result = this._retrieveCustomerApplicationsIncomingHandler.Handle(retrieveCustomerApplicationsIncomingCmd);
        }
        private void ExecuteRetrieveFileOutgoing(RetrieveFileOutgoingOptions retrieveFileOutgoingOptions)
        {
            retrieveFileOutgoingOptions = _mapper.Map(_defaultOptions.RetrieveFileOutgoingOptions, retrieveFileOutgoingOptions);
            DisplayCommandInfo(retrieveFileOutgoingOptions);
            var retrieveFileOutgoingCmd = RetrieveFileOutgoingCmd.Create(retrieveFileOutgoingOptions, _userInfo);
            var result = this._retrieveFileOutgoingHandler.Handle(retrieveFileOutgoingCmd);
        }
        private void ExecuteRetrieveFileIncoming(RetrieveFileIncomingOptions retrieveFileIncomingOptions)
        {
            retrieveFileIncomingOptions = _mapper.Map(_defaultOptions.RetrieveFileIncomingOptions, retrieveFileIncomingOptions);
            DisplayCommandInfo(retrieveFileIncomingOptions);
            var retrieveFileIncomingCmd = RetrieveFileIncomingCmd.Create(retrieveFileIncomingOptions, _userInfo);
            var result = this._retrieveFileIncomingHandler.Handle(retrieveFileIncomingCmd);
        }
        private void ExecuteRetrieveFilesIncomingHistoric(RetrieveFilesIncomingHistoricOptions retrieveFilesIncomingHistoricOptions)
        {
            retrieveFilesIncomingHistoricOptions = _mapper.Map(_defaultOptions.RetrieveFilesIncomingHistoricOptions, retrieveFilesIncomingHistoricOptions);
            DisplayCommandInfo(retrieveFilesIncomingHistoricOptions);
            var retrieveFilesIncomingHistoricCmd = RetrieveFilesIncomingHistoricCmd.Create(retrieveFilesIncomingHistoricOptions, _userInfo);
            var result = this._retrieveFilesIncomingHistoricHandler.Handle(retrieveFilesIncomingHistoricCmd);
        }
        private void ExecuteRetrieveFilesIncoming(RetrieveFilesIncomingOptions retrieveFilesIncomingOptions)
        {
            retrieveFilesIncomingOptions = _mapper.Map(_defaultOptions.RetrieveFilesIncomingOptions, retrieveFilesIncomingOptions);
            DisplayCommandInfo(retrieveFilesIncomingOptions);
            var retrieveFilesIncomingCmd = RetrieveFilesIncomingCmd.Create(retrieveFilesIncomingOptions, _userInfo);
            var result = this._retrieveFilesIncomingHandler.Handle(retrieveFilesIncomingCmd);
        }
        private void ExecuteRetrieveFilesOutgoingHistoric(RetrieveFilesOutgoingHistoricOptions retrieveFilesOutgoingHistoricOptions)
        {
            retrieveFilesOutgoingHistoricOptions = _mapper.Map(_defaultOptions.RetrieveFilesOutgoingHistoricOptions, retrieveFilesOutgoingHistoricOptions);
            DisplayCommandInfo(retrieveFilesOutgoingHistoricOptions);
            var retrieveFilesOutgoingHistoricCmd = RetrieveFilesOutgoingHistoricCmd.Create(retrieveFilesOutgoingHistoricOptions, _userInfo);
            var result = this._retrieveFilesOutgoingHistoricHandler.Handle(retrieveFilesOutgoingHistoricCmd);
        }
        private void ExecuteRetrieveFilesOutgoing(RetrieveFilesOutgoingOptions retrieveFilesOutgoingOptions)
        {
            retrieveFilesOutgoingOptions = _mapper.Map(_defaultOptions.RetrieveFilesOutgoingOptions, retrieveFilesOutgoingOptions);
            DisplayCommandInfo(retrieveFilesOutgoingOptions);
            var retrieveFilesOutgoingCmd = RetrieveFilesOutgoingCmd.Create(retrieveFilesOutgoingOptions, _userInfo);
            var result = this._retrieveFilesOutgoingHandler.Handle(retrieveFilesOutgoingCmd);
        }
        private void ExecuteDownloadFilesOutgoing(DownloadFilesOutgoingOptions downloadFilesOutgoingOptions)
        {
            downloadFilesOutgoingOptions = _mapper.Map(_defaultOptions.DownloadFilesOutgoingOptions, downloadFilesOutgoingOptions);
            DisplayCommandInfo(downloadFilesOutgoingOptions);
            var downloadFilesOutgoingCmd = DownloadFilesOutgoingCmd.Create(downloadFilesOutgoingOptions, _userInfo);
            var result = this._downloadFilesOutgoingHandler.Handle(downloadFilesOutgoingCmd);
        }
        private void ExecuteDownloadFilesIncoming(DownloadFilesIncomingOptions downloadFilesIncomingOptions)
        {
            downloadFilesIncomingOptions = _mapper.Map(_defaultOptions.DownloadFilesIncomingOptions, downloadFilesIncomingOptions);
            DisplayCommandInfo(downloadFilesIncomingOptions);
            var downloadFilesIncomingCmd = DownloadFilesIncomingCmd.Create(downloadFilesIncomingOptions, _userInfo);
            var result = this._downloadFilesIncomingHandler.Handle(downloadFilesIncomingCmd);
        }

        private void DisplayCommandInfo(IOptions options)
        {
            _logger.LogInformation($"Executing: {options.GetType().Name.Replace("Options", ". Arguments:")}:{JsonConvert.SerializeObject(options)}");
        }
    }
}

