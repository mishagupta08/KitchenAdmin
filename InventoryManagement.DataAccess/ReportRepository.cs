using InventoryManagement.API.Controllers;
using InventoryManagement.API.Models;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static InventoryManagement.Entity.Common.StockReportModel;

namespace InventoryManagement.DataAccess
{
    public class ReportRepository: IReportRepository
    {
        ReportAPIController objReportAPI = new ReportAPIController();
        public List<ProductDetails> GetAllProducts(decimal CategoryCode)
        {
            return (objReportAPI.GetAllProducts(CategoryCode));
        }
        public List<PartyModel> GetAllParty()
        {
            return (objReportAPI.GetAllParty());
        }

        public List<StockDetailList> GetStockReport(string FromDate, string ToDate, string ProductCode, string PartyCode)
        {
            return (objReportAPI.GetStockReport(FromDate, ToDate, ProductCode, PartyCode));
        }

        public List<SalesReport> GetSalesReport(string FromDate, string ToDate, string CustomerId, string ProductCode, string CategoryCode, string PartyCode, string BType, string SalesType, string InvoiceType , string BillNo, string FType)
        {
            return (objReportAPI.GetSalesReport(FromDate, ToDate, CustomerId, ProductCode, CategoryCode, PartyCode, BType, SalesType,InvoiceType, BillNo, FType));
        }
        public List<StockJv> GetStockJvReport(string FromDate, string ToDate, string PartyCode,string ViewType)
        {
            return (objReportAPI.GetStockJvReport(FromDate, ToDate, PartyCode, ViewType));
        }
        public List<PurchaseReport> GetPurchaseSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ReportType,string InvoiceNo)
        {
            return (objReportAPI.GetPurchaseSummary(FromDate,ToDate,PartyCode,SupplierCode,ReportType, InvoiceNo));
        }
       
        public List<PurchaseReport> GetPurchaseDetailSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ProductCode)
        {
            return (objReportAPI.GetPurchaseDetailSummary(FromDate,ToDate,PartyCode,SupplierCode,ProductCode));
        }
        public List<string> GetYearList()
        {
            return (objReportAPI.GetYearList());
        }
       public  List<PurchaseReport> GetMonthWisePurchaseSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode, string SupplierCode)
        {
            return (objReportAPI.GetMonthWisePurchaseSummary(Year,IsQuantity,IsAmount,PartyCode,SupplierCode));
        }
       public List<string> GetSalesYearList()
       {
            return (objReportAPI.GetSalesYearList());
        }
       public List<SalesReport> GetMonthWiseSalesSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode)
        {
            return (objReportAPI.GetMonthWiseSalesSummary(Year, IsQuantity, IsAmount, PartyCode));
        }
        public List<StockReportModel> GetStockReceiptReport(string CategoryCode, string ProductCode, string PartyCode, string StateCode, string FromDate, string ToDate, string LoginPartyCode, bool isSummary)
        {
            return (objReportAPI.GetStockReceiptReport(CategoryCode,ProductCode,PartyCode,StateCode,FromDate,ToDate,LoginPartyCode,isSummary));
        }
        public List<SelectListItem> GetStateList()
        {
            return (objReportAPI.GetStateList());
        }
        public List<PartyWiseWalletDetails> GetPartyWiseWalletReport(string FromDate, string ToDate, string PartyCode, string ViewType)
        {
            return (objReportAPI.GetPartyWiseWalletReport(FromDate, ToDate, PartyCode, ViewType));
        }
        public List<PaymentSummaryReport> GetPaymentSummaryReport(string FromDate, string ToDate, string PartyCode, string Type)
        {
            return (objReportAPI.GetPaymentSummaryReport(FromDate, ToDate, PartyCode, Type));
        }

        public List<SaleRegister> GetSaleRegisterReport(string FromDate, string ToDate, string PartyCode)
        {
            return (objReportAPI.GetSaleRegisterReport(FromDate, ToDate, PartyCode));
        }

        public List<PaymentMode> GetPaymodeList()
        {
            return (objReportAPI.GetPaymodeList());
        }

        public List<SalesReturnReport> GetSalesReturnReport(string FromDate, string ToDate, string ProductCode, string CategoryCode, string PartyCode, string PartyType, string Type)
        {
            return (objReportAPI.GetSalesReturnReport(FromDate, ToDate, ProductCode, CategoryCode, PartyCode, PartyType, Type));
        }
       
        public List<MonthlySumm> GetMonthlyReport(string PartyCode, string BillType)
        {
            return (objReportAPI.GetMonthlyReport(PartyCode,  BillType));
        }

        public string GetOrderProductList(string OrderId)
        {
            return (objReportAPI.GetOrderProductList(OrderId));
        }

        public List<FoodOrderMain> GetOrderReport(string FromDate, string ToDate, string Stall, string Kitchen, string Status)
        {
            return (objReportAPI.GetOrderReport(FromDate,ToDate,Stall,Kitchen,Status));
        }
    }
}