using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    var obj = db.SEC_User.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password) && a.IsActive == true).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.UserName.ToString();

                        #region SEC_LoginHistory
                        SEC_LoginHistory sEC_LoginHistory = new SEC_LoginHistory();
                        sEC_LoginHistory.Created = DateTime.Now;
                        sEC_LoginHistory.Modified = DateTime.Now;
                        sEC_LoginHistory.LoginTime = DateTime.Now;
                        sEC_LoginHistory.LogoutTime = DateTime.Now;
                        sEC_LoginHistory.Remarls = "Login..";
                        if (Session["UserID"] != null)
                        {
                            sEC_LoginHistory.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }
                        if (ModelState.IsValid)
                        {
                            db.SEC_LoginHistory.Add(sEC_LoginHistory);
                            db.SaveChanges();
                            if (sEC_LoginHistory.LoginHistoryID > 0)
                            {
                                Session["LoginHistoryID"] = sEC_LoginHistory.LoginHistoryID.ToString();
                            }
                        }

                        #endregion SEC_LoginHistory

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
            try
            {
                if (Session["LoginHistoryID"] != null)
                {
                    #region SEC_LoginHistory Update
                    using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
                    {
                        Int32 id = Convert.ToInt16(Session["LoginHistoryID"].ToString());
                        SEC_LoginHistory sEC_LoginHistory = db.SEC_LoginHistory.Find(id);
                        db.Entry(sEC_LoginHistory).State = EntityState.Modified;
                        sEC_LoginHistory.Modified = DateTime.Now;
                        sEC_LoginHistory.LogoutTime = DateTime.Now;
                        sEC_LoginHistory.Remarls = "Logout..";
                        db.SaveChanges();
                    }
                    #endregion SEC_LoginHistory Update

                }
            }
            catch (Exception ex)
            {

            }

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
