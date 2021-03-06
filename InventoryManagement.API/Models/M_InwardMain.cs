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
    
    public partial class M_InwardMain
    {
        public decimal IdNo { get; set; }
        public decimal FSessId { get; set; }
        public string InwardBy { get; set; }
        public string InwardByName { get; set; }
        public decimal GrNo { get; set; }
        public string InwardNo { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public System.DateTime RecvDate { get; set; }
        public string RefNo { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal TotalTradeDiscount { get; set; }
        public decimal TotalLotDiscount { get; set; }
        public decimal CashDiscPer { get; set; }
        public decimal TotalCashDiscount { get; set; }
        public decimal TotalQty { get; set; }
        public decimal TotalFreeQty { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTaxAmt { get; set; }
        public decimal TotalEAmt { get; set; }
        public decimal RndOff { get; set; }
        public decimal NetPayable { get; set; }
        public string Remarks { get; set; }
        public string ActiveStatus { get; set; }
        public string InwardFor { get; set; }
        public string InwardForName { get; set; }
        public string Status { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public string Version { get; set; }
        public decimal TotalOrdQty { get; set; }
        public decimal TotalShortQty { get; set; }
        public decimal TotalDemQty { get; set; }
        public decimal TotalExpQty { get; set; }
        public decimal TotalRemQty { get; set; }
        public string OrderNo { get; set; }
        public string OrderBy { get; set; }
        public int CourierId { get; set; }
        public string CourierName { get; set; }
        public string TransId { get; set; }
        public string TransName { get; set; }
        public string LRNo { get; set; }
        public Nullable<System.DateTime> LRDate { get; set; }
        public string UID { get; set; }
        public decimal FreightAmt { get; set; }
        public decimal OtherCharges { get; set; }
        public string TaxBase { get; set; }
        public decimal TotalShortAmt { get; set; }
        public decimal TotalDemAmt { get; set; }
        public decimal TotalExpAmt { get; set; }
        public decimal TotalDedcAmt { get; set; }
        public string GenDN { get; set; }
        public string GenDNBy { get; set; }
        public int SDNNo { get; set; }
        public string DNNo { get; set; }
        public Nullable<System.DateTime> DNDate { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
    }
}
