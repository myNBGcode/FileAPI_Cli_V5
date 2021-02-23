using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredTransactionInfo
    {
        [DataMember(Name = "trnNo")]
        public string TrnNo { get; set; }

        [DataMember(Name = "trnId")]
        public string TrnId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "createdDate")]
        public DateTime? CreatedDate { get; set; }

        [DataMember(Name = "executionDate")]
        public DateTime? ExecutionDate { get; set; }

        [DataMember(Name = "cancellationDate")]
        public DateTime? CancelationDate { get; set; }

        [DataMember(Name = "frequency")]
        public int? Frequency { get; set; }

        [DataMember(Name = "totalPayments")]
        public int? TotalPayments { get; set; }

        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "rowsNum")]
        public int RowsNum { get; set; }

        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        [DataMember(Name = "totalAmount")]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "creditAccount")]
        public string CreditAccount { get; set; }

        [DataMember(Name = "details")]
        public DeferredDetails[] Details { get; set; }

        [DataMember(Name = "historical")]
        public DeferredHistoryExecution[] Historical { get; set; }
    }
}
