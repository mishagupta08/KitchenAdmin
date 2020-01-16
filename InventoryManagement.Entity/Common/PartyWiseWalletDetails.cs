using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PartyWiseWalletDetails
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public decimal CrAmtD { get; set; }
        public decimal DrAmtD { get; set; }
        public string CrAmt { get; set; }
        public string DrAmt { get; set; }
        public string CrTo { get; set; }
        public string DrTo { get; set; }
        public string Balance { get; set; }
        public string Narration { get; set; }
        public string TransDateStr { get; set; }
        public DateTime TransDate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}