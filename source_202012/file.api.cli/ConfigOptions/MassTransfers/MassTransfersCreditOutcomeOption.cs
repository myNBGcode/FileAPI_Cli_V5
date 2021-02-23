using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("masstransferscreditoutcome", HelpText = "Response File for the Mass Credit Transfers send for payment.")]
    public class MassTransfersCreditOutcomeOption : IOptions
    {
        [Option("fileId", Required = false, HelpText = "The id of the file")]
        public string FileId { get; set; }

        [Option("downloadfolder", Required = false, HelpText = "Download location path. Omit to use the current folder")]
        public string DownloadFolder { get; set; }
    }
}
