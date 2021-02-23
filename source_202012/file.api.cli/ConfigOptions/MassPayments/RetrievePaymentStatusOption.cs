using CommandLine;
using System.Collections.Generic;

namespace FileapiCli.ConfigOptions
{
    [Verb("retrievestatus", HelpText = "Retrieve Payment Status for individual payments.")]
    public class RetrievePaymentStatusOption : IOptions
    {
        [Option(Required = false, HelpText = "The id of the file")]
        public string fileid { get; set; }
     
        [Option(Required = false, HelpText = "Download location path. Omit to use the current folder")]
        public string DownloadFolder { get; set; }
    }
}
