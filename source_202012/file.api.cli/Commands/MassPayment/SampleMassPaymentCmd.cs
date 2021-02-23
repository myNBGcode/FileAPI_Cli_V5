using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileapiCli.Commands
{
    public class SampleMassPaymentCmd : CliCommand<SampleMassPaymentResult>
    {
        public string FileFormat { get; set; }
        public string DownloadFolder { get; set; }

        protected SampleMassPaymentCmd(IUserInfo userInfo)
           : base(userInfo) { }

        internal static SampleMassPaymentCmd Create(SampleMassPaymentOption opts, IUserInfo userInfo)
        {
            //ValidateInput(opts);
            var cmd = new SampleMassPaymentCmd(userInfo)
            { 
                
                FileFormat=opts.FileType,
                DownloadFolder=opts.DownloadFolder
                
            };
            return cmd;
        }
        public static bool ValidateInput(SampleMassPaymentOption opts)
        {
            string[] validTypes = { "json", "xml", "csv" };
            
            if (!validTypes.Contains(opts.FileType))
            {
                throw new ArgumentException($"{nameof(opts.FileType)} is invalid. Valid Types are {string.Join(", ", validTypes) }");
            }
            if (!Directory.Exists(opts.DownloadFolder))
            {
                throw new ArgumentException($"{nameof(opts.DownloadFolder)} not found on user's PC.");
            }
            return true;
        }
    }

    public class SampleMassPaymentResult : IResult
    {
        public string Result { get; set; }
    }
}



