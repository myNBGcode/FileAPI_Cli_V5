using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class ResultPayCreditWithFileResponse
    {
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

    }
}
