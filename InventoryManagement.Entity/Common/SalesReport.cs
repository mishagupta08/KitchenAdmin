using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class SalesReport
    {
        //Bill Wise Summary

        //public string Mode { get; set; }
        public string RefIDNo { get; set; }
        public string RefName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string PrintType { get; set; }
        //public string Type { get; set; }
        public string BillNo { get; set; }
        public decimal InternalsBillNo { get; set; }
        public string InternalBillNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string StrBillDate { get; set; }
        public string SoldBy { get; set; }
        public string PartyName { get; set; }
        public string PartyCode { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string Amount { get; set; }
        public string NetAmount { get; set; }
        public string TaxAmount { get; set; }
        public string FType { get; set; }
        //Date Wise Summary

        public DateTime RecordDate { get; set; }
        public string NoOfBills { get; set; }
        public string TotalBV { get; set; }
        public string TotalQty { get; set; }
        public string TotalBillAmt { get; set; }
        public string TotalPaidByWallet { get; set; }
        public string TotalTaxAmount { get; set; }
        public string TotalAmount { get; set; }
        //Product Wise Summary

        public string IdNo { get; set; }
        public string Name { get; set; }
        public string ProdCode { get; set; }
        public string CategoryCode { get; set; }
        public decimal CatCode { get; set; }
        public string Categoryname { get; set; }
        public string ProductName { get; set; }
        public string Qty { get; set; }
        public string Rate { get; set; }
        public decimal OfferUID { get; set; }
        public string DiscPer { get; set; }
        public string DiscAmt { get; set; }
        public string TaxPer { get; set; }
        public string TaxAmt { get; set; }
        public string BasicAmt { get; set; }
        public string TotalAmt { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string CGSTAmount { get; set; }
        public string SGSTAmount { get; set; }
        public string IGSTAmount { get; set; }

        public string ErrorMsg { get; set; }
        
        public string TotalSales { get; set; }
        public string Jan_Qty { get; set; }
        public string Jan_Sales { get; set; }
        public string Feb_Qty { get; set; }
        public string Feb_Sales { get; set; }
        public string March_Qty { get; set; }
        public string March_Sales { get; set; }
        public string April_Qty { get; set; }
        public string April_Sales { get; set; }
        public string May_Qty { get; set; }
        public string May_Sales { get; set; }
        public string June_Qty { get; set; }
        public string June_Sales { get; set; }
        public string July_Qty { get; set; }
        public string July_Sales { get; set; }
        public string August_Qty { get; set; }
        public string August_Sales { get; set; }
        public string Sep_Qty { get; set; }
        public string Sep_Sales { get; set; }
        public string Oct_Qty { get; set; }
        public string Oct_Sales { get; set; }
        public string Nov_Qty { get; set; }
        public string Nov_Sales { get; set; }
        public string Dec_Qty { get; set; }
        public string Dec_Sales { get; set; }
        public int BillYear { get; set; }
       

        //delete bills
        public string Reason { get; set; }
        public decimal UserId { get; set; }
        public decimal FsessId { get; set; }
        public bool IsDelete { get; set; }
        public string ListBillsToDelete { get; set; }

        public string InvoiceType { get; set; }

        //sales return report
        public string PartyType { get; set; }

        //update delivery detail
        public string CourierName { get; set; }
        public string DocWeight { get; set; }
        public string DocketNo { get; set; }
        public DateTime? DocketDate { get; set; }
        public DateTime? DOD { get; set; }
        public string DelvAddress { get; set; }
        public string CourierId { get; set; }
        public string NetPayable { get; set; }
        public DateTime? DispDate { get; set; }
        public string OrderNo { get; set; }
        public string MobileNO { get; set; }
        public string CID { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string BillType { get; set; }

    }

    public class UpdateDeliveryDetails
    {
        public string ErrorMessage { get; set; }
        public string ListObjHidden { get; set; }
        public List<SalesReport> DeliverDetailList { get; set; }
        public int LoggedInUser { get; set; }
    }
}