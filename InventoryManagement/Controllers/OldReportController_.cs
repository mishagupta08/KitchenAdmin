using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        TransactionManager objTransactManager = new TransactionManager();
        ProductManager objProductManager = new ProductManager();
        ReportManager objReportManager = new ReportManager();
        // GET: Report
        [SessionExpire]
        public ActionResult StockReport()
        {
            StockReportModel objModel = new StockReportModel();
            objModel.ProductDetailsList = new List<ProductDetails>();

            //objModel.ProductDetailsList = objReportManager.GetAllProducts(0);
            //var jsonSerialiser = new JavaScriptSerializer();
            //if (objModel.ProductDetailsList.Count > 0)
            //{
            //    //var jsonProduct = jsonSerialiser.Serialize(objModel.ProductDetailsList);
            //    var jsonProduct=Json(objModel.ProductDetailsList, JsonRequestBehavior.AllowGet);
            //    jsonProduct.MaxJsonLength = int.MaxValue;
            //    ViewBag.ProductJsonList = jsonProduct;
            //}
            //else
            //{
            //    ViewBag.ProductJsonList = "";
            //}
            return View(objModel);
        }

        [HttpPost]
        public ActionResult GetStockReport(string CategoryCode, string ProductCode, string PartyCode, bool IsBatchWise, string StockType)
        {
            List<StockReportModel> objStockReportModel = new List<StockReportModel>();
            objStockReportModel = objReportManager.GetStockReport(CategoryCode, ProductCode, PartyCode, IsBatchWise, StockType);
            return Json(objStockReportModel, JsonRequestBehavior.AllowGet);
        }

        //BillWise Sales Report
        [SessionExpire]
        public ActionResult SalesReport()
        {
            List<SelectListItem> objListInvoiceType = new List<SelectListItem>();
            objListInvoiceType.Add(new SelectListItem { Text = "All", Value = "" });
            objListInvoiceType.Add(new SelectListItem { Text = "Repurchase Invoice", Value = "RI" });
            objListInvoiceType.Add(new SelectListItem { Text = "Joining Invoice", Value = "JI" });
            ViewBag.InvoiceTypes = objListInvoiceType;
            return View();
        }
        [SessionExpire]
        public ActionResult DateSalesReport()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult ProductSalesReport()
        {
            return View();
        }

        [SessionExpire]
        public ActionResult StockJvReport()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GetStockJvReport(string FromDate, string ToDate, string PartyCode,string ViewType)
        {
            List<StockJv> objStockJv = new List<StockJv>();
            objStockJv = objReportManager.GetStockJvReport(FromDate, ToDate, PartyCode, ViewType);
            var jsonStock = Json(objStockJv, JsonRequestBehavior.AllowGet);
            jsonStock.MaxJsonLength = int.MaxValue;
            return jsonStock;

        }

        [HttpPost]
        public ActionResult GetSalesReport(string FromDate, string ToDate, string CustomerId, string ProductCode, string CategoryCode, string PartyCode, string BType, string SalesType, string InvoiceType,string BillNo)
        {
            List<SalesReport> objSalesList = new List<SalesReport>();
            objSalesList = objReportManager.GetSalesReport(FromDate, ToDate, CustomerId, ProductCode, CategoryCode, PartyCode, BType, SalesType, InvoiceType, BillNo);
            var jsonProduct = Json(objSalesList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
            //string returnresult = "";
            //if (string.IsNullOrEmpty(FromDate))
            //{
            //    returnresult = ToDate;
            //}
            //else if(string.IsNullOrEmpty(ToDate))
            //{
            //    returnresult = FromDate;
            //}
            //else
            //{
            //    returnresult = "All right";
            //}
            //return Json(returnresult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetPurchaseSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ReportType,string InvoiceNo)
        {
            List<PurchaseReport> objPurchaseList = new List<PurchaseReport>();
            objPurchaseList = objReportManager.GetPurchaseSummary(FromDate, ToDate, PartyCode, SupplierCode, ReportType, InvoiceNo);
            var jsonProduct = Json(objPurchaseList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
            
        }

        [SessionExpire]
        public ActionResult PurchaseInvoicePrint(string Pm)
        {
            List<PurchaseReport> objInvoice = new List<PurchaseReport>();
            if (Session["LoginUser"] != null)
            {
                var base64DecodedBytes = System.Convert.FromBase64String(Pm);
                string InvoiceNoValue = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);
                objInvoice = objTransactManager.GetPurchaseInvoice(InvoiceNoValue);
            }            
            return View(objInvoice);
        }
        public ActionResult GetAllCategory()
        {
            List<CategoryDetails> objCategory = new List<CategoryDetails>();
            objCategory = objProductManager.GetCategoryList("Y");

            return Json(objCategory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllProduct(decimal CategoryCode)
        {
            List<ProductDetails> objProduct = new List<ProductDetails>();
            objProduct = objReportManager.GetAllProducts(CategoryCode);
            var jsonProduct = Json(objProduct, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;

        }
        //public ActionResult GetAllParty()
        //{
        //    List<PartyModel> objparty = new List<PartyModel>();
        //    objparty = objReportManager.GetAllParty();
        //    return Json(objparty, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetAllParty()
        {
            List<PartyModel> objparty = new List<PartyModel>();
            string LoginPartyCode = "";
            decimal LoginStateCode = 0;
            if (Session["LoginUser"] != null)
            {
                LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                LoginStateCode = (Session["LoginUser"] as User).StateCode;
            }
            objparty = objTransactManager.GetAllParty(LoginPartyCode, LoginStateCode);
            return Json(objparty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllPartyListForReports()
        {
            List<PartyModel> objparty = new List<PartyModel>();
            
            objparty = objReportManager.GetAllParty();
            return Json(objparty, JsonRequestBehavior.AllowGet);
        }
        [SessionExpire]
        public ActionResult PurchaseSummary()
        {
            return View();
        }
       
        public ActionResult GetSupplier()
        {
            List<PartyModel> objListSupplier = new List<PartyModel>();
            decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
            string LoginPartyCode= (Session["LoginUser"] as User).PartyCode;
            objListSupplier = objTransactManager.GetAllSupplierList(LoginPartyCode,LoginStateCode);
            return Json(objListSupplier, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult PurchaseDetailSummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMonthWisePurchaseSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode, string SupplierCode)
        {
            List<PurchaseReport> objPurchaseList = new List<PurchaseReport>();
            objPurchaseList = objReportManager.GetMonthWisePurchaseSummary(Year,IsQuantity, IsAmount, PartyCode, SupplierCode);
            var jsonProduct = Json(objPurchaseList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;

        }

        [HttpPost]
        public ActionResult GetMonthWiseSalesSummary(string Year, bool IsQuantity, bool IsAmount, string PartyCode)
        {
            List<SalesReport> objSalesList = new List<SalesReport>();
            objSalesList = objReportManager.GetMonthWiseSalesSummary(Year, IsQuantity, IsAmount, PartyCode);
            var jsonProduct = Json(objSalesList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;

        }

        [HttpPost]
        public ActionResult GetPurchaseDetailSummary(string FromDate, string ToDate, string PartyCode, string SupplierCode, string ProductCode)
        {
            List<PurchaseReport> objPurchaseList = new List<PurchaseReport>();
            objPurchaseList = objReportManager.GetPurchaseDetailSummary(FromDate, ToDate, PartyCode, SupplierCode, ProductCode);
            var jsonProduct = Json(objPurchaseList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;

        }
        [SessionExpire]
        public ActionResult OrderHistory()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult MonthlySummary()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMonthlyReport(string PartyCode, string BillType)
        {
            List<MonthlySumm> objSumm = new List<MonthlySumm>();
            objSumm = objReportManager.GetMonthlyReport(PartyCode, BillType);
            var jsonProduct = Json(objSumm, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        [SessionExpire]
        public ActionResult MonthWiseSalesSummary()
        {
            List<string> objYear = new List<string>();
            objYear = objReportManager.GetSalesYearList();
            List<SelectListItem> objList = new List<SelectListItem>();
            if (objYear.Count() > 0)
            {
                objList.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            }
            else
            {
                objList.Add(new SelectListItem
                {
                    Text = "--No Record--",
                    Value = "0"
                });
            }
            foreach (var obj in objYear)
            {
                objList.Add(new SelectListItem
                {
                    Text = obj,
                    Value = obj
                });
            }
            ViewBag.ListYear = objList;
            return View();
        }
        [SessionExpire]
        public ActionResult MonthWisePurchaseSummary()
        {
            List<string> objYear = new List<string>();
            objYear = objReportManager.GetYearList();
            List<SelectListItem> objList = new List<SelectListItem>();
            if (objYear.Count() > 0)
            {
                objList.Add(new SelectListItem
                {
                    Text = "All",
                    Value = "0"
                });
            }
            else
            {
                objList.Add(new SelectListItem
                {
                    Text= "--No Record--",
                    Value = "0"
                });
            }
            foreach (var obj in objYear)
            {
                objList.Add(new SelectListItem
                {
                    Text = obj,
                    Value = obj
                });
            }
            ViewBag.ListYear = objList;
            return View();
        }

        [SessionExpire]
        public ActionResult StockReceiptReport()
        {
            StockReportModel objModel = new StockReportModel();
            objModel.ProductDetailsList = new List<ProductDetails>();
            objModel.StateList = objReportManager.GetStateList();


            objModel.CategoryList = objProductManager.GetCategoryList("Y");
            return View(objModel);
        }

        [HttpPost]
        public ActionResult GetStockReceiptReport(string CategoryCode, string ProductCode, string PartyCode, string StateCode, string FromDate, string ToDate, bool isSummary)
        {
            List<StockReportModel> objStockReportModel = new List<StockReportModel>();
            string LoginPartyCode = "";
            if (Session["LoginUser"] != null)
            {
                LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
            }
            objStockReportModel = objReportManager.GetStockReceiptReport(CategoryCode, ProductCode, PartyCode, StateCode, FromDate, ToDate, LoginPartyCode, isSummary);
            var jsonProduct = Json(objStockReportModel, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
            //return Json(objStockReportModel, JsonRequestBehavior.AllowGet);
        }
        [SessionExpire]
        public ActionResult PartyWiseBalanceReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetPartyWiseWalletReport(string FromDate, string ToDate, string PartyCode, string ViewType)
        {
            List<PartyWiseWalletDetails> objWalletDetails = new List<PartyWiseWalletDetails>();
            objWalletDetails = objReportManager.GetPartyWiseWalletReport(FromDate, ToDate,PartyCode,ViewType);
            var jsonProduct = Json(objWalletDetails, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
           
        }
        [SessionExpire]
        public ActionResult PaymentSummary()
        {
            PaymentSummary objPaymentSummary = new PaymentSummary();
            try
            {
                if (Session["LoginUser"] != null)
                {
                    objPaymentSummary.PaymentMode = objReportManager.GetPaymodeList();
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objPaymentSummary);
        }

        [HttpPost]
        public ActionResult GetPaymentSummaryReport(string FromDate, string ToDate, string PartyCode, string Type)
        {
            List<PaymentSummaryReport> objWalletDetails = new List<PaymentSummaryReport>();
            objWalletDetails = objReportManager.GetPaymentSummaryReport(FromDate, ToDate, PartyCode, Type);
            var jsonProduct = Json(objWalletDetails, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }
        [SessionExpire]
        public ActionResult SaleRegister()
        {
            PaymentSummary objPaymentSummary = new PaymentSummary();
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                }                
            }
            catch (Exception ex)
            {

            }
            return View(objPaymentSummary);
        }

        [HttpPost]
        public ActionResult GetSaleRegisterReport(string FromDate, string ToDate, string PartyCode)
        {
            List<SaleRegister> objWalletDetails = new List<SaleRegister>();
            objWalletDetails = objReportManager.GetSaleRegisterReport(FromDate, ToDate, PartyCode);
            var jsonProduct = Json(objWalletDetails, JsonRequestBehavior.AllowGet);            
            return jsonProduct;
        }

        [SessionExpire]
        public ActionResult SalesReturnReport()
        {
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult GetSalesReturnReport(string FromDate, string ToDate, string ProductCode, string CategoryCode, string PartyCode, string PartyType, string Type)
        {
            List<SalesReturnReport> objWalletDetails = new List<SalesReturnReport>();
            objWalletDetails = objReportManager.GetSalesReturnReport(FromDate, ToDate, ProductCode, CategoryCode, PartyCode, PartyType, Type);
            var jsonProduct = Json(objWalletDetails, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        [SessionExpire]
        public ActionResult PurchaseReturnReport()
        {
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult GetPurchaseReturnReport(string FromDate, string ToDate, string ProductCode, string CategoryCode, string SupplierCode, string Type)
        {
            List<SalesReturnReport> objWalletDetails = new List<SalesReturnReport>();
            objWalletDetails = objReportManager.GetPurchaseReturnReport(FromDate, ToDate, ProductCode, CategoryCode, SupplierCode, Type);
            var jsonProduct = Json(objWalletDetails, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        [SessionExpire]
        public ActionResult PartyTargetReport()
        {
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                }

                List<SelectListItem> objCategoryList = new List<SelectListItem>();
                var result = objProductManager.GetCategoryList("Y");
                SubCategoryDetails model = new SubCategoryDetails();
                bool f = true;
                foreach (var item in result)
                {
                    SelectListItem tempobj = new SelectListItem();
                    //SelectListItem tempobj1 = new SelectListItem();
                    tempobj.Text = item.CategoryName;
                    tempobj.Value = item.CategoryId.ToString();
                    if (f == true)
                    {
                        f = false;
                        model.CategoryId = int.Parse(item.CategoryId.ToString());
                        //model.SubCatId = int.Parse(item.ToString());
                    }

                    objCategoryList.Add(tempobj);
                }

                ViewBag.ListCategory = objCategoryList;
            }
            catch (Exception ex)
            {

            }
            return View();
        }

    }
}