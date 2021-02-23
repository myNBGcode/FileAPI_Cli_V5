using System.Net;
using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("sendtoethnofiles", HelpText = "Sends file to Ethnofiles")]
    public class SendToEthnoFilesOptions : IOptions
    {
        [Option("fileid", Required = false, HelpText = "Required. File Guid from fileAPI.")]
        public string FileId { get; set; }

        [Option("inputfile", HelpText = "Filename with full path. (add this if you want to use the --datafromfilename command)")]
        public string InputFile { get; set; }

        [Option("idfield", HelpText = "File Type Id")]
        public string IdField { get; set; }

        [Option("rowcount", HelpText = "The count of the rows in the file")]
        public int? RowCount { get; set; }

        [Option("totalsum", HelpText = "The total sum of the file")]
        public float? TotalSum { get; set; }

        [Option("debtoriban", HelpText = "Debtor IBAN")]
        public string DebtorIban { get; set; }

        [Option("tannumber", HelpText = "Tan Number")]
        public string TanNumber { get; set; }

        [Option("locale", HelpText = "Locale")]
        public string Locale { get; set; }

        [Option("inboxid", HelpText = "Inbox id")]
        public string InboxId { get; set; }

        [Option("xactionid", HelpText = "Xaction Id")]
        public string XactionId { get; set; }

        [Option("issmsotp", HelpText = "Flag that indicates if is SmsOtp")]
        public bool? IsSmsOtp { get; set; }

        [Option("debtorname", HelpText = "Debtor name")]
        public string DebtorName { get; set; }

        [Option("acceptterms", HelpText = " Accept terms flag")]
        public bool? AcceptTerms { get; set; }

        [Option("accepttrnterms", HelpText = "Accept Ttn Terms flag")]
        public bool? AcceptTrnTerms { get; set; }
     
        [Option("datafromfilename", Default = false, HelpText = "False by default. Set it to true to get row count and total sum from file name. The file name format must be XXXXXXXXXXXXXX_YYYYMMDD_XX_00000_000000.00 -.FTI.XML Row count part is 00000 and total sum part is 000000.00")]
        public bool DataFromFileName { get; set; }

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"NbOfTxs\".")]
        public string RowCountFromPainXml { get; set; }

        [Option(HelpText = "If the parameter value is \"001\", the program will try to deserialize the xml input file according to SEPA 001 ISO 20022, and read the row count from the xml header node \"CtrlSum\".")]
        public string TotalSumFromPainXml { get; set; }
    }
}
