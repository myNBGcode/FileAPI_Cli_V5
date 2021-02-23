using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class CustomerApplication
    {
        /// <summary>
        /// Customer application id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
        /// <summary>
        /// Filename pattern
        /// </summary>
        [DataMember(Name = "filenamePattern")]
        public string FilenamePattern { get; set; }
        /// <summary>
        /// Validation Type (countAndSum, countOnly, none)
        /// </summary>
        [DataMember(Name = "validationType")]
        public string ValidationType { get; set; }
        /// <summary>
        /// Conversion id
        /// </summary>
        [DataMember(Name = "conversionId")]
        public string ConversionId { get; set; }
        /// <summary>
        /// IsXml indicator
        /// </summary>
        [DataMember(Name = "isXml")]
        public bool IsXml { get; set; }
        /// <summary>
        /// IsRecallXml indicator
        /// </summary>
        [DataMember(Name = "isRecallXml")]
        public bool IsRecallXml { get; set; } 
    }
}
