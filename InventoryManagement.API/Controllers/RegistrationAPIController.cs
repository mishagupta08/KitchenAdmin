using InventoryManagement.API.Models;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InventoryManagement.API.Controllers
{
    public class RegistrationAPIController : ApiController
    {
        public ResponseDetail SavePartyDetails(PartyModel objPartyModel)
        {
            ResponseDetail objResponse = new ResponseDetail();
            objResponse.ResponseMessage = "Something Went Wrong!";
            objResponse.ResponseStatus = "FAILED";
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    M_LedgerMaster objDTLedger = new M_LedgerMaster();
                    decimal MaxUserId = 0;
                    MaxUserId = (from r in entity.Inv_M_UserMaster
                                 where r.ActiveStatus == "Y"
                                 select r.UserId
                               ).DefaultIfEmpty(0).Max();
                    objDTLedger = (from r in entity.M_LedgerMaster
                                   where r.PartyCode == objPartyModel.PartyCode
                                   select r
                                ).FirstOrDefault();
                    if (objDTLedger == null)
                    {
                        objDTLedger = new M_LedgerMaster();
                    }
                    else
                    {

                    }
                    //    ShiftDataintoTemptable("M_LedgerMaster", "TempLedgerMaster", ",'" + objPartyModel.LoginUser.UserId + "',Getdate()", " AND PartyCode='" + objPartyModel.PartyCode + "'");

                    if (objPartyModel != null)
                    {
                        if (objPartyModel.IsSupplier == false)
                        {
                            objDTLedger.GroupId = objPartyModel.GroupId;
                            objDTLedger.PGroupId = objPartyModel.PGroupId;
                            objDTLedger.UserPartyCode = string.IsNullOrEmpty(objPartyModel.UserPartyCode) ? "" : objPartyModel.UserPartyCode;
                            if (objPartyModel.IsActionName == "Add")
                            {
                                decimal MaxPcode = (from r in entity.M_LedgerMaster
                                                    where r.GroupId == objPartyModel.GroupId //&& r.ActiveStatus == "Y" 'Cmnted on 28Aug18
                                                    select r.PCode
                                           ).DefaultIfEmpty(0).Max();
                                MaxPcode = MaxPcode + 1;
                                objDTLedger.PCode = MaxPcode;
                            }
                            objDTLedger.PartyCode = objPartyModel.PartyCode;
                            objDTLedger.PartyName = objPartyModel.PartyName;
                            objDTLedger.ParentPartyCode = objPartyModel.ParentPartyCode;
                            objDTLedger.Address1 = string.IsNullOrEmpty(objPartyModel.Address1) ? "" : objPartyModel.Address1;
                            objDTLedger.Address2 = string.IsNullOrEmpty(objPartyModel.Address2) ? "" : objPartyModel.Address2;
                            objDTLedger.StateCode = objPartyModel.StateCode;
                            objDTLedger.CityCode = objPartyModel.CityCode;
                            objDTLedger.CityName = objPartyModel.CityName;
                            objDTLedger.Tehsil = string.IsNullOrEmpty(objPartyModel.Tehsil) ? "" : objPartyModel.Tehsil;
                            objDTLedger.PinCode = objPartyModel.PinCode;
                            objDTLedger.PhoneNo = string.IsNullOrEmpty(objPartyModel.PhoneNo) ? "" : objPartyModel.PhoneNo;
                            objDTLedger.MobileNo = objPartyModel.MobileNo;
                            objDTLedger.FaxNo = string.IsNullOrEmpty(objPartyModel.FaxNo) ? "" : objPartyModel.FaxNo;
                            objDTLedger.PanNo = string.IsNullOrEmpty(objPartyModel.PanNo) ? "" : objPartyModel.PanNo;
                            objDTLedger.TinNo = string.IsNullOrEmpty(objPartyModel.GSTIN) ? "" : objPartyModel.GSTIN;
                            objDTLedger.STaxNo = string.IsNullOrEmpty(objPartyModel.STaxNo) ? "" : objPartyModel.STaxNo;
                            objDTLedger.CstNo = "";
                            objDTLedger.BankAcNo = string.IsNullOrEmpty(objPartyModel.BankAccNo) ? "" : objPartyModel.BankAccNo;
                            objDTLedger.BankCode = objPartyModel.BankCode;
                            objDTLedger.BankName = string.IsNullOrEmpty(objPartyModel.BankName) ? "" : objPartyModel.BankName;
                            objDTLedger.RequestTo = string.IsNullOrEmpty(objPartyModel.RequestTo) ? "" : objPartyModel.RequestTo;
                            objDTLedger.AccountVerify = string.IsNullOrEmpty(objPartyModel.AccountVerify) ? "" : objPartyModel.AccountVerify;
                            objDTLedger.RecommandBy = string.IsNullOrEmpty(objPartyModel.RecommandBy) ? "" : objPartyModel.RecommandBy;
                            objDTLedger.ContactPerson = string.IsNullOrEmpty(objPartyModel.ContactPerson) ? "" : objPartyModel.ContactPerson;
                            objDTLedger.E_MailAdd = string.IsNullOrEmpty(objPartyModel.EmailAddress) ? "" : objPartyModel.EmailAddress;
                            objDTLedger.ActiveStatus = objPartyModel.ActiveStatus;
                            objDTLedger.OnWebSite ="Y";
                            objDTLedger.CreditLimit = objPartyModel.CreditLimit;
                            objDTLedger.Remarks = string.IsNullOrEmpty(objPartyModel.Remarks) ? "" : objPartyModel.Remarks;
                            objDTLedger.RecTimeStamp = DateTime.Now;
                            objDTLedger.NewFld1 = string.IsNullOrEmpty(objPartyModel.NewFId1) ? "" : objPartyModel.NewFId1;
                            objDTLedger.NewFld2 = string.IsNullOrEmpty(objPartyModel.NewFId2) ? "" : objPartyModel.NewFId2;
                            objDTLedger.NewFld3 = string.IsNullOrEmpty(objPartyModel.NewFId3) ? "" : objPartyModel.NewFId3;
                            objDTLedger.NewFld4 = string.IsNullOrEmpty(objPartyModel.NewFId4) ? "" : objPartyModel.NewFId4;
                            objDTLedger.Company = "";
                            objDTLedger.UserId = objPartyModel.LoginUser.UserId;
                            objDTLedger.UserName = objPartyModel.LoginUser.UserName;

                            objDTLedger.RecvdCForm = "N";
                            var i = 0;
                            if (objPartyModel.IsActionName == "Add")
                            {
                                objDTLedger.LastModified = "";
                                entity.M_LedgerMaster.Add(objDTLedger);
                            }
                            else if (objPartyModel.IsActionName == "Delete")
                            {
                                objDTLedger.ActiveStatus = "N";
                            }
                            else
                            {
                                objDTLedger.LastModified = "";
                            }
                            try
                            {
                                i = entity.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }
                            if (objPartyModel.IsActionName == "Edit" || i > 0)
                            {
                                Inv_M_UserMaster objDTUserMaster = new Inv_M_UserMaster();
                                objDTUserMaster = (from r in entity.Inv_M_UserMaster
                                                   where r.FCode == objPartyModel.PartyCode
                                                   select r
                                                 ).FirstOrDefault();
                                if (objDTUserMaster == null)
                                {
                                    objDTUserMaster = new Inv_M_UserMaster();
                                    string version = (from result in entity.M_NewHOVersionInfo select result.VersionNo).FirstOrDefault();
                                    objDTUserMaster.UserName = objPartyModel.objUserDetails.UserName;
                                    objDTUserMaster.Passw = objPartyModel.objUserDetails.Password;
                                    objDTUserMaster.BranchCode = objPartyModel.PartyCode;
                                    objDTUserMaster.FCode = objPartyModel.PartyCode;
                                    objDTUserMaster.Version = version;
                                    MaxUserId = MaxUserId + 1;
                                    objDTUserMaster.UserId = MaxUserId;
                                    objDTUserMaster.Remarks = "";
                                    objDTUserMaster.LUserId = objPartyModel.LoginUser.UserId;
                                    objDTUserMaster.LastModified = "";
                                    objDTUserMaster.ActiveStatus = objPartyModel.ActiveStatus;
                                    objDTUserMaster.CreateBy = objPartyModel.LoginUser.UserName;
                                    objDTUserMaster.GroupId = 101;
                                    objDTUserMaster.IsAdmin = "N";
                                    objDTUserMaster.LastIP = "";
                                    objDTUserMaster.LastLoginTime = DateTime.Now;
                                    objDTUserMaster.LastLogOutTime = "";
                                    objDTUserMaster.LoginStatus = "N";
                                    objDTUserMaster.Status = objPartyModel.ActiveStatus;
                                    objDTUserMaster.RecTimeStamp = DateTime.Now;
                                    objDTUserMaster.CreateBy = objPartyModel.objUserDetails.UserName;
                                    objDTUserMaster.CreateDate = DateTime.Now;
                                }
                                if (objPartyModel.IsActionName == "Add")
                                {
                                    entity.Inv_M_UserMaster.Add(objDTUserMaster);

                                }
                                else if (objPartyModel.IsActionName == "Delete")
                                {
                                   // ShiftDataintoTemptable("Inv_M_UserMaster", "Inv_TempUserMaster", ",'" + objPartyModel.LoginUser.UserId + "',Getdate()", " AND BranchCode='" + objPartyModel.PartyCode + "'");
                                    objDTUserMaster.LastModified= "Party deactivated by " + objPartyModel.LoginUser.UserName + " on " + DateTime.Now.ToString();
                                    objDTUserMaster.ActiveStatus = "N";
                                }
                                i = 0;
                                try
                                {
                                    i = entity.SaveChanges();
                                }
                                catch (Exception ex)
                                {

                                }

                                if (objPartyModel.IsActionName == "Edit" || i > 0)
                                {
                                    if (objPartyModel.IsActionName == "Add")
                                    {
                                        objResponse.ResponseMessage = "Saved Successfully!";
                                    }
                                    else if (objPartyModel.IsActionName == "Edit")
                                    {
                                        objResponse.ResponseMessage = "Updated Successfully!";
                                    }
                                    else
                                    {
                                        objResponse.ResponseMessage = "Deleted Successfully!";
                                    }
                                    objResponse.ResponseStatus = "OK";
                                }
                                else
                                {
                                    objResponse.ResponseMessage = "Something Went Wrong!";
                                    objResponse.ResponseStatus = "FAILED";
                                }
                            }
                            else
                            {
                                objResponse.ResponseMessage = "Something Went Wrong!";
                                objResponse.ResponseStatus = "FAILED";
                            }
                        }
                        else
                        {
                            //supplier save
                            objDTLedger.GroupId = 5;
                            objDTLedger.PGroupId = 0;
                            objDTLedger.UserPartyCode = string.IsNullOrEmpty(objPartyModel.UserPartyCode) ? "" : objPartyModel.UserPartyCode;
                            if (objPartyModel.IsActionName == "Add")
                            {
                                decimal MaxPcode = (from r in entity.M_LedgerMaster
                                                    where r.GroupId == 5 //&& r.ActiveStatus == "Y" Cmnted on 28Aug18
                                                    select r.PCode
                                           ).DefaultIfEmpty(0).Max();
                                MaxPcode = MaxPcode + 1;
                                objDTLedger.PCode = MaxPcode;
                            }
                            objDTLedger.PartyCode = objPartyModel.PartyCode;
                            objDTLedger.PartyName = objPartyModel.PartyName;
                            objDTLedger.ParentPartyCode = "0";
                            objDTLedger.Address1 = string.IsNullOrEmpty(objPartyModel.Address1) ? "" : objPartyModel.Address1;
                            objDTLedger.Address2 = string.IsNullOrEmpty(objPartyModel.Address2) ? "" : objPartyModel.Address2;
                            objDTLedger.StateCode = objPartyModel.StateCode;
                            objDTLedger.CityCode = objPartyModel.CityCode;
                            objDTLedger.CityName = objPartyModel.CityName;
                            objDTLedger.Tehsil = string.IsNullOrEmpty(objPartyModel.Tehsil) ? "" : objPartyModel.Tehsil;
                            objDTLedger.PinCode = objPartyModel.PinCode;
                            objDTLedger.PhoneNo = string.IsNullOrEmpty(objPartyModel.PhoneNo) ? "" : objPartyModel.PhoneNo;
                            objDTLedger.MobileNo = objPartyModel.MobileNo;
                            objDTLedger.FaxNo = string.IsNullOrEmpty(objPartyModel.FaxNo) ? "" : objPartyModel.FaxNo;
                            objDTLedger.PanNo = string.IsNullOrEmpty(objPartyModel.PanNo) ? "" : objPartyModel.PanNo;
                            objDTLedger.TinNo = string.IsNullOrEmpty(objPartyModel.GSTIN) ? "" : objPartyModel.GSTIN;
                            objDTLedger.STaxNo = string.IsNullOrEmpty(objPartyModel.STaxNo) ? "" : objPartyModel.STaxNo;
                            objDTLedger.CstNo = "";
                            objDTLedger.BankAcNo = string.IsNullOrEmpty(objPartyModel.BankAccNo) ? "" : objPartyModel.BankAccNo;
                            objDTLedger.BankCode = objPartyModel.BankCode;
                            objDTLedger.BankName = string.IsNullOrEmpty(objPartyModel.BankName) ? "" : objPartyModel.BankName;
                            objDTLedger.RequestTo = string.IsNullOrEmpty(objPartyModel.RequestTo) ? "" : objPartyModel.RequestTo;
                            objDTLedger.AccountVerify = string.IsNullOrEmpty(objPartyModel.AccountVerify) ? "" : objPartyModel.AccountVerify;
                            objDTLedger.RecommandBy = string.IsNullOrEmpty(objPartyModel.RecommandBy) ? "" : objPartyModel.RecommandBy;
                            objDTLedger.ContactPerson = string.IsNullOrEmpty(objPartyModel.ContactPerson) ? "" : objPartyModel.ContactPerson;
                            objDTLedger.E_MailAdd = string.IsNullOrEmpty(objPartyModel.EmailAddress) ? "" : objPartyModel.EmailAddress;
                            objDTLedger.ActiveStatus = objPartyModel.ActiveStatus;
                            objDTLedger.OnWebSite = objPartyModel.OnWebsite;
                            objDTLedger.CreditLimit = objPartyModel.CreditLimit;
                            objDTLedger.Remarks = string.IsNullOrEmpty(objPartyModel.Remarks) ? "" : objPartyModel.Remarks;
                            objDTLedger.RecTimeStamp = DateTime.Now;
                            objDTLedger.NewFld1 = string.IsNullOrEmpty(objPartyModel.NewFId1) ? "" : objPartyModel.NewFId1;
                            objDTLedger.NewFld2 = string.IsNullOrEmpty(objPartyModel.NewFId2) ? "" : objPartyModel.NewFId2;
                            objDTLedger.NewFld3 = string.IsNullOrEmpty(objPartyModel.NewFId3) ? "" : objPartyModel.NewFId3;
                            objDTLedger.NewFld4 = string.IsNullOrEmpty(objPartyModel.NewFId4) ? "" : objPartyModel.NewFId4;
                            objDTLedger.Company = "";
                            objDTLedger.UserId = objPartyModel.LoginUser.UserId;
                            objDTLedger.UserName = objPartyModel.LoginUser.UserName;

                            objDTLedger.RecvdCForm = "N";
                            var i = 0;
                            if (objPartyModel.IsActionName == "Add")
                            {
                                objDTLedger.LastModified = "";
                                entity.M_LedgerMaster.Add(objDTLedger);
                            }
                            else if (objPartyModel.IsActionName == "Delete")
                            {
                                objDTLedger.ActiveStatus = "N";
                            }
                            else
                            {
                                objDTLedger.LastModified = "";
                            }
                            try
                            {
                                i = entity.SaveChanges();
                            }
                            catch (Exception ex)
                            {

                            }
                            if (objPartyModel.IsActionName == "Edit" || i > 0)
                            {
                                
                                    if (objPartyModel.IsActionName == "Add")
                                    {
                                        objResponse.ResponseMessage = "Saved Successfully!";
                                    }
                                    else if (objPartyModel.IsActionName == "Edit")
                                    {
                                        objResponse.ResponseMessage = "Updated Successfully!";
                                    }
                                    else
                                    {
                                        objResponse.ResponseMessage = "Deleted Successfully!";
                                    }
                                    objResponse.ResponseStatus = "OK";
                                
                            }
                            else
                            {
                                objResponse.ResponseMessage = "Something Went Wrong!";
                                objResponse.ResponseStatus = "FAILED";
                            }
                        }
                    }
                         
                }
            }
            catch(Exception ex)
            {

            }

            return objResponse;
        }
        
        public List<StateModel> GetStateList()
        {
            List<StateModel> objStateModel = new List<StateModel>();
            try
            {
                string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BKDHServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(AppConnectionString);                
                string query = "Select * from M_StateDivMaster where ActiveStatus='Y'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                List<StateModel> M_StateDivMasterList = new List<StateModel>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        M_StateDivMasterList.Add(new StateModel{
                            StateCode = decimal.Parse(reader["StateCode"].ToString()),
                             StateName = reader["StateName"].ToString()
                        });
                    }
                }
                using (var entity=new BKDHEntities11())
                {
                    
                    var CompanyDetails = (from r in entity.M_CompanyMaster
                                          where r.ActiveStatus=="Y"
                                          select r
                                          ).FirstOrDefault();
                    foreach (var obj in M_StateDivMasterList)
                    {

                        if (CompanyDetails.CompState == obj.StateCode)
                        {
                            obj.IsCompanyState = true;
                        }
                        else
                        {
                            obj.IsCompanyState = false;
                        }
                        objStateModel.Add(obj);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return objStateModel;

        }
        public List<CityModel> GetCityList()
        {
            List<CityModel> objCityModel = new List<CityModel>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    //var result = (from r in entity.M_CityStateMaster
                    //                 where r.ActiveStatus == "Y"
                    //                 select new CityModel
                    //                 {
                    //                     CityCode=r.CityCode,
                    //                     CityName=r.CityName,
                    //                     StateCode=r.StateCode
                    //                 }
                    //               ).ToList();

                    string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BKDHServices"].ConnectionString;
                    SqlConnection SC = new SqlConnection(AppConnectionString);                    
                    string query = "Select * from M_CityStateMaster where ActiveStatus='Y'";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = SC;
                    SC.Close();
                    SC.Open();
                    List<CityModel> M_CityStateMasterList = new List<CityModel>();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            M_CityStateMasterList.Add(new CityModel
                            {
                                StateCode = decimal.Parse(reader["StateCode"].ToString()),
                                CityCode = decimal.Parse(reader["CityCode"].ToString()),
                                CityName = reader["StateName"].ToString(),
                            });
                        }
                    }
                    var CompanyDetails = (from r in entity.M_CompanyMaster
                                          where r.ActiveStatus == "Y"
                                          select r
                                          ).FirstOrDefault();
                    foreach (var obj in M_CityStateMasterList)
                    {

                        //if (CompanyDetails.CompCity.ToLower() == obj.CityName.ToLower())
                        //{
                        //    obj.IsCompanyCity = true;
                        //}
                        //else
                        //{
                            obj.IsCompanyCity = false;
                        //}
                        objCityModel.Add(obj);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return objCityModel;
        }

        public List<PartyModel> GetParentParty(decimal GroupId,bool ForStall)
        {
            List<PartyModel> objParentParty = new List<PartyModel>();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    if (ForStall == true)
                    {
                        objParentParty = (from r in entity.M_LedgerMaster
                                          where r.GroupId == 100
                                          select new PartyModel
                                          {
                                              PartyCode = r.PartyCode,
                                              PartyName = r.PartyName
                                          }
                                        ).ToList();
                    }
                    else
                    {
                        objParentParty = (from r in entity.M_LedgerMaster
                                          where r.GroupId ==0
                                          select new PartyModel
                                          {
                                              PartyCode = r.PartyCode,
                                              PartyName = r.PartyName
                                          }
                                        ).ToList();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return objParentParty;
        }
        public PartyModel GetParyOnPartyCode(string PartyCode, bool IsSupplier)
        {
            PartyModel objParty = new PartyModel();
            try
            {
                string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BKDHServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(AppConnectionString);                
                string query = "Select * from M_StateDivMaster where ActiveStatus='Y'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                List<StateModel> M_StateDivMasterList = new List<StateModel>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        M_StateDivMasterList.Add(new StateModel
                        {
                            StateCode = decimal.Parse(reader["StateCode"].ToString()),
                            StateName = reader["StateName"].ToString()
                        });
                    }
                }
                using (var entity=new BKDHEntities11())
                {
                    if (IsSupplier == false)
                    {
                        // objParty = (from r in entity.M_LedgerMaster
                        //                 where r.ActiveStatus == "Y"
                        //                 from p in entity.M_GroupMaster
                        //                 where p.GroupId == r.GroupId
                        //                 from s in entity.M_LedgerMaster
                        //                 where s.PartyCode == r.ParentPartyCode
                        //                 from st in M_StateDivMasterList
                        //             where st.StateCode == r.StateCode
                        //                 from u in entity.Inv_M_UserMaster
                        //                 where u.FCode == r.PartyCode
                        //                 select new PartyModel
                        //                 {
                        //                     GroupId = r.GroupId,
                        //                     GroupName = p.GroupName,
                        //                     PGroupId = r.PGroupId,
                        //                     UserPartyCode = r.UserPartyCode,
                        //                     PCode = r.PCode,
                        //                     PartyCode = r.PartyCode,
                        //                     PartyName = r.PartyName,
                        //                     ParentPartyCode = r.ParentPartyCode,
                        //                     ParentpartyName = s.PartyName,
                        //                     Address1 = r.Address1,
                        //                     Address2 = r.Address2,
                        //                     StateCode = r.StateCode,
                        //                     StateName = st.StateName,
                        //                     CityCode = r.CityCode,
                        //                     CityName = r.CityName,
                        //                     Tehsil = r.Tehsil,
                        //                     PinCode = r.PinCode,
                        //                     PhoneNo = r.PhoneNo,
                        //                     MobileNo = r.MobileNo,
                        //                     FaxNo = r.FaxNo,
                        //                     PanNo = r.PanNo,
                        //                     GSTIN = r.TinNo,
                        //                     STaxNo = r.STaxNo,
                        //                     //CstNo = objPartyModel.CstNo;
                        //                     BankAccNo = r.BankAcNo,
                        //                     BankCode = r.BankCode,
                        //                     BankName = r.BankName,
                        //                     RequestTo = r.RequestTo,
                        //                     AccountVerify = r.AccountVerify,
                        //                     RecommandBy = r.RecommandBy,
                        //                     ContactPerson = r.ContactPerson,
                        //                     EmailAddress = r.E_MailAdd,
                        //                     ActiveStatus = r.ActiveStatus,
                        //                     OnWebsite = r.OnWebSite,
                        //                     CreditLimit = r.CreditLimit,
                        //                     Remarks = r.Remarks,
                        //                     RecTimeStamp = r.RecTimeStamp,
                        //                     NewFId1 = r.NewFld1,
                        //                     NewFId2 = r.NewFld2,
                        //                     NewFId3 = r.NewFld3,
                        //                     NewFId4 = r.NewFld4,
                        //                     Company = r.Company,
                        //                     RecvdCForm = r.RecvdCForm,
                        //                     objUserDetails = new User
                        //                     {
                        //                         UserName = u.UserName,
                        //                         Password = u.Passw
                        //                     }
                        //                 }
                        //).FirstOrDefault();
                        objParty = (from p in (from r in entity.M_LedgerMaster
                                                   where //r.ActiveStatus == "Y" && 'Cmnted on 28Aug18
                                                   r.PartyCode == PartyCode
                                               from p in entity.M_GroupMaster
                                                   where p.GroupId == r.GroupId
                                                   from s in entity.M_LedgerMaster
                                                   where s.PartyCode == r.ParentPartyCode
                                                   //from st in M_StateDivMasterList
                                                   //where st.StateCode == r.StateCode
                                                   from u in entity.Inv_M_UserMaster
                                                   where u.BranchCode == r.PartyCode
                                                   select new
                                                   {
                                                       GroupId = r.GroupId,
                                                       GroupName = p.GroupName,
                                                       PGroupId = r.PGroupId,
                                                       UserPartyCode = r.UserPartyCode,
                                                       PCode = r.PCode,
                                                       PartyCode = r.PartyCode,
                                                       PartyName = r.PartyName,
                                                       ParentPartyCode = r.ParentPartyCode,
                                                       ParentpartyName = s.PartyName,
                                                       Address1 = r.Address1,
                                                       Address2 = r.Address2,
                                                       StateCode = r.StateCode,
                                                       //StateName = st.StateName,
                                                       CityCode = r.CityCode,
                                                       CityName = r.CityName,
                                                       Tehsil = r.Tehsil,
                                                       PinCode = r.PinCode,
                                                       PhoneNo = r.PhoneNo,
                                                       MobileNo = r.MobileNo,
                                                       FaxNo = r.FaxNo,
                                                       PanNo = r.PanNo,
                                                       GSTIN = r.TinNo,
                                                       STaxNo = r.STaxNo,
                                                       //CstNo = objPartyModel.CstNo;
                                                       BankAccNo = r.BankAcNo,
                                                       BankCode = r.BankCode,
                                                       BankName = r.BankName,
                                                       RequestTo = r.RequestTo,
                                                       AccountVerify = r.AccountVerify,
                                                       RecommandBy = r.RecommandBy,
                                                       ContactPerson = r.ContactPerson,
                                                       EmailAddress = r.E_MailAdd,
                                                       ActiveStatus = r.ActiveStatus,
                                                       OnWebsite = r.OnWebSite,
                                                       CreditLimit = r.CreditLimit,
                                                       Remarks = r.Remarks,
                                                       RecTimeStamp = r.RecTimeStamp,
                                                       NewFId1 = r.NewFld1,
                                                       NewFId2 = r.NewFld2,
                                                       NewFId3 = r.NewFld3,
                                                       NewFId4 = r.NewFld4,
                                                       Company = r.Company,
                                                       RecvdCForm = r.RecvdCForm,
                                                       objUserDetails = new User
                                                       {
                                                           UserName = u.UserName,
                                                           Password = u.Passw
                                                       }
                                                   }

                                         )

                                         .AsEnumerable()
                                        from st in M_StateDivMasterList
                                        where st.StateCode == p.StateCode

                                        select new PartyModel
                                        {
                                            GroupId = p.GroupId,
                                            GroupName = p.GroupName,
                                            PGroupId = p.PGroupId,
                                            UserPartyCode = p.UserPartyCode,
                                            PCode = p.PCode,
                                            PartyCode = p.PartyCode,
                                            PartyName = p.PartyName,
                                            ParentPartyCode = p.ParentPartyCode,
                                            ParentpartyName = p.PartyName,
                                            Address1 = p.Address1,
                                            Address2 = p.Address2,
                                            StateCode = p.StateCode,
                                            StateName = st.StateName,
                                            CityCode = p.CityCode,
                                            CityName = p.CityName,
                                            Tehsil = p.Tehsil,
                                            PinCode = p.PinCode,
                                            PhoneNo = p.PhoneNo,
                                            MobileNo = p.MobileNo,
                                            FaxNo = p.FaxNo,
                                            PanNo = p.PanNo,
                                            GSTIN = p.GSTIN,
                                            STaxNo = p.STaxNo,
                                            //CstNo = objPartyModel.CstNo;
                                            BankAccNo = p.BankAccNo,
                                            BankCode = p.BankCode,
                                            BankName = p.BankName,
                                            RequestTo = p.RequestTo,
                                            AccountVerify = p.AccountVerify,
                                            RecommandBy = p.RecommandBy,
                                            ContactPerson = p.ContactPerson,
                                            EmailAddress = p.EmailAddress,
                                            ActiveStatus = p.ActiveStatus,
                                            OnWebsite = p.OnWebsite,
                                            CreditLimit = p.CreditLimit,
                                            Remarks = p.Remarks,
                                            RecTimeStamp = p.RecTimeStamp,
                                            NewFId1 = p.NewFId1,
                                            NewFId2 = p.NewFId2,
                                            NewFId3 = p.NewFId3,
                                            NewFId4 = p.NewFId4,
                                            Company = p.Company,
                                            RecvdCForm = p.RecvdCForm,
                                            objUserDetails = new User
                                            {
                                                UserName = p.objUserDetails.UserName,
                                                Password = p.objUserDetails.Password
                                            }
                                        }
                        ).FirstOrDefault();
                    }
                    else
                    {
                        //objParty = (from r in entity.M_LedgerMaster
                        //                where r.ActiveStatus == "Y" && r.GroupId == 5
                        //                from st in M_StateDivMasterList
                        //            where st.StateCode == r.StateCode

                        //                select new PartyModel
                        //                {
                        //                    GroupId = r.GroupId,
                        //                    GroupName = "",
                        //                    PGroupId = r.PGroupId,
                        //                    UserPartyCode = r.UserPartyCode,
                        //                    PCode = r.PCode,
                        //                    PartyCode = r.PartyCode,
                        //                    PartyName = r.PartyName,
                        //                    ParentPartyCode = r.ParentPartyCode,
                        //                    ParentpartyName = "",
                        //                    Address1 = r.Address1,
                        //                    Address2 = r.Address2,
                        //                    StateCode = r.StateCode,
                        //                    StateName = st.StateName,
                        //                    CityCode = r.CityCode,
                        //                    CityName = r.CityName,
                        //                    Tehsil = r.Tehsil,
                        //                    PinCode = r.PinCode,
                        //                    PhoneNo = r.PhoneNo,
                        //                    MobileNo = r.MobileNo,
                        //                    FaxNo = r.FaxNo,
                        //                    PanNo = r.PanNo,
                        //                    GSTIN = r.TinNo,
                        //                    STaxNo = r.STaxNo,
                        //                    //CstNo = objPartyModel.CstNo;
                        //                    BankAccNo = r.BankAcNo,
                        //                    BankCode = r.BankCode,
                        //                    BankName = r.BankName,
                        //                    RequestTo = r.RequestTo,
                        //                    AccountVerify = r.AccountVerify,
                        //                    RecommandBy = r.RecommandBy,
                        //                    ContactPerson = r.ContactPerson,
                        //                    EmailAddress = r.E_MailAdd,
                        //                    ActiveStatus = r.ActiveStatus,
                        //                    OnWebsite = r.OnWebSite,
                        //                    CreditLimit = r.CreditLimit,
                        //                    Remarks = r.Remarks,
                        //                    RecTimeStamp = r.RecTimeStamp,
                        //                    NewFId1 = r.NewFld1,
                        //                    NewFId2 = r.NewFld2,
                        //                    NewFId3 = r.NewFld3,
                        //                    NewFId4 = r.NewFld4,
                        //                    Company = r.Company,
                        //                    RecvdCForm = r.RecvdCForm,
                        //                    objUserDetails = new User
                        //                    {
                        //                        UserName = "",
                        //                        Password = ""
                        //                    }
                        //                }
                        //                      ).FirstOrDefault();
                        objParty = (from p in (from r in entity.M_LedgerMaster
                                               where //r.ActiveStatus == "Y" &&  'Cmnted on 28Aug18
                                               r.GroupId == 5 && r.PartyCode==PartyCode
                                                   select new
                                                   {
                                                       GroupId = r.GroupId,
                                                       GroupName = "",
                                                       PGroupId = r.PGroupId,
                                                       UserPartyCode = r.UserPartyCode,
                                                       PCode = r.PCode,
                                                       PartyCode = r.PartyCode,
                                                       PartyName = r.PartyName,
                                                       ParentPartyCode = r.ParentPartyCode,
                                                       ParentpartyName = "",
                                                       Address1 = r.Address1,
                                                       Address2 = r.Address2,
                                                       StateCode = r.StateCode,
                                                       //StateName = st.StateName,
                                                       CityCode = r.CityCode,
                                                       CityName = r.CityName,
                                                       Tehsil = r.Tehsil,
                                                       PinCode = r.PinCode,
                                                       PhoneNo = r.PhoneNo,
                                                       MobileNo = r.MobileNo,
                                                       FaxNo = r.FaxNo,
                                                       PanNo = r.PanNo,
                                                       GSTIN = r.TinNo,
                                                       STaxNo = r.STaxNo,
                                                       //CstNo = objPartyModel.CstNo;
                                                       BankAccNo = r.BankAcNo,
                                                       BankCode = r.BankCode,
                                                       BankName = r.BankName,
                                                       RequestTo = r.RequestTo,
                                                       AccountVerify = r.AccountVerify,
                                                       RecommandBy = r.RecommandBy,
                                                       ContactPerson = r.ContactPerson,
                                                       EmailAddress = r.E_MailAdd,
                                                       ActiveStatus = r.ActiveStatus,
                                                       OnWebsite = r.OnWebSite,
                                                       CreditLimit = r.CreditLimit,
                                                       Remarks = r.Remarks,
                                                       RecTimeStamp = r.RecTimeStamp,
                                                       NewFId1 = r.NewFld1,
                                                       NewFId2 = r.NewFld2,
                                                       NewFId3 = r.NewFld3,
                                                       NewFId4 = r.NewFld4,
                                                       Company = r.Company,
                                                       RecvdCForm = r.RecvdCForm,
                                                       objUserDetails = new User
                                                       {
                                                           UserName = "",
                                                           Password = ""
                                                       }
                                                   }).AsEnumerable()
                                        from st in M_StateDivMasterList
                                        where p.StateCode == st.StateCode

                                        select new PartyModel
                                        {
                                            GroupId = p.GroupId,
                                            GroupName = "",
                                            PGroupId = p.PGroupId,
                                            UserPartyCode = p.UserPartyCode,
                                            PCode = p.PCode,
                                            PartyCode = p.PartyCode,
                                            PartyName = p.PartyName,
                                            ParentPartyCode = p.ParentPartyCode,
                                            ParentpartyName = "",
                                            Address1 = p.Address1,
                                            Address2 = p.Address2,
                                            StateCode = p.StateCode,
                                            StateName = st.StateName,
                                            CityCode = p.CityCode,
                                            CityName = p.CityName,
                                            Tehsil = p.Tehsil,
                                            PinCode = p.PinCode,
                                            PhoneNo = p.PhoneNo,
                                            MobileNo = p.MobileNo,
                                            FaxNo = p.FaxNo,
                                            PanNo = p.PanNo,
                                            GSTIN = p.GSTIN,
                                            STaxNo = p.STaxNo,
                                            //CstNo = objPartyModel.CstNo;
                                            BankAccNo = p.BankAccNo,
                                            BankCode = p.BankCode,
                                            BankName = p.BankName,
                                            RequestTo = p.RequestTo,
                                            AccountVerify = p.AccountVerify,
                                            RecommandBy = p.RecommandBy,
                                            ContactPerson = p.ContactPerson,
                                            EmailAddress = p.EmailAddress,
                                            ActiveStatus = p.ActiveStatus,
                                            OnWebsite = p.OnWebsite,
                                            CreditLimit = p.CreditLimit,
                                            Remarks = p.Remarks,
                                            RecTimeStamp = p.RecTimeStamp,
                                            NewFId1 = p.NewFId1,
                                            NewFId2 = p.NewFId2,
                                            NewFId3 = p.NewFId3,
                                            NewFId4 = p.NewFId4,
                                            Company = p.Company,
                                            RecvdCForm = p.RecvdCForm,
                                            objUserDetails = new User
                                            {
                                                UserName = "",
                                                Password = ""
                                            }
                                        }
                                              ).FirstOrDefault();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return objParty;
        }
        public List<PartyModel> GetAllPartyList(bool IsSupplier)
        {
            List<PartyModel> objPartyList = new List<PartyModel>();
            try
            {
                string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BKDHServices"].ConnectionString;
                SqlConnection SC = new SqlConnection(AppConnectionString);
                
                string query = "Select * from M_StateDivMaster where ActiveStatus='Y'";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = SC;
                SC.Close();
                SC.Open();
                List<StateModel> M_StateDivMasterList = new List<StateModel>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        M_StateDivMasterList.Add(new StateModel
                        {
                            StateCode = decimal.Parse(reader["StateCode"].ToString()),
                            StateName = reader["StateName"].ToString()
                        });
                    }
                }
                using (var entity=new BKDHEntities11())
                {

                    
                        //(from profile in entity.M_LedgerMaster
                        // where profile.ActiveStatus == "Y"
                                        
                        // .AsEnumerable() // Continue in memory
                        // join param in M_StateDivMasterList on
                        //     new { profile.StateCode } equals
                        //     new { param.StateCode }
                        // select profile).ToList();
                        objPartyList = (from p in (from r in entity.M_LedgerMaster
                                        where  r.PartyCode != "WR"
                                                   from g in entity.M_GroupMaster
                                        where g.GroupId == r.GroupId
                                        from s in entity.M_LedgerMaster
                                        where s.PartyCode == r.ParentPartyCode                                         
                                        select new
                                        {
                                            GroupId = r.GroupId,
                                            GroupName = g.GroupName,
                                            PGroupId = r.PGroupId,
                                            UserPartyCode = r.UserPartyCode,
                                            PCode = r.PCode,
                                            PartyCode = r.PartyCode,
                                            PartyName = r.PartyName,
                                            ParentPartyCode = r.ParentPartyCode,
                                            ParentpartyName = s.PartyName,
                                            Address1 = r.Address1,
                                            Address2 = r.Address2,
                                            StateCode = r.StateCode,
                                            //StateName = st.StateName,
                                            CityCode = r.CityCode,
                                            CityName = r.CityName,
                                            Tehsil = r.Tehsil,
                                            PinCode = r.PinCode,
                                            PhoneNo = r.PhoneNo,
                                            MobileNo = r.MobileNo,
                                            FaxNo = r.FaxNo,
                                            PanNo = r.PanNo,
                                            GSTIN = r.TinNo,
                                            STaxNo = r.STaxNo,
                                            //CstNo = objPartyModel.CstNo;
                                            BankAccNo = r.BankAcNo,
                                            BankCode = r.BankCode,
                                            BankName = r.BankName,
                                            RequestTo = r.RequestTo,
                                            AccountVerify = r.AccountVerify,
                                            RecommandBy = r.RecommandBy,
                                            ContactPerson = r.ContactPerson,
                                            EmailAddress = r.E_MailAdd,
                                            ActiveStatus = r.ActiveStatus,
                                            OnWebsite = r.OnWebSite,
                                            CreditLimit = r.CreditLimit,
                                            Remarks = r.Remarks,
                                            RecTimeStamp = r.RecTimeStamp,
                                            NewFId1 = r.NewFld1,
                                            NewFId2 = r.NewFld2,
                                            NewFId3 = r.NewFld3,
                                            NewFId4 = r.NewFld4,
                                            Company = r.Company,
                                            RecvdCForm = r.RecvdCForm,
                                            objUserDetails = new User
                                            {
                                                UserName = "",
                                                Password = ""
                                            }
                                        }
                                        
                                        )
                                        
                                        .AsEnumerable()
                                        from st in M_StateDivMasterList where st.StateCode== p.StateCode

                                        select new PartyModel
                                        {
                                            GroupId = p.GroupId,
                                            GroupName = p.GroupName,
                                            PGroupId = p.PGroupId,
                                            UserPartyCode = p.UserPartyCode,
                                            PCode = p.PCode,
                                            PartyCode = p.PartyCode,
                                            PartyName = p.PartyName,
                                            ParentPartyCode = p.ParentPartyCode,
                                            ParentpartyName = p.ParentpartyName,
                                            Address1 = p.Address1,
                                            Address2 = p.Address2,
                                            StateCode = p.StateCode,
                                            StateName = st.StateName,
                                            CityCode = p.CityCode,
                                            CityName = p.CityName,
                                            Tehsil = p.Tehsil,
                                            PinCode = p.PinCode,
                                            PhoneNo = p.PhoneNo,
                                            MobileNo = p.MobileNo,
                                            FaxNo = p.FaxNo,
                                            PanNo = p.PanNo,
                                            GSTIN = p.GSTIN,
                                            STaxNo = p.STaxNo,
                                            //CstNo = objPartyModel.CstNo;
                                            BankAccNo = p.BankAccNo,
                                            BankCode = p.BankCode,
                                            BankName = p.BankName,
                                            RequestTo = p.RequestTo,
                                            AccountVerify = p.AccountVerify,
                                            RecommandBy = p.RecommandBy,
                                            ContactPerson = p.ContactPerson,
                                            EmailAddress = p.EmailAddress,
                                            ActiveStatus = p.ActiveStatus,
                                            OnWebsite = p.OnWebsite,
                                            CreditLimit = p.CreditLimit,
                                            Remarks = p.Remarks,
                                            RecTimeStamp = p.RecTimeStamp,
                                            NewFId1 = p.NewFId1,
                                            NewFId2 = p.NewFId2,
                                            NewFId3 = p.NewFId3,
                                            NewFId4 = p.NewFId4,
                                            Company = p.Company,
                                            RecvdCForm = p.RecvdCForm,
                                            objUserDetails = new User
                                            {
                                                UserName = p.objUserDetails.UserName,
                                                Password = p.objUserDetails.Password
                                            }
                                        }
                       ).ToList();
                    }
                if (IsSupplier == false)
                {
                    objPartyList = objPartyList.Where(r => r.GroupId == 100).ToList();
                }
                else
                {
                    objPartyList = objPartyList.Where(r => r.GroupId == 105).ToList();
                }

            }
            catch(Exception ex)
            {

            }
            return objPartyList;
        }

        public string GetPartyCode(string SelectedParentPartyCode,string SelectedGroupId)
        {
            string PartyCode = "";
            decimal GroupId = 0;
            try
            {
                if (!string.IsNullOrEmpty(SelectedGroupId))
                {
                    GroupId = decimal.Parse(SelectedGroupId);
                }
                using(var entity=new BKDHEntities11())
                {
                    if (SelectedGroupId == "" && SelectedParentPartyCode == "")
                    {
                        decimal MaxPcode = (from r in entity.M_LedgerMaster
                                            where r.GroupId == 5 //&& r.ActiveStatus == "Y"  'Cmnted on 28Aug18
                                            select r.PCode
                                         ).DefaultIfEmpty(0).Max();
                        MaxPcode = MaxPcode + 1;
                        if (MaxPcode.ToString().Length == 1)
                        {
                            PartyCode = "S" + "0"+MaxPcode.ToString().ToUpper().Trim();
                        }
                        else
                        {
                            PartyCode = "S" +MaxPcode.ToString().ToUpper().Trim();
                        }
                        
                    }
                    else
                    {
                        decimal MaxPcode = (from r in entity.M_LedgerMaster
                                            where r.GroupId == GroupId //&& r.ActiveStatus == "Y" 'Cmnted on 28Aug18
                                            select r.PCode
                                         ).DefaultIfEmpty(0).Max();
                        MaxPcode = MaxPcode + 1;
                        string GroupPrefix = (from r in entity.M_GroupMaster
                                              where r.ActiveStatus == "Y" && r.GroupId == GroupId
                                              select r.Prefix
                                            ).FirstOrDefault();
                        string StrPcode= MaxPcode.ToString();
                        if (StrPcode.Count() < 2)
                        {
                            var countNum = StrPcode.Count();
                            var ToBeAddedDigits = 2 - countNum;
                            //strMaxUserSBillNo = maxUserSBillNo.ToString().PadLeft(ToBeAddedDigits,'0');
                            for (var j = 0; j < ToBeAddedDigits; j++)
                            {
                                StrPcode = "0" + StrPcode;
                            }
                            // maxUserSBillNo = decimal.Parse(strMaxUserSBillNo);
                        }
                        PartyCode = SelectedParentPartyCode + GroupPrefix.ToUpper().Trim() + StrPcode.ToUpper().Trim();
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return PartyCode;
        }

        public PartyModel IsDuplicatePartyUserPartyCode(string IsActionType, string PartyCode, string UserPartyCode)
        {
            PartyModel objPartyDetail = new PartyModel();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    objPartyDetail.GroupName = "";
                    if (UserPartyCode == "")
                    {
                        objPartyDetail.GroupName = "OK";
                        return objPartyDetail;
                    }
                    objPartyDetail = (from r in entity.M_LedgerMaster
                                  where r.UserPartyCode == UserPartyCode && r.PartyCode != PartyCode
                                  select new PartyModel {
                                      PartyCode=r.PartyCode
                                  }
                                    ).FirstOrDefault();
                   
                }
            }
            catch (Exception ex)
            {

            }
            return objPartyDetail;
        }

        public ResponseDetail IsDuplicatePartyUserName(string IsActionType,string PartyCode,string UserName)
        {
            ResponseDetail objResponse = new ResponseDetail();
            try
            {
                using(var entity=new BKDHEntities11())
                {
                    //if (IsActionType == "Add")
                    //{
                    //    var result = (from r in entity.Inv_M_UserMaster
                    //                  where r.UserName == UserName
                    //                  select r
                    //                ).FirstOrDefault();
                    //    if (result != null)
                    //    {
                    //        objResponse.ResponseStatus = "FAILED";
                    //        objResponse.ResponseMessage = "Match Found!";
                    //    }
                    //    else
                    //    {
                    //        objResponse.ResponseStatus = "OK";
                    //        objResponse.ResponseMessage = "No Match Found!";
                    //    }
                    //}
                    //else if (IsActionType == "Edit")
                    //{
                    objResponse.ResponseStatus = "OK";
                    objResponse.ResponseMessage = "No Match Found!";
                    var result = (from r in entity.Inv_M_UserMaster
                                      where r.UserName == UserName && r.BranchCode != PartyCode
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
                   // }
               }
            }
            catch(Exception ex)
            {

            }
            return objResponse;
        }

        public List<GroupModel> GetGroupList()
        {
            List<GroupModel> objGroupList = new List<GroupModel>();
            try
            {
                using (var entity = new BKDHEntities11())
                {
                    objGroupList = (from r in entity.M_GroupMaster
                                    where r.ActiveStatus == "Y"
                                    
                                    select new GroupModel
                                    {
                                        GroupName = r.GroupName,
                                        GroupId = r.GroupId
                                    }

                                  ).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {

            }
            return objGroupList;
        }

        //private void ShiftDataintoTemptable(string TblName, string TempTblName, string columns, string wherecond)
        //{
        //    try
        //    {
        //        string AppConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["InventoryServices"].ConnectionString;
        //        using (SqlConnection SC = new SqlConnection(AppConnectionString))
        //        {
        //            string query = "INSERT " + TempTblName + " Select *" + columns + " FROM " + TblName + " WHERE 1=1 " + wherecond;
        //            SC.Open();
        //            SqlCommand cmd = new SqlCommand(query, SC);
        //            cmd.ExecuteNonQuery();
        //            SC.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
    }

    
}
