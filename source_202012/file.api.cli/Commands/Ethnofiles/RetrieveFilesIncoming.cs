using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class RetrieveFilesIncomingCmd : CliCommand<RetrieveFilesIncomingResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFilesIncomingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFilesIncomingCmd Create(RetrieveFilesIncomingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFilesIncomingCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 0,
                IsHistorical = false,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo
            };
            return cmd;
        }
    }
    public class RetrieveFilesIncomingResult : IResult
    {

    }
}
