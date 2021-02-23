using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class RetrieveFilesOutgoingCmd : CliCommand<RetrieveFilesOutgoingResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFilesOutgoingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFilesOutgoingCmd Create(RetrieveFilesOutgoingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFilesOutgoingCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 1,
                IsHistorical = false,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo
            };
            return cmd;
        }
    }
    public class RetrieveFilesOutgoingResult : IResult
    {

    }
}
