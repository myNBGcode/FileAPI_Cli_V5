using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve file request
    /// </summary>
    [DataContract]
    public class RetrieveFileRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name ="userId")]
        public string UserId { get; set; }
        /// <summary>
        /// Customer application file id
        /// </summary>
        [DataMember(Name = "customerApplicationFileId")]
        public int CustomerApplicationFileId { get; set; }
        /// <summary>
        /// File Direction (0:incoming, 1:outgoing)
        /// </summary>
        [DataMember(Name = "fileDirection")]
        public int FileDirection { get; set; }
        /// <summary>
        /// IsHistorical indicator
        /// </summary>
        [DataMember(Name = "isHistorical")]
        public bool IsHistorical { get; set; }
    }
}
