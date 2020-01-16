using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PurchaseBill
    {
        public string BillNo { get; set; }      
        public DateTime BillDate { get; set; }
        public string SoldBy { get; set; }
        public string SoldByName { get; set; }
        public string partyCode { get; set; }
        public string partyName { get; set; }
        public decimal TotalAmount { get; set;}
        public decimal TotalTax { get; set; }
        public decimal RoundOff { get; set; }
        public decimal NetPayable { get; set; }
        public string FType { get; set; }
        public List<ProductModel> ProductList { get; set; }
    }

    public class PurchaseReturnModel
    {        
        public string BillNo { get; set; }
        public int LoggedInUserId { get; set; }
        public string refno { get; set; }
        public string LoggedInUserIP { get; set; }
        public string reason { get; set; }
        public string partytype { get; set; }
        public string Ftype { get; set; }
        public string returnto { get; set; }
        public string returntoName { get; set; }
        public string returnby { get; set; }
        public string returnbyName { get; set; }
        public string remark { get; set; }
        public DateTime BillDate { get; set; }
        public string OrderNo { get; set; }
        public string EntryBy { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal tAmt { get; set; }
        public decimal taxAmt { get; set; }
        public decimal rndOff { get; set; }
        public decimal netPay { get; set; }
        public decimal TotalBV { get; set; }
        public decimal TotalRP { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTAmt { get; set; }
        public List<ProductModel> ProductList { get; set; }
        public string objProductListStr { get; set; }
    }
}