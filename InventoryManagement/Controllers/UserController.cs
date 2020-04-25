using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Common;
using InventoryManagement.Entity.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserManager objUserManager = new UserManager();
        RegistrationManager objRegistrationManager = new RegistrationManager();
        // GET: User
        [SessionExpire]
        public ActionResult UserMasterList()
        {
            return View();
        }
        [SessionExpire]
        public ActionResult AddUser(string IsActionName, string UserCode)
        {
            User objModel = new User();
            var result = objUserManager.GetPartyListForUsers();
            
            List<SelectListItem> PartyList = new List<SelectListItem>();
            foreach (var obj in result)
            {
                PartyList.Add(new SelectListItem
                {
                    Text = obj.PartyName,
                    Value = obj.PartyCode.ToString()
                });
            }
            ViewBag.PartyList = PartyList;

            
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
            if (result.Count > 0)
            {
                objModel.FCode = result[0].PartyCode;                
            }
            if (IsActionName == "Add")
            {
               
                
                objModel.ActiveStatus = "Y";
            }
            else
            {
                if (!string.IsNullOrEmpty(UserCode))
                {
                    int UId = int.Parse(UserCode);
                    objModel = objUserManager.GetUser(UId);

                }
            }
            objModel.IsActionName = IsActionName;

            var GroupLists = objRegistrationManager.GetGroupList();
           
            List<SelectListItem> GroupList = new List<SelectListItem>();
            foreach (var obj in GroupLists)
            {
                if (IsActionName == "Add")
                {
                    if (obj.GroupId != 0 && obj.GroupId != 100 && obj.GroupId != 105 && obj.GroupId != 101)
                    {
                        GroupList.Add(new SelectListItem
                        {

                            Text = obj.GroupName,
                            Value = obj.GroupId.ToString()

                        });
                    }
                }
                else {
                    if (obj.GroupId != 0 && obj.GroupId != 100 && obj.GroupId != 105)
                    {
                        GroupList.Add(new SelectListItem
                        {

                            Text = obj.GroupName,
                            Value = obj.GroupId.ToString()

                        });
                    }
                }
            }
            ViewBag.GroupList = GroupList;

            return View(objModel);
        }
        public ActionResult GetAllUserList()
        {
            List<User> objUserList = new List<User>();
            objUserList = objUserManager.GetAllUserList();
            var jsonUserList = Json(objUserList, JsonRequestBehavior.AllowGet);
            jsonUserList.MaxJsonLength = int.MaxValue;
            return jsonUserList;
        }
        [HttpPost]
        public ActionResult SaveUserDetails(User objModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
           
            objResponse = objUserManager.AddEditUserDetails(objModel, Session["LoginUser"] as User);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IsDuplicateUserName(string IsActionType, string UserId, string UserName)
        {
            return Json(objUserManager.IsDuplicateUserName(IsActionType, UserId, UserName), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPartyGroupId(string SelectedPartyCode)
        {
            return Json(objUserManager.GetPartyGroupId(SelectedPartyCode), JsonRequestBehavior.AllowGet);
        }
        [SessionExpire]
        public ActionResult SetUserRights()
        {
            UserPermissionMasterModel objUserList = new UserPermissionMasterModel();
            objUserList.UserList = objUserManager.GetUserList();
            if (objUserList.UserList != null && objUserList.UserList.Count() > 0)
            {
                ViewBag.Selecteduser = objUserList.UserList[0].UserId;
            }
            else
            {
                ViewBag.Selecteduser = 0;
            }
            return View(objUserList);
        }
        
        public ActionResult GetPermissionList(string UserId)
        {
            decimal userID = 0;
            if (!(string.IsNullOrEmpty(UserId)))
            {
                userID = decimal.Parse(UserId);
            }
            return Json(objUserManager.ListUserPermittedMenus(userID),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveUserRights(UserPermissionMasterModel PermittedList)
        {
            ResponseDetail objResponse = new ResponseDetail();
            List<UserPermissionMasterModel> objPermissionList = new List<UserPermissionMasterModel>();

            if (!string.IsNullOrEmpty(PermittedList.ListPermittedMenuList))
            {
                var objects = JArray.Parse(PermittedList.ListPermittedMenuList); // parse as array  
                foreach (JObject root in objects)
                {
                    UserPermissionMasterModel objTemp = new UserPermissionMasterModel();
                    foreach (KeyValuePair<String, JToken> app in root)
                    {
                        if (app.Key == "MenuId")
                        {
                            objTemp.MenuId = (decimal)app.Value;
                        }
                        //else if (app.Key == "UserId")
                        //{
                        //    objTemp.UserId = (decimal)app.Value;
                        //}
                        else if (app.Key == "IsPermitted")
                        {
                            objTemp.IsPermitted = (bool)app.Value;
                        }
                       
                        
                        
                    }
                    objTemp.CurrentLoginUser = Session["LoginUser"] as User;
                    objTemp.UserId = PermittedList.UserId;
                    objPermissionList.Add(objTemp);
                }
            }
                objResponse = objUserManager.SetUserRights(objPermissionList);
            return Json(objResponse, JsonRequestBehavior.AllowGet);
        }
    }
}