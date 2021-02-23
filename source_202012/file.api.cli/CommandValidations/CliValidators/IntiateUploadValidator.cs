using FileapiCli.Core;
using System;

namespace FileapiCli.Commands.Validations
{
    class IntiateUploadValidator : ICommandValidator<InitiateUploadCmd>
    {
        public bool IsCommandValid(InitiateUploadCmd command)
        {

            if (!System.IO.File.Exists(command.InputFile))
            {
                throw new ArgumentException($"File:\"{command.InputFile}\" not found on users PC.");
            }
            return true;
        }
    }
}
