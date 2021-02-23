using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Send file to ethnofiles response
    /// </summary>
    [DataContract]
    public class SendFileResponse
    {
        /// <summary>
        /// Inserted customer application file id
        /// </summary>
        [DataMember(Name = "customerApplicationFileId")]
        public int CustomerApplicationFileId { get; set; }
    }
}
