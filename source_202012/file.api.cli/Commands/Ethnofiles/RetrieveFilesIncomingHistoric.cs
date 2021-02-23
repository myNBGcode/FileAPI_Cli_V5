using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class RetrieveFilesIncomingHistoricCmd : CliCommand<RetrieveFilesIncomingHistoricResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFilesIncomingHistoricCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFilesIncomingHistoricCmd Create(RetrieveFilesIncomingHistoricOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFilesIncomingHistoricCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 0,
                IsHistorical = true,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo
            };
            return cmd;
        }
    }
    public class RetrieveFilesIncomingHistoricResult : IResult
    {

    }
}
