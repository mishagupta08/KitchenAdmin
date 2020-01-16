using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: CategoryMaster
        ProductManager objProductManager = new ProductManager();
        // public ActionResult CategoryMaster()
        // {
        //     CategoryDetails objCategoryModel = new CategoryDetails();
        //     objCategoryModel.IsAdd = "Add";
        //     objCategoryModel.IsActive = true;
        //     return View(objCategoryModel);
        // }

        // // POST: SaveCategoryMaster
        // [HttpPost]
        // public ActionResult SaveCategoryMaster(CategoryDetails model)
        // {
        //     ResponseDetail objResponse = new ResponseDetail();
        //     if (model != null)
        //     {
        //         if (InventorySession.LoginUser != null)
        //         {
        //             model.UserDetails = InventorySession.LoginUser;
        //         }
        //         objResponse = objProductManager.AddCategoryDetails(model);
        //     }
        //     return Json(objResponse,JsonRequestBehavior.AllowGet);
        // }

        // // GET: SubCategoryMaster
        // public ActionResult SubCategoryMaster()
        // {
        //     List<SelectListItem> objCategoryList = new List<SelectListItem>();
        //     List<SelectListItem> objChildCategoryList = new List<SelectListItem>();
        //     var result = objProductManager.GetCategoryList("Y");
        //     SubCategoryDetails model = new SubCategoryDetails();
        //     bool f = true;
        //     foreach(var item in result)
        //     {
        //         SelectListItem tempobj = new SelectListItem();
        //         //SelectListItem tempobj1 = new SelectListItem();
        //         tempobj.Text = item.CategoryName;
        //         tempobj.Value = item.CategoryId.ToString();
        //         if (f == true)
        //         {
        //             f = false;
        //             model.CategoryId = int.Parse(item.CategoryId.ToString());
        //             //model.SubCatId = int.Parse(item.ToString());
        //         }

        //         objCategoryList.Add(tempobj);
        //     }

        //     ViewBag.ListCategory = objCategoryList;
        //    // ViewBag.ListChildCategory = objCategoryList;
        //     model.IsAdd = "Add";
        //     return View(model);
        // }

        // // POST: SaveSubCategoryMaster
        // [HttpPost]
        // public ActionResult SaveSubCategoryMaster(SubCategoryDetails model)
        // {
        //     ResponseDetail objResponse = new ResponseDetail();

        //     if (model != null)
        //     {
        //         if (InventorySession.LoginUser != null)
        //         {
        //             model.UserDetails = InventorySession.LoginUser;
        //         }
        //         objResponse = objProductManager.AddSubCategoryDetails(model);
        //     }
        //     return Json(objResponse, JsonRequestBehavior.AllowGet);
        // }

        // [HttpPost] 
        // public ActionResult GetFullSubCategoryList()
        // {
        //     List<SubCategoryDetails> objList = objProductManager.GetSubcategoryDetails(0,"");
        //     return Json(objList, JsonRequestBehavior.AllowGet);
        // }

        // [HttpPost]
        // public ActionResult GetFullCategoryList()
        // {
        //     List<CategoryDetails> objList = objProductManager.GetCategoryList("");
        //     return Json(objList, JsonRequestBehavior.AllowGet);
        // }

        // // GET: ProductMaster
        // public ActionResult ProductMaster()
        // {
        //     List<CategoryDetails> objCategoryDetails = objProductManager.GetCategoryList("Y");

        //     List<SelectListItem> ListCategoryDetails = new List<SelectListItem>();
        //     List<SelectListItem> ListSubCategoryDetails = new List<SelectListItem>();
        //     ProductDetails model = new ProductDetails();
        //     bool f = true;
        //     foreach (var obj in objCategoryDetails)
        //     {
        //         SelectListItem objTemp = new SelectListItem();
        //         objTemp.Text = obj.CategoryName;
        //         objTemp.Value = obj.CategoryId.ToString();
        //         if (f)
        //         {
        //             objTemp.Selected = true;
        //             model.CategoryId = obj.CategoryId;
        //             f = false;
        //         }
        //         ListCategoryDetails.Add(objTemp);
        //     }
        //     f = true;
        //     List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(model.CategoryId,"Y");
        //     foreach (var obj in objSubCategoryDetails)
        //     {
        //         SelectListItem objTemp = new SelectListItem();
        //         objTemp.Text = obj.subCategoryName;
        //         objTemp.Value = obj.SubCatId.ToString();
        //         if (f)
        //         {
        //             objTemp.Selected = true;
        //             model.SubCatgeoryId = (int)obj.SubCatId;
        //             f = false;
        //         }
        //         ListSubCategoryDetails.Add(objTemp);
        //     }
        //     ViewBag.ListCategory = ListCategoryDetails;
        //     ViewBag.ListSubCategory = ListSubCategoryDetails;
        //     List<SelectListItem> objBarcodeType = new List<SelectListItem>();
        //     objBarcodeType.Add(new SelectListItem { Text="System Generated",Value= "System Generated" });
        //     objBarcodeType.Add(new SelectListItem { Text = "Other", Value = "Other" });
        //     ViewBag.ListBarcodetype = objBarcodeType;
        //     model.ProductBarcodeDetails = new BarcodeDetails();
        //     model.ProductBarcodeDetails.BarcodeType = "System Generated";
        //     model.ProductBarcodeDetails.Barcode = objProductManager.MaxBarCode().ToString();
        //     model.ProductCode = objProductManager.MaxProductCode();
        //     return View(model);
        // }
        // // POST: SaveProductMaster
        // [HttpPost]
        // public ActionResult SaveProductMaster(ProductDetails model,HttpPostedFileBase upload)
        // {
        //     ResponseDetail objResponse = new ResponseDetail();
        //     try
        //     {
        //         if (upload != null)
        //         {
        //             var path = Server.MapPath("~/ProductImages");
        //             var paths = path.Split(':');
        //             if (paths != null || paths.Count() > 0)
        //             {
        //                 path = paths[0] + ":\\" + "ProductImages";
        //             }
        //             if (upload != null && upload.FileName != null)
        //             {
        //                 if (!Directory.Exists(path))
        //                 {
        //                     //DirectoryInfo dInfo = new DirectoryInfo(path);
        //                     //DirectorySecurity dSecurity = dInfo.GetAccessControl();
        //                     //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
        //                     //dInfo.SetAccessControl(dSecurity);

        //                     Directory.CreateDirectory(path);


        //                 }

        //                 //string myfile = Guid.NewGuid() + "-" + BannerImage.FileName;
        //                 string myfile = Guid.NewGuid() + "-" + Path.GetFileName(upload.FileName);


        //                 var productImage = Path.Combine(path, myfile);
        //                 upload.SaveAs(productImage);
        //                 //model.BannerImage = "/BannerImage/" + myfile;
        //                 model.ProductImagePath = productImage;
        //             }
        //         }
        //         if (InventorySession.LoginUser != null)
        //         {
        //             model.UserDetails = InventorySession.LoginUser;


        //         }
        //         objResponse = objProductManager.SaveProductMaster(model);
        //     }
        //     catch(Exception ex)
        //     {

        //     }

        //     return Json(objResponse, JsonRequestBehavior.AllowGet);
        // }

        // // GET: CheckDuplicateMaster
        // [HttpPost]
        // public ActionResult CheckDuplicateMaster(CheckDuplicateModel model)
        // {
        //     ResponseDetail objResponse = new ResponseDetail();
        //     objResponse = objProductManager.IsMasterExists(model);
        //     return Json(objResponse,JsonRequestBehavior.AllowGet);
        // }
        //[HttpPost]
        // public ActionResult GetActiveListOfSubCat(CheckDuplicateModel model)
        // {
        //     List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(0, "Y");
        //     return Json(objSubCategoryDetails, JsonRequestBehavior.AllowGet);
        // }
        public ActionResult CategoryMaster()
        {
            CategoryDetails objCategoryModel = new CategoryDetails();
            objCategoryModel.IsAdd = "Add";
            objCategoryModel.IsActive = true;
            return View(objCategoryModel);
        }

        // POST: SaveCategoryMaster
        [HttpPost]
        public ActionResult SaveCategoryMaster(CategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            if (model != null)
            {
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;
                }
                objResponse = objProductManager.AddCategoryDetails(model);
            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        // GET: SubCategoryMaster
        [SessionExpire]
        public ActionResult SubCategoryMaster()
        {
            List<SelectListItem> objCategoryList = new List<SelectListItem>();
            List<SelectListItem> objChildCategoryList = new List<SelectListItem>();
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
            // ViewBag.ListChildCategory = objCategoryList;
            model.IsAdd = "Add";
            model.IsActive = true;
            return View(model);
        }

        // POST: SaveSubCategoryMaster
        [HttpPost]
        public ActionResult SaveSubCategoryMaster(SubCategoryDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();

            if (model != null)
            {
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;
                }
                objResponse = objProductManager.AddSubCategoryDetails(model);
            }
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullSubCategoryList()
        {
            List<SubCategoryDetails> objList = objProductManager.GetSubcategoryDetails(0, "");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetFullCategoryList()
        {
            List<CategoryDetails> objList = objProductManager.GetCategoryList("");
            return Json(objList, JsonRequestBehavior.AllowGet);
        }

        // GET: ProductMaster
        [SessionExpire]
        public ActionResult ProductMaster(string ActionName, string ProductCode)
        {
            List<CategoryDetails> objCategoryDetails = objProductManager.GetCategoryList("Y");

            List<SelectListItem> ListCategoryDetails = new List<SelectListItem>();
            List<SelectListItem> ListSubCategoryDetails = new List<SelectListItem>();
            ProductDetails model = new ProductDetails();
            bool f = true;
            foreach (var obj in objCategoryDetails)
            {
                SelectListItem objTemp = new SelectListItem();
                objTemp.Text = obj.CategoryName;
                objTemp.Value = obj.CategoryId.ToString();
                if (f)
                {
                    objTemp.Selected = true;
                    model.CategoryId = obj.CategoryId;
                    f = false;
                }
                ListCategoryDetails.Add(objTemp);
            }
            f = true;
            List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(model.CategoryId, "Y");
            foreach (var obj in objSubCategoryDetails)
            {
                SelectListItem objTemp = new SelectListItem();
                objTemp.Text = obj.subCategoryName;
                objTemp.Value = obj.SubCatId.ToString();
                if (f)
                {
                    objTemp.Selected = true;
                    model.SubCatgeoryId = (int)obj.SubCatId;
                    f = false;
                }
                ListSubCategoryDetails.Add(objTemp);
            }
            ViewBag.ListCategory = ListCategoryDetails;
            ViewBag.ListSubCategory = ListSubCategoryDetails;
            List<SelectListItem> objBarcodeType = new List<SelectListItem>();
            objBarcodeType.Add(new SelectListItem { Text = "System Generated", Value = "System Generated" });
            objBarcodeType.Add(new SelectListItem { Text = "Other", Value = "Other" });
            ViewBag.ListBarcodetype = objBarcodeType;
            model.ProductBarcodeDetails = new BarcodeDetails();
            model.ProductBarcodeDetails.BarcodeType = "System Generated";
            model.ProductBarcodeDetails.Barcode = objProductManager.MaxBarCode().ToString();
            model.ProductBarcodeDetails.ExisitingBarcode = model.ProductBarcodeDetails.Barcode;
            model.ProductCode = objProductManager.MaxProductCode();
            model.ProductImagePath = "/images/DefaultProduct.jpg";
            if (!string.IsNullOrEmpty(ActionName))
            {
                if (ActionName == "Edit")
                {

                    if (!string.IsNullOrEmpty(ProductCode))
                    {
                        //if (!string.IsNullOrEmpty(Passedmodel.ProductImagePath))
                        //{
                        //    var test = Url.Action("GetImage", "Common", new { imageName = "-1" });
                        //    var productPic = test.Replace("-1", Passedmodel.ProductImagePath);
                        //    Passedmodel.ProductImagePath = productPic;
                        //}
                        //else
                        //{
                        //    Passedmodel.ProductImagePath = "/images/DefaultProduct.jpg";
                        //}
                        //model = Passedmodel;
                        decimal productId = decimal.Parse(ProductCode);
                        decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
                        model = objProductManager.GetProductDetail(productId, LoginStateCode);
                        if (model != null)
                        {
                            model.IsAdd = "Edit";
                            model.MinQty = string.IsNullOrEmpty(model.MinQtyStr) ? 0 : decimal.Parse(model.MinQtyStr);

                            if (!string.IsNullOrEmpty(model.ProductImagePath))
                            {
                                var test = Url.Action("GetImage", "Common", new { imageName = "-1" });
                                //var productPic = test.Replace("-1", model.ProductImagePath);
                                //model.ProductImagePath = productPic;
                            }
                            else
                            {
                                model.ProductImagePath = "/images/DefaultProduct.jpg";
                            }
                        }
                    }
                }
                else if (ActionName == "Delete")
                {
                    model.IsAdd = "Delete";
                }
                else
                {
                    model.IsAdd = "Add";
                }
            }
            return View(model);
        }
        // POST: SaveProductMaster
        [HttpPost]
        public ActionResult SaveProductMaster(ProductDetails model, HttpPostedFileBase upload)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (upload != null)
                {
                    var path = Server.MapPath("~/images/ProductImages");
                    //var paths = path.Split(':');
                    //if (paths != null || paths.Count() > 0)
                    //{
                    //    path = paths[0] + ":\\" + "ProductImages";
                    //}
                    if (upload != null && upload.FileName != null)
                    {
                        if (!Directory.Exists(path))
                        {
                            //DirectoryInfo dInfo = new DirectoryInfo(path);
                            //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            //dInfo.SetAccessControl(dSecurity);

                            Directory.CreateDirectory(path);
                        }

                        //string myfile = Guid.NewGuid() + "-" + BannerImage.FileName;
                        string myfile = Guid.NewGuid() + "-" + Path.GetFileName(upload.FileName);
                        myfile = myfile.Replace(" ", "").Replace(" ", "");

                        var productImage = Path.Combine(path, myfile);
                        upload.SaveAs(productImage);
                        //model.BannerImage = "/BannerImage/" + myfile;
                        model.ProductImagePath = myfile;
                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Add";
                objResponse = objProductManager.SaveProductMaster(model);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult ProductList()
        {

            return View();
        }

        //GET:ListProduct
        [HttpPost]
        public ActionResult GetProductList()
        {
            List<ProductDetails> objProductList = new List<ProductDetails>();
            decimal LoginStateCode = (Session["LoginUser"] as User).StateCode;
            objProductList = objProductManager.GetProductList(LoginStateCode);
            var jsonProduct = Json(objProductList, JsonRequestBehavior.AllowGet);
            jsonProduct.MaxJsonLength = int.MaxValue;
            return jsonProduct;
        }

        [HttpPost]
        public ActionResult EditProductMaster(ProductDetails model, HttpPostedFileBase upload)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                if (upload != null)
                {
                    var path = Server.MapPath("~/images/ProductImages");
                    //var paths = path.Split(':');
                    //if (paths != null || paths.Count() > 0)
                    //{
                    //    path = paths[0] + ":\\" + "ProductImages";
                    //}
                    if (upload != null && upload.FileName != null)
                    {
                        if (!Directory.Exists(path))
                        {
                            //DirectoryInfo dInfo = new DirectoryInfo(path);
                            //DirectorySecurity dSecurity = dInfo.GetAccessControl();
                            //dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                            //dInfo.SetAccessControl(dSecurity);

                            Directory.CreateDirectory(path);


                        }

                        //string myfile = Guid.NewGuid() + "-" + BannerImage.FileName;
                        string myfile = Guid.NewGuid() + "-" + Path.GetFileName(upload.FileName);
                        myfile = myfile.Replace(" ", "").Replace(" ", "");

                        var productImage = Path.Combine(path, myfile);
                        upload.SaveAs(productImage);
                        //model.BannerImage = "/BannerImage/" + myfile;
                        model.ProductImagePath = myfile;
                    }
                }
                if (Session["LoginUser"] != null)
                {
                    model.UserDetails = Session["LoginUser"] as User;


                }
                model.IsAdd = "Edit";
                objResponse = objProductManager.EditProductMaster(model);
            }
            catch (Exception ex)
            {

            }

            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DeleteProductMaster(ProductDetails model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            model.IsAdd = "Delete";
            objResponse = objProductManager.DeleteProductMaster(model);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        // GET: CheckDuplicateMaster
        [HttpPost]
        public ActionResult CheckDuplicateMaster(CheckDuplicateModel model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse = objProductManager.IsMasterExists(model);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetActiveListOfSubCat(CheckDuplicateModel model)
        {
            List<SubCategoryDetails> objSubCategoryDetails = objProductManager.GetSubcategoryDetails(0, "Y");
            return Json(objSubCategoryDetails, JsonRequestBehavior.AllowGet);
        }

        [SessionExpire]
        public ActionResult BarcodeMaster()
        {
            
            ProductDetails model = new ProductDetails();
            
            List<SelectListItem> objBarcodeType = new List<SelectListItem>();
            objBarcodeType.Add(new SelectListItem { Text = "System Generated", Value = "System Generated" });
            objBarcodeType.Add(new SelectListItem { Text = "Other", Value = "Other" });
            ViewBag.ListBarcodetype = objBarcodeType;
            model.ProductBarcodeDetails = new BarcodeDetails();
            model.ProductBarcodeDetails.BarcodeType = "System Generated";
            model.ProductBarcodeDetails.Barcode = objProductManager.MaxBarCode().ToString();
            model.ProductBarcodeDetails.ExisitingBarcode = model.ProductBarcodeDetails.Barcode;
                        
            return View(model);
        }

        public ActionResult SaveBarcode(ProductDetails Model)
        {
            ResponseDetail objResponse = new ResponseDetail();
            List<BarcodeDetails> barcodeList = new List<BarcodeDetails>();

            if (!string.IsNullOrEmpty(Model.ProductBarcodeDetails.objProductListStr))
            {
                var objects = JArray.Parse(Model.ProductBarcodeDetails.objProductListStr); // parse as array  
                foreach (JObject root in objects)
                {
                    BarcodeDetails objTemp = new BarcodeDetails();
                    foreach (KeyValuePair<String, JToken> app in root)
                    {
                        //ProductGrid.push({ "MDate": MDate, "EDate": EDate, "remark": remark, "Status": Status })             
                        if (app.Key == "productCode")
                        {
                            objTemp.ProductCode = (string)app.Value;
                        }
                        if (app.Key == "productName")
                        {
                            objTemp.ProductName = (string)app.Value;
                        }
                        if (app.Key == "pRate")
                        {
                            objTemp.PurchaseRate = (int)app.Value;
                        }
                        if (app.Key == "MRP")
                        {
                            objTemp.MRP = (int)app.Value;
                        }
                        if (app.Key == "dp")
                        {
                            objTemp.DP = (int)app.Value;
                        }
                        if (app.Key == "barcodetype")
                        {
                            objTemp.BarcodeType = (string)app.Value;
                        }
                        if (app.Key == "barcode")
                        {
                            objTemp.Barcode = (string)app.Value;
                        }
                        if (app.Key == "Isexpirable")
                        {
                            objTemp.IsExpirable = (string)app.Value;
                        }
                        if (app.Key == "Status")
                        {
                            objTemp.IsActive = (bool)app.Value;
                        }
                        if (app.Key == "remark")
                        {
                            objTemp.Remarks = (string)app.Value;
                        }
                        if (app.Key == "MDate")
                        {
                            objTemp.MfgDateStr = (string)app.Value;
                        }
                        if (app.Key == "EDate")
                        {
                            objTemp.ExpDateStr = (string)app.Value;
                        }
                    }
                    objTemp.UserId = (Session["LoginUser"] as User).UserId;
                    objTemp.UserName= (Session["LoginUser"] as User).UserName;
                    barcodeList.Add(objTemp);
                }                
            }
           
            objResponse = objProductManager.SaveBarcode(barcodeList);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
    }
}