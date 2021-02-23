using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("download", HelpText = "Downloads a File by its GUID, in a specified directory.")]
    public class DownloadOptions : IOptions
    {
        [Option('f', "fileid", Required =false, HelpText = "The id of the file to download. Usually a Guid.")]
        public string FileId { get; set; }

        [Option('d', "downloadfolder", HelpText = "Download location path. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
