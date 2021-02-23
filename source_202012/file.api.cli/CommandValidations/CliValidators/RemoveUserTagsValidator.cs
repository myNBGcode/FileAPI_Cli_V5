using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    class RemoveUserTagsValidator : ICommandValidator<RemoveUserTagsCmd>
    {
        public bool IsCommandValid(RemoveUserTagsCmd command)
        {
            return true;
        }
    }
}
