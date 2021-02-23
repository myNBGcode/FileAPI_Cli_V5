using System.Runtime.Serialization;

namespace proxy.types
{
    [DataContract]
    public class CommissionInfo
    {


        /// <summary>
        /// Commission Of the Bank sending remittance
        /// </summary>
        [DataMember(Name = "otherBankCommission")]
        public decimal OtherBankCommission { get; set; }

        /// <summary>
        /// Commission Of the Agen pe DIAS
        /// </summary>
        [DataMember(Name = "agentCommission")]
        public decimal AgentCommission { get; set; }


        /// <summary>Προμήθεια ΕΤΕ</summary>
        /// <summary>
        /// Nbg Commission
        /// </summary>
        [DataMember(Name = "nbgCommission")]
        public decimal NbgCommission { get; set; }

        /// <summary>Έξοδα Εντολέα (DEBT/OUR)</summary>
        [DataMember(Name = "deptExpences")]
        public decimal DeptExpences { get; set; }
        [DataMember(Name = "nonStpExpences")]
        public decimal NonStpExpences { get; set; }
        [DataMember(Name = "urgentExpences")]
        public decimal UrgentExpences { get; set; }
        [DataMember(Name = "exchangeProfit")]
        public decimal ExchangeProfit { get; set; }

        [DataMember(Name = "sumCommission")]
        public decimal SumCommission { get; set; }

        /// <summary>
        /// Full Bank Title=>used in UI
        /// </summary>
        [DataMember(Name = "bankTitle")]
        public string BankTitle { get; set; }

        [DataMember(Name = "depositor")]
        public string Depositor { get; set; }
    }
}
