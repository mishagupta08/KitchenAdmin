//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InventoryManagement.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class trnFoodOrderMain
    {
        public decimal OrderId { get; set; }
        public string OrderBy { get; set; }
        public string PartyName { get; set; }
        public string OrderToKitchen { get; set; }
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
        public int userId { get; set; }
    }
}
