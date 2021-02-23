using CommandLine;
using System.Collections.Generic;

namespace FileapiCli.ConfigOptions
{
    [Verb("addusertags", HelpText = "Add User Tags To A File")]
    public class AddUserTagsOptions : IOptions
    {
        [Option('f',"fileid", HelpText = "The id of the file to add Tags to. Usually a Guid.", Required =false)]
        public string FileId { get; set; }

        [Option('t', "usertags", HelpText = "User Tags to add separated by a comma", Required =false)]
        public string UserTags { get; set; }
    }
}
