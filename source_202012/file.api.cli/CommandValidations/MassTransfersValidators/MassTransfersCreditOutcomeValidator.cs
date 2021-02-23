using System;
using System.IO;
using FileapiCli.Commands.MassTransfers;
using FileapiCli.Core;
using Serilog;

namespace FileapiCli.Commands.Validations
{
    class MassTransfersCreditOutcomeValidator : ICommandValidator<MassTransfersCreditOutcomeCmd>
    {
        public bool IsCommandValid(MassTransfersCreditOutcomeCmd command)
        {
            if (!Guid.TryParse(command.FileId, out _))
            {
                throw new ArgumentException($"{nameof(command.FileId)} is not a valid Guid.");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                Log.Warning("Download Folder not specified. Using current directory.");
                var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder = Path.GetDirectoryName(path) + @"\";
            }
            if (!Directory.Exists(command.DownloadFolder))
            {
                throw new ArgumentException($"Download folder: \"{command.DownloadFolder}\" not found on user's PC.");
            }
            return true;
        }
    }
}
