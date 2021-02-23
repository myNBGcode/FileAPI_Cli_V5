using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Retrieve File List request
    /// </summary>
    [DataContract]
    public class RetrieveFileListRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name ="userId")]
        public string UserId { get; set; }
        /// <summary>
        /// File Direction (0:incoming, 1:outgoing)
        /// </summary>
        [DataMember(Name = "fileDirection")]
        public int FileDirection { get; set; }
        /// <summary>
        /// IsHistorical indicator
        /// </summary>
        [DataMember(Name = "isHistorical")]
        public bool IsHistorical { get; set; }
        /// <summary>
        /// Creation date from. Required.
        /// </summary>
        [DataMember(Name = "dateFrom")]
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Creation date to
        /// </summary>
        [DataMember(Name = "dateTo")]
        public DateTime? DateTo { get; set; }
    }
}
