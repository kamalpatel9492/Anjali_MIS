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
            private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext ctx = HttpContext.Current;
                if (HttpContext.Current.Session["UserID"] == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/index");
                    return;
                }
                else if (HttpContext.Current.Session["UserID"] != null)
                {
                    Int32 _UserID = 0;
                    if (HttpContext.Current.Session["UserID"] != null)
                    {
                        _UserID = Convert.ToInt16(HttpContext.Current.Session["UserID"].ToString());
                    }
                    if (_UserID > 0)
                    {
                        string actionName = filterContext.ActionDescriptor.ActionName;
                        string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                        Boolean UserIsAdmin = (db.SEC_User.Where(e => e.UserID == _UserID && e.IsAdmin == true)).Any();

                        if (!UserIsAdmin)
                        {
                            Boolean UserHasRights = (db.SEC_UserPrivileges.Where(e => e.UserID == _UserID && e.SYS_Module.Controller == controllerName).ToList()).Any();
                            if (UserHasRights == false)
                            {
                                filterContext.Result = new RedirectResult("~/Home/index");
                                return;
                            }
                        }
                    }

                }
                base.OnActionExecuting(filterContext);
            }
        }

        public static int GetFinYearID()
        {
            return 2;
        }
    }
}