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
    
    public partial class M_FiscalMaster
    {
        public decimal AutoId { get; set; }
        public decimal FSessId { get; set; }
        public System.DateTime FrmDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public string FSeries { get; set; }
        public string ActiveStatus { get; set; }
        public decimal StartBillNoFrom { get; set; }
        public decimal StartUserBillNoFrom { get; set; }
        public System.DateTime RecTimeStamp { get; set; }
        public string BillSeries { get; set; }
        public int StartPOFrom { get; set; }
        public int StartPIFrom { get; set; }
    }
}
