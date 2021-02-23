using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("samplemasspayment", HelpText = "Download a sample mass payment file")]
    public class SampleMassPaymentOption : IOptions
    {
        [Option("filetype", Required = false, HelpText = "The type of the sample file to receive. Supported Types json, xml, csv")]
        public string FileType { get; set; }

        [Option("DownloadFolder", Required = false, HelpText = "Required. Download location path")]
        public string DownloadFolder { get; set; }

    }
}
