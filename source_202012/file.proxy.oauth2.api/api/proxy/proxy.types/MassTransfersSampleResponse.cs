using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class MassTransfersSampleResponse
    {
        [DataMember(Name = "content")]
        public byte[] Content { get; set; }
    }
}
