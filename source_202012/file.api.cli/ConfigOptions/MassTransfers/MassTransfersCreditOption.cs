using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("masstransferscredit", HelpText = "Execute mass credit transfers.")]
    public class MassTransfersCreditOption : IOptions
    {
        #region uploadOptions

        [Option('i', "inputfile", Required = false, HelpText = "Filename with full path, of the file to be uploaded. (Used in Upload and Process File operations.)")]
        public string InputFile { get; set; }

        [Option('d', "description", HelpText = "Short description of the file")]
        public string Description { get; set; }

        [Option('f', "folderid", HelpText = "Folder Guid")]
        public string FolderId { get; set; }

        [Option('m', "metadata", HelpText = "File metadata")]
        public string MetaData { get; set; }

        [Option('t', "usertags", HelpText = "Tags separated by comma ',' without spaces (Used in Upload and Process File operations.)")]
        public string UserTags { get; set; }

        #endregion

        [Option(HelpText = nameof(DebitAccount))]
        public string DebitAccount { get; set; }

        [Option(HelpText = nameof(IsPayroll))]
        public bool? IsPayroll { get; set; }

        public string TanNumber { get; set; }

        public string FileId { get; set; }
    }
}
