using System;
using FileapiCli.Commands.MassTransfers;
using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    class MassTransfersCreditValidator : ICommandValidator<MassTransfersCreditCmd>
    {
        public bool IsCommandValid(MassTransfersCreditCmd command)
        {
            if (!Guid.TryParse(command.FileId, out _))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a valid Guid.");
            }

            return true;
        }
    }
}
