using FileapiCli.Commands;
using FileapiCli.Core;

namespace FileapiCli.CommandValidations.Validations
{
    class SetPasswordValidator : ICommandValidator<SetPasswordCmd>
    {
        public bool IsCommandValid(SetPasswordCmd command)
        {
            return true;
        }
    }
}
