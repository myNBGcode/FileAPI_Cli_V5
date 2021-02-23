using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileapiCli.Commands
{
    public class RemoveUserTagsCmd : CliCommand<RemoveUserTagsCmdResult>
    {
        public string FileId { get; set; }

        public string RemoveUserTagsFileId { get; set; }

        public string[] UserTags { get; set; }

        protected RemoveUserTagsCmd(IUserInfo userInfo) : base(userInfo)
        {
        }
        internal static RemoveUserTagsCmd Create(RemoveUserTagOptions opts, IUserInfo userInfo)
        {
            //ValidateInput(opts);
            var cmd = new RemoveUserTagsCmd(userInfo)
            {
                FileId = opts.FileId,
                RemoveUserTagsFileId = opts.FileId,
                UserTags = opts.UserTags.Split(",").Select(p => p.Trim()).ToArray()
            };
            return cmd;
        }
        public static bool ValidateInput(RemoveUserTagOptions opts)
        {
            Guid result;
            if (!Guid.TryParse(opts.FileId, out result))
            {
                throw new ArgumentException($"{nameof(opts.FileId)} is not a valid Guid.");
            }
            return true;
        }
    }

    public class RemoveUserTagsCmdResult : IResult
    {

    }
}