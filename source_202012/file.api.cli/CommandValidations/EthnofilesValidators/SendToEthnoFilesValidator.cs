using System;
using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    class SendToEthnoFilesValidator : ICommandValidator<SendFileToEthnoFilesCmd>
    {
        public bool IsCommandValid(SendFileToEthnoFilesCmd command)
        {
            if (!Guid.TryParse(command.FileId, out Guid result))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a Guid.");
            }
         
            return true;
        }
    }
}
