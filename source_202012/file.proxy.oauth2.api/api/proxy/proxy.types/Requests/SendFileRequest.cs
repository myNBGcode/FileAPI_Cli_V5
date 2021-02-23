using System;
using System.Runtime.Serialization;

namespace proxy.types
{
    /// <summary>
    /// Send file to ethnofiles request
    /// </summary>
    [DataContract]
    public class SendFileRequest
    {
        /// <summary>
        /// User id
        /// </summary>
        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        /// <summary>
        /// Customer Application Id
        /// </summary>
        [DataMember(Name = "customerApplicationId")]
        public int CustomerApplicationId { get; set; }
        /// <summary>
        /// File id from the File Management Platform
        /// </summary>
        [DataMember(Name = "fileId")]
        public Guid FileId { get; set; }
        /// <summary>
        /// Filename
        /// </summary>
        [DataMember(Name = "filename")]
        public string Filename { get; set; }
        /// <summary>
        /// Total records of the file. Required when the 
        /// customer application's validation type is countAndSum, or countOnly.
        /// </summary>
        [DataMember(Name = "totalrecords")]
        public int? TotalRecords { get; set; }
        /// <summary>
        /// Total amount of the file. Required when the 
        /// customer application's validation type is countAndSum.
        /// </summary>
        [DataMember(Name = "totalamount")]
        public decimal? TotalAmount { get; set; }
        /// <summary>
        /// Requested execution date.
        /// </summary>
        [DataMember(Name = "executionDate")]
        public DateTime? ExecutionDate { get; set; }
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
        /// <summary>
        /// Tan number.
        /// </summary>
        [DataMember(Name = "tanNumber")]
        public string TanNumber { get; set; }
        /// <summary>
        /// Is sms otp indicator.
        /// </summary>
        [DataMember(Name = "isSmsOtp")]
        public bool? IsSmsOtp { get; set; }
        /// <summary>
        /// Accept temrs indicator.
        /// </summary>
        [DataMember(Name = "acceptTerms")]
        public bool AcceptTerms { get; set; }
        /// <summary>
        /// Accept trn terms indicator.
        /// </summary>
        [DataMember(Name = "acceptTrnTerms")]
        public bool AcceptTrnTerms { get; set; }
    }
}
