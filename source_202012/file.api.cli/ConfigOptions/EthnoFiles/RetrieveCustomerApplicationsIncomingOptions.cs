using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("retrievecustomerapplicationsincoming", HelpText = "Retrieves incoming customer applications.")]
    public class RetrieveCustomerApplicationsIncomingOptions : IOptions
    {
        [Option('d', "downloadfolder", HelpText = "Download location path for the .json list file. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
