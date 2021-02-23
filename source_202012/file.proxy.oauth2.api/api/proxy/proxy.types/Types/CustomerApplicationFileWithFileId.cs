using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Customer Application File with file id
    /// </summary>
    [DataContract]
    public class CustomerApplicationFileWithFileId : CustomerApplicationFile
    {
        /// <summary>
        /// File id from Files db
        /// </summary>
        [DataMember(Name = "fileId")]
        public Guid FileId { get; set; }
    }
}
