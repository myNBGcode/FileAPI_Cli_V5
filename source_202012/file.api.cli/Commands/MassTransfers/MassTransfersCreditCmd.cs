using FileapiCli.ConfigOptions;
using FileapiCli.Core;

namespace FileapiCli.Commands.MassTransfers
{
    public class MassTransfersCreditCmd : CliCommand<MassTransfersCreditResult>
    {
        public string InputFile { get; set; }
        public string Description { get; set; }
        public string FolderId { get; set; }
        public string MetaData { get; set; }
        public string UserTags { get; set; }
        public string DebitAccount { get; set; }
        public bool? IsPayroll { get; set; }
       // public string TanNumber { get; set; }
        public string FileId { get; set; }

        protected MassTransfersCreditCmd(IUserInfo userInfo)
            : base(userInfo) { }

        internal static MassTransfersCreditCmd Create(MassTransfersCreditOption opts, IUserInfo userInfo)
        {
            var cmd = new MassTransfersCreditCmd(userInfo)
            {
                InputFile = opts.InputFile,
                Description = opts.Description,
                FolderId = opts.FolderId,
                MetaData = opts.MetaData,
                UserTags = opts.UserTags,
                DebitAccount = opts.DebitAccount,
                IsPayroll = opts.IsPayroll,
                //TanNumber = opts.TanNumber,
                FileId = opts.FileId
            };
            
            return cmd;
        }
    }

    public class MassTransfersCreditResult : IResult
    {
    }
}
