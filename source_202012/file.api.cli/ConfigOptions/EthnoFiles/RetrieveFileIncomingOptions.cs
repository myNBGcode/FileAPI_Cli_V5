using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("retrievefileincoming", HelpText = "Retrieves incoming file for user.")]
    public class RetrieveFileIncomingOptions : IOptions
    {
        [Option('i', "customerapplicationfileid", HelpText = "The customer application file id. Required.")]
        public int? CustomerApplicationFileId { get; set; }
        [Option('h', "isHistorical", HelpText = " Is historical file flag. If not defined returns both historical and non histrorical.")]
        public bool? IsHistorical { get; set; }
        [Option('d', "downloadfolder", HelpText = "Download location path. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
