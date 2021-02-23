using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands.MassTransfers
{
    public class MassTransfersCreditOutcomeCmd : CliCommand<MassTransfersCreditOutcomeResult>
    {

        public string FileId { get; set; }
        public string DownloadFolder { get; set; }

        protected MassTransfersCreditOutcomeCmd(IUserInfo userInfo)
            : base(userInfo) { }

        internal static MassTransfersCreditOutcomeCmd Create(MassTransfersCreditOutcomeOption opts, IUserInfo userInfo)
        {
            var cmd = new MassTransfersCreditOutcomeCmd(userInfo)
            {
                FileId = opts.FileId,
                DownloadFolder = opts.DownloadFolder
            };
            return cmd;
        }
    }

    public class MassTransfersCreditOutcomeResult : IResult
    {
    }
}
