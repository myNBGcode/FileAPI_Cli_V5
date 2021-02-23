using System;
using FileapiCli.Core;
using Serilog;

namespace FileapiCli.Commands.Validations
{
    class MassPaymentValidator : ICommandValidator<MassPaymentCmd>
    {
        public bool IsCommandValid(MassPaymentCmd command)
        {
            if (!Guid.TryParse(command.FileId, out Guid _))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a valid Guid.");
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

                throw new ArgumentException($"{nameof(command.DownloadFolder)} directory does not exist.Please give a valid directory");
            }
            return true;
        }
    }
}
