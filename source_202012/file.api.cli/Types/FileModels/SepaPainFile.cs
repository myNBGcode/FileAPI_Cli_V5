namespace FileapiCli
{
    public class SepaPainFile
    {
        public Document Document { get; set; }
    }

    public class Document
    {
        public CstmrCdtTrfInitn CstmrCdtTrfInitn { get; set; }
    }

    public class CstmrCdtTrfInitn
    {
        public GrpHdr GrpHdr { get; set; }
    }

    public class GrpHdr
    {
        public int NbOfTxs { get; set; }
        public decimal CtrlSum { get; set; }
    }
}
