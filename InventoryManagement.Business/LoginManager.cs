using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class LoginManager:ILoginManager
    {
        LoginRepository objLoginRepo = new LoginRepository();
        public User ValidateUser(LoginModel model)
        {
            User objResponse = objLoginRepo.ValidateUser(model);
            return objResponse;
        }
        public ResponseDetail ChangePassword(ChangePassword model)
        {
            ResponseDetail objResponse = objLoginRepo.ChangePassword(model);
            return objResponse;
        }
    }
}