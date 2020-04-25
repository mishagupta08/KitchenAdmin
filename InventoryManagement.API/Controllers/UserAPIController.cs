using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class UserAPIController : ApiController
    {
        public ResponseDetail SetUserRights(List<UserPermissionMasterModel> objPermissionList)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseStatus = "FAILED";
            objResponse.ResponseMessage = "Something went wrong!";
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    
                    var FullPermissionList = (from r in entity.Web_M_UserPermissionMaster select r).ToList();
                    if (FullPermissionList.Count > 0)
                    {
                        foreach (var obj in objPermissionList)
                        {
                            Web_M_UserPermissionMaster objDTUserPermission = new Web_M_UserPermissionMaster();
                            var IsExistsRecord = (from r in FullPermissionList where r.MenuId == obj.MenuId && r.GroupId == obj.UserId select r).FirstOrDefault();
                            if (obj.IsPermitted)
                            {
                                if (IsExistsRecord != null)
                                {

                                }
                                else
                                {
                                    objDTUserPermission.GroupId = obj.UserId;
                                    objDTUserPermission.MenuId = obj.MenuId;
                                    objDTUserPermission.UserId = obj.CurrentLoginUser.UserId;
                                    objDTUserPermission.RecTimeStamp = DateTime.Now;
                                    entity.Web_M_UserPermissionMaster.Add(objDTUserPermission);
                                }
                            }
                            else
                            {
                                if (IsExistsRecord != null)
                                {
                                    objDTUserPermission = IsExistsRecord;
                                    entity.Web_M_UserPermissionMaster.Remove(objDTUserPermission);
                                }
                            }
                        }
                    }
                    else
                    {
                            foreach (var obj in objPermissionList)
                            {
                                Web_M_UserPermissionMaster objDTUserPermission = new Web_M_UserPermissionMaster();
                            if (obj.IsPermitted)
                            {
                                objDTUserPermission.GroupId = obj.UserId;
                                objDTUserPermission.MenuId = obj.MenuId;
                                objDTUserPermission.UserId = obj.CurrentLoginUser.UserId;
                                objDTUserPermission.RecTimeStamp = DateTime.Now;
                                entity.Web_M_UserPermissionMaster.Add(objDTUserPermission);
                            }
                            }
                    }
                    int i = 0;
                    i = entity.SaveChanges();
                    if (i > 0)
                    {
                        objResponse.ResponseMessage = "Saved Successfully";
                        objResponse.ResponseStatus = "OK";
                    }
                }   
            }
            catch(Exception ex)
            {

            }
            return objResponse;
        }

        public List<UserPermissionMasterModel> ListUserPermittedMenus(decimal UserId)
        {
            List<UserPermissionMasterModel> objUserPermittedMenus = new List<UserPermissionMasterModel>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    objUserPermittedMenus = (from r in entity.Web_M_UserPermissionMaster
                                             where r.GroupId == UserId
                                             select new UserPermissionMasterModel
                                             {
                                                 MenuId = r.MenuId,
                                                 UserId = r.GroupId,
                                                 IsPermitted = true,

                                             }).ToList();
                    var FullMenuList = (from r in entity.Web_M_MenuMaster
                                        from g in entity.M_GrpPermissionMaster
                                        from l in entity.M_LedgerMaster
                                        from u in entity.Inv_M_UserMaster
                                        where r.ActiveStatus == "Y" && u.BranchCode == l.PartyCode && l.GroupId == g.GroupID
                                        && g.MenuID == r.MenuId && u.UserId == UserId
                                        select new MenuMasterModel
                                        {
                                            MenuId = r.MenuId,
                                            MenuName = r.MenuName,
                                            ParentId = r.ParentId,
                                            Sequence = r.Sequence,
                                            Hierarchy = r.Hierar,
                                            ChildSequence = r.ChildSequence
                                        }).ToList();
                    if (objUserPermittedMenus.Count == 0)
                    {
                        objUserPermittedMenus = new List<UserPermissionMasterModel>();
                        foreach (var obj in FullMenuList)
                        {
                            objUserPermittedMenus.Add(new UserPermissionMasterModel
                            {
                                UserId = UserId,
                                MenuId = obj.MenuId,
                                MenuName = obj.MenuName,
                                IsPermitted = false,
                                ParentId = obj.ParentId,
                                Sequence = obj.Sequence,
                                ChildSequence = obj.ChildSequence,
                                ParentName = FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault(),
                            });
                        }


                        //objUserPermittedMenus.Add(new UserPermissionMasterModel
                        //{
                        //    MenuList = FullMenuList
                        //});
                    }
                    else
                    {
                        int j = 0;
                        foreach (var obj in FullMenuList)
                        {
                            var IsExists = objUserPermittedMenus.Where(m => m.MenuId == obj.MenuId).Select(m => m).FirstOrDefault();
                            if (IsExists == null)
                            {
                                objUserPermittedMenus.Add(new UserPermissionMasterModel
                                {
                                    UserId = UserId,
                                    MenuId = obj.MenuId,
                                    MenuName = obj.MenuName,
                                    IsPermitted = false,
                                    ParentId = obj.ParentId,
                                    Sequence = obj.Sequence,
                                    ChildSequence = obj.ChildSequence,
                                    ParentName = FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == obj.ParentId).Select(m => m.MenuName).FirstOrDefault(),
                                });
                            }
                            else
                            {

                                objUserPermittedMenus.Where(m => m.MenuId == obj.MenuId).Select(m =>
                                {
                                    m.MenuId = m.MenuId;
                                    m.UserId = m.UserId;
                                    m.MenuName = obj.MenuName;
                                    m.IsPermitted = m.IsPermitted;
                                    m.ParentId = obj.ParentId;
                                    m.ParentName = FullMenuList.Where(k => k.MenuId == obj.ParentId).Select(k => k.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(k => k.MenuId == obj.ParentId).Select(k => k.MenuName).FirstOrDefault();
                                    m.Sequence = obj.Sequence;
                                    m.ChildSequence = obj.ChildSequence;
                                    return m;
                                }).ToList();
                            }
                            j++;
                        }
                        //if (objUserPermittedMenus.Count != FullMenuList.Count)
                        //{
                        //    var ListId = objUserPermittedMenus.Select(p => p.MenuId).ToList();
                        //    var ResultList = FullMenuList.Select(m =>m.MenuId).ToList();
                        //    var RemainingList = ResultList.Except(ListId).ToList();
                        //    foreach (var Id in RemainingList)
                        //    {
                        //        objUserPermittedMenus.Add(new UserPermissionMasterModel
                        //        {
                        //            UserId = UserId,
                        //            MenuId = Id,
                        //            MenuName = FullMenuList.Where(m => m.MenuId == Id).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == Id).Select(m => m.MenuName).FirstOrDefault(),
                        //            IsPermitted = false,
                        //            ParentId = FullMenuList.Where(m => m.MenuId == Id).Select(m => m.ParentId).FirstOrDefault() == null ? 0 : FullMenuList.Where(m => m.MenuId == Id).Select(m => m.ParentId).FirstOrDefault(),
                        //            ParentName = FullMenuList.Where(m => m.MenuId == FullMenuList.Where(p => p.MenuId == Id).Select(p => p.ParentId).FirstOrDefault()).Select(m => m.MenuName).FirstOrDefault() == null ? "" : FullMenuList.Where(m => m.MenuId == FullMenuList.Where(p => p.MenuId == Id).Select(p => p.ParentId).FirstOrDefault()).Select(m => m.MenuName).FirstOrDefault(),
                        //        });
                        //    }
                        //}

                    }
                }
            }
            catch (Exception ex)
            {

            }
            //  objUserPermittedMenus = objUserPermittedMenus.OrderBy(m=>m.ChildSequence).OrderBy(m=>m.Sequence).ToList();
            objUserPermittedMenus = objUserPermittedMenus.Where(m => m.MenuName != null).OrderBy(m => m.ChildSequence).OrderBy(m => m.Sequence).ToList();


            return objUserPermittedMenus;
        }

        public List<User> GetUserList()
        {
            List<User> objUserList = new List<User>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    objUserList = (from r in entity.Inv_M_UserMaster
                                   where r.ActiveStatus == "Y" && r.IsAdmin=="N"
                                   select new User
                                   {
                                     UserId=(int)r.UserId,
                                     UserName=r.UserName
                                   }
                                 ).ToList();
                }
            }
            catch(Exception ex){

            }
            return objUserList;
        }
        public List<User> GetAllUserList()
        {
            List<User> objUserList = new List<User>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    objUserList = (from r in entity.Inv_M_UserMaster
                                   where r.GroupId != 0 && r.GroupId != 105 && r.GroupId != 101
                                   join p in entity.M_LedgerMaster on r.BranchCode equals p.PartyCode
                                   select new User
                                   {
                                       UserId = (int)r.UserId,
                                       UserName = r.UserName,
                                       Password=r.Passw,
                                       FCode=r.FCode,
                                       PartyName=p.PartyName,
                                       Remarks=r.Remarks,
                                       ActiveStatus=r.ActiveStatus
                                   }
                                 ).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objUserList;
        }
        public User GetUser(int UserId)
        {
            User objUser = new User();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    objUser = (from r in entity.Inv_M_UserMaster
                                   where r.UserId==UserId
                                   
                                   select new User
                                   {
                                       UserId = (int)r.UserId,
                                       UserName = r.UserName,
                                       Password = r.Passw,
                                       FCode = r.FCode,
                                       GroupId=(int)r.GroupId,
                                       Remarks = r.Remarks,
                                       ActiveStatus = r.ActiveStatus
                                   }
                                 ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return objUser;
        }
        public decimal GetPartyGroupId(string PartyCode)
        {
            decimal GroupId = 0;
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    GroupId = (from r in entity.M_LedgerMaster
                               where r.PartyCode == PartyCode
                               select r.GroupId
                             ).FirstOrDefault();

                }
            }
            catch(Exception ex)
            {

            }
            return GroupId;
        }

        public List<PartyModel> GetPartyListForUsers()
        {
            List<PartyModel> objListParty = new List<PartyModel>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    objListParty = (from r in entity.M_LedgerMaster
                                    where r.ActiveStatus == "Y" && r.GroupId != 0 && r.GroupId != 105
                                    select new PartyModel
                                    {
                                        PartyCode = r.PartyCode,
                                        PartyName=r.PartyName
                                    }).ToList();
                }
            }
            catch(Exception ex)
            {

            }
            return objListParty;
        }
        public ResponseDetail IsDuplicateUserName(string IsActionType, string UserCode, string UserName)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    if (IsActionType == "Add")
                    {
                        var result = (from r in entity.Inv_M_UserMaster
                                      where r.UserName == UserName
                                      select r
                                    ).FirstOrDefault();
                        if (result != null)
                        {
                            objResponse.ResponseStatus = "FAILED";
                            objResponse.ResponseMessage = "Match Found!";
                        }
                        else
                        {
                            objResponse.ResponseStatus = "OK";
                            objResponse.ResponseMessage = "No Match Found!";
                        }
                    }
                    else if (IsActionType == "Edit")
                    {
                        int UId = 0;
                        if (!string.IsNullOrEmpty(UserCode))
                        {
                            UId = int.Parse(UserCode);
                        }
                        var result = (from r in entity.Inv_M_UserMaster
                                      where r.UserName == UserName && r.UserId != UId
                                      select r
                                    ).FirstOrDefault();
                        if (result != null)
                        {
                            objResponse.ResponseStatus = "FAILED";
                            objResponse.ResponseMessage = "Match Found!";
                        }
                        else
                        {
                            objResponse.ResponseStatus = "OK";
                            objResponse.ResponseMessage = "No Match Found!";
                        }
                    }
                    else
                    {
                        objResponse.ResponseStatus = "OK";
                        objResponse.ResponseMessage = "No Match Found!";
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }
        public ResponseDetail AddEditUserDetails(User objModel,User LoggedUser)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something went wrong!";
            objResponse.ResponseStatus = "FAILED";
            Inv_M_UserMaster DTUser = new Inv_M_UserMaster();
            Inv_TempUserMaster TempDTUser = new Inv_TempUserMaster();
            string Version = "";
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    Version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                    decimal maxUserId = 0;
                    maxUserId = (from r in entity.Inv_M_UserMaster                                 
                                 select r.UserId
                               ).DefaultIfEmpty(0).Max();
                    maxUserId = maxUserId + 1;
                     DTUser = (from r in entity.Inv_M_UserMaster where r.UserId == objModel.UserId  select r).FirstOrDefault();
                    if (DTUser == null)
                    {
                        DTUser = new Inv_M_UserMaster();
                    }
                    else
                    {
                        ////insert into temp table
                        //TempDTUser.UserId = DTUser.UserId;
                        //TempDTUser.UserName = DTUser.UserName;
                        //TempDTUser.Passw = DTUser.Passw;
                        //TempDTUser.Remarks = DTUser.Remarks;
                        //TempDTUser.Status = DTUser.Status;
                        //TempDTUser.FCode = DTUser.FCode;
                        //TempDTUser.BranchCode = DTUser.BranchCode;
                        //TempDTUser.ActiveStatus = DTUser.ActiveStatus;
                        //TempDTUser.CreateBy = DTUser.CreateBy;
                        //TempDTUser.RecTimeStamp = DTUser.RecTimeStamp;
                        //TempDTUser.CreateDate = DTUser.CreateDate;
                        //TempDTUser.GroupId = DTUser.GroupId;
                        //TempDTUser.IsAdmin = DTUser.IsAdmin;
                        //TempDTUser.LastIP = DTUser.LastIP;
                        //TempDTUser.LastLoginTime = DTUser.LastLoginTime;
                        //TempDTUser.LastLogOutTime = DTUser.LastLogOutTime;
                        //TempDTUser.LastModified = DTUser.LastModified;
                        //TempDTUser.LoginStatus = DTUser.LoginStatus;
                        //TempDTUser.LUserId = DTUser.LUserId;
                        //TempDTUser.MRecTimeStamp = DateTime.Now.Date;
                        //TempDTUser.MUserId = LoggedUser.UserId;
                        //TempDTUser.TId = 0;
                        //TempDTUser.UId = DTUser.UId;
                        //TempDTUser.Version = DTUser.Version;
                        //entity.Inv_TempUserMaster.Add(TempDTUser);
                        string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BKDHServices"].ConnectionString;
                        SqlConnection SC = new SqlConnection(AppConnectionString);

                        //string query = "INSERT Inv_TempUserMaster Select *,'"+ LoggedUser.UserId +"',Getdate() FROM Inv_M_UserMaster WHERE UserID='"+ DTUser.UserId + "'";
                        //SC.Open();
                        //SqlCommand cmd = new SqlCommand(query,SC);
                        //cmd.ExecuteNonQuery();
                        //SC.Close();
                    }
                        //updating values
                        if (objModel.IsActionName == "Delete")
                        {
                            DTUser.ActiveStatus = "N";
                        }
                        else
                        {
                            DTUser.BranchCode = objModel.FCode;
                            DTUser.FCode = objModel.FCode;
                           
                            
                            DTUser.LastModified = DateTime.Now.Date.ToString();
                        
                        
                            DTUser.IsAdmin = "N";
                            DTUser.LastIP = "0";
                            DTUser.LastLoginTime = DateTime.Now.Date;
                            DTUser.LastLogOutTime = "";
                            DTUser.LoginStatus = "";
                            DTUser.LUserId = LoggedUser.UserId;
                            DTUser.Passw = objModel.Password;
                            DTUser.RecTimeStamp = DateTime.Now.Date;
                            DTUser.Remarks = objModel.Remarks != null ? objModel.Remarks : ""; 
                            DTUser.Status = objModel.ActiveStatus;
                            //DTUser.UId = 0;
                            DTUser.UserName = objModel.UserName;
                            
                            if (objModel.IsActionName == "Add")
                            {
                                DTUser.ActiveStatus = "Y";
                            DTUser.GroupId = objModel.GroupId;
                            DTUser.CreateBy = LoggedUser.UserId.ToString();
                                DTUser.CreateDate = DateTime.Now.Date;
                                DTUser.UserId = maxUserId;
                                entity.Inv_M_UserMaster.Add(DTUser);
                            }
                            DTUser.Version = Version;
                        }
                        int i = entity.SaveChanges();
                        if(objModel.IsActionName=="Edit" || i > 0)
                        {
                            if(objModel.IsActionName == "Edit")
                            {
                                objResponse.ResponseMessage = "Updated Successfully!";
                            }
                            else if(objModel.IsActionName == "Add")
                            {
                                objResponse.ResponseMessage = "Saved Successfully!";
                            }
                            else
                            {
                                objResponse.ResponseMessage = "Deleted Successfully!";
                            }
                            
                            objResponse.ResponseStatus = "OK";
                        }
                        else
                        {
                            objResponse.ResponseMessage = "Something went wrong!";
                            objResponse.ResponseStatus = "FAILED";
                        }                    
                }
            }
            catch (Exception ex)
            {

            }
            return objResponse;
        }
    }
}
