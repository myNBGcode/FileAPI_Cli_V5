using CommandLine;
using System.Collections.Generic;

namespace FileapiCli.ConfigOptions
{
    [Verb("upload", HelpText = "Uploads File from a specified path")]
    public class UploadOptions : IOptions
    {
        [Option('i', "inputfile", Required = false, HelpText = "Filename with full path, of the file to be uploaded. (Used in Upload and Process File operations.)")]
        public string InputFile { get; set; }

        [Option('d', "description", HelpText = "Short description of the file")]
        public string Description { get; set; }

        [Option('f', "folderid", HelpText = "Folder Guid")]
        public string FolderId { get; set; }

        [Option('m', "metadata", HelpText = "Folder Guid. (Used in Upload and Process File operations.)")]
        public string MetaData { get; set; }

        [Option('t', "usertags", HelpText = "Tags separated by comma ',' without spaces (Used in Upload and Process File operations.)")]
        public string UserTags { get; set; }
       
    }
}
