using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
   public  class FoodOrderDetail
    {
        public decimal Id { get; set; }
        public decimal OrderId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public int ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string KitchenID { get; set; }
        public int CookID { get; set; }
        public int PckID { get; set; }
        public int DelvID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> TaxPer { get; set; }
        public Nullable<decimal> TaxAmt { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<decimal> TotalTax { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string  CookStatus { get; set; }
        public string SuperVisiorStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string CookRemark { get; set; }
        public string SuperVisorRemark { get; set; }
        public string DeliveryRemark { get; set; }
        public Nullable<int> SCID { get; set; }
        public Nullable<int> AssignTo { get; set; }
    }

    public class FoodOrderMain
    {
        public decimal OrderId { get; set; }
        public string OrderBy { get; set; }
        public string PartyName { get; set; }
        public string OrderToKitchen { get; set; }
        public string KitchenName { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string Paymode { get; set; }
        public Nullable<decimal> chNo { get; set; }
        public Nullable<System.DateTime> ChDate { get; set; }
        public Nullable<decimal> ChAmt { get; set; }
        public string BankName { get; set; }
        public decimal TotalOrdQty { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTaxAmt { get; set; }
        public decimal RndOff { get; set; }
        public decimal NetPayable { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public System.DateTime RecTimeStamp { get; set; }        
    }
}