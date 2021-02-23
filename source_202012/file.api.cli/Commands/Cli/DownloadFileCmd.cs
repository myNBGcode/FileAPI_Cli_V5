using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.IO;

namespace FileapiCli.Commands
{
    public class DownloadFileCmd : CliCommand<DownloadFileCmdResult>
    {
        public string FileId { get; set; }
        public string DownloadFolder { get; set; }

        protected DownloadFileCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static DownloadFileCmd Create (DownloadOptions opts, IUserInfo userInfo)
        {
            var downloadFileCmd = new DownloadFileCmd(userInfo)
            {
                FileId = opts.FileId,
                DownloadFolder=opts.DownloadFolder
            };

            return downloadFileCmd;
        }
        public static bool ValidateInput(DownloadOptions opts)
        {
            Guid result;
            if (!Guid.TryParse(opts.FileId, out result))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            if (!Directory.Exists(opts.DownloadFolder))
            {
                throw new ArgumentException($"{nameof(opts.DownloadFolder)} not found on user's PC.");
            }
            return true;
        }
    }

    public class DownloadFileCmdResult : IResult
    {
        
    }
}



