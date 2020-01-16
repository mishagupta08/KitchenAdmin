using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using NPOI.SS.UserModel;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        // GET: Transaction

        TransactionManager objTransacManager = new TransactionManager();
        ProductManager objProductManager = new ProductManager();
        [SessionExpire]
        public ActionResult DistributorBill()
        {

            DistributorBillModel objDistributorModel = new DistributorBillModel();
            try
            {
                objDistributorModel.objCustomer = new CustomerDetail();
                objDistributorModel.objProduct = new ProductModel();
                List<SelectListItem> objBankList = new List<SelectListItem>();
                var result = objTransacManager.GetBankList();
                objDistributorModel.objProduct.PayDetails = new PayDetails();
                foreach (var obj in result)
                {
                    if (obj.BankCode == 0)
                    {
                        objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString(), Selected = true });
                        objDistributorModel.objProduct.PayDetails.BDBankName = obj.BankCode.ToString();
                    }
                    else
                    {
                        objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString() });
                    }
                }
                ViewBag.BankNames = objBankList;
                List<SelectListItem> CardTypes = new List<SelectListItem>();
                CardTypes.Add(new SelectListItem { Text = "Credit Card", Value = "CC" });
                CardTypes.Add(new SelectListItem { Text = "Debit Card", Value = "DB" });
                ViewBag.CardTypes = CardTypes;

                objDistributorModel.objProduct.PayDetails.CardType = "CC";

                //List<SelectListItem> objListCustomerTypes = new List<SelectListItem>();
                //objListCustomerTypes.Add(new SelectListItem { Text = "New", Value = "New" });
                //objListCustomerTypes.Add(new SelectListItem { Text = "Existing", Value = "Existing" });
                //ViewBag.CustomerType = objListCustomerTypes;

                //ViewBag.ConfigDetails = objTransacManager.GetConfigDetails();
                //objDistributorModel.objConfigDetails = objTransacManager.GetConfigDetails();

                objDistributorModel.objCustomer.CustomerType = "New";
                //var OfferList = objTransacManager.GetOfferList();
                List<SelectListItem> OfferSelectList = new List<SelectListItem>();
                OfferSelectList.Add(new SelectListItem { Text = "--Choose Offer--", Value = "0" });
                //foreach (var obj in OfferList)
                //{ OfferSelectList.Add(new SelectListItem { Value = obj.ProdCode.ToString(), Text = obj.ProductName }); }
                ViewBag.OfferList = OfferSelectList;

                var KitIdlist = objTransacManager.GetKitIdList();
                List<SelectListItem> KidIsListObj = new List<SelectListItem>();
                KidIsListObj.Add(new SelectListItem { Text = "--Select Kit--", Value = "0" });
                foreach (var obj in KitIdlist)
                {
                    KidIsListObj.Add(new SelectListItem { Text = obj.KitName, Value = obj.KitId.ToString() });
                }
                ViewBag.objKitList = KidIsListObj;
                objDistributorModel.objCustomer.KitId = 0;
                //objDistributorModel.objCustomer.IsRegisteredCustomer = true;
                //objDistributorModel.objCustomer.ReferenceIdNo = InventorySession.LoginUser.PartyCode;
                //objDistributorModel.objCustomer.ReferenceName = InventorySession.LoginUser.PartyName;
                InventorySession.StoredDistributorValues = null;

            }
            catch (Exception ex)
            {
                LogError(ex);
                Console.Write("Exception:", ex.Message);
            }
            return View(objDistributorModel);
        }
        public ActionResult GetOfferList( string Doj, string UpgradeDate, bool IsFirstBill, bool ActiveStatus)
        {
            return Json(objTransacManager.GetOfferList( Doj,  UpgradeDate,  IsFirstBill,  ActiveStatus), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetKitDescription(decimal KitId)
        {
            return Json(objTransacManager.GetKitDescription(KitId), JsonRequestBehavior.AllowGet);
        }
        public void LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", "DistributorBill");
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        [SessionExpire]
        public ActionResult PartyBill()
        {
            DistributorBillModel objDistributorModel = new DistributorBillModel();
            objDistributorModel.objCustomer = new CustomerDetail();
            objDistributorModel.objProduct = new ProductModel();
            List<SelectListItem> objBankList = new List<SelectListItem>();
            var result = objTransacManager.GetBankList();
            objDistributorModel.objProduct.PayDetails = new PayDetails();
            foreach (var obj in result)
            {
                if (obj.BankCode == 0)
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString(), Selected = true });
                    objDistributorModel.objProduct.PayDetails.BDBankName = obj.BankCode.ToString();
                }
                else
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString() });
                }
            }
            ViewBag.BankNames = objBankList;
            List<SelectListItem> CardTypes = new List<SelectListItem>();
            CardTypes.Add(new SelectListItem { Text = "Credit Card", Value = "CC" });
            CardTypes.Add(new SelectListItem { Text = "Debit Card", Value = "DB" });
            ViewBag.CardTypes = CardTypes;

            objDistributorModel.objProduct.PayDetails.CardType = "CC";

            //List<SelectListItem> objListCustomerTypes = new List<SelectListItem>();
            //objListCustomerTypes.Add(new SelectListItem { Text = "New", Value = "New" });
            //objListCustomerTypes.Add(new SelectListItem { Text = "Existing", Value = "Existing" });
            //ViewBag.CustomerType = objListCustomerTypes;

            //ViewBag.ConfigDetails = objTransacManager.GetConfigDetails();
            //objDistributorModel.objConfigDetails = objTransacManager.GetConfigDetails();

            objDistributorModel.objCustomer.CustomerType = "New";
            //objDistributorModel.objCustomer.IsRegisteredCustomer = true;
            //objDistributorModel.objCustomer.ReferenceIdNo = InventorySession.LoginUser.PartyCode;
            //objDistributorModel.objCustomer.ReferenceName = InventorySession.LoginUser.PartyName;

            return View(objDistributorModel);
        }

        [SessionExpire]
        public ActionResult CustomerBill()
        {
            DistributorBillModel objDistributorModel = new DistributorBillModel();
            objDistributorModel.objCustomer = new CustomerDetail();
            objDistributorModel.objProduct = new ProductModel();
            List<SelectListItem> objBankList = new List<SelectListItem>();
            var result = objTransacManager.GetBankList();
            objDistributorModel.objProduct.PayDetails = new PayDetails();
            foreach (var obj in result)
            {
                if (obj.BankCode == 0)
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString(), Selected = true });
                    objDistributorModel.objProduct.PayDetails.BDBankName = obj.BankCode.ToString();
                }
                else
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString() });
                }
            }
            ViewBag.BankNames = objBankList;
            List<SelectListItem> CardTypes = new List<SelectListItem>();
            CardTypes.Add(new SelectListItem { Text = "Credit Card", Value = "CC" });
            CardTypes.Add(new SelectListItem { Text = "Debit Card", Value = "DB" });
            ViewBag.CardTypes = CardTypes;

            objDistributorModel.objProduct.PayDetails.CardType = "CC";



            objDistributorModel.objCustomer.CustomerType = "New";

            return View(objDistributorModel);
        }

        [HttpPost]
        public ActionResult SaveDistributorBill(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objModel.objProductListStr))
                {
                    var objects = JArray.Parse(objModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            // var appName = app.Key;
                            //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                            if (app.Key == "Code")
                            {
                                objTemp.ProdCode = (int)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "FreeQty")
                            {
                                objTemp.FreeQty = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "CVValue")
                            {
                                objTemp.CVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.PVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "BVValue")
                            {
                                objTemp.BVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscPer")
                            {
                                objTemp.DiscPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmt")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxPer")
                            {
                                objTemp.TaxPer = (decimal)app.Value;
                            }
                            else if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "TotalAmount")
                            {
                                objTemp.TotalAmount = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxType")
                            {
                                objTemp.TaxType = (string)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;

                            }
                            else if (app.Key == "AvailStock")
                            {
                                objTemp.StockAvailable = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnPer")
                            {
                                objTemp.CommissionPer = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnAmt")
                            {
                                objTemp.CommissionAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "RPValue")
                            {
                                objTemp.RPValue = (decimal)app.Value;
                            }
                            else if (app.Key == "ProductTye")
                            {
                                objTemp.ProductTye = (string)app.Value;
                            }
                        }
                        objModel.objListProduct.Add(objTemp);
                    }
                    objModel.objCustomer.UserDetails = Session["LoginUser"] as User;
                    // Retrive the Name of HOST
                    string hostName = Dns.GetHostName();
                    // Get the IP  
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                    objModel.objProduct.UID = myIP + currentDate;
                    objResponse = objTransacManager.SaveDistributorBill(objModel);
                    //if (objResponse.ResponseStatus == "OK")
                    //{
                    //    InventorySession.StoredDistributorValues = objResponse.ResponseDetailsToPrint;
                    //}
                }
            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult AddLessStockJV(string JvType)
        {
            StockJv objModel = new StockJv();
            objModel.objListGroup = new List<GroupModel>();
            objModel.objListGroup = objTransacManager.GetGroupList();
            objModel.GroupId = objModel.objListGroup[0].GroupId;
            //objModel.objPartyList = new List<PartyModel>();
            //string LoginPartyCode = InventorySession.LoginUser.PartyCode;
            //decimal StateCode = InventorySession.LoginUser.StateCode;
            //objModel.objPartyList = objTransacManager.GetAllParty(LoginPartyCode, StateCode);
            if (!string.IsNullOrEmpty(JvType))
            {
                if (JvType == "Add")
                {
                    objModel.isAdd = true;
                }
                else
                {
                    objModel.isAdd = false;
                }
            }
            return View(objModel);
        }

        [HttpPost]
        public ActionResult SaveStockJv(StockJv objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objModel.objProductListStr))
                {
                    var objects = JArray.Parse(objModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            // var appName = app.Key;
                            //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                            if (app.Key == "Code")
                            {
                                objTemp.ProdCode = (int)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }

                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }

                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }

                        }
                        objModel.objListProduct.Add(objTemp);
                    }
                    objModel.LoginUser = Session["LoginUser"] as User;


                    objResponse = objTransacManager.SaveStockJv(objModel);

                }

            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCustInfo(string IdNo)
        {
            CustomerDetail model = new CustomerDetail();
            model = objTransacManager.GetCustInfo(IdNo);
            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult GetProductInfo(string SearchType, string data, bool isCForm, string BillType, bool IsBillOnMrp,string OfferID)
        {
            List<ProductModel> model = new List<ProductModel>();
            model = objTransacManager.GetproductInfo(SearchType, data, isCForm, BillType, (Session["LoginUser"] as User).StateCode, (Session["LoginUser"] as User).PartyCode, IsBillOnMrp, OfferID);
            return Json(model, JsonRequestBehavior.AllowGet);

        }
        
        public ActionResult GetAllProductNames()
        {
            List<string> model = objTransacManager.GetAutocompleteProductNames();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProductNamesOnly()
        {
            List<string> model = objTransacManager.GetAutocompProductsOnly();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOfferProductNamesOnly(decimal OfferID)
        {
            List<string> model = objTransacManager.GetOfferProductNamesOnly(OfferID);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

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
            objparty = objTransacManager.GetAllParty(LoginPartyCode, LoginStateCode);
            return Json(objparty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllSupplier()
        {
            List<PartyModel> objparty = new List<PartyModel>();
            string LoginPartyCode = "";
            decimal LoginStateCode = 0;
            if (Session["LoginUser"] != null)
            {
                LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                LoginStateCode = (Session["LoginUser"] as User).StateCode;
            }
            objparty = objTransacManager.GetAllSupplierList(LoginPartyCode, LoginStateCode);
            return Json(objparty, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult InvoicePrint(string Pm)
        {
            DistributorBillModel model = new DistributorBillModel();
            if (Session["LoginUser"] != null)
            {
                var base64DecodedBytes = System.Convert.FromBase64String(Pm);
                string BillNoValue = System.Text.Encoding.UTF8.GetString(base64DecodedBytes);                
                string CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;
                model = objTransacManager.getInvoice(BillNoValue, CurrentPartyCode);
                if (model == null)
                {
                    model = new DistributorBillModel();
                }
            }
            string Viewname = string.Empty;
            if (model.BillType == "S")
            {
                Viewname = "StockInvoicePrint";
            }
            else
            {
                Viewname = "InvoicePrint";
            }
            return View(Viewname, model);
        }

        [HttpPost]
        public async Task<ActionResult> SendOTP(string MobileNo, string TotalBillAmount)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = await Task.Run(() => (objTransacManager.SendOTP(MobileNo, TotalBillAmount)));
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult PurchaseInvoice()
        {


            return View();
        }

        [HttpPost]
        public ActionResult SavePurchaseInvoice(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                try
                {
                    if (!string.IsNullOrEmpty(objModel.objProductListStr))
                    {
                        var objects = JArray.Parse(objModel.objProductListStr); // parse as array  
                        foreach (JObject root in objects)
                        {
                            ProductModel objTemp = new ProductModel();
                            foreach (KeyValuePair<String, JToken> app in root)
                            {
                                // var appName = app.Key;
                                //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                                if (app.Key == "Code")
                                {
                                    objTemp.ProdCode = (int)app.Value;
                                }
                                else if (app.Key == "ProductName")
                                {
                                    objTemp.ProductName = (string)app.Value;
                                }
                                else if (app.Key == "Rate")
                                {
                                    objTemp.Rate = (decimal)app.Value;
                                }
                                else if (app.Key == "Barcode")
                                {
                                    objTemp.Barcode = app.Value.ToString();
                                }
                                else if (app.Key == "BatchNo")
                                {
                                    objTemp.BatchNo = app.Value.ToString();
                                }
                                else if (app.Key == "MRP")
                                {
                                    objTemp.MRP = (decimal?)app.Value;
                                }
                                else if (app.Key == "Qty")
                                {
                                    objTemp.Quantity = (decimal)app.Value;
                                }
                                else if (app.Key == "PV")
                                {
                                    objTemp.PV = (decimal)app.Value;
                                }
                                else if (app.Key == "CV")
                                {
                                    objTemp.CV = (decimal)app.Value;
                                }
                                else if (app.Key == "BV")
                                {
                                    objTemp.BV = (decimal)app.Value;
                                }
                                else if (app.Key == "RP")
                                {
                                    objTemp.RP = (decimal)app.Value;
                                }
                                else if (app.Key == "DP")
                                {
                                    objTemp.DP = (decimal)app.Value;
                                }
                                else if (app.Key == "CVValue")
                                {
                                    objTemp.CVValue = (decimal)app.Value;
                                }
                                else if (app.Key == "PVValue")
                                {
                                    objTemp.PVValue = (decimal)app.Value;
                                }
                                else if (app.Key == "BVValue")
                                {
                                    objTemp.BVValue = (decimal)app.Value;
                                }
                                else if (app.Key == "DiscPer")
                                {
                                    objTemp.DiscPer = (decimal)app.Value;
                                }
                                else if (app.Key == "DiscAmt")
                                {
                                    objTemp.DiscAmt = (decimal)app.Value;
                                }
                                else if (app.Key == "TaxAmt")
                                {
                                    objTemp.TaxAmt = (decimal)app.Value;
                                }
                                else if (app.Key == "TaxPer")
                                {
                                    objTemp.TaxPer = (decimal)app.Value;
                                }
                                else if (app.Key == "Amount")
                                {
                                    objTemp.Amount = (decimal)app.Value;
                                }
                                else if (app.Key == "TotalAmount")
                                {
                                    objTemp.TotalAmount = (decimal)app.Value;
                                }
                                else if (app.Key == "TaxType")
                                {
                                    objTemp.TaxType = (string)app.Value;
                                }
                                else if (app.Key == "PVValue")
                                {
                                    objTemp.DiscAmt = (decimal)app.Value;

                                }
                                else if (app.Key == "AvailStock")
                                {
                                    objTemp.StockAvailable = (decimal)app.Value;
                                }
                                else if (app.Key == "CommsnPer")
                                {
                                    objTemp.CommissionPer = (decimal)app.Value;
                                }
                                else if (app.Key == "CommsnAmt")
                                {
                                    objTemp.CommissionAmt = (decimal)app.Value;
                                }
                                else if (app.Key == "RPValue")
                                {
                                    objTemp.RPValue = (decimal)app.Value;
                                }

                            }
                            objModel.objListProduct.Add(objTemp);
                        }
                        objModel.objCustomer.UserDetails = Session["LoginUser"] as User;
                        // Retrive the Name of HOST
                        string hostName = Dns.GetHostName();
                        // Get the IP  
                        string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                        string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                        objModel.objProduct.UID = myIP + currentDate;
                        objResponse = objTransacManager.SavePurchaseInvoice(objModel);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult OrderCreation()
        {
            PartyOrderModel objPartyModel = new PartyOrderModel();

            objPartyModel.objProduct = new ProductModel();
            List<SelectListItem> objBankList = new List<SelectListItem>();
            var result = objTransacManager.GetBankList();
            objPartyModel.objProduct.PayDetails = new PayDetails();
            foreach (var obj in result)
            {
                if (obj.BankCode == 0)
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString(), Selected = true });
                    objPartyModel.objProduct.PayDetails.BDBankName = obj.BankCode.ToString();
                }
                else
                {
                    objBankList.Add(new SelectListItem { Text = obj.BankName, Value = obj.BankCode.ToString() });
                }
            }
            ViewBag.BankNames = objBankList;
            List<SelectListItem> CardTypes = new List<SelectListItem>();
            CardTypes.Add(new SelectListItem { Text = "Credit Card", Value = "Credit Card" });
            CardTypes.Add(new SelectListItem { Text = "Debit Card", Value = "Debit Card" });
            ViewBag.CardTypes = CardTypes;

            objPartyModel.objProduct.PayDetails.CardType = "Credit Card";
            objPartyModel.objProduct.PayDetails.PayMode = "BD";
            string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
            objPartyModel.OrderNo = objTransacManager.GetOrderNo(LoginPartyCode);
            objPartyModel.PartyWalletBalance = objTransacManager.GetPartyWalletBalance(LoginPartyCode);
            objPartyModel.OrderBy = LoginPartyCode;
            objPartyModel.OrderTo = (Session["LoginUser"] as User).ParentPartyCode;
            return View(objPartyModel);
        }

        [HttpPost]
        public ActionResult SavePartyOrderDetails(PartyOrderModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objModel.objProductListStr))
                {
                    var objects = JArray.Parse(objModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            // var appName = app.Key;
                            //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                            if (app.Key == "Code")
                            {
                                objTemp.ProdCode = (int)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "CVValue")
                            {
                                objTemp.CVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.PVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "BVValue")
                            {
                                objTemp.BVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscPer")
                            {
                                objTemp.DiscPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmt")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxPer")
                            {
                                objTemp.TaxPer = (decimal)app.Value;
                            }
                            else if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "TotalAmount")
                            {
                                objTemp.TotalAmount = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxType")
                            {
                                objTemp.TaxType = (string)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;

                            }
                            else if (app.Key == "AvailStock")
                            {
                                objTemp.StockAvailable = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnPer")
                            {
                                objTemp.CommissionPer = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnAmt")
                            {
                                objTemp.CommissionAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "RPValue")
                            {
                                objTemp.RPValue = (decimal)app.Value;
                            }

                        }
                        objModel.objListProduct.Add(objTemp);
                    }
                    objModel.LoginUser = Session["LoginUser"] as User;
                    // Retrive the Name of HOST
                    string hostName = Dns.GetHostName();
                    // Get the IP  
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmm");
                    objModel.objProduct.UID = "";
                    objResponse = objTransacManager.SavePartyOrderDetails(objModel);

                }
            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult PendingOrder()
        {
            return View();
        }
      
        [SessionExpire]
        public ActionResult DispatchOrder()
        {
            return View();
        }
        public ActionResult GetOrderList(string FromDate, string ToDate, string PartyCode, string ViewType, string IdNo, string OrderNo, string DispMode)
        {
            string CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;
            string IsAdmin = (Session["LoginUser"] as User).IsAdmin;
            //if (IsAdmin != "Y")
            if (OrderNo != "" && OrderNo != "0")
            {
                FromDate = "All"; ToDate = "All";PartyCode = "0";IdNo = "0";
            }
            else { if (CurrentPartyCode != System.Web.Configuration.WebConfigurationManager.AppSettings["WRPartyCode"])
                    PartyCode = CurrentPartyCode;}
            List<DisptachOrderModel> objDispatchOrderList = new List<DisptachOrderModel>();
            objDispatchOrderList = objTransacManager.GetDispatchOrderList(FromDate, ToDate, PartyCode, ViewType, IdNo, OrderNo, DispMode);
            return Json(objDispatchOrderList, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RejectOrder(string OrderNo, string RejectReason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objTransacManager.RejectOrder(OrderNo, RejectReason, (Session["LoginUser"] as User).UserId);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOrderProduct(string OrderNo)
        {
            List<ProductModel> objModel = new List<ProductModel>();
            objModel = objTransacManager.GetOrderProduct(OrderNo, (Session["LoginUser"] as User).PartyCode);
            return Json(objModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveDispatchOrderDetails(DisptachOrderModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            List<DisptachOrderModel> objDispatchList = new List<DisptachOrderModel>();
            try
            {
                if (objModel != null)
                {

                    if (!string.IsNullOrEmpty(objModel.OrderList))
                    {
                        var objects = JArray.Parse(objModel.OrderList); // parse as array  
                        foreach (JObject root in objects)
                        {
                            DisptachOrderModel objTemp = new DisptachOrderModel();
                            foreach (KeyValuePair<String, JToken> app in root)
                            {
                                // var appName = app.Key;
                                //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                                if (app.Key == "OrderNo")
                                {
                                    objTemp.OrderNo = (int)app.Value;
                                }
                                else if (app.Key == "SoldBy")
                                {
                                    objTemp.SoldBy = (Session["LoginUser"] as User).PartyCode;
                                }
                                else if (app.Key == "IsDispatched")
                                {
                                    objTemp.IsDispatched = (bool)app.Value;
                                }


                            }
                            objDispatchList.Add(objTemp);
                        }

                        objResponse = objTransacManager.SaveDispatchOrderdetails(objDispatchList);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RejectFranchiseOrder(string OrderNo, string RejectReason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objTransacManager.RejectFranchiseOrder(OrderNo, RejectReason, (Session["LoginUser"] as User).UserId);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOrderDetails(string OrderBy, string OrderTo, string Status)
        {
            string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
            List<PartyOrderModel> objOrderList = objTransacManager.GetOrderList(OrderBy,OrderTo,Status);
            return Json(objOrderList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetOrderProductDetails(string OrderNo, string OrderBy)
        {
            List<ProductModel> objOrderList = objTransacManager.GetOrderProductList(OrderNo, OrderBy);
            return Json(objOrderList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveDispatchOrder(PartyOrderModel objPartyOrderModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objPartyOrderModel != null)
            {
                objPartyOrderModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objPartyOrderModel.objProductListStr))
                {
                    var objects = JArray.Parse(objPartyOrderModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            // var appName = app.Key;
                            //    var ProductGrid = [{"AvailStock":"", "SNo": "", "Code": "", "ProductName": "", "MRP": "", "DP": "", "Rate": "","BatchNo":"", "Barcode": "", "RP": "", "BV": "", "CV": "", "PV": "", "Qty": "", "RPValue": "", "BVValue": "", "CVValue": "", "PVValue": "", "CommsnPer": "", "CommsnAmt": "", "DiscPer": "", "DiscAmt": "", "Amount": "", "TaxType": "", "TaxPer": "", "TaxAmt": "", "TotalAmount": ""}];
                            if (app.Key == "Code")
                            {
                                objTemp.ProdCode = (int)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "CVValue")
                            {
                                objTemp.CVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.PVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "BVValue")
                            {
                                objTemp.BVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscPer")
                            {
                                objTemp.DiscPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmt")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxPer")
                            {
                                objTemp.TaxPer = (decimal)app.Value;
                            }
                            else if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "TotalAmount")
                            {
                                objTemp.TotalAmount = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxType")
                            {
                                objTemp.TaxType = (string)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;

                            }
                            else if (app.Key == "AvailStock")
                            {
                                objTemp.StockAvailable = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnPer")
                            {
                                objTemp.CommissionPer = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnAmt")
                            {
                                objTemp.CommissionAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "RPValue")
                            {
                                objTemp.RPValue = (decimal)app.Value;
                            }
                            else if (app.Key == "OfferUID")
                            {
                                objTemp.OfferUID = (decimal)app.Value;
                            }
                            else if (app.Key == "ProductType")
                            {
                                objTemp.ProductType = app.Value.ToString();
                            }
                        }
                        objPartyOrderModel.objListProduct.Add(objTemp);
                    }
                    objPartyOrderModel.LoginUser = Session["LoginUser"] as User;
                    // Retrive the Name of HOST
                    string hostName = Dns.GetHostName();
                    // Get the IP  
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                    objPartyOrderModel.objProduct.UID = myIP + currentDate;
                    objResponse = objTransacManager.SaveDispatchOrder(objPartyOrderModel);
                    //if (objResponse.ResponseStatus == "OK")
                    //{
                    //    InventorySession.StoredDistributorValues = objResponse.ResponseDetailsToPrint;
                    //}
                }

            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult DeleteBills()
        {
            return View();
        }


        public ActionResult DeleteBill(string BillNo, decimal FsessId, string Reason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            List<SalesReport> objBillList = new List<SalesReport>();
            try
            {
                decimal UserId = (Session["LoginUser"] as User).UserId;
                objResponse = objTransacManager.DeleteBills(BillNo, FsessId, UserId, Reason);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult SalesReturn()
        {
            try
            {
                if (Session["LoginUser"] != null)
                {
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                    string returnNo = objTransacManager.GetSalesReturnNumber(LoginPartyCode);
                    ViewBag.returnNo = returnNo;
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        public ActionResult GetListOfPartyBills(string party, string partyType)
        {
            List<PartyBill> objResponse = new List<PartyBill>();
            try
            {
                objResponse = objTransacManager.GetBillList(partyType, party);
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetListOfSupplierBills(string supplier)
        {
            List<PartyBill> objResponse = new List<PartyBill>();
            try
            {
                objResponse = objTransacManager.GetListOfSupplierBills(supplier);
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetListOfBillProducts(string BillNo)
        {
            DistributorBillModel objResponse = new DistributorBillModel();
            try
            {
                objResponse = objTransacManager.getInvoice(BillNo, "");
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse.objListProduct, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetBillDetail(string BillNo)
        {
            PartyBill objResponse = new PartyBill();
            try
            {
                objResponse = objTransacManager.GetBillDetail(BillNo);
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetPurchaseDetail(string BillNo)
        //{
        //    PurchaseBill objResponse = new PurchaseBill();
        //    try
        //    {
        //        objResponse = objTransacManager.GetPurchaseDetail(BillNo);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return Json(objResponse, JsonRequestBehavior.AllowGet);
        //}



        [HttpPost]
        public ActionResult SaveReturnOrder(SalesReturnModel objPartyOrderModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objPartyOrderModel != null)
            {
                objPartyOrderModel.ProductList = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objPartyOrderModel.objProductListStr))
                {
                    var objects = JArray.Parse(objPartyOrderModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            if (app.Key == "ProductCode")
                            {
                                objTemp.ProductCodeStr = (string)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "CVValue")
                            {
                                objTemp.CVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.PVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "BVValue")
                            {
                                objTemp.BVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscPer")
                            {
                                objTemp.DiscPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmount")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "GST")
                            {
                                objTemp.GSTPer = (decimal)app.Value;
                            }
                            else if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "TotalAmount")
                            {
                                objTemp.TotalAmount = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "CommissionPer")
                            {
                                objTemp.CommissionPer = (decimal)app.Value;
                            }
                            else if (app.Key == "CommissionAmt")
                            {
                                objTemp.CommissionAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "RPValue")
                            {
                                objTemp.RPValue = (decimal)app.Value;
                            }
                            else if (app.Key == "ReturnQty")
                            {
                                objTemp.ReturnQty = (decimal)app.Value;
                            }

                        }
                        objPartyOrderModel.ProductList.Add(objTemp);
                    }
                    objPartyOrderModel.LoggedInUserId = (Session["LoginUser"] as User).UserId;
                    objPartyOrderModel.EntryBy = (Session["LoginUser"] as User).PartyCode;
                    // Retrive the Name of HOST
                    string hostName = Dns.GetHostName();
                    // Get the IP  
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                    objPartyOrderModel.LoggedInUserIP = myIP;
                    objResponse = objTransacManager.SaveOrderReturn(objPartyOrderModel);

                }

            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public ActionResult SavePurchaseReturnOrder(PurchaseReturnModel objPartyOrderModel)
        //{
        //    ResponseDetail objResponse = new ResponseDetail();
        //    if (objPartyOrderModel != null)
        //    {
        //        objPartyOrderModel.ProductList = new List<ProductModel>();
        //        if (!string.IsNullOrEmpty(objPartyOrderModel.objProductListStr))
        //        {
        //            var objects = JArray.Parse(objPartyOrderModel.objProductListStr); // parse as array  
        //            foreach (JObject root in objects)
        //            {
        //                ProductModel objTemp = new ProductModel();
        //                foreach (KeyValuePair<String, JToken> app in root)
        //                {
        //                    if (app.Key == "ProductCode")
        //                    {
        //                        objTemp.ProductCodeStr = (string)app.Value;
        //                    }
        //                    else if (app.Key == "ProductName")
        //                    {
        //                        objTemp.ProductName = (string)app.Value;
        //                    }
        //                    else if (app.Key == "Rate")
        //                    {
        //                        objTemp.Rate = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "Barcode")
        //                    {
        //                        objTemp.Barcode = app.Value.ToString();
        //                    }
        //                    else if (app.Key == "BatchNo")
        //                    {
        //                        objTemp.BatchNo = app.Value.ToString();
        //                    }
        //                    else if (app.Key == "MRP")
        //                    {
        //                        objTemp.MRP = (decimal?)app.Value;
        //                    }
        //                    else if (app.Key == "Qty")
        //                    {
        //                        objTemp.Quantity = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "PV")
        //                    {
        //                        objTemp.PV = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "CV")
        //                    {
        //                        objTemp.CV = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "BV")
        //                    {
        //                        objTemp.BV = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "RP")
        //                    {
        //                        objTemp.RP = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "DP")
        //                    {
        //                        objTemp.DP = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "CVValue")
        //                    {
        //                        objTemp.CVValue = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "PVValue")
        //                    {
        //                        objTemp.PVValue = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "BVValue")
        //                    {
        //                        objTemp.BVValue = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "DiscPer")
        //                    {
        //                        objTemp.DiscPer = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "DiscAmt")
        //                    {
        //                        objTemp.DiscAmt = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "TaxAmount")
        //                    {
        //                        objTemp.TaxAmt = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "GST")
        //                    {
        //                        objTemp.GSTPer = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "Amount")
        //                    {
        //                        objTemp.Amount = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "TotalAmount")
        //                    {
        //                        objTemp.TotalAmount = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "PVValue")
        //                    {
        //                        objTemp.DiscAmt = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "CommissionPer")
        //                    {
        //                        objTemp.CommissionPer = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "CommissionAmt")
        //                    {
        //                        objTemp.CommissionAmt = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "RPValue")
        //                    {
        //                        objTemp.RPValue = (decimal)app.Value;
        //                    }
        //                    else if (app.Key == "ReturnQty")
        //                    {
        //                        objTemp.ReturnQty = (decimal)app.Value;
        //                    }

        //                }
        //                objPartyOrderModel.ProductList.Add(objTemp);
        //            }
        //            objPartyOrderModel.LoggedInUserId = (Session["LoginUser"] as User).UserId;
        //            objPartyOrderModel.EntryBy = (Session["LoginUser"] as User).PartyCode;
        //            // Retrive the Name of HOST
        //            string hostName = Dns.GetHostName();
        //            // Get the IP  
        //            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
        //            string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
        //            objPartyOrderModel.LoggedInUserIP = myIP;
        //            objResponse = objTransacManager.SavePurchaseReturnOrder(objPartyOrderModel);
        //        }
        //    }
        //    else
        //    {
        //        objResponse.ResponseMessage = "Something went wrong!";
        //        objResponse.ResponseStatus = "FAILED";
        //    }

        //    return Json(objResponse, JsonRequestBehavior.AllowGet);
        //}
        [SessionExpire]
        public ActionResult DeletePurchaseInvoice()
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
        public ActionResult DeletePurchaseInvoices(string InwardNo, decimal FsessId, string Reason)
        {
            ResponseDetail objResponse = new ResponseDetail();
            List<SalesReport> objBillList = new List<SalesReport>();
            try
            {
                decimal UserId = (Session["LoginUser"] as User).UserId;
                objResponse = objTransacManager.DeletePurchaseInvoice(InwardNo, FsessId, UserId, Reason);
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult PartyTargetMaster()
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

        [HttpPost]
        public ActionResult SavePartyTargetDetails(PartyTargetMaster objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (Session["LoginUser"] != null)
                {
                    int userid = (Session["LoginUser"] as User).UserId;
                    objModel.UserID = userid;
                    objResponse = objTransacManager.SavePartyTargetDetails(objModel);
                }
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult UpdateDeliveryDetail()
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
        [SessionExpire]
        public ActionResult OldBills()
        {
           return View();
        }

        public ActionResult GetOldBills(string FromDate, string ToDate,string IdNo, string OrderNo )
        {
            string CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;
            string IsAdmin = (Session["LoginUser"] as User).IsAdmin;
            string PartyCode = "ALL";
            if (CurrentPartyCode != System.Web.Configuration.WebConfigurationManager.AppSettings["WRPartyCode"])
            PartyCode = CurrentPartyCode;
            List<OldBills> objDispatchOrderList = new List<OldBills>();
            objDispatchOrderList = objTransacManager.GetOldBills(FromDate, ToDate,  IdNo, OrderNo,PartyCode);
            return Json(objDispatchOrderList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBillProducts(string billNo)
        {
          
            List<ProductModel> objDispatchOrderList = new List<ProductModel>();
            objDispatchOrderList = objTransacManager.GetBillProducts(billNo);
            return Json(objDispatchOrderList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRecordToUpdateDelDetails(string FromDate, string ToDate, string PartyCode, string Fcode, string status)
        {
            List<SalesReport> objResponse = objTransacManager.GetRecordToUpdateDelDetails(FromDate, ToDate, PartyCode, Fcode, status);
            var jsonProduct = Json(objResponse, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }
        public ActionResult UpdateDeliveryDetails(UpdateDeliveryDetails obj)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (Session["LoginUser"] != null)
                {
                    obj.LoggedInUser = (Session["LoginUser"] as User).UserId;
                }
                obj.DeliverDetailList = new List<SalesReport>();
                if (!string.IsNullOrEmpty(obj.ListObjHidden))
                {
                    var objects = JArray.Parse(obj.ListObjHidden); // parse as array  
                    foreach (JObject root in objects)
                    {
                        SalesReport objTemp = new SalesReport();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {

                            if (app.Key == "BillNo")
                            {
                                objTemp.BillNo = (string)app.Value;
                            }
                            else if (app.Key == "BillDate")
                            {
                                objTemp.BillDate = (app.Value.ToString() != "" && app.Value != null && app.Value.Type != JTokenType.Null) ? Convert.ToDateTime(DateTime.ParseExact(app.Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) : (DateTime?)null;
                            }
                            else if (app.Key == "SoldBy")
                            {
                                objTemp.SoldBy = (string)app.Value;
                            }
                            else if (app.Key == "PartyName")
                            {
                                objTemp.PartyName = app.Value.ToString();
                            }
                            else if (app.Key == "PartyCode")
                            {
                                objTemp.PartyCode = app.Value.ToString();
                            }
                            else if (app.Key == "Name")
                            {
                                objTemp.Name = (string)app.Value;
                            }
                            else if (app.Key == "CourierName")
                            {
                                objTemp.CourierName = (string)app.Value;
                            }
                            else if (app.Key == "DocWeight")
                            {
                                objTemp.DocWeight = (string)app.Value;
                            }
                            else if (app.Key == "DocketNo")
                            {
                                objTemp.DocketNo = (string)app.Value;
                            }
                            else if (app.Key == "DocketDate")
                            {
                                objTemp.DocketDate = (app.Value.ToString() != "" && app.Value != null && app.Value.Type != JTokenType.Null) ? Convert.ToDateTime(DateTime.ParseExact(app.Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) : (DateTime?)null;
                            }
                            else if (app.Key == "DOD")
                            {
                                objTemp.DOD = (app.Value.ToString() != "" && app.Value != null && app.Value.Type != JTokenType.Null) ? Convert.ToDateTime(DateTime.ParseExact(app.Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) : (DateTime?)null;
                            }
                            else if (app.Key == "DelvAddress")
                            {
                                objTemp.DelvAddress = (string)app.Value;
                            }
                            else if (app.Key == "CID")
                            {
                                objTemp.CID = (string)app.Value;
                            }
                            else if (app.Key == "DispDate")
                            {
                                objTemp.DispDate = (app.Value.ToString() != "" && app.Value != null && app.Value.Type != JTokenType.Null) ? Convert.ToDateTime(DateTime.ParseExact(app.Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture)) : (DateTime?)null;
                            }

                            else if (app.Key == "NetPayable")
                            {
                                objTemp.NetPayable = (string)app.Value;
                            }
                            else if (app.Key == "MobileNO")
                            {
                                objTemp.MobileNO = (string)app.Value;
                            }
                            else if (app.Key == "OrderNo")
                            {
                                objTemp.OrderNo = (string)app.Value;
                            }
                        }
                        obj.DeliverDetailList.Add(objTemp);
                    }
                }

                objResponse = objTransacManager.UpdateDeliveryDetails(obj);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult UpgradeID()
        {
 return View("Upgrade_ID_With_Package");
        }

        [HttpPost]
        public ActionResult ActivateIdWithPackage(UpgradeID objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objModel.productstring))
                {
                    var objects = JArray.Parse(objModel.productstring); // parse as array  
                    decimal totalBV = 0;
                    decimal totalRP = 0;
                    decimal totalPV = 0;
                    decimal totalCV = 0;
                    decimal TotalNetPayable = 0;
                    decimal Totaltax = 0;
                    decimal TotalDisc = 0;
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {

                            if (app.Key == "ProductId")
                            {
                                objTemp.ProductCodeStr = (string)app.Value;
                            }
                            if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }

                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "ProductTye")
                            {
                                objTemp.ProductTye = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Quantity")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmt")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxPer")
                            {
                                objTemp.TaxPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }


                        }
                        objTemp.BVValue = objTemp.BV * objTemp.Quantity;
                        objTemp.RPValue = objTemp.RP * objTemp.Quantity;
                        objTemp.CVValue = objTemp.CV * objTemp.Quantity;
                        objTemp.PVValue = objTemp.PV * objTemp.Quantity;

                        totalBV += objTemp.BVValue ?? 0;
                        totalRP += objTemp.RPValue ?? 0;
                        totalCV += objTemp.CVValue ?? 0;
                        totalPV += objTemp.PVValue ?? 0;
                        Totaltax += objTemp.TaxAmt ?? 0;

                        TotalDisc += objTemp.DiscAmt ?? 0;
                        TotalNetPayable += objTemp.Amount;
                        objModel.objListProduct.Add(objTemp);
                    }
                    objModel.objProduct = new ProductModel();
                    objModel.objCustomer = new CustomerDetail();
                    objModel.objCustomer.UserDetails = Session["LoginUser"] as User;
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                    objModel.objProduct.UID = myIP + currentDate;
                    objModel.objProduct.TotalDiscount = TotalDisc;// TotalNetPayable + Totaltax - objModel.KitAmount;
                    objModel.objProduct.TotalBV = totalBV;
                    objModel.objProduct.TotalRP = totalRP;
                    objModel.objProduct.TotalCV = totalCV;
                    objModel.objProduct.TotalPV = totalPV;
                    objModel.objProduct.TotalAmount = TotalNetPayable;
                    objModel.objProduct.TotalTaxAmount = Totaltax;
                    objModel.objProduct.TotalNetPayable = objModel.KitAmount;
                    objResponse = objTransacManager.ActivateIdWithPackage(objModel);
                }

            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerKitDetail(string IdNo)
        {
            UpgradeID model = new UpgradeID();
            string CurrentPartyCode = "";
            if (Session["LoginUser"] != null)
                CurrentPartyCode = (Session["LoginUser"] as User).PartyCode;
            bool IsHO = false;
            if (CurrentPartyCode == System.Web.Configuration.WebConfigurationManager.AppSettings["WRPartyCode"])
                IsHO = true;

            model = objTransacManager.GetCustomerKitDetail(IdNo);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetKitProductList(string kitId)
        {
            UpgradeID model = new UpgradeID();
            model = objTransacManager.GetKitProductList(kitId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload()
        {
            UpdateDeliveryDetails objResponse = new UpdateDeliveryDetails();
            objResponse.ErrorMessage = "";
            try
            {
                HttpPostedFileBase upload = null;
                string filename = string.Empty;
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        filename = Path.GetFileName(Request.Files[i].FileName);
                        upload = files[i];
                    }
                    if (upload != null && upload.ContentLength > 0)
                    {
                        try
                        {
                            DataTable dt = GetDataTableFromSpreadsheet(upload, filename, false);
                            objResponse.DeliverDetailList = SaveExcelReportData(dt);
                            objResponse.ErrorMessage = "OK";
                        }
                        catch (Exception e)
                        {
                            objResponse.ErrorMessage = "error--" + e.Message + "--Inner Exception--" + e.InnerException;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                objResponse.ErrorMessage = "error--" + e.Message + "--Inner Exception--" + e.InnerException;
            }

            var jsonProduct = Json(objResponse, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        private static ISheet GetFileStream(HttpPostedFileBase MyExcelStream, string filename1, bool ReadOnly)
        {

            HttpPostedFileBase files = MyExcelStream; //Read the Posted Excel File  
            ISheet sheet; //Create the ISheet object to read the sheet cell values  
            string filename = Path.GetFileName(files.FileName); //get the uploaded file name  
            var fileExt = Path.GetExtension(filename); //get the extension of uploaded excel file  
            if (fileExt == ".xls")
            {
                HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream); //HSSWorkBook object will read the Excel 97-2000 formats  
                sheet = hssfwb.GetSheetAt(0); //get first Excel sheet from workbook  
            }
            else
            {
                XSSFWorkbook hssfwb = new XSSFWorkbook(files.InputStream); //XSSFWorkBook will read 2007 Excel format  
                sheet = hssfwb.GetSheetAt(0); //get first Excel sheet from workbook   
            }

            return sheet;
        }


        public static DataTable GetDataTableFromSpreadsheet(HttpPostedFileBase MyExcelStream, string filename, bool ReadOnly)
        {

            try
            {
                var sh = GetFileStream(MyExcelStream, filename, ReadOnly);
                var dtExcelTable = new DataTable();
                dtExcelTable.Rows.Clear();
                dtExcelTable.Columns.Clear();
                var headerRow = sh.GetRow(0);
                int colCount = headerRow.LastCellNum;
                for (var c = 0; c < colCount; c++)
                    dtExcelTable.Columns.Add(headerRow.GetCell(c).ToString());
                var i = 1;
                var currentRow = sh.GetRow(i);
                while (currentRow != null)
                {
                    var dr = dtExcelTable.NewRow();
                    for (var j = 0; j < currentRow.Cells.Count; j++)
                    {
                        var cell = currentRow.GetCell(j);

                        if (cell != null)
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        DateTime date = cell.DateCellValue;
                                        dr[j] = date.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        dr[j] = cell.NumericCellValue.ToString();
                                    }
                                    break;
                                case CellType.String:
                                    dr[j] = cell.StringCellValue;
                                    break;
                                case CellType.Blank:
                                    dr[j] = string.Empty;
                                    break;
                            }
                    }
                    dtExcelTable.Rows.Add(dr);
                    i++;
                    currentRow = sh.GetRow(i);
                }
                return dtExcelTable;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Save qci report data
        /// </summary>
        /// <param name="dt"></param>
        public List<SalesReport> SaveExcelReportData(DataTable dt)
        {
            var SalesReportList = new List<SalesReport>();
            try
            {
                if (dt == null)
                {
                    return null;
                }

                var userDetail = new SalesReport();
                DataColumnCollection columns = dt.Columns;

                foreach (DataRow row in dt.Rows)
                {

                    userDetail = new SalesReport();
                    if (columns.Contains("Bill No"))
                    {
                        userDetail.BillNo = Convert.ToString(row["Bill No"]);
                    }
                    if (columns.Contains("Party Name"))
                    {
                        userDetail.PartyName = Convert.ToString(row["Party Name"]);
                    }
                    if (columns.Contains("ID No"))
                    {
                        userDetail.PartyCode = Convert.ToString(row["ID No"]);
                    }
                    if (columns.Contains("Bill By"))
                    {
                        userDetail.SoldBy = Convert.ToString(row["Bill By"]);
                    }
                    if (columns.Contains("Name"))
                    {
                        userDetail.Name = Convert.ToString(row["Name"]);
                    }
                    if (columns.Contains("Courier"))
                    {
                        userDetail.CourierName = Convert.ToString(row["Courier"]);
                    }
                    if (columns.Contains("Weight"))
                    {
                        userDetail.DocWeight = Convert.ToString(row["Weight"]);
                    }
                    if (columns.Contains("Docket No"))
                    {
                        userDetail.DocketNo = Convert.ToString(row["Docket No"]);
                    }
                    if (columns.Contains("Delv Address"))
                    {
                        userDetail.DelvAddress = Convert.ToString(row["Delv Address"]);
                    }
                    if (columns.Contains("CID"))
                    {
                        userDetail.CID = Convert.ToString(row["CID"]);
                    }
                    if (columns.Contains("Net Pay"))
                    {
                        userDetail.NetPayable = Convert.ToString(row["Net Pay"]);
                    }
                    if (columns.Contains("Mobile No"))
                    {
                        userDetail.MobileNO = Convert.ToString(row["Mobile No"]);
                    }
                    if (columns.Contains("Order No"))
                    {
                        userDetail.OrderNo = Convert.ToString(row["Order No"]);
                    }

                    try
                    {
                        if (columns.Contains("Disp Date"))
                        {
                            var date = Convert.ToString(row["Disp Date"]);
                            if (!String.IsNullOrEmpty(date) && !date.StartsWith("#"))
                            {
                                userDetail.DispDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (columns.Contains("Bill Date"))
                        {
                            var date = Convert.ToString(row["Bill Date"]);
                            if (!String.IsNullOrEmpty(date) && !date.StartsWith("#"))
                            {
                                userDetail.BillDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (columns.Contains("Docket Date"))
                        {
                            var date = Convert.ToString(row["Docket Date"]);
                            if (!String.IsNullOrEmpty(date) && !date.StartsWith("#"))
                            {
                                userDetail.DocketDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        if (columns.Contains("DOD"))
                        {
                            var date = Convert.ToString(row["DOD"]);
                            if (!String.IsNullOrEmpty(date) && !date.StartsWith("#"))
                            {
                                userDetail.DOD = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    SalesReportList.Add(userDetail);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return SalesReportList;
        }

        [HttpPost]
        public ActionResult CheckForOffer(DistributorBillModel objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (objModel != null)
            {
                objModel.objListProduct = new List<ProductModel>();
                if (!string.IsNullOrEmpty(objModel.objProductListStr))
                {
                    var objects = JArray.Parse(objModel.objProductListStr); // parse as array  
                    foreach (JObject root in objects)
                    {
                        ProductModel objTemp = new ProductModel();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            if (app.Key == "Code")
                            {
                                objTemp.ProdCode = (int)app.Value;
                            }
                            else if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            else if (app.Key == "Rate")
                            {
                                objTemp.Rate = (decimal)app.Value;
                            }
                            else if (app.Key == "Barcode")
                            {
                                objTemp.Barcode = app.Value.ToString();
                            }
                            else if (app.Key == "BatchNo")
                            {
                                objTemp.BatchNo = app.Value.ToString();
                            }
                            else if (app.Key == "MRP")
                            {
                                objTemp.MRP = (decimal?)app.Value;
                            }
                            else if (app.Key == "Qty")
                            {
                                objTemp.Quantity = (decimal)app.Value;
                            }
                            else if (app.Key == "PV")
                            {
                                objTemp.PV = (decimal)app.Value;
                            }
                            else if (app.Key == "CV")
                            {
                                objTemp.CV = (decimal)app.Value;
                            }
                            else if (app.Key == "BV")
                            {
                                objTemp.BV = (decimal)app.Value;
                            }
                            else if (app.Key == "RP")
                            {
                                objTemp.RP = (decimal)app.Value;
                            }
                            else if (app.Key == "DP")
                            {
                                objTemp.DP = (decimal)app.Value;
                            }
                            else if (app.Key == "CVValue")
                            {
                                objTemp.CVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.PVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "BVValue")
                            {
                                objTemp.BVValue = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscPer")
                            {
                                objTemp.DiscPer = (decimal)app.Value;
                            }
                            else if (app.Key == "DiscAmt")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxAmt")
                            {
                                objTemp.TaxAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxPer")
                            {
                                objTemp.TaxPer = (decimal)app.Value;
                            }
                            else if (app.Key == "Amount")
                            {
                                objTemp.Amount = (decimal)app.Value;
                            }
                            else if (app.Key == "TotalAmount")
                            {
                                objTemp.TotalAmount = (decimal)app.Value;
                            }
                            else if (app.Key == "TaxType")
                            {
                                objTemp.TaxType = (string)app.Value;
                            }
                            else if (app.Key == "PVValue")
                            {
                                objTemp.DiscAmt = (decimal)app.Value;

                            }
                            else if (app.Key == "AvailStock")
                            {
                                objTemp.StockAvailable = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnPer")
                            {
                                objTemp.CommissionPer = (decimal)app.Value;
                            }
                            else if (app.Key == "CommsnAmt")
                            {
                                objTemp.CommissionAmt = (decimal)app.Value;
                            }
                            else if (app.Key == "RPValue")
                            {
                                objTemp.RPValue = (decimal)app.Value;
                            }
                            else if (app.Key == "SubCatId")
                            {
                                objTemp.SubCatId = (decimal)app.Value;
                            }
                        }
                        objModel.objListProduct.Add(objTemp);
                    }
                    objModel.objCustomer.UserDetails = Session["LoginUser"] as User;
                    // Retrive the Name of HOST
                    string hostName = Dns.GetHostName();
                    // Get the IP  
                    string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    string currentDate = DateTime.Now.ToString("yyyyMMddHHmmssfff"); ;
                    objModel.objProduct.UID = myIP + currentDate;
                    objResponse = objTransacManager.CheckForOffer(objModel);
                }

            }
            else
            {
                objResponse.ResponseMessage = "Something went wrong!";
                objResponse.ResponseStatus = "FAILED";
            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PackUnpack()
        {
            PackUnpack objPackUnpack = new PackUnpack();
            try
            {
                if (Session["LoginUser"] != null)
                {
                    int LoginPartyId = (Session["LoginUser"] as User).UserId;
                    string LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
                    objPackUnpack.kitlist = objTransacManager.GetKitList();
                    objPackUnpack.UserId = LoginPartyId;
                    objPackUnpack.UserCode = LoginPartyCode;
                }
            }
            catch (Exception ex)
            {

            }
            return View(objPackUnpack);
        }

        public ActionResult GetPackUnpackProducts(string PackUnpack, decimal KitId, string prodID)
        {
            string LoginPartyCode = string.Empty;
            if (Session["LoginUser"] != null)
            {
                LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
            }
            List<PackUnpackProduct> objResponse = objTransacManager.GetPackUnpackProducts(PackUnpack, KitId, prodID, LoginPartyCode);
            var jsonProduct = Json(objResponse, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;

        }

        [HttpPost]
        public ActionResult SavePackUnpack(PackUnpack obj)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                obj.productList = new List<PackUnpackProduct>();
                if (!string.IsNullOrEmpty(obj.productstring))
                {
                    var objects = JArray.Parse(obj.productstring); // parse as array  
                    foreach (JObject root in objects)
                    {
                        PackUnpackProduct objTemp = new PackUnpackProduct();
                        foreach (KeyValuePair<String, JToken> app in root)
                        {
                            if (app.Key == "ProductId")
                            {
                                objTemp.ProductId = (string)app.Value;
                            }
                            if (app.Key == "ProductName")
                            {
                                objTemp.ProductName = (string)app.Value;
                            }
                            if (app.Key == "Qunatity")
                            {
                                objTemp.Qunatity = (string)app.Value;
                            }
                            if (app.Key == "AvailStock")
                            {
                                objTemp.AvailStock = (string)app.Value;
                            }
                        }
                        obj.productList.Add(objTemp);
                    }
                }
                objResponse = objTransacManager.SavePackUnpack(obj);
            }
            catch (Exception ex)
            {

            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

    }
}