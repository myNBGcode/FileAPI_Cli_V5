using AutoMapper;
using FileapiCli.ConfigOptions;
using FileapiCli.Core;
using System;
using System.IO;
using System.Linq;
using FileapiCli.Commands;

namespace FileapiCli.CommandHandlers
{
   
    public class UploadFileCmd : CliCommand<UploadFileCmdResult> 
    {
        public Guid FileId { get; set; }

        public string InputFile { get; set; }

        public string Description { get; set; }

        public string FolderId { get; set; }

        public string MetaData { get; set; }

        public string UserTags { get; set; }

        public int ChuckSize { get; set; }

        public int? TotalChucks { get; set; }

        protected UploadFileCmd(IUserInfo userInfo) : base(userInfo)
        {
        }

        internal static UploadFileCmd Create(UploadOptions opts, string inputFile, Guid fileId, int chunkSize, int? totalChucks, IMapper mapper, IUserInfo userInfo)
        {
            ValidateInput(opts);
            var cmd = new UploadFileCmd(userInfo);
            cmd = mapper.Map<UploadOptions, UploadFileCmd>(opts, cmd); 
                
            cmd.FileId = fileId;
            cmd.InputFile = inputFile;
            cmd.ChuckSize = chunkSize;
            cmd.TotalChucks = totalChucks;
            return cmd;
        }
       
        public static bool ValidateInput(UploadOptions opts)
        {
            if (!File.Exists(opts.InputFile))
            {
                throw new ArgumentException($"{nameof(opts.InputFile)} not found on user's PC");
            }
            return true;
        }
    }
    public class UploadFileCmdResult : IResult
    {
        public bool Success { get; set; }
    }
}