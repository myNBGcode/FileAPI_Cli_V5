using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("sendethnofiles", HelpText = "Send a file to Ethnofiles")]
    public class SendEthnofilesOptions : IOptions
    {
        #region UploadOptions
        [Option('i', "inputfile", Required = false, HelpText = "Filename with full path, of the file to be uploaded.")]
        public string InputFile { get; set; }

        [Option('d', "description", HelpText = "Short description of the file.")]
        public string Description { get; set; }

        [Option('f', nameof(FolderId), HelpText = "Folder Guid.")]
        public string FolderId { get; set; }

        [Option('m', nameof(Metadata), HelpText = "File metadata.")]
        public string Metadata { get; set; }

        [Option('t', nameof(UserTags), HelpText = "Tags separated by coma ',' .")]
        public string UserTags { get; set; }

        #endregion

        #region EthnoSendOptions

        [Option('c', nameof(CustomerApplicationId), HelpText = "Required. Customer application Id.")]
        public string CustomerApplicationId { get; set; }

        [Option('r', nameof(TotalRecords), HelpText = "The count of the rows in the file.")]
        public int? TotalRecords { get; set; }

        [Option('a', nameof(TotalAmount), HelpText = "The total amount of the file.")]
        public decimal? TotalAmount { get; set; }

        [Option(HelpText = "Debtor Name")]
        public string DebtorName { get; set; }

        [Option(HelpText = "Debtor Iban")]
        public string DebtorIban { get; set; }

        [Option(HelpText = "Tan Number")]
        public string TanNumber { get; set; }

        [Option(HelpText = "Flag that indicates if is SmsOtp")]
        public bool? IsSmsOtp { get; set; }

        [Option(HelpText = " Accept terms flag")]
        public bool? AcceptTerms { get; set; }

        [Option(HelpText = "Accept Trn Terms flag")]
        public bool? AcceptTrnTerms { get; set; }

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"NbOfTxs\".")]
        public string RowCountFromPainXml { get; set; }

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"CtrlSum\".")]
        public string TotalSumFromPainXml { get; set; }

        #endregion
    }
}
