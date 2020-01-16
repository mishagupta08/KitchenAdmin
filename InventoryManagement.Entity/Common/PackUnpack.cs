using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PackUnpack
    {
        public List<kit> kitlist { get; set; }
        public List<PackUnpackProduct> productList { get; set; }
        public string productstring { get; set; }
        public decimal kitId { get; set; }
        public string  kitName { get; set; }
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string PackOrUnpack { get; set; }
        public int qunatity { get; set; }
    }

    public class kit
    {
        public decimal kitId { get; set; }
        public string kitName { get; set; }
        public string productId { get; set; }
        public string barcode { get; set; }
        public string batchcode { get; set; }
    }

    public class PackUnpackProduct
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Qunatity { get; set; }
        public string AvailStock { get; set; }
        public int MaxPack { get; set; }
    }
}