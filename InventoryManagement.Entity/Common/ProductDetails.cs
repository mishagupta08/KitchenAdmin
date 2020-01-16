using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class ProductDetails
    {
        public TaxDetails ProductTaxDetails { get; set; }
        public BarcodeDetails ProductBarcodeDetails { get; set; }
        public CategoryDetails ProductCategoryDetails { get; set; }
        public SubCategoryDetails ProductSubCategoryDetails { get; set; }
        public CurrentStockModel ProductCurrentStockDetails { get; set; }
        public int ProductId { get; set; }
        public int ProductCode { get; set; }
        public string MinQtyStr { get; set; }
        public string ProductCodeStr { get; set; }
        public string UserDefinedCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public int SubCatgeoryId { get; set; }
        public bool IsActive { get; set; }
        public decimal BV { get; set; }
        public decimal CV { get; set; }
        public decimal PV { get; set; }
        public decimal RP { get; set; }
        public decimal DiscountPer { get; set; }
        public decimal DiscountInRs { get; set; }
        public decimal ProductCommission { get; set; }
        public string Message { get; set; }
        public string MessageStatus { get; set; }
        public string OnWebsite { get; set; }
        public string SpecialOffer { get; set; }
        public string HotSell { get; set; }
        public string ProductImagePath { get; set; }
        public string IsAdd { get; set; }
        public decimal MinQty { get; set; }
        public string HSNCode { get; set; }
        public User UserDetails { get; set; }
    }
}