using FileapiCli.Core;
using Serilog;
using System;

namespace FileapiCli.Commands.Validations
{
    class RetrieveFileIncomingValidator : ICommandValidator<RetrieveFileIncomingCmd>
    {
        public bool IsCommandValid(RetrieveFileIncomingCmd command)
        {
            if (command.CustomerApplicationFileId == 0)
            {
                throw new ArgumentException($"{nameof(command.CustomerApplicationFileId)} is required and cannot be empty.");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                Log.Warning("Download Folder not specified. Using current directory.");
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder = System.IO.Path.GetDirectoryName(path) + @"\";
            }
            if (!System.IO.Directory.Exists(command.DownloadFolder))
            {
                throw new ArgumentException($"{nameof(command.DownloadFolder)}  directory does not exist.Please give a valid directory");
            }
            return true;
        }
    }
}
