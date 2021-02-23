using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredTransRequest
    {

        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }

        [DataMember(Name = "recordTransId")]
        public string RecordTransId { get; set; }

        [DataMember(Name = "channel")]
        public string Channel { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "fromDate")]
        public DateTime? FromDate { get; set; }

        [DataMember(Name = "toDate")]
        public DateTime? ToDate { get; set; }

        [DataMember(Name = "counter")]
        public int? Counter { get; set; }

    }
}
