using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RetrieveFileOutgoingCmd : CliCommand<RetrieveFileOutgoingResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public int CustomerApplicationFileId { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFileOutgoingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFileOutgoingCmd Create(RetrieveFileOutgoingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFileOutgoingCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                CustomerApplicationFileId = opts.CustomerApplicationFileId == null ? 0 : (int)opts.CustomerApplicationFileId,
                IsHistorical = opts.IsHistorical == null ? false : (bool)opts.IsHistorical,
                FileDirection = 1
            };
            return cmd;
        }
    }
    public class RetrieveFileOutgoingResult : IResult
    {

    }
}
