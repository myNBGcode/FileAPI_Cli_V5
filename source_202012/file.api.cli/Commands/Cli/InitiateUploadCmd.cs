using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileapiCli.Commands
{
    public class InitiateUploadCmd : CliCommand<InitiateUploadCmdResult>, IResult
    {
        public string InputFile { get; set; }

        public Guid? FolderId { get; set; }

        public string FileName { get; set; }

        public string FileDescription { get; set; }

        public string[] UserTags { get; set; }
        
        public long FileSize { get; set; }
        
        public string Metadata { get; set; }

        protected InitiateUploadCmd(IUserInfo userInfo) : base(userInfo)
        {
        }

        internal static InitiateUploadCmd CreateCommand(UploadOptions opts, IUserInfo userInfo)
        {
            ValidateInput(opts);
            var fileInfo = new FileInfo(opts.InputFile);
            var cmd =new InitiateUploadCmd(userInfo)
            {
                InputFile = opts.InputFile,
                FileName = System.IO.Path.GetFileName(opts.InputFile),
                FileSize = (long)Math.Ceiling((decimal)fileInfo.Length / 1024 / 1024),
                FileDescription = opts.Description,
                FolderId = Guid.TryParse(opts.FolderId, out Guid folderId) ? (Guid?)folderId : null,
                Metadata = opts.MetaData,
                UserTags = opts.UserTags.Split(",").Select(p=>p.Trim()).ToArray()
            };
            return cmd;
        }
        public static bool ValidateInput(UploadOptions opts)
        {
            if (!File.Exists(opts.InputFile))
            {
                throw new ArgumentException($"{nameof(opts.InputFile)}  not found on users PC.");
            }
            if (!string.IsNullOrEmpty(opts.FolderId))
            {
                if (!Guid.TryParse(opts.FolderId, out Guid result))
                {
                    throw new ArgumentException($"{nameof(opts.FolderId)} is not a Guid.");
                }
            }
            return true;
        }
    }

    public class InitiateUploadCmdResult: IResult
    {
        public string FileId { get; set; }

        public int ChuckSize { get; set; }

        public int? TotalChucks { get; set; }
    }



}
