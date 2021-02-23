using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class ResultPayCreditWithFileRequest
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        [DataMember(Name = "transactionFileId")]
        public string TransactionFileId { get; set; }
    }
}
