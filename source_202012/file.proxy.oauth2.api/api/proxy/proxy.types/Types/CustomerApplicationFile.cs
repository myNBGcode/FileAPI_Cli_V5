using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Customer Application File
    /// </summary>
    [DataContract]
    public class CustomerApplicationFile
    {
        /// <summary>
        /// Customer Application File id
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }
        /// <summary>
        /// Customer Application id
        /// </summary>
        [DataMember(Name = "customerApplicationId")]
        public int CustomerApplicationId { get; set; }
        /// <summary>
        /// Filename
        /// </summary>
        [DataMember(Name = "filename")]
        public string Filename { get; set; }
        /// <summary>
        /// Description from customer application
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
        /// <summary>
        /// File Direction (0:incoming, 1:outgoing)
        /// </summary>
        [DataMember(Name = "fileDirection")]
        public int FileDirection { get; set; }
        /// <summary>
        /// XActionInfo eg. action's user id
        /// </summary>
        [DataMember(Name = "xActionInfo")]
        public string XActionInfo { get; set; }
        /// <summary>
        /// Creation date
        /// </summary>
        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// IsHistorical indicator
        /// </summary>
        [DataMember(Name ="isHistorical")]
        public bool IsHistorical { get; set; }
    }
}
