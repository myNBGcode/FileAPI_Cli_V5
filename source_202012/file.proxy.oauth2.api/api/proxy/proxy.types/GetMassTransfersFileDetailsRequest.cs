using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class GetMassTransfersFileDetailsRequest
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "transId")]
        public string TransId { get; set; }
    }
}
