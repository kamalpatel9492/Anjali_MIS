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
	public class DIA_CassettController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: DIA_Cassett
        public ActionResult Index()
        {
            var dIA_Cassett = db.DIA_Cassett.Include(d => d.SYS_Company).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_Cassett.ToList());
        }

        // GET: DIA_Cassett/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Cassett dIA_Cassett = db.DIA_Cassett.Find(id);
            if (dIA_Cassett == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Cassett);
        }

        // GET: DIA_Cassett/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			DIA_Cassett _dIA_Cassett = new DIA_Cassett();
			return View("Edit", _dIA_Cassett);
		}

        // POST: DIA_Cassett/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CassettsID,CassettsNo,Capacity,CompanyID,UserID,Created,Modified,Remarks,StatusID")] DIA_Cassett dIA_Cassett)
        {
            if (ModelState.IsValid)
            {
				dIA_Cassett.Created = DateTime.Now;
				dIA_Cassett.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					dIA_Cassett.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.DIA_Cassett.Add(dIA_Cassett);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Cassett.CompanyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Cassett.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Cassett.UserID);
            return View(dIA_Cassett);
        }

        // GET: DIA_Cassett/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Cassett dIA_Cassett = db.DIA_Cassett.Find(id);
            if (dIA_Cassett == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Cassett.CompanyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Cassett.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Cassett.UserID);
            return View(dIA_Cassett);
        }

        // POST: DIA_Cassett/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CassettsID,CassettsNo,Capacity,CompanyID,UserID,Created,Modified,Remarks,StatusID")] DIA_Cassett dIA_Cassett)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIA_Cassett).State = EntityState.Modified;
				dIA_Cassett.Modified = Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					dIA_Cassett.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Cassett.CompanyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Cassett.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Cassett.UserID);
            return View(dIA_Cassett);
        }

        // GET: DIA_Cassett/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Cassett dIA_Cassett = db.DIA_Cassett.Find(id);
            if (dIA_Cassett == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Cassett);
        }

        // POST: DIA_Cassett/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DIA_Cassett dIA_Cassett = db.DIA_Cassett.Find(id);
            db.DIA_Cassett.Remove(dIA_Cassett);
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
