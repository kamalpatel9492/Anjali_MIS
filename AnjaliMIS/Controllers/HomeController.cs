using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;

namespace AnjaliMIS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModal objUser)
        {
            if (ModelState.IsValid)
            {
                using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
                {
                    var obj = db.SEC_User.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Enter valid Credentials..");
                    }
                }
            }
            return View(objUser);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("index");
        }

        // GET: PartialViewMenu
        public ActionResult _MainMenu()
        {
            DB_A157D8_AnjaliMISEntities1 usersEntities = new DB_A157D8_AnjaliMISEntities1();
            List<MenuViewModal> _MenuViewModal = new List<MenuViewModal>();
            // var data = usersEntities.PR_SEC_UserPrivileges_SelectByUserID(Convert.ToInt32(Session["UserID"].ToString())).ToList();


            using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
            {
                try
                {
                    //var outParam = new SqlParameter("@UserID", SqlDbType.Int);
                    //outParam.Direction = ParameterDirection.Output;

                    var sql = "exec PR_SEC_UserPrivileges_SelectByUserID @UserID";

                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@UserID", Convert.ToInt32(Session["UserID"].ToString())));

                    //parameterList.Add(outParam);
                    SqlParameter[] parameters = parameterList.ToArray();

                    var data1 = db.Database.SqlQuery<MenuViewModal>(sql, parameters).ToList();

                    _MenuViewModal = data1.Select(dataElement => new MenuViewModal()
                    {
                        ModuleID = dataElement.ModuleID,
                        ModuleName = dataElement.ModuleName,
                        Controller = dataElement.Controller,
                        Action = dataElement.Action,
                        URL = dataElement.URL,
                        IconName = dataElement.IconName,
                        StrictlyStop = dataElement.StrictlyStop,
                        Sequence = dataElement.Sequence,
                        UserID = dataElement.UserID
                    }).ToList();
                }
                catch (Exception ex)
                {
                    //SaveErrorLog("JobRepository", "GetJobListByPaging", ex);

                }
            }


            return PartialView("_MainMenu", _MenuViewModal);
            //return PartialView(_MenuViewModal);
        }
    }
}
