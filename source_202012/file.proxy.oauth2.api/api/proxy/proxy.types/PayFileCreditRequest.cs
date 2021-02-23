using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class PayFileCreditRequest
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

        //[DataMember(Name = "tanNumber")]
        //public string TanNumber { get; set; }

        //[DataMember(Name = "isSmsOtp")]
        //public bool? IsSmsOtp { get; set; }
    }
}
