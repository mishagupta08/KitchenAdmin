using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class UserManager: IUserManager
    {
        UserRepository objUserAPI = new UserRepository();
        public List<User> GetUserList()
        {
            return (objUserAPI.GetUserList());
        }
        public ResponseDetail SetUserRights(List<UserPermissionMasterModel> objPermissionList)
        {
            return (objUserAPI.SetUserRights(objPermissionList));
        }
        public List<UserPermissionMasterModel> ListUserPermittedMenus(decimal UserId)
        {
            return (objUserAPI.ListUserPermittedMenus(UserId));
        }
        public List<User> GetAllUserList()
        {
            return (objUserAPI.GetAllUserList());
        }
        public User GetUser(int UserId)
        {
            return (objUserAPI.GetUser(UserId));
        }
        public decimal GetPartyGroupId(string PartyCode)
        {
            return (objUserAPI.GetPartyGroupId(PartyCode));
        }
        public ResponseDetail AddEditUserDetails(User objModel, User LoggedUser)
        {
            return (objUserAPI.AddEditUserDetails(objModel, LoggedUser));
        }
        public List<PartyModel> GetPartyListForUsers()
        {
            return (objUserAPI.GetPartyListForUsers());
        }
        public ResponseDetail IsDuplicateUserName(string IsActionType, string UserCode, string UserName)
        {
            return (objUserAPI.IsDuplicateUserName(IsActionType, UserCode, UserName));
        }
    }
}