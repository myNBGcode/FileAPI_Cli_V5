using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Customer Application with user that has access to it
    /// </summary>
    public class CustomerApplicationWithUsername : CustomerApplication
    {
        /// <summary>
        /// User that has access to it. If is empty 
        /// then every users of the company has access to it.
        /// </summary>
        [DataMember(Name = "restrictionUsername")]
        public string RestrictionUsername { get; set; }
    }
}
