using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class DeferredFileRecordResponse
    {
        [DataMember(Name = "serialNum")]
        public string SerialNum { get; set; }

        /// <summary>
        /// Λογαριασμός χρέωσης
        /// </summary>
        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        /// <summary>
        /// λογαριασμός χρέωσης εξόδων
        /// </summary>
        [DataMember(Name = "debitAccountCharges")]
        public string DebitAccountCharges { get; set; }

        /// <summary>
        /// Σύστημα εκκαθάρισης CSM
        /// </summary>
        [DataMember(Name = "sysType")]
        public string SysType { get; set; }

        /// <summary>
        /// CSM Code
        /// </summary>
        [DataMember(Name = "sysCode")]
        public string SysCode { get; set; }

        /// <summary>
        /// CSM Bank
        /// </summary>
        [DataMember(Name = "sysBic")]
        public string SysBic { get; set; }

        /// <summary>
        /// Χώρα συστήματος εκκαθάρισης
        /// </summary>
        [DataMember(Name = "sysCountry")]
        public string SysCountry { get; set; }

        /// <summary>
        /// τράπεζα Πίστωσης
        /// </summary>
        [DataMember(Name = "bicCode")]
        public string BicCode { get; set; }

        /// <summary>
        /// Όνομα Τράπεζας
        /// </summary>
        [DataMember(Name = "bankTitle")]
        public string BankTitle { get; set; }

        /// <summary>
        /// Τύπος Λογαριασμού: ΤΕΧΤ, ΙΒΑΝ
        /// </summary>
        [DataMember(Name = "accountType")]
        public string AccountType { get; set; }

        /// <summary>
        /// Iban ή λογαριασμός πίστωσης
        /// </summary>
        [DataMember(Name = "iban")]
        public string IBAN { get; set; }

        /// <summary>
        /// Δικαιούχος
        /// </summary>
        [DataMember(Name = "beneficiary")]
        public string Beneficiary { get; set; }

        /// <summary>
        /// 2ος Δικαιούχος
        /// </summary>
        [DataMember(Name = "beneficiary2")]
        public string Beneficiary2 { get; set; }

        /// <summary>
        /// 3ος δικαιούχος
        /// </summary>
        [DataMember(Name = "beneficiary3")]
        public string Beneficiary3 { get; set; }

        /// <summary>
        /// Διεύθυνση Δικαιούχου
        /// </summary>
        [DataMember(Name = "beneficiaryAddress")]
        public string BeneficiaryAddress { get; set; }

        /// <summary>
        /// ποσό εντολής (input)
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// νόμισμα
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// BEN, OUR, SHA
        /// </summary>
        [DataMember(Name = "charges")]
        public string Charges { get; set; }

        /// <summary>
        /// ένδειξη αν είναι επείγουσα
        /// </summary>
        [DataMember(Name = "emergency")]
        public bool Emergency { get; set; }

        /// <summary>
        /// αιτιολογία
        /// </summary>
        [DataMember(Name = "reason")]
        public string Reason { get; set; }

        /// <summary>
        /// Τύπος εμβάσματος: 1 για import ή 2 για other_payment
        /// </summary>
        [DataMember(Name = "remCategory")]
        public string RemCategory { get; set; }

        /// <summary>
        /// Κωδικός σκοπού εμβάσματος (3ψήφιος)
        /// </summary>
        [DataMember(Name = "remType")]
        public string RemType { get; set; }

        /// <summary>
        /// Κωδικός είδους εμπορέυματος (4ψήφιος)
        /// </summary>
        [DataMember(Name = "importedGood")]
        public string ImportedGood { get; set; }

        /// <summary>
        /// Ανάλυση προμηθειών
        /// </summary>
        [DataMember(Name = "commissionInfo")]
        public CommissionInfo CommisionData { get; set; }

        /// <summary>
        /// Σύνολο εξόδων (output)
        /// </summary>
        [DataMember(Name = "sumCommission")]
        public decimal? SumCommission { get; set; }

        /// <summary>
        /// Συνολική χρέωση (output)
        /// </summary>
        [DataMember(Name = "debitAmount")]
        public decimal? DebitAmount { get; set; }

        /// <summary>
        /// Καθαρό ποσό χρέωσης (output)
        /// </summary>
        [DataMember(Name = "netAmount")]
        public decimal? NetAmount { get; set; }

        /// <summary>
        /// Κωδικός εμβάσματος
        /// </summary>
        [DataMember(Name = "orderNo")]
        public string OrderNo { get; set; }

        /// <summary>
        /// '000' για επιτυχημένη συναλλαγή
        /// </summary>
        [DataMember(Name = "msgCode")]
        public string MsgCode { get; set; }

        /// <summary>
        /// Μήνυμα λάθους
        /// </summary>
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
