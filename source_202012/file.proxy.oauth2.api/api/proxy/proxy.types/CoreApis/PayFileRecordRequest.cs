using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class PayFileRecordRequest
    {
        [DataMember(Name = "serialNum")]
        public string SerialNum { get; set; }

        /// <summary>
        /// Λογαριασμός χρέωσης
        /// </summary>
        [DataMember(Name = "debitAccount")]
        public string DebitAccount { get; set; }

        [DataMember(Name = "debitAccountValid")]
        public bool DebitAccountValid { get; set; }

        /// <summary>
        /// Λογαριασμός χρέωσης προμηθειών
        /// </summary>
        [DataMember(Name = "debitAccountCharges")]
        public string DebitAccountCharges { get; set; }

        [DataMember(Name = "debitAccountChargesValid")]
        public bool DebitAccountChargesValid { get; set; }

        /// <summary>
        /// Σύστημα εκκαθάρισης CSM
        /// </summary>
        [DataMember(Name = "sysType")]
        public string SysType { get; set; }

        [DataMember(Name = "sysTypeValid")]
        public bool SysTypeValid { get; set; }

        /// <summary>
        /// CSM Code
        /// </summary>
        [DataMember(Name = "sysCode")]
        public string SysCode { get; set; }

        [DataMember(Name = "sysCodeValid")]
        public bool SysCodeValid { get; set; }

        /// <summary>
        /// CSM Bank
        /// </summary>
        [DataMember(Name = "sysBic")]
        public string SysBic { get; set; }

        [DataMember(Name = "sysBicValid")]
        public bool SysBicValid { get; set; }

        /// <summary>
        /// Χώρα συστήματος εκκαθάρισης
        /// </summary>
        [DataMember(Name = "sysCountry")]
        public string SysCountry { get; set; }

        [DataMember(Name = "sysCountryValid")]
        public bool SysCountryValid { get; set; }

        /// <summary>
        /// Τράπεζα Πίστωσης
        /// </summary>
        [DataMember(Name = "bicCode")]
        public string BicCode { get; set; }


        [DataMember(Name = "bicCodeValid")]
        public bool BicCodeValid { get; set; }

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

        [DataMember(Name = "accountTypeValid")]
        public bool AccountTypeValid { get; set; }


        [DataMember(Name = "differentAccountValid")]
        public bool DifferentAccountValid { get; set; }

        /// <summary>
        /// Iban ή λογαριασμός πίστωσης
        /// </summary>
        [DataMember(Name = "iban")]
        public string IBAN { get; set; }

        [DataMember(Name = "ibanValid")]
        public bool IBANValid { get; set; }

        /// <summary>
        /// Δικαιούχος
        /// </summary>
        [DataMember(Name = "beneficiary")]
        public string Beneficiary { get; set; }

        [DataMember(Name = "beneficiaryValid")]
        public bool BeneficiaryValid { get; set; }


        /// <summary>
        /// Διεύθυνση Δικαιούχου
        /// </summary>
        [DataMember(Name = "beneficiaryAddress")]
        public string BeneficiaryAddress { get; set; }

        [DataMember(Name = "beneficiaryAddressValid")]
        public bool BeneficiaryAddressValid { get; set; }

        /// <summary>
        /// Ποσό 
        /// </summary>
        [DataMember(Name = "amount")]
        public decimal Amount { get; set; }

        [DataMember(Name = "amountValid")]
        public bool AmountValid { get; set; }

        /// <summary>
        /// Νόμισμα
        /// </summary>
        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "currencyValid")]
        public bool CurrencyValid { get; set; }

        /// <summary>
        /// Έξοδα (BEN, OUR, SHA)
        /// </summary>
        [DataMember(Name = "charges")]
        public string Charges { get; set; }

        [DataMember(Name = "chargesValid")]
        public bool ChargesValid { get; set; }

        /// <summary>
        /// Ένδειξη αν είναι επείγουσα
        /// </summary>
        [DataMember(Name = "emergency")]
        public bool? Emergency { get; set; }

        /// <summary>
        /// Αιτιολογία
        /// </summary>
        [DataMember(Name = "reason")]
        public string Reason { get; set; }

        [DataMember(Name = "reasonValid")]
        public bool ReasonValid { get; set; }

        /// <summary>
        /// Τύπος εμβάσματος: 1 για import ή 2 για other_payment
        /// </summary>
        [DataMember(Name = "remCategory")]
        public string RemCategory { get; set; }

        [DataMember(Name = "remCategoryValid")]
        public bool RemCategoryValid { get; set; }

        /// <summary>
        /// Κωδικός σκοπού εμβάσματος (3ψήφιος)
        /// </summary>
        [DataMember(Name = "remType")]
        public string RemType { get; set; }

        [DataMember(Name = "remTypeValid")]
        public bool RemTypeValid { get; set; }

        /// <summary>
        /// Κωδικός είδους εμπορέυματος (4ψήφιος)
        /// </summary>
        [DataMember(Name = "importedGood")]
        public string ImportedGood { get; set; }

        [DataMember(Name = "importedGoodValid")]
        public bool ImportedGoodValid { get; set; }

        /// <summary>
        /// Ανάλυση προμηθειών
        /// </summary>
        [DataMember(Name = "commissionInfo")]
        public CommissionInfo CommisionData { get; set; }

        /// <summary>
        /// Σύνολο προμηθειών
        /// </summary>
        [DataMember(Name = "totalCommission")]
        public decimal TotalCommission { get; set; }

        [DataMember(Name = "error")]
        public string Error { get; set; }

        [DataMember(Name = "creditorBeneficiaries")]
        public string[] CreditorBeneficiaries { get; set; }

    }
}
