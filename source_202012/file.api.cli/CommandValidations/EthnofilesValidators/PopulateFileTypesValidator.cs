
using FileapiCli.Commands;
using FileapiCli.Core;

namespace FileapiCli.CommandValidations.EthnofilesValidators
{
    class PopulateFileTypesValidator : ICommandValidator<PopulateFileTypesCmd>
    {
        public bool IsCommandValid(PopulateFileTypesCmd command)
        {
            return true;
        }
    }
}
