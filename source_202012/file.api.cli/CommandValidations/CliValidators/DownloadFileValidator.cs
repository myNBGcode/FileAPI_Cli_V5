using FileapiCli.Core;
using Serilog;
using System;

namespace FileapiCli.Commands.Validations
{
    class DownloadFileValidator : ICommandValidator<DownloadFileCmd>
    {
        public bool IsCommandValid(DownloadFileCmd command)
        {
            if (!Guid.TryParse(command.FileId, out Guid result))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a Guid.");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                Log.Warning("Download Folder not specified. Using current directotry.");
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder = System.IO.Path.GetDirectoryName(path) + @"\";
            }
            if (!System.IO.Directory.Exists(command.DownloadFolder)) {

                throw new ArgumentException($"{nameof(command.DownloadFolder)}  directory does not exist.Please give a valid directory");
            }
            return true;
        }
    }
}
