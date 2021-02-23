using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredActiveAllResponse
    {
        [DataMember(Name = "trans")]
        public DeferredTransactionInfo[] Trans { get; set; }
    }
}
