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
    
    public partial class OLDBillMain
    {
        public decimal AID { get; set; }
        public string BillNo { get; set; }
        public System.DateTime BillDate { get; set; }
        public string FCode { get; set; }
        public string PartyName { get; set; }
        public decimal BVValue { get; set; }
        public decimal MRPValue { get; set; }
        public decimal NetPayable { get; set; }
        public string Username { get; set; }
        public Nullable<System.DateTime> DRecTimeStamp { get; set; }
        public int TotalQty { get; set; }
        public string InvTYpe { get; set; }
        public string SoldBy { get; set; }
    }
}
