using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class DownloadFilesOutgoingCmd : CliCommand<DownloadFilesOutgoingResult>
    {
        public int FileDirection { get; set; }
        public bool? IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        public int MaxItems { get; set; }

        protected DownloadFilesOutgoingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }

        internal static DownloadFilesOutgoingCmd Create(DownloadFilesOutgoingOptions opts, IUserInfo userInfo)
        {
            var cmd = new DownloadFilesOutgoingCmd(userInfo)
            {
                DownloadFolder = opts.DownloadFolder,
                FileDirection = 1,
                IsHistorical = opts.IsHistorical,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo,
                MaxItems = opts.MaxItems == null ? 0 : (int)opts.MaxItems
            };
            return cmd;
        }
    }
    public class DownloadFilesOutgoingResult : IResult
    {

    }

}

