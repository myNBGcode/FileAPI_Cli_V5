using FileapiCli.Core;
using System;

namespace FileapiCli.Commands.Validations
{
    class RequestPaymentStatusValidator : ICommandValidator<RequestStatusCmd>
    {
        public bool IsCommandValid(RequestStatusCmd command)
        {
            if (!Guid.TryParse(command.FileId, out Guid result))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }
}
