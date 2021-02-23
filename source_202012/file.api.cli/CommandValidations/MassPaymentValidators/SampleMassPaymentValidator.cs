using FileapiCli.Core;
using System;
using System.Linq;
using System.IO;

namespace FileapiCli.Commands.Validations
{
    class SampleMassPaymentValidator : ICommandValidator<SampleMassPaymentCmd>
    {
        public bool IsCommandValid(SampleMassPaymentCmd command)
        {
            string[] validTypes = { "json", "xml", "csv" };

            if (!validTypes.Contains(command.FileFormat))
            {
                throw new ArgumentException($"{nameof(command.FileFormat)} is invalid. Valid Types are {string.Join(", ", validTypes) }");
            }
            if (string.IsNullOrEmpty(command.DownloadFolder))
            {
                //use execution path if DownloadFolder not specified.
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
                command.DownloadFolder= System.IO.Path.GetDirectoryName(path);
            }
            if (!Directory.Exists(command.DownloadFolder))
            {
                throw new ArgumentException($"Download folder: \"{command.DownloadFolder}\" not found on user's PC.");
            }
            return true;
        }
    }
}
