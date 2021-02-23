using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class MassTransfersSampleRequest
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "fileFormat")]
        public string FileFormat { get; set; }
    }
}
