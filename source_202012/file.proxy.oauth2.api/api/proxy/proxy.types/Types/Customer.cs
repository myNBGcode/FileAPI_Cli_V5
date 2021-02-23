using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Customer
    /// </summary>
    [DataContract]
    public class Customer
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        [DataMember(Name = "id")]
        public long Id { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        [DataMember(Name = "code")]
        public string Code { get; set; }
        /// <summary>
        /// Username
        /// </summary>
        [DataMember(Name = "username")]
        public string Username { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
