using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class SampleMassTransfersCmd : CliCommand<SampleMassTransfersResult>
    {
        public string FileFormat { get; set; }
        public string DownloadFolder { get; set; }

        protected SampleMassTransfersCmd(IUserInfo userInfo)
            : base(userInfo) { }

        internal static SampleMassTransfersCmd Create(SampleMassTransfersOption opts, IUserInfo userInfo)
        {
            var cmd = new SampleMassTransfersCmd(userInfo)
            {

                FileFormat = opts.FileType,
                DownloadFolder = opts.DownloadFolder

            };

            return cmd;
        }
    }

    public class SampleMassTransfersResult : IResult
    {
        public string Result { get; set; }
    }
}
