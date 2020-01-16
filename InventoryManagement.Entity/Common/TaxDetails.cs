using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class TaxDetails
    {
        public int TaxId { get; set; }
        public int ProductId { get; set; }
        public decimal VatTax { get; set; }
        public decimal GSTTax { get; set; }
        public decimal CstTax { get; set; }
        public decimal CForm { get; set; }
        public bool IsActive { get; set; }
        public string GeneratedBy { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string IsAdd { get; set; }
        public int StateCode { get; set; }
        public decimal AValue { get; set; }
        public decimal STax { get; set; }
        public string TaxType { get; set; }
    }
}