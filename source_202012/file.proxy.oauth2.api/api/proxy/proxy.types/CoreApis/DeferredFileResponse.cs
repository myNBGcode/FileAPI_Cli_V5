using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredFileResponse
    {
        /// <summary>
        /// το id της συναλλαγής που ολοκλήρωσε την πληρωμή 
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        [DataMember(Name = "chargesAccount")]
        public string ChargesAccount { get; set; }

        [DataMember(Name = "totalFileAmount")]
        public decimal? TotalFileAmount { get; set; }

        [DataMember(Name = "totalAmount")]
        public decimal? TotalAmount { get; set; }

        [DataMember(Name = "totalCommission")]
        public decimal? TotalCommission { get; set; }

        [DataMember(Name = "totalRecords")]
        public int TotalRecords { get; set; }

        [DataMember(Name = "totalSuccRecords")]
        public int TotalSuccRecords { get; set; }

        /// <summary>If <code>true</code> this transaction is deferred.</summary>
        [DataMember(Name = "isDeferred")]
        public bool? IsDeferred { get; set; }

        [DataMember(Name = "tanCheck")]
        public string TanCheck { get; set; }

        [DataMember(Name = "transactionDate")]
        public DateTime TransactionDate { get; set; }

        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "fileType")]
        public string FileType { get; set; }

        [DataMember(Name = "records")]
        public DeferredFileRecordResponse[] Records { get; set; }

    }
}
