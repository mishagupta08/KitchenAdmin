using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InventoryManagement.Entity.Common.StockReportModel;

namespace InventoryManagement.DataAccess.Contract
{
    public interface IReportRepository
    {
        List<ProductDetails> GetAllProducts(decimal CategoryCode);
        List<PartyModel> GetAllParty();
        List<StockReportModel> GetStockReport(string CategoryCode, string ProductCode, string PartyCode, bool IsBatchWise, string StockType);
        List<SalesReport> GetSalesReport(string FromDate, string ToDate, string CustomerId, string ProductCode, string CategoryCode, string PartyCode, string BType, string SalesType,string InvoiceType,string BillNo, string FType);
        List<StockJv> GetStockJvReport(string FromDate, string ToDate, string PartyCode,string ViewType);
        List<PurchaseReport> GetPurchaseSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ReportType, string InvoiceNo);
        
        List<string> GetYearList();
        List<string> GetSalesYearList();
        List<StockReportModel> GetStockReceiptReport(string CategoryCode, string ProductCode, string PartyCode, string StateCode, string FromDate, string ToDate, string LoginPartyCode, bool isSummary);
        List<SelectListItem> GetStateList();
        List<SalesReport> GetMonthWiseSalesSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode);
        List<PurchaseReport> GetMonthWisePurchaseSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode, string SupplierCode);
        List<PurchaseReport> GetPurchaseDetailSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ProductCode);
        List<PartyWiseWalletDetails> GetPartyWiseWalletReport(string FromDate, string ToDate, string PartyCode, string ViewType);

    }
}
