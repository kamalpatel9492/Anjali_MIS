using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;

namespace AnjaliMIS
{
    public class CommonConfig
    {
        public class SessionTimeoutAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/index");
                    return;
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}