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
    public class SEC_LoginHistoryController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: SEC_LoginHistory
        public ActionResult Index()
        {
            var sEC_LoginHistory = db.SEC_LoginHistory.OrderByDescending(o => o.LoginTime).OrderByDescending(o => o.LogoutTime).Include(s => s.SEC_User);
            return View(sEC_LoginHistory.ToList());
        }

        // GET: SEC_LoginHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_LoginHistory sEC_LoginHistory = db.SEC_LoginHistory.Find(id);
            if (sEC_LoginHistory == null)
            {
                return HttpNotFound();
            }
            return View(sEC_LoginHistory);
        }

        // GET: SEC_LoginHistory/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            return View();
        }

        // POST: SEC_LoginHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginHistoryID,UserID,LoginTime,LogoutTime,Created,Remarls,Modified")] SEC_LoginHistory sEC_LoginHistory)
        {
            if (ModelState.IsValid)
            {
                db.SEC_LoginHistory.Add(sEC_LoginHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_LoginHistory.UserID);
            return View(sEC_LoginHistory);
        }

        // GET: SEC_LoginHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_LoginHistory sEC_LoginHistory = db.SEC_LoginHistory.Find(id);
            if (sEC_LoginHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_LoginHistory.UserID);
            return View(sEC_LoginHistory);
        }

        // POST: SEC_LoginHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginHistoryID,UserID,LoginTime,LogoutTime,Created,Remarls,Modified")] SEC_LoginHistory sEC_LoginHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sEC_LoginHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_LoginHistory.UserID);
            return View(sEC_LoginHistory);
        }

        // GET: SEC_LoginHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_LoginHistory sEC_LoginHistory = db.SEC_LoginHistory.Find(id);
            if (sEC_LoginHistory == null)
            {
                return HttpNotFound();
            }
            return View(sEC_LoginHistory);
        }

        // POST: SEC_LoginHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SEC_LoginHistory sEC_LoginHistory = db.SEC_LoginHistory.Find(id);
            db.SEC_LoginHistory.Remove(sEC_LoginHistory);
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
