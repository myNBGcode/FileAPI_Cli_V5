using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace FileapiCli.Core
{
    public abstract class CliCommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
        where TResult : IResult, new()
    {
        protected readonly ILogger<FileCliInvoker> _logger;
        protected readonly ICliService _cliService;
        protected readonly ICommandValidator<TCommand>_validator;

        protected CliCommandHandler(ILoggerFactory loggerFactory, ICliService cliService, ICommandValidator<TCommand> validator)
        {
            _logger = loggerFactory.CreateLogger<FileCliInvoker>();
            _cliService = cliService;
            _validator = validator;
        }

        public TResult Handle(TCommand command)
        {
            // Any other crosscutting concerns can go here. Authorization,Validation,  etc.
            _logger.LogDebug($"Executing Command Handler: {GetType().Name}");
            _logger.LogDebug($"Executing:{command.GetType().Name}: with arguments: {JsonConvert.SerializeObject(command)}");
            if (_validator !=null)
            {
                if (!_validator.IsCommandValid(command)) {
                    throw new ArgumentException("Command is not valid.");
                }
            }
            TResult _response;
            try
            {
                _response = DoHandle(command);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, $"Error in {typeof(TCommand).Name}");
                throw;
            }
            return _response;
        }

        protected abstract TResult DoHandle(TCommand request);
    }
}
