using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    class SendEthnofilesValidator : ICommandValidator<SendEthnofilesCmd>
    {
        public bool IsCommandValid(SendEthnofilesCmd command)
        {
            return true;
        }
    }
}
