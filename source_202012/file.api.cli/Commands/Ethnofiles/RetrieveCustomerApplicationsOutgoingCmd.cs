using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RetrieveCustomerApplicationsOutgoingCmd : CliCommand<RetrieveCustomerApplicationsOutgoingResult>
    {
        public string DownloadFolder { get; set; }
        public int FileDirection { get; set; }
        protected RetrieveCustomerApplicationsOutgoingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveCustomerApplicationsOutgoingCmd Create(RetrieveCustomerApplicationsOutgoingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveCustomerApplicationsOutgoingCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 1
            };
            return cmd;
        }
    }
    public class RetrieveCustomerApplicationsOutgoingResult : IResult
    {

    }
}
