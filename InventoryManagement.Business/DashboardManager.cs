using InventoryManagement.Business.Contract;
using InventoryManagement.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.Business
{
    public class DashboardManager: IDashboardManager
    {
        DashboardRepository objDashboardRepo = new DashboardRepository();
        public decimal GetFWalletBalance(string LoginPartyCode)
        {
            return (objDashboardRepo.GetFWalletBalance(LoginPartyCode));
        }
    }
}