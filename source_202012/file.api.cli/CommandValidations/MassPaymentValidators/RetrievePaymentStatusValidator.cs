using FileapiCli.Core;
using System;
using System.IO;

namespace FileapiCli.Commands.Validations
{
    class RetrievePaymentStatusValidator : ICommandValidator<RetrieveStatusCmd>
    {
        public bool IsCommandValid(RetrieveStatusCmd command)
        {
            if (!Guid.TryParse(command.FileId, out Guid result))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a valid Guid.");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder = System.IO.Path.GetDirectoryName(path) + @"\"; 
            }
            if (!Directory.Exists(command.DownloadFolder))
            {
                throw new ArgumentException($"Download folder: \"{command.DownloadFolder}\" not found on user's PC.");
            }
            return true;
        }
    }
}
