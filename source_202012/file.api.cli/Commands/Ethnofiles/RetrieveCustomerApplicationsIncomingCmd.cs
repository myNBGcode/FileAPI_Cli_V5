using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands
{
    public class RetrieveCustomerApplicationsIncomingCmd : CliCommand<RetrieveCustomerApplicationsIncomingResult>
    {
        public string DownloadFolder { get; set; }
        public int FileDirection { get; set; }
        protected RetrieveCustomerApplicationsIncomingCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RetrieveCustomerApplicationsIncomingCmd Create(RetrieveCustomerApplicationsIncomingOptions opts, IUserInfo userInfo)
        {
            var cmd = new RetrieveCustomerApplicationsIncomingCmd(userInfo)
            {
                DownloadFolder=opts.DownloadFolder,
                FileDirection = 0
            };
            return cmd;
        }
    }
    public class RetrieveCustomerApplicationsIncomingResult : IResult
    {

    }
}
