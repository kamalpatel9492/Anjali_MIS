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

        public static int GetCompanyID()
        {
            return 4;
        }

        public static string GetNextNumber(string _NumberFor)
        {
            var db = new DB_A157D8_AnjaliMISEntities1();

            Int32 _NewIssueNo = 0;
            String _NewIssueReturnNo = "";

            if (_NumberFor == "Issue")
            {
                Int32 TotalForMonth = db.INV_IssueReturn.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year && p.IssueReturnNo.Contains("IS")).Count();
                Int32 NextCount = TotalForMonth + 1;
                _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                _NewIssueReturnNo = "IS-" + _NewIssueNo;

                #region Check No
                INV_IssueReturn _INV_IssueReturnNo = db.INV_IssueReturn.Where(w => w.IssueReturnNo == _NewIssueReturnNo).FirstOrDefault();
                if (_INV_IssueReturnNo != null)
                {
                    NextCount = NextCount + 1;
                    _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                    _NewIssueReturnNo = "IS-" + _NewIssueNo;
                }
                #endregion Check No

            }
            if (_NumberFor == "Return")
            {
                Int32 TotalForMonth = db.INV_IssueReturn.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year && p.IssueReturnNo.Contains("RN")).Count();
                Int32 NextCount = TotalForMonth + 1;
                _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                _NewIssueReturnNo = "RN-" + _NewIssueNo;

                #region Check No
                INV_IssueReturn _INV_IssueReturnNo = db.INV_IssueReturn.Where(w => w.IssueReturnNo == _NewIssueReturnNo).FirstOrDefault();
                if (_INV_IssueReturnNo != null)
                {
                    NextCount = NextCount + 1;
                    _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                    _NewIssueReturnNo = "RN-" + _NewIssueNo;
                }
                #endregion Check No

            }
            if (_NumberFor == "Invoice")
            {
                Int32 TotalForMonth = db.INV_Invoice.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year).Count();
                Int32 NextCount = TotalForMonth + 1;
                _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                _NewIssueReturnNo = "INV-" + _NewIssueNo;

                #region Check No
                INV_Invoice _INV_IssueReturnNo = db.INV_Invoice.Where(w => w.InvoiceNo == _NewIssueReturnNo).FirstOrDefault();
                if (_INV_IssueReturnNo != null)
                {
                    NextCount = NextCount + 1;
                    _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                    _NewIssueReturnNo = "INV-" + _NewIssueNo;
                }
                #endregion Check No

            }
            if (_NumberFor == "PO")
            {
                Int32 TotalForMonth = db.INV_PurchaseOrder.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year).Count();
                Int32 NextCount = TotalForMonth + 1;
                _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                _NewIssueReturnNo = "PO-" + _NewIssueNo;

                #region Check No
                INV_PurchaseOrder _INV_IssueReturnNo = db.INV_PurchaseOrder.Where(w => w.PONo == _NewIssueReturnNo).FirstOrDefault();
                if (_INV_IssueReturnNo != null)
                {
                    NextCount = NextCount + 1;
                    _NewIssueNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                    _NewIssueReturnNo = "PO-" + _NewIssueNo;
                }
                #endregion Check No

            }

            return _NewIssueReturnNo;
        }

        public static int GetStatusPending()
        {
            return 1;
        }
    }
}