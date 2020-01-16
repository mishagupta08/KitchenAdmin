using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class SalesReturnReport
    {
        public string ReturnBy { get; set; }
        public string BillNo { get; set; }
        public string ReturnByName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Tax { get; set; }
        public string TaxAmt { get; set; }
        public string TotalAmt { get; set; }
        public string BasicAmt { get; set; }
        public string ReturnTo { get; set; }
        public string STRNo { get; set; }
        public string STRDate { get; set; }
    }
}