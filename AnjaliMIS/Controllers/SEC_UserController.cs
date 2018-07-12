using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;
using static AnjaliMIS.CommonConfig;

namespace AnjaliMIS.Controllers
{
    [SessionTimeout]
    public class SEC_UserController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: SEC_User
        public ActionResult Index()
        {
            var sEC_User = db.SEC_User.Include(s => s.EMP_Employee1).Include(s => s.SEC_User2);
            return View(sEC_User.ToList());
        }

        // GET: SEC_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_User sEC_User = db.SEC_User.Find(id);
            if (sEC_User == null)
            {
                return HttpNotFound();
            }
            return View(sEC_User);
        }

        // GET: SEC_User/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName");
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName");
            SEC_User sEC_User = new SEC_User();
            return View("Edit", sEC_User);
        }

        // POST: SEC_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,CreatedByUserID,EmployeeID,UserName,Password,IsAdmin,IsActive,Created,Modified,Remarks")] SEC_User sEC_User)
        {
            if (ModelState.IsValid)
            {
                sEC_User.Created = DateTime.Now;
                sEC_User.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    sEC_User.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SEC_User.Add(sEC_User);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_User.CreatedByUserID);
            return View(sEC_User);
        }

        // GET: SEC_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_User sEC_User = db.SEC_User.Find(id);
            if (sEC_User == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_User.CreatedByUserID);
            return View(sEC_User);
        }

        // POST: SEC_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,CreatedByUserID,EmployeeID,UserName,Password,IsAdmin,IsActive,Created,Modified,Remarks")] SEC_User sEC_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sEC_User).State = EntityState.Modified;
                sEC_User.Created = Convert.ToDateTime(sEC_User.Created);
                sEC_User.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    sEC_User.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_User.CreatedByUserID);
            return View(sEC_User);
        }

        // GET: SEC_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_User sEC_User = db.SEC_User.Find(id);
            if (sEC_User == null)
            {
                return HttpNotFound();
            }
            return View(sEC_User);
        }

        // POST: SEC_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SEC_User sEC_User = db.SEC_User.Find(id);
            db.SEC_User.Remove(sEC_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
