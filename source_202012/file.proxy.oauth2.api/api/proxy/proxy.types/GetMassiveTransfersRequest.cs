using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class GetMassiveTransfersRequest
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "fromDate")]
        public DateTime? FromDate { get; set; }

        [DataMember(Name = "toDate")]
        public DateTime? ToDate { get; set; }

    }
}
