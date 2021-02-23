using System;
using AutoMapper;
using FileapiCli.CommandHandlers;
using FileapiCli.Commands;
using FileapiCli.Commands.MassTransfers;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FileapiCli
{
    public class MassTransfersInvoker
    {
        private readonly ILogger<FileCliInvoker> _logger;
        private readonly IUserInfo _userInfo;
        private readonly DefaultOptions _defaultOptions;
        private readonly IMapper _mapper;

        private readonly ICommandHandler<SampleMassTransfersCmd, SampleMassTransfersResult> _sampleMassTransfersHandler;
        private readonly ICommandHandler<MassTransfersCreditOutcomeCmd, MassTransfersCreditOutcomeResult> _massTransfersCreditOutcomeHandler;
        private readonly ICommandHandler<MassTransfersCreditCmd, MassTransfersCreditResult> _massTransfersCreditHandler;
        private readonly ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> _initiateUploadHandler;
        private readonly ICommandHandler<UploadFileCmd, UploadFileCmdResult> _uploadFileHandler;

        public MassTransfersInvoker(IUserInfo userInfo,
            ILoggerFactory loggerFactory,
            DefaultOptions defaultOptions,
            IMapper mapper,
            ICommandHandler<SampleMassTransfersCmd, SampleMassTransfersResult> sampleMassTransfersHandler, 
            ICommandHandler<MassTransfersCreditOutcomeCmd, MassTransfersCreditOutcomeResult> massTransfersCreditOutcomeHandler, 
            ICommandHandler<MassTransfersCreditCmd, MassTransfersCreditResult> massTransfersCreditHandler, 
            ICommandHandler<InitiateUploadCmd, InitiateUploadCmdResult> initiateUploadHandler, 
            ICommandHandler<UploadFileCmd, UploadFileCmdResult> uploadFileHandler)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            _logger = loggerFactory.CreateLogger<FileCliInvoker>();
            _userInfo = userInfo;
            _defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _sampleMassTransfersHandler = sampleMassTransfersHandler ?? throw new ArgumentNullException(nameof(sampleMassTransfersHandler));
            _massTransfersCreditOutcomeHandler = massTransfersCreditOutcomeHandler ?? throw new ArgumentNullException(nameof(massTransfersCreditOutcomeHandler));
            _massTransfersCreditHandler = massTransfersCreditHandler ?? throw new ArgumentNullException(nameof(massTransfersCreditHandler));
            _initiateUploadHandler = initiateUploadHandler ?? throw new ArgumentNullException(nameof(ArgumentNullException));
            _uploadFileHandler = uploadFileHandler ?? throw new ArgumentException(nameof(uploadFileHandler));
        }


        internal void Run(object parserResult)
        {
            switch (parserResult)
            {
                case SampleMassTransfersOption sampleMassTransferOption:
                {
                    ExecuteSampleMassTransfersFile(sampleMassTransferOption);
                    break;
                }
                case MassTransfersCreditOutcomeOption massTransfersCreditOutcomeOption:
                {
                    ExecuteMassTransfersCreditOutcome(massTransfersCreditOutcomeOption);
                    break;
                }
                case MassTransfersCreditOption massTransfersCreditOption:
                {
                    ExecuteMassTransfersCredit(massTransfersCreditOption);
                    break;
                }

                default:
                    Console.WriteLine($"A instance of type {parserResult.GetType().Name}");
                    break;
            }
        }

        
        private void ExecuteSampleMassTransfersFile(SampleMassTransfersOption sampleMassTransferOption)
        {
            sampleMassTransferOption = _mapper.Map(_defaultOptions.SampleMassTransfersOption, sampleMassTransferOption);
            DisplayCommandInfo(sampleMassTransferOption);
            var cmd = SampleMassTransfersCmd.Create(sampleMassTransferOption, _userInfo);
            var result = this._sampleMassTransfersHandler.Handle(cmd);
        }

        private void ExecuteMassTransfersCreditOutcome(MassTransfersCreditOutcomeOption massTransfersCreditOutcomeOption)
        {
            massTransfersCreditOutcomeOption = _mapper.Map(_defaultOptions.MassTransfersCreditOutcomeOption, massTransfersCreditOutcomeOption);
            DisplayCommandInfo(massTransfersCreditOutcomeOption);
            var massCmd = MassTransfersCreditOutcomeCmd.Create(massTransfersCreditOutcomeOption, _userInfo);
            var massResult = _massTransfersCreditOutcomeHandler.Handle(massCmd);
        }

        private void ExecuteMassTransfersCredit(MassTransfersCreditOption massTransfersCreditOption)
        {
            massTransfersCreditOption = _mapper.Map(_defaultOptions.MassTransfersCreditOption, massTransfersCreditOption);

            var uploadOptions = _mapper.Map<MassTransfersCreditOption, UploadOptions>(massTransfersCreditOption);
            _logger.LogDebug($"Upload options:{JsonConvert.SerializeObject(uploadOptions)}");
            var cmd = InitiateUploadCmd.CreateCommand(uploadOptions, _userInfo);
            var result = _initiateUploadHandler.Handle(cmd);
            var uploadCmd = UploadFileCmd.Create(uploadOptions, cmd.InputFile, Guid.Parse(result.FileId), result.ChuckSize, result.TotalChucks, _mapper, _userInfo);
            var uploadResult = _uploadFileHandler.Handle(uploadCmd);

            massTransfersCreditOption.FileId = uploadCmd.FileId.ToString();
            DisplayCommandInfo(massTransfersCreditOption);

            var massCmd = MassTransfersCreditCmd.Create(massTransfersCreditOption, _userInfo);
            var massResult = _massTransfersCreditHandler.Handle(massCmd);
        }
        
        private void DisplayCommandInfo(IOptions options)
        {
            _logger.LogInformation($"Executing: {options.GetType().Name.Replace("Option", ". Arguments:")}:{JsonConvert.SerializeObject(options)}");
        }
    }
}
