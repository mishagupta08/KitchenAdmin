using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class OfferProducts
    {public decimal offerID { get; set; }
        public string ProdID { get; set; }
        public decimal Qty { get; set; }
        public decimal FreeQty { get; set; }
        public string IsFlexible { get; set; }
        public decimal MinBV { get; set; }
        public decimal MinAmt { get; set; }
        public decimal TotalQty { get; set; }

    }
}