using InventoryManagement.API.Controllers;
using InventoryManagement.DataAccess.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventoryManagement.DataAccess
{
    public class DashboardRepository: IDashboardRepository
    {
        DashboardAPIController objDashboardApi = new DashboardAPIController();
        public decimal GetFWalletBalance(string LoginPartyCode)
        {
            return (objDashboardApi.GetFWalletBalance(LoginPartyCode));
        }
    }
}