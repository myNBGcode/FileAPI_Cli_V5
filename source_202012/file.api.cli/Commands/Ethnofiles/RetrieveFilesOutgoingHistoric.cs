using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class RetrieveFilesOutgoingHistoricCmd : CliCommand<RetrieveFilesOutgoingHistoricResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFilesOutgoingHistoricCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFilesOutgoingHistoricCmd Create(RetrieveFilesOutgoingHistoricOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFilesOutgoingHistoricCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 1,
                IsHistorical = true,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo
            };
            return cmd;
        }
    }
    public class RetrieveFilesOutgoingHistoricResult : IResult
    {

    }
}
