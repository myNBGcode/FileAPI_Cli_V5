using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class SendEthnofilesCmd : CliCommand<SendEthnofilesCmdResult>
    {
        public string InputFile { get; set; }
        public Guid FileId { get; set; }
        public int CustomerApplicationId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? TotalRecords { get; set; }
        public string DebtorIban { get; set; }
        public string DebtorName { get; set; }
        public bool? IsSmsOtp { get; set; }
        public bool AcceptTerms { get; set; }
        public bool AcceptTrnTerms { get; set; }
        public string TanNumber { get; set; }
        public string TotalSumFromPainXml { get; set; }
        public string RowCountFromPainXml { get; set; }
        

        protected SendEthnofilesCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static SendEthnofilesCmd Create(SendEthnofilesOptions opts, Guid fileId, IUserInfo userInfo)
        {
            var cmd = new SendEthnofilesCmd(userInfo)
            {
                InputFile = opts.InputFile,
                FileId = fileId,
                CustomerApplicationId = int.Parse(opts.CustomerApplicationId),
                TotalAmount = opts.TotalAmount,
                TotalRecords = opts.TotalRecords,
                DebtorIban = opts.DebtorIban,
                DebtorName = opts.DebtorName,
                IsSmsOtp = opts.IsSmsOtp,
                AcceptTerms = opts.AcceptTerms.GetValueOrDefault(),
                AcceptTrnTerms = opts.AcceptTrnTerms.GetValueOrDefault(),
                TanNumber = opts.TanNumber,
                TotalSumFromPainXml = opts.TotalSumFromPainXml,
                RowCountFromPainXml = opts.RowCountFromPainXml
            };
            return cmd;
        }

    }

    public class SendEthnofilesCmdResult : IResult
    {
    }
}
