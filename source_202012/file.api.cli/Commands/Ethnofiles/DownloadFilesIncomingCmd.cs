using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FileapiCli.Commands
{
    public class DownloadFilesIncomingCmd : CliCommand<DownloadFilesIncomingResult>
    {
        public int FileDirection { get; set; }
        public bool? IsHistorical { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DownloadFolder { get; set; }
        public int MaxItems { get; set; }

        protected DownloadFilesIncomingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }

        internal static DownloadFilesIncomingCmd Create(DownloadFilesIncomingOptions opts, IUserInfo userInfo)
        {
            var cmd = new DownloadFilesIncomingCmd(userInfo)
            {
                DownloadFolder = opts.DownloadFolder,
                FileDirection = 0,
                IsHistorical = opts.IsHistorical,
                DateFrom = opts.DateFrom == null ? DateTime.MinValue : (DateTime)opts.DateFrom,
                DateTo = opts.DateTo,
                MaxItems = opts.MaxItems == null ? 0 : (int)opts.MaxItems
        };
            return cmd;
        }
    }
    public class DownloadFilesIncomingResult : IResult
    {

    }

}

