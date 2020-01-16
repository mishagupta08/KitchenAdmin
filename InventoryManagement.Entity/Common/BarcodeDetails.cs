using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class BarcodeDetails
    {
        public int ProductBarcodeId { get; set; }
        public int ProductId { get; set; }
        public string BarcodeType { get; set; }
        public string Barcode { get; set; }
        public String BType { get; set; }
        public string GeneratedBy { get; set; }
        public DateTime GenerateDate { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal MRP { get; set; }
        public decimal DP { get; set; }
        public string IsExpirable { get; set; }
        public string ExpDateStr { get; set; }
        public string MfgDateStr { get; set; }
        public DateTime? ExpDate { get; set; }
        public DateTime? MfgDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public string IsAdd { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string objProductListStr { get; set; }
        public string ExisitingBarcode { get; set; }
    }
}