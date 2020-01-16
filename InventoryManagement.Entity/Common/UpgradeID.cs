using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class UpgradeID
    {
        public string IDno { get; set; }
        public string MemberName { get; set; }
        public int KitId { get; set; }
        public string KitName { get; set; }
        public decimal KitAmount { get; set; }
        public string productstring { get; set; }
        public string MacAdres { get; set; }           
        public decimal KitBV { get; set; }
        public List<kits> NewKitList { get; set; }
        public int NewKitId { get; set; }
        public string NewKitName { get; set; }
        public int promoId { get; set; }
        public string BillType { get; set; }
        public List<ProductModel> objListProduct { get; set; }
        public CustomerDetail objCustomer { get; set; }
        public ProductModel objProduct { get; set; }
        public string TaxType { get; set; }
    }

    public class kits
    {
        public int kitId { get; set; }
        public string kitName { get; set; }
    }
}