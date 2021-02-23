using System;
using System.Collections.Generic;
using System.Text;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands.MassPayment
{
    public class MassPaymentOutcomeCmd : CliCommand<MassPaymentOutcomeResult>
    {
        public string FileId { get; set; }
        public string DownloadFolder { get; set; }

        protected MassPaymentOutcomeCmd(IUserInfo userInfo)
            : base(userInfo) { }

        internal static MassPaymentOutcomeCmd Create(MassPaymentOutcomeOption opts, IUserInfo userInfo)
        {
            var cmd = new MassPaymentOutcomeCmd(userInfo)
            {
                FileId = opts.FileId,
                DownloadFolder = opts.DownloadFolder
            };
            return cmd;
        }
        public static bool ValidateInput(MassPaymentOutcomeOption opts)
        {
            if (!Guid.TryParse(opts.FileId, out Guid _))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class MassPaymentOutcomeResult : IResult
    {
    }
}
