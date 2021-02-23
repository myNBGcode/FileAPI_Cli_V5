using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Sepa Set File Status As Sent Request.
    /// </summary>
    [DataContract]
    public class SepaSetFileStatusAsSentRequest
    {
        /// <summary>
        /// User id.
        /// </summary>
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        /// <summary>
        /// The file id in the Sepa db.
        /// </summary>
        [DataMember(Name = "fileId")]
        public string FileId { get; set; }
        /// <summary>
        /// Conversion id.
        /// </summary>
        [DataMember(Name = "convId")]
        public string ConvId { get; set; }
    }
}
