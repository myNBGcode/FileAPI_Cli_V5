using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("retrievecustomerapplicationsoutgoing", HelpText = "Retrieves outgoing customer applications.")]
    public class RetrieveCustomerApplicationsOutgoingOptions : IOptions
    {
        [Option('d', "downloadfolder", HelpText = "Download location path for the .json list file. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
