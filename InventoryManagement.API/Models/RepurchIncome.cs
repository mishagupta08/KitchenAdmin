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
    
    public partial class RepurchIncome
    {
        public decimal RId { get; set; }
        public decimal SessId { get; set; }
        public decimal FormNo { get; set; }
        public string BillNo { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public decimal RepurchIncome1 { get; set; }
        public string Imported { get; set; }
        public string BillType { get; set; }
        public string SoldBy { get; set; }
        public int Msessid { get; set; }
        public decimal KitID { get; set; }
        public decimal DSessID { get; set; }
        public string type { get; set; }
        public decimal Qvp { get; set; }
        public decimal OrderNo { get; set; }
        public System.DateTime OrderDt { get; set; }
        public string IsDispatch { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public decimal LegNo { get; set; }
        public string Remarks { get; set; }
        public string PreStatus { get; set; }
        public decimal RQvp { get; set; }
        public int FSessId { get; set; }
    }
}
