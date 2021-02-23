using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class MassPaymentCmd : CliCommand<MassPaymentResult>
    {
        public string FileId { get; set; }
        public string Filename { get; set; }
        public string DebtorName { get; set; }
        public string DebtorTelephone { get; set; }
        public string DebtorIBAN { get; set; }
        public string Ccy { get; set; }
        public bool AcceptDuplicate { get; set; }
        public string DownloadFolder { get; set; }

        protected MassPaymentCmd(IUserInfo userInfo) : base(userInfo) {}

        internal static MassPaymentCmd Create(MassPaymentOption opts, IUserInfo userInfo)
        {
            var cmd = new MassPaymentCmd(userInfo)
            {
              
                FileId= opts.FileId,
                Filename = opts.Filename,
                DebtorName=opts.DebtorName,
                DebtorTelephone=opts.DebtorTelephone,
                DebtorIBAN=opts.DebtorIBan,
                Ccy=opts.Ccy,
                AcceptDuplicate=opts.AcceptDuplicate,
                DownloadFolder = opts.DownloadFolder,
            };
            return cmd;
        }
        public static bool ValidateInput(MassPaymentOption opts)
        {
            if (!Guid.TryParse(opts.FileId, out Guid _))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class MassPaymentResult : IResult
    {
        public string Result { get; set; }
    }
}



