using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class VerifyCreditWithFileRequest
    {
        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        [DataMember(Name = "fileType")]
        public string FileType { get; set; }

        [DataMember(Name = "isPayroll")]
        public bool? IsPayroll { get; set; }

        [DataMember(Name = "userId")]
        public string UserID { get; set; }
    }
}
