using CommandLine;
using System.Collections.Generic;

namespace FileapiCli.ConfigOptions
{
    [Verb("removeusertags", HelpText = "Remove User Tags From A File")]
    public class RemoveUserTagOptions : IOptions
    {
        [Option('f',"fileid", Required = false, HelpText = "The id of the file to remove tags from. Usually a Guid.")]
        public string FileId { get; set; }

        [Option('t', "usertags", Required = false, HelpText = "User tags to remove separated by a comma")]
        public string UserTags { get; set; }
    }
}
