using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredHistoryMaster
    {
        [DataMember(Name = "originalTotalPayments")]
        public string OriginalTotalPayments { get; set; }

        [DataMember(Name = "transactionName")]
        public string TransactionName { get; set; }

        [DataMember(Name = "deletionUserId")]
        public string DeletionUserId { get; set; }

        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        [DataMember(Name = "originalExecutionDate")]
        public DateTime? OriginalExecutionDate { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "deletionDate")]
        public DateTime? DeletionDate { get; set; }

        [DataMember(Name = "totalPayments")]
        public string totalPayments { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "executionDate")]
        public DateTime? ExecutionDate { get; set; }

    }
}