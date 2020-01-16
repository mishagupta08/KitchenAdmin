using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PaymentSummary
    {
        public List<PaymentMode> PaymentMode { get; set; }
        public List<PartyModel> PartyList { get; set; }
        public string SelectedPaymentMode { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }        
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int GroupId { get; set; }

    }

    public class PaymentMode {
        public string payMode { get; set; }
        public string prefix { get; set; }
    }

    public class PaymentSummaryReport
    {
        
        public string BillDate { get; set; }
        public string BillNo { get; set; }
        public string BillBy { get; set; }
        public string Name { get; set; }
        public string Order { get; set; }
        public string IDNo { get; set; }
        public string IdName { get; set; }
        public string Amount { get; set; }
        public string Cash { get; set; }
        public string Cheque { get; set; }
        public string dd { get; set; }
        public string BankDeposit { get; set; }
        public string CreditCard { get; set; }
        public string DeditCard { get; set; }
        public string NetBanking { get; set; }
        public string Credit { get; set; }
        public string Wallet { get; set; }
    }
}