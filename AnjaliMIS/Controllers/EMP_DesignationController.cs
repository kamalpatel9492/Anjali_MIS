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
	public class EMP_DesignationController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: EMP_Designation
        public ActionResult Index()
        {
            var eMP_Designation = db.EMP_Designation.Include(e => e.SEC_User);
            return View(eMP_Designation.ToList());
        }

        // GET: EMP_Designation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Designation eMP_Designation = db.EMP_Designation.Find(id);
            if (eMP_Designation == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Designation);
        }

        // GET: EMP_Designation/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			EMP_Designation eMP_Designation = new EMP_Designation();
			return View("Edit", eMP_Designation);
		}

        // POST: EMP_Designation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignationID,Designation,Remarks,Created,Modified,UserID")] EMP_Designation eMP_Designation)
        {
            if (ModelState.IsValid)
            {
				eMP_Designation.Created = DateTime.Now;
				eMP_Designation.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					eMP_Designation.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.EMP_Designation.Add(eMP_Designation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Designation.UserID);
            return View(eMP_Designation);
        }

        // GET: EMP_Designation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Designation eMP_Designation = db.EMP_Designation.Find(id);
            if (eMP_Designation == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Designation.UserID);
            return View(eMP_Designation);
        }

        // POST: EMP_Designation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignationID,Designation,Remarks,Created,Modified,UserID")] EMP_Designation eMP_Designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMP_Designation).State = EntityState.Modified;
				eMP_Designation.Modified = Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					eMP_Designation.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Designation.UserID);
            return View(eMP_Designation);
        }

        // GET: EMP_Designation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Designation eMP_Designation = db.EMP_Designation.Find(id);
            if (eMP_Designation == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Designation);
        }

        // POST: EMP_Designation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMP_Designation eMP_Designation = db.EMP_Designation.Find(id);
            db.EMP_Designation.Remove(eMP_Designation);
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
