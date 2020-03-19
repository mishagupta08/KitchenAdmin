using InventoryManagement.API.Models;
using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static InventoryManagement.Entity.Common.StockReportModel;

namespace InventoryManagement.Business
{
    public class ReportManager: IReportManager
    {
        ReportRepository objReportRepo = new ReportRepository();
        public List<ProductDetails> GetAllProducts(decimal CategoryCode)
        {
            return (objReportRepo.GetAllProducts(CategoryCode));
        }
        public List<PartyModel> GetAllParty()
        {
            return (objReportRepo.GetAllParty());
        }
        public List<StockReportModel> GetStockReport(string CategoryCode, string ProductCode, string PartyCode, bool IsBatchWise, string StockType)
        {
            return (objReportRepo.GetStockReport(CategoryCode, ProductCode, PartyCode, IsBatchWise, StockType));
        }
        public List<SalesReport> GetSalesReport(string FromDate, string ToDate, string CustomerId, string ProductCode, string CategoryCode, string PartyCode, string BType, string SalesType, string InvoiceType,string BillNo, string FType)
        {
            return (objReportRepo.GetSalesReport(FromDate, ToDate, CustomerId, ProductCode, CategoryCode, PartyCode, BType, SalesType,InvoiceType, BillNo, FType));
        }
        public List<StockJv> GetStockJvReport(string FromDate, string ToDate, string PartyCode,string ViewType)
        {
            return (objReportRepo.GetStockJvReport(FromDate, ToDate, PartyCode, ViewType));
        }
        public List<PurchaseReport> GetPurchaseSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ReportType,string InvoiceNo)
        {
            return (objReportRepo.GetPurchaseSummary(FromDate, ToDate, PartyCode, SupplierCode, ReportType, InvoiceNo));
        }
        
        public List<PurchaseReport> GetPurchaseDetailSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ProductCode)
        {
            return (objReportRepo.GetPurchaseDetailSummary(FromDate, ToDate, PartyCode, SupplierCode, ProductCode));
        }
        public List<string> GetYearList()
        {
            return (objReportRepo.GetYearList());
        }
        public List<PurchaseReport> GetMonthWisePurchaseSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode, string SupplierCode)
        {
            return (objReportRepo.GetMonthWisePurchaseSummary(Year, IsQuantity, IsAmount, PartyCode, SupplierCode));
        }
        public List<string> GetSalesYearList()
        {
            return (objReportRepo.GetSalesYearList());
        }
        public List<SalesReport> GetMonthWiseSalesSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode)
        {
            return (objReportRepo.GetMonthWiseSalesSummary(Year, IsQuantity, IsAmount, PartyCode));
        }
        public List<StockReportModel> GetStockReceiptReport(string CategoryCode, string ProductCode, string PartyCode, string StateCode, string FromDate, string ToDate, string LoginPartyCode, bool isSummary)
        {
            return (objReportRepo.GetStockReceiptReport(CategoryCode, ProductCode, PartyCode, StateCode, FromDate, ToDate, LoginPartyCode, isSummary));
        }
        public List<SelectListItem> GetStateList()
        {
            return (objReportRepo.GetStateList());
        }
        public List<PartyWiseWalletDetails> GetPartyWiseWalletReport(string FromDate, string ToDate, string PartyCode, string ViewType)
        {
            return (objReportRepo.GetPartyWiseWalletReport(FromDate, ToDate, PartyCode, ViewType));
        }
        public List<PaymentSummaryReport> GetPaymentSummaryReport(string FromDate, string ToDate, string PartyCode, string Type)
        {
            return (objReportRepo.GetPaymentSummaryReport(FromDate, ToDate, PartyCode, Type));
        }

        public List<SaleRegister> GetSaleRegisterReport(string FromDate, string ToDate, string PartyCode)
        {
            return (objReportRepo.GetSaleRegisterReport(FromDate, ToDate, PartyCode));
        }

        public List<PaymentMode> GetPaymodeList()
        {
            return (objReportRepo.GetPaymodeList());


        }

        public List<SalesReturnReport> GetSalesReturnReport(string FromDate, string ToDate, string ProductCode, string CategoryCode, string PartyCode, string PartyType, string Type)
        {
            return (objReportRepo.GetSalesReturnReport(FromDate, ToDate, ProductCode, CategoryCode, PartyCode, PartyType, Type));
        }
        
        public List<MonthlySumm> GetMonthlyReport(string PartyCode, string BillType)
        {
            return (objReportRepo.GetMonthlyReport(PartyCode, BillType));
        }

        public string GetOrderProductList(string OrderId)
        {
            return (objReportRepo.GetOrderProductList(OrderId));
        }

        public List<FoodOrderMain> GetOrderReport(string FromDate, string ToDate)
        {
            return (objReportRepo.GetOrderReport(FromDate, ToDate));
        }
    }
}