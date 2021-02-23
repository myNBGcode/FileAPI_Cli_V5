using CommandLine;

namespace FileapiCli.ConfigOptions
{
    [Verb("masspayment", HelpText = "Execute Mass Payment")]
    public class MassPaymentOption : IOptions
    {
       
       // supplied by InitiateUpload 
        public string FileId { get; set; }
        public string Filename { get; set; }

        #region uploadOptions

        [Option('i', "inputfile", Required = false, HelpText = "Filename with full path, of the file to be uploaded.")]
        public string InputFile { get; set; }

        [Option('d', "description", HelpText = "Short description of the file")]
        public string Description { get; set; }

        [Option('f', "folderid", HelpText = "Folder Guid")]
        public string FolderId { get; set; }

        [Option('m', "metadata", HelpText = "Folder Guid. (Used in Upload and Process File operations.)")]
        public string MetaData { get; set; }

        [Option('t', "usertags", HelpText = "Tags separated by comma ',' without spaces (Used in Upload and Process File operations.)")]
        public string UserTags { get; set; }

        #endregion

        [Option(HelpText = nameof(DebtorName))]
        public string DebtorName { get; set; }
        
        [Option(HelpText = nameof(DebtorTelephone))]
        public string DebtorTelephone { get; set; }

        [Option(HelpText = nameof(DebtorIBan))]
        public string DebtorIBan { get; set; }

        [Option(HelpText = nameof(Ccy))]
        public string Ccy { get; set; }
        
        [Option(HelpText = nameof(AcceptDuplicate))]
        public bool AcceptDuplicate { get; set; }

        [Option('p', "downloadfolder", HelpText = "Download location path for the .json response. Omit to use the current path.", Required = false)]
        public string DownloadFolder { get; set; }
    }
}
