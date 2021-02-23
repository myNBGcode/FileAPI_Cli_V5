using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve file response. Doesn't return the actual file content 
    /// but the related file id from the file management platform.
    /// </summary>
    [DataContract]
    public class RetrieveFileResponse
    {
        /// <summary>
        /// File id from the file management platform
        /// </summary>
        [DataMember(Name ="fileId")]
        public Guid FileId { get; set; }
    }
}
