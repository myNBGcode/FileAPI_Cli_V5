using System.Collections.Generic;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve File List Response
    /// </summary>
    [DataContract]
    public class RetrieveFileListResponse
    {
        /// <summary>
        /// Customer Application Files
        /// </summary>
        [DataMember(Name = "customerApplicationFiles")]
        public IEnumerable<CustomerApplicationFile> CustomerApplicationFiles { get; set; }
    }
}
