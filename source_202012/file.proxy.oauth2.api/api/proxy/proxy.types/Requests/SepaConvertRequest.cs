using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Sepa convert request.
    /// </summary>
    [DataContract]
    public class SepaConvertRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        /// <summary>
        /// The unconverted file id in the Files db.
        /// </summary>
        [DataMember(Name = "unconvertedFileId")]
        public Guid UnconvertedFileId { get; set; }
        /// <summary>
        /// The customer application id.
        /// </summary>
        [DataMember(Name = "customerApplicationId")]
        public int CustomerApplicationId { get; set; }
        /// <summary>
        /// The conversion id.
        /// </summary>
        [DataMember(Name = "conversionId")]
        public string ConversionId { get; set; }
        /// <summary>
        /// Is xml indicator.
        /// </summary>
        [DataMember(Name = "isXml")]
        public bool IsXml { get; set; }
        /// <summary>
        /// Debtor name.
        /// </summary>
        [DataMember(Name = "debtorName")]
        public string DebtorName { get; set; }
        /// <summary>
        /// Debtor IBAN.
        /// </summary>
        [DataMember(Name = "debtorIBAN")]
        public string DebtorIBAN { get; set; }
    }
}
