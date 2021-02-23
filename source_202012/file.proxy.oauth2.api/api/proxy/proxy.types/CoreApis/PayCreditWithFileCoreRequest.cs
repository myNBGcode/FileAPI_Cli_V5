using System.Runtime.Serialization;

namespace proxy.types
{
    public class PayCreditWithFileCoreRequest
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "fileType")]
        public string FileType { get; set; }

        [DataMember(Name = "totalAmount")]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "totalRecords")]
        public int TotalRecords { get; set; }

        [DataMember(Name = "isPayroll")]
        public bool? IsPayroll { get; set; }

        //[DataMember(Name = "userId")]
        //public string UserID { get; set; }

        //[DataMember(Name = "debitAccount")]
        //public string DebitAccount { get; set; }

        //[DataMember(Name = "startDate")]
        //public DateTime? StartDate { get; set; }

        //[DataMember(Name = "chargesAccount")]
        //public string ChargesAccount { get; set; }

        //[DataMember(Name = "fileName")]
        //public string FileName { get; set; }

        //[DataMember(Name = "fileType")]
        //public string FileType { get; set; }

        //[DataMember(Name = "isPayroll")]
        //public bool? IsPayroll { get; set; }

        //[DataMember(Name = "records")]
        //public PayFileRecordRequest[] Records { get; set; }

        //[DataMember(Name = "excludedRecords")]
        //public PayFileRecordRequest[] ExcludedRecords { get; set; }

        //[DataMember(Name = "tanNumber")]
        //public string TanNumber { get; set; }

        //[DataMember(Name = "channel")]
        //public string Channel { get; set; }

        //[DataMember(Name = "comments")]
        //public string Comments { get; set; }

        //[DataMember(Name = "multipleTransactions")]
        //public bool MultipleTransactions { get; set; }

        //[DataMember(Name = "isSmsOtp")]
        //public bool? IsSmsOtp { get; set; }

        //[DataMember(Name = "acceptTerms")]
        //public bool? AcceptTerms { get; set; }

        //[DataMember(Name = "acceptDuplicate")]
        //public bool? AcceptDuplicate { get; set; }

        //[DataMember(Name = "deferredOnly")]
        //public bool? DeferredOnly { get; set; }
    }
}
