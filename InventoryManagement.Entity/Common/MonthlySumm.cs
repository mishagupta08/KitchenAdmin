using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class MonthlySumm
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string InvoiceType { get; set; }
        public string Mnth { get; set; }
        public decimal NetPayable { get; set; }
        public decimal PvValue { get; set; }
    }
}