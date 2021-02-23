using CommandLine;
using System;

namespace FileapiCli.ConfigOptions
{
    [Verb("retrievefilesoutgoing", HelpText = "Retrieves outgoing files for user.")]
    public class RetrieveFilesOutgoingOptions :IOptions
    {
        [Option('f', "datefrom", HelpText = "Starting date (yyyy-mm-dd) of the time period files are retrieved from. Required")]
        public DateTime? DateFrom { get; set; }
        [Option('t', "dateto", HelpText = "End date (yyyy-mm-dd) of the time period files are retrieved from. Will fetch up to latest if ommited", Default = null)]
        public DateTime? DateTo { get; set; }
        [Option('d', "downloadfolder", HelpText = "Download location path for the .json list file. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
