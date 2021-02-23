using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class FileCreditVerifyResponse
    {
        [DataMember(Name = "sumAmClear")]
        public decimal? SumAmClear { get; set; }

        [DataMember(Name = "commission")]
        public decimal? Commission { get; set; }

        [DataMember(Name = "sumAmTotal")]
        public decimal? SumAmTotal { get; set; }

        [DataMember(Name = "totalRecords")]
        public int TotalRecords { get; set; }

        [DataMember(Name = "totalSuccRecords")]
        public int TotalSuccRecords { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "hasErrors")]
        public bool HasErrors { get; set; }

        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "chargesAccount")]
        public string ChargesAccount { get; set; }

        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "fileType")]
        public string FileType { get; set; }

        [DataMember(Name = "commissionInfo")]
        public CommissionInfo CommisionData { get; set; }

        [DataMember(Name = "records")]
        public PayFileRecordRequest[] Records { get; set; }

    }
}
