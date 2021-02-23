using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve Customer Applications Request
    /// </summary>
    [DataContract]
    public class RetrieveCustomerApplicationsRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        /// <summary>
        /// File Direction (0:incoming, 1:outgoing)
        /// </summary>
        [DataMember(Name = "fileDirection")]
        public int FileDirection { get; set; }
    }
}
