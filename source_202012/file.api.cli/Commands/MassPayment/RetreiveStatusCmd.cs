using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RetrieveStatusCmd : CliCommand<RetrieveStatusResult>
    {
        public string FileId { get; internal set; }
       
        public string DownloadFolder { get; set; }
  
        protected RetrieveStatusCmd(IUserInfo userInfo)
           : base(userInfo) {}

        internal static RetrieveStatusCmd Create(RetrievePaymentStatusOption opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveStatusCmd(userInfo)
            {
                FileId = opts.fileid,
                DownloadFolder = opts.DownloadFolder
            };
            return cmd;
        }
        public static bool ValidateInput(RetrievePaymentStatusOption opts)
        {
            if (!Guid.TryParse(opts.fileid, out Guid _))
            {
                throw new ArgumentException($"{nameof(opts.fileid)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class RetrieveStatusResult : IResult
    {
        public string Result { get; set; }
    }
}



