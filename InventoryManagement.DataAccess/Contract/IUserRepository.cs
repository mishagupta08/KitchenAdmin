using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.DataAccess.Contract
{
    public interface IUserRepository
    {
        List<User> GetUserList();
        ResponseDetail SetUserRights(List<UserPermissionMasterModel> objPermissionList);
        List<UserPermissionMasterModel> ListUserPermittedMenus(decimal UserId);
        List<User> GetAllUserList();
        User GetUser(int UserId);
        decimal GetPartyGroupId(string PartyCode);
        ResponseDetail AddEditUserDetails(User objModel, User LoggedUser);
        List<PartyModel> GetPartyListForUsers();
        ResponseDetail IsDuplicateUserName(string IsActionType, string UserCode, string UserName);
    }
}
