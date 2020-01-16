using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagement.App_Start
{
    public class SessionCheck 
    {        
    }

    public class SessionExpire : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current == null ||HttpContext.Current.Session == null || HttpContext.Current.Session["LoginUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/SessionExpire");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}