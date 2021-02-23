using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("samplemasstransfers", HelpText = "Download a sample massive transfers file")]
    public class SampleMassTransfersOption : IOptions
    {
        [Option("filetype", Required = false, HelpText = "The type of the sample file to receive. Supported Types xml, csv")]
        public string FileType { get; set; }

        [Option("DownloadFolder", Required = false, HelpText = "Required. Download location path")]
        public string DownloadFolder { get; set; }
    }
}
