using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RetrieveFileIncomingCmd : CliCommand<RetrieveFileIncomingResult>
    {
        public int FileDirection { get; set; }
        public bool IsHistorical { get; set; }
        public int CustomerApplicationFileId { get; set; }
        public string DownloadFolder { get; set; }
        protected RetrieveFileIncomingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveFileIncomingCmd Create(RetrieveFileIncomingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveFileIncomingCmd(userInfo)
            {
                DownloadFolder = opts.DownloadFolder,
                CustomerApplicationFileId = opts.CustomerApplicationFileId == null ? 0 : (int)opts.CustomerApplicationFileId,
                IsHistorical = opts.IsHistorical == null ? false : (bool)opts.IsHistorical,
                FileDirection = 0
            };
            return cmd;
        }
    }
    public class RetrieveFileIncomingResult : IResult
    {

    }
}
