using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class DisptachOrderModel
    {
        public decimal OrderNo { get; set; }
        public string OrderDateStr { get; set; }
        public DateTime OrderDate { get; set; }
        public string IdNo { get; set; }
        public string Name { get; set; }
        public decimal OrderAmount { get; set; }
        public string Remarks { get; set; }
        public string Address { get; set; }
        public decimal Pincode { get; set; }
        public decimal Mobile { get; set; }
        public string OrderType { get; set; }
        public string DispBy { get; set; }
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string OrderStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string OrderList { get; set; }
        public string SoldBy { get; set; }
        public string ViewType { get; set; }
        public string Filter { get; set; }
        public bool IsDispatched { get; set; }
        public string Reject { get; set; }
    }
}