using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Sepa convert response.
    /// </summary>
    [DataContract]
    public class SepaConvertResponse
    {
        /// <summary>
        /// The file id from the Files db.
        /// </summary>
        [DataMember(Name = "fileId")]
        public Guid FileId { get; set; }
        /// <summary>
        /// The sepa file id in the Sepa db.
        /// </summary>
        [DataMember(Name = "sepaFileId")]
        public string SepaFileId { get; set; }
        /// <summary>
        /// The sepa file status.
        /// </summary>
        [DataMember(Name = "sepaFileStatus")]
        public string SepaFileStatus { get; set; }
        /// <summary>
        /// Number of xml transactions.
        /// </summary>
        [DataMember(Name = "xmlTransactions")]
        public int? XmlTransactions { get; set; }
        /// <summary>
        /// The xml total amount.
        /// </summary>
        [DataMember(Name = "xmlTotalAmount")]
        public decimal? XmlTotalAmount { get; set; }
        /// <summary>
        /// The sepa file name.
        /// </summary>
        [DataMember(Name = "sepaFilename")]
        public string SepaFilename { get; set; }
    }
}
