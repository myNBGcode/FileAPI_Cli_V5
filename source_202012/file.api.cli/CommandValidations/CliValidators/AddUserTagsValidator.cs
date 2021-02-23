using FileapiCli.Core;
using System;

namespace FileapiCli.Commands.Validations
{
    class AddUserTagsValidator : ICommandValidator<AddUserTagsCmd>
    {
        public bool IsCommandValid(AddUserTagsCmd command)
        {
            if (string.IsNullOrEmpty(command.FileId))
            {
                throw new ArgumentException($"{nameof(command.FileId)} cannot be empty");
            }
            Guid result;
            if (!Guid.TryParse(command.FileId, out result))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a Guid.");
            }
            return true;
        }
    }
}
