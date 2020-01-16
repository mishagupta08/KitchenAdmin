using InventoryManagement.App_Start;
using InventoryManagement.Business;
using InventoryManagement.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        DashboardManager objDashboardManager = new DashboardManager();
        [SessionExpire]
        public ActionResult Dashboard()
        {
            string LoginPartyCode = "";
            if (Session["LoginUser"] != null)
            {
                LoginPartyCode = (Session["LoginUser"] as User).PartyCode;
            }
            decimal FWalletBalance = objDashboardManager.GetFWalletBalance(LoginPartyCode);
            ViewBag.WalletBalance = FWalletBalance;
            return View();
        }
    }
}