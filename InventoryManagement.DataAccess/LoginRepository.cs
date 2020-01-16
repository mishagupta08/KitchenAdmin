using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class LoginRepository:ILoginRepository
    {
       LoginAPIController objLoginAPI = new LoginAPIController();
       public User ValidateUser(LoginModel model)
       {
            User objResponse = objLoginAPI.ValidateUser(model);
            return objResponse;
       }
        public ResponseDetail ChangePassword(ChangePassword model)
        {
            ResponseDetail objResponse = objLoginAPI.ChangePassword(model);
            return objResponse;
        }
    }
}