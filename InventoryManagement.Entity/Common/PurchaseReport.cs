using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Entity.Common
{
    public class PurchaseReport
    {
        public string PartyCode { get; set; }
        public string PartyName { get; set; }
        public string InvoiceDate { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string ReportType { get; set; }
        //supplierwise
        public string Code { get; set; }
        public string Name { get; set; }
        public string TotalQty { get; set; }
        public string Amount { get; set; }
        public string TradeDisc { get; set; }
        public string CashDisc { get; set; }
        public string TaxAmount { get; set; }
        public string NetAmount { get; set; }
        //invoice wise
        public string InvoiceFor { get; set; }
        public DateTime InvoiceDateStr { get; set; }
        public string InvoiceNo { get; set; }
        public string RefNo { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }

        //for purchase invoice
        public string Remarks { get; set; }
        public ProductModel objproduct { get; set; }
        public bool IsNewBill { get; set; }
        public string TotalAmount { get; set; }
        public string TotalTradeDisc { get; set; }
        public string TotalTaxAmt { get; set; }
        public string CashDiscount { get; set; }
        public string RndOff { get; set; }
        public string NetPayable { get; set; }
        public string SoldByName { get; set; }
        public string SoldByAddress { get; set; }
        public string SoldByCity { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAdd { get; set; }
        public string GSTIN { get; set; }
        public string ProductName { get; set; }
        public int ProductCode { get; set; }

        public string TotalPurchase { get; set; }
        public string Jan_Qty { get; set; }
        public string Jan_Pur { get; set; }
        public string Feb_Qty { get; set; }
        public string Feb_Pur { get; set; }
        public string March_Qty { get; set; }
        public string March_Pur { get; set; }
        public string April_Qty { get; set; }
        public string April_Pur { get; set; }
        public string May_Qty { get; set; }
        public string May_Pur { get; set; }
        public string June_Qty { get; set; }
        public string June_Pur { get; set; }
        public string July_Qty { get; set; }
        public string July_Pur { get; set; }
        public string August_Qty { get; set; }
        public string August_Pur { get; set; }
        public string Sep_Qty { get; set; }
        public string Sep_Pur { get; set; }
        public string Oct_Qty { get; set; }
        public string Oct_Pur { get; set; }
        public string Nov_Qty { get; set; }
        public string Nov_Pur { get; set; }
        public string Dec_Qty { get; set; }
        public string Dec_Pur { get; set; }

        public decimal FSessId { get; set; }

        // purchase Return
        public string Categoryname { get; set; }
    }
}