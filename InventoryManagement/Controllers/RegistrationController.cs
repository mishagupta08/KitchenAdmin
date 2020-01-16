using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class RegistrationController : Controller
    {
        RegistrationManager objRegistrationManager = new RegistrationManager();
        TransactionManager objTransactManager = new TransactionManager();
        // GET: Registration
        [SessionExpire]
        public ActionResult PartyRegistration()
        {
           
            return View();
        }
        [SessionExpire]
        public ActionResult SupplierRegistration()
        {
            return View();
        }
        public ActionResult GetAllPartyList(bool IsSupplier)
        {
            List<PartyModel> objListModel = new List<PartyModel>();
            objListModel = objRegistrationManager.GetAllPartyList(IsSupplier);
            var jsonPartyList = Json(objListModel, JsonRequestBehavior.AllowGet);
            jsonPartyList.MaxJsonLength = int.MaxValue;
            return jsonPartyList;
        }

        [SessionExpire]
        public ActionResult AddEditPartyRegistration(string IsActionName,string PartyCode)
        {
            PartyModel objModel = new PartyModel();
            objModel.GroupList = objRegistrationManager.GetGroupList();
            objModel.IsSupplier = false;
            List<SelectListItem> GroupList = new List<SelectListItem>();
            foreach(var obj in objModel.GroupList)
            {
                if (obj.GroupId == 0)
                {
                    GroupList.Add(new SelectListItem
                {
                    
                    Text = obj.GroupName,
                    Value = obj.GroupId.ToString()
                        
                });
                }
            }
            ViewBag.GroupList = GroupList;

            //objModel.BankList = objTransactManager.GetBankList();
            //List<SelectListItem> BankList = new List<SelectListItem>();
            //foreach (var obj in objModel.BankList)
            //{
            //    BankList.Add(new SelectListItem
            //    {
            //        Text = obj.BankName,
            //        Value = obj.BankCode.ToString()
            //    });
            //}
            //ViewBag.BankList = BankList;

            //List<SelectListItem> objOnWebsite = new List<SelectListItem>();
            //objOnWebsite.Add(new SelectListItem
            //{
            //    Text="Yes",
            //    Value="Y"
            //});

            //objOnWebsite.Add(new SelectListItem
            //{
            //    Text = "No",
            //    Value = "N"
            //});
            //ViewBag.OnWebsite = objOnWebsite;
            List<SelectListItem> objActiveStatus = new List<SelectListItem>();
            objActiveStatus.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });

            objActiveStatus.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            ViewBag.ActiveStatus = objActiveStatus;

            objModel.StateList = objRegistrationManager.GetStateList();
            List<SelectListItem> StateList = new List<SelectListItem>();
            foreach (var obj in objModel.StateList)
            {
                if (obj.StateCode != 0)
                {
                    StateList.Add(new SelectListItem
                    {
                        Text = obj.StateName,
                        Value = obj.StateCode.ToString()
                    });
                }
            }
            ViewBag.StateList = StateList;
            objModel.StateCode = objModel.StateList.Where(r => r.IsCompanyState == true).Select(m => m.StateCode).FirstOrDefault();
            
            objModel.CityList = objRegistrationManager.GetCityList();
            List<SelectListItem> CityList = new List<SelectListItem>();
            foreach (var obj in objModel.CityList)
            {
                if (obj.StateCode == objModel.StateCode)
                {
                    if (obj.CityCode != 0)
                    {
                        CityList.Add(new SelectListItem
                        {
                            Text = obj.CityName,
                            Value = obj.CityCode.ToString()
                        });
                    }
                }
            }
            objModel.CityCode = objModel.CityList.Where(r => r.IsCompanyCity == true).Select(m => m.CityCode).FirstOrDefault();
            objModel.CityName = objModel.CityList.Where(r => r.IsCompanyCity == true).Select(m => m.CityName).FirstOrDefault();
            ViewBag.CityList = CityList;

            List<PartyModel> objParentPartyList = new List<PartyModel>();
            if (objModel.GroupList.Count > 0)
            {
                objParentPartyList = objRegistrationManager.GetParentParty(objModel.GroupList[0].GroupId);
            }
                List<SelectListItem> ParentParty = new List<SelectListItem>();
                foreach (var obj in objParentPartyList)
                {
                    ParentParty.Add(new SelectListItem
                    {
                        Text = obj.PartyName,
                        Value = obj.PartyCode.ToString()
                    });
                }
            
            ViewBag.ParentPartyList = ParentParty;
           
            
            if (IsActionName == "Add")
            {
                if (objParentPartyList.Count > 0 && objModel.GroupList.Count > 0) {
                    objModel.PartyCode = objRegistrationManager.GetPartyCode(objParentPartyList[0].PartyCode.ToString(), objModel.GroupList[0].GroupId.ToString());
                }
                objModel.GroupId = objModel.GroupList[0].GroupId;
                objModel.ParentPartyCode = objParentPartyList[0].PartyCode;
                
                //objModel.BankCode = objModel.BankList[0].BankCode;
                //objModel.BankName = objModel.BankList[0].BankName;
                objModel.OnWebsite = "Y";
                objModel.ActiveStatus = "Y";
            }
            else
            {
                if (!string.IsNullOrEmpty(PartyCode))
                {

                    objModel = objRegistrationManager.GetParyOnPartyCode(PartyCode,false);
                    
                }
            }
            return View(objModel);
        }

        [SessionExpire]
        public ActionResult AddEditSupplierRegistration(string IsActionName, string PartyCode)
        {
            PartyModel objModel = new PartyModel();
           
            objModel.IsSupplier = true;
            

            objModel.BankList = objTransactManager.GetBankList();
            List<SelectListItem> BankList = new List<SelectListItem>();
            foreach (var obj in objModel.BankList)
            {
                BankList.Add(new SelectListItem
                {
                    Text = obj.BankName,
                    Value = obj.BankCode.ToString()
                });
            }
            ViewBag.BankList = BankList;

            List<SelectListItem> objOnWebsite = new List<SelectListItem>();
            objOnWebsite.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "Y"
            });

            objOnWebsite.Add(new SelectListItem
            {
                Text = "No",
                Value = "N"
            });
            ViewBag.OnWebsite = objOnWebsite;
            

            objModel.StateList = objRegistrationManager.GetStateList();
            List<SelectListItem> StateList = new List<SelectListItem>();
            StateList.Add(new SelectListItem
            {
                Text = "--Select State--",
                Value = "0"
            });
            foreach (var obj in objModel.StateList)
            {
                if (obj.StateCode != 0)
                {
                    StateList.Add(new SelectListItem
                    {
                        Text = obj.StateName,
                        Value = obj.StateCode.ToString()
                    });
                }
            }
            ViewBag.StateList = StateList;
            if (objModel.StateList.Count > 0)
            {
                objModel.StateCode = objModel.StateList[0].StateCode;
            }
            objModel.CityList = objRegistrationManager.GetCityList();
            List<SelectListItem> CityList = new List<SelectListItem>();
            CityList.Add(new SelectListItem
            {
                Text = "--Select City--",
                Value = "0"
            });
            foreach (var obj in objModel.CityList)
            {
                if (obj.CityCode != 0)
                {
                    if (obj.StateCode == objModel.StateCode)
                    {

                        CityList.Add(new SelectListItem
                        {
                            Text = obj.CityName,
                            Value = obj.CityCode.ToString()
                        });

                    }
                }
            }
            if (objModel.CityList.Count > 0)
            {
                objModel.CityCode = objModel.CityList[0].CityCode;
                objModel.CityName = objModel.CityList[0].CityName;
            }
            ViewBag.CityList = CityList;
            objModel.IsActionName = IsActionName;//05Sep18

            if (IsActionName == "Add")
            {
                if (objModel.BankList.Count > 0)
                {
                    objModel.BankCode = objModel.BankList[0].BankCode;
                    objModel.BankName = objModel.BankList[0].BankName;
                }
                objModel.OnWebsite = "Y";
                objModel.ActiveStatus = "Y";
                objModel.PartyCode = objRegistrationManager.GetPartyCode("", "");
            }
            else
            {
                if (!string.IsNullOrEmpty(PartyCode))
                {

                    objModel = objRegistrationManager.GetParyOnPartyCode(PartyCode,true);
                }
            }
            return View(objModel);
        }

        public ActionResult GetPartyCodeStr(string SelectedParentPartyCode,string SelectedGroupId)
        {
            string PartyCode = objRegistrationManager.GetPartyCode(SelectedParentPartyCode, SelectedGroupId);
            return Json(PartyCode,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SavePartyDetails(PartyModel objPartyModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objPartyModel.LoginUser = Session["LoginUser"] as User;
            objResponse = objRegistrationManager.SavePartyDetails(objPartyModel);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsDuplicatePartyUserName(string IsActionType, string PartyCode, string UserName)
        {
            return Json(objRegistrationManager.IsDuplicatePartyUserName(IsActionType,PartyCode,UserName),JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsDuplicatePartyUserPartyCode(string IsActionType, string PartyCode, string UserPartycode)
        {
            return Json(objRegistrationManager.IsDuplicatePartyUserPartyCode(IsActionType, PartyCode, UserPartycode), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAllCities()
        {
            return Json(objRegistrationManager.GetCityList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetParentPartyList(decimal GroupId)
        {
            List<PartyModel> objParentPartyList = new List<PartyModel>();
            objParentPartyList= objRegistrationManager.GetParentParty(GroupId);
            return Json(objParentPartyList,JsonRequestBehavior.AllowGet);
        }
    }
}