using FileapiCli.CommandHandlers;
using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    class UploadFileValidator : ICommandValidator<UploadFileCmd>
    {
        public bool IsCommandValid(UploadFileCmd command)
        {
            return true;
        }
    }
}
