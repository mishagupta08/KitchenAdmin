using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class StockReportModel
    {
        public int SNo { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string StallCode { get; set; }
        public string Stall { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public decimal CatCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string RateOrDP { get; set; }
        public string MRP { get; set; }
        public string Qty { get; set; }
        public decimal Quantity { get; set; }
        public string StockValue { get; set; }
        public string DPStockValue { get; set; }
        public string MRPSTockValue { get; set; }
        public string PrintType { get; set; }

        //if BatchWise Selected show fields
        public bool IsBatchWise { get; set; }
        public string ExpDate { get; set; }
        public string MfgDate { get; set; }
        public DateTime ExpDateD { get; set; }
        public DateTime MfgDateD { get; set; }
        public string Expired { get; set; }
        public string BatchNo { get; set; }
        public string Barcode { get; set; }
        public int GroupId { get; set; }

        public List<ProductDetails> ProductDetailsList { get; set; }

        //stock reciept report
        //if summary report
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<CategoryDetails> CategoryList { get; set; }
        public string TaxAmt { get; set; }
        public string BasicAmt { get; set; }
        public string TotalAmt { get; set; }
        public string SoldBy { get; set; }

        //if detail report
        public string StrNo { get; set; }
        public string StrDate { get; set; }
        public string TaxPer { get; set; }
        public string StnNo { get; set; }
        public class SelectListItem
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }
        public string UserName { get; set; }
        public decimal? openingStock { get; set; }
        public decimal? InStock { get; set; }
        public decimal? OutStock { get; set; }

    }


    public class StockDetailList
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string StockDate { get; set; }
        public decimal OpStock { get; set; }
        public decimal InStock { get; set; }
        public decimal StockOut { get; set; }
        public decimal StockWaste { get; set; }
        public decimal ClsStock { get; set; }
    }
}