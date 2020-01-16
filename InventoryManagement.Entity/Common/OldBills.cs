using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class OldBills
    {
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string FCode { get; set; }
        public string PartyName { get; set; }
        public decimal NetPayable { get; set; }
        public string BillDateStr { get; set; }
        public decimal BVValue { get; set; }
        public string Username { get; set; }
        public string IsDeleted { get; set; }
    }
}