using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Business.Contract
{
    public interface ILoginManager
    {
        User ValidateUser(LoginModel model);
        ResponseDetail ChangePassword(ChangePassword model);
    }
}
