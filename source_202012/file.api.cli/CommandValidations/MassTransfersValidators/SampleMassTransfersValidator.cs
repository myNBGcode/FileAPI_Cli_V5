using System;
using System.IO;
using System.Linq;
using FileapiCli.Core;

namespace FileapiCli.Commands.Validations
{
    internal class SampleMassTransfersValidator : ICommandValidator<SampleMassTransfersCmd>
    {
        public bool IsCommandValid(SampleMassTransfersCmd command)
        {
            string[] validTypes = { "xml", "csv", "XML", "CSV" };

            if (!validTypes.Contains(command.FileFormat))
            {
                throw new ArgumentException($"{nameof(command.FileFormat)} is invalid. Valid Types are {string.Join(", ", validTypes) }");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder = Path.GetDirectoryName(path);
            }
            if (!Directory.Exists(command.DownloadFolder))
            {
                throw new ArgumentException($"Download folder: \"{command.DownloadFolder}\" not found on user's PC.");
            }
            return true;
        }
    }
}
