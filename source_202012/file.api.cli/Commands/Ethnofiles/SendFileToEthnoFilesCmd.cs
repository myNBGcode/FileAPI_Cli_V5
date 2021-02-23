using AutoMapper;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;

namespace FileapiCli.Commands
{
    public class SendFileToEthnoFilesCmd : CliCommand<SendFileToEthnoFilesCmdResult2>
    {
        #region Fields
        // not an option
        // but mandatory by the service. What is it?
        public string InputFile { get; set; }
        
        public string FileId { get; set; }
   
        public string Idfield { get; set; }
        public int? RowCount { get; set; }
        public float? TotalSum { get; set; }
        public string DebtorIban { get; set; }
        public string TanNumber { get; set; }
        public string Locale { get; set; }
        public string InboxId { get; set; }
        public string XactionId { get; set; }
        public bool IsSmsOtp { get; set; }
        public string DebtorName { get; set; }
        public bool? AcceptTerms { get; set; }
        public bool? AcceptTrnTerms { get; set; }
        public bool DataFromFileName { get; set; }
        public string RowCountFromPainXml { get; set; }
        public string TotalSumFromPainXml { get; set; }


        #endregion
        protected SendFileToEthnoFilesCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static SendFileToEthnoFilesCmd Create(SendToEthnoFilesOptions opts, IMapper mapper, IUserInfo userInfo)
        {
            var cmd = new SendFileToEthnoFilesCmd(userInfo);
            cmd = mapper.Map<SendToEthnoFilesOptions, SendFileToEthnoFilesCmd>(opts, cmd);
           
            return cmd;
        }
        public static bool ValidateInput(SendToEthnoFilesOptions opts)
        {
            Guid result;
            if (!Guid.TryParse(opts.FileId, out result))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }
    public class SendFileToEthnoFilesCmdResult2 : IResult
    {
    }
}
