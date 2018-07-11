using System;
using System.Collections.Generic;
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
    }
}
