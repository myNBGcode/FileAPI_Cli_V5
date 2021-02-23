using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileapiCli.Commands
{
    public class AddUserTagsCmd : CliCommand<AddUserTagsCmdResult>
    {
        public string[] UserTags { get; set; }
        public string FileId { get; set; }

        protected AddUserTagsCmd(IUserInfo userInfo) : base(userInfo)
        {
        }

        internal static AddUserTagsCmd Create(AddUserTagsOptions opts, IUserInfo userInfo)
        {
            //ValidateInput(opts);
            var cmd = new AddUserTagsCmd(userInfo)
            {
                FileId = opts.FileId,
                UserTags = opts.UserTags.Split(",").Select(p=>p.Trim()).ToArray()
            };
            return cmd;
        }

        public static bool ValidateInput(AddUserTagsOptions addUserTagsOptions)
        {
            Guid result;
            if (!Guid.TryParse(addUserTagsOptions.FileId, out result))
            {
                throw new ArgumentException($"{nameof(addUserTagsOptions.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class AddUserTagsCmdResult : IResult
    {
        public string Result { get; set; }
    }
}



