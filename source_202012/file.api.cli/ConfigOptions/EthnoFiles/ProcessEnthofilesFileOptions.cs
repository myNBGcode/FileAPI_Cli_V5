using CommandLine;
using System.Collections.Generic;

namespace FileapiCli.ConfigOptions
{
    [Verb("processenthofilesfile", HelpText = "Uploads a file to file api and sends it to ethnofiles")]
    public class ProcessEnthofilesFileOptions : IOptions
    {
        #region UploadOptions
        [Option('i', "inputfile", Required = false, HelpText = "Filename with full path, of the file to be uploaded. (Used in Upload and Process File operations.)")]
        public string InputFile { get; set; }

        [Option('d', "description", HelpText = "Short description of the file")]
        public string Description { get; set; }

        [Option('f', nameof(FolderId), HelpText = "Folder Guid")]
        public string FolderId { get; set; }

        [Option('m', nameof(Metadata), HelpText = "Folder Guid. (Used in Upload and Process File operations.)")]
        public string Metadata { get; set; }

        [Option('t', nameof(UserTags), HelpText = "Tags separated by coma ',' (Used in Upload and Process File operations.)")]
        public string UserTags { get; set; }

        #endregion

        #region EthnoSendOptions

        [Option(HelpText = " File Type Id")]
        public string IdField { get; set; }

        [Option(HelpText = "The count of the rows in the file")]
        public int? RowCount { get; set; }

        [Option(HelpText = "The total sum of the file")]
        public float? TotalSum { get; set; }

        [Option(HelpText = "Debtor Iban")]
        public string DebtorIban { get; set; }

        [Option(HelpText = "Tan Number")]
        public string TanNumber { get; set; }

        [Option(HelpText = "Locale")]
        public string Locale { get; set; }

        [Option(HelpText = "Inbox Id")]
        public string InboxId { get; set; }

        [Option(HelpText = "Xaction Id")]
        public string XactionId { get; set; }

        [Option(HelpText = "Flag that indicates if is SmsOtp")]
        public bool? IsSmsOtp { get; set; }

        [Option(HelpText = "Debtor Name")]
        public string DebtorName { get; set; }

        [Option(HelpText = " Accept terms flag")]
        public bool? AcceptTerms { get; set; }

        [Option(HelpText = "Accept Ttn Terms flag")]
        public bool? AcceptTrnTerms { get; set; }

        [Option(Default = false, HelpText = "False by default. Set it to true to get row count and total sum from file name. The file name format must be XXXXXXXXXXXXXX_YYYYMMDD_XX_00000_000000.00 -.FTI.XML Row count part is 00000 and total sum part is 000000.00")]
        public bool DataFromFilename { get; set; }
        #endregion

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"NbOfTxs\".")]
        public string RowCountFromPainXml { get; set; }

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"CtrlSum\".")]
        public string TotalSumFromPainXml { get; set; }
    }
}
