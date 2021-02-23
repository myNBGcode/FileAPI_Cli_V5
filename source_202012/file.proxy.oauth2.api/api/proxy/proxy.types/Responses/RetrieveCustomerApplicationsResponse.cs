using System.Collections.Generic;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve Customer Applications Response
    /// </summary>
    [DataContract]
    public class RetrieveCustomerApplicationsResponse
    {
        /// <summary>
        /// Customer applications
        /// </summary>
        [DataMember(Name ="customerApplications")]
        public IEnumerable<CustomerApplication> CustomerApplications { get; set; }
    }
}
