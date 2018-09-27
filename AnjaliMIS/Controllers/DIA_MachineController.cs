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
	public class DIA_MachineController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: DIA_Machine
        public ActionResult Index()
        {
            var dIA_Machine = db.DIA_Machine.Include(d => d.SYS_Company).Include(d => d.SEC_User);
            return View(dIA_Machine.ToList());
        }

        // GET: DIA_Machine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Machine dIA_Machine = db.DIA_Machine.Find(id);
            if (dIA_Machine == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Machine);
        }

        // GET: DIA_Machine/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			DIA_Machine _dIA_Machine = new DIA_Machine();
			return View("Edit", _dIA_Machine);
		}

        // POST: DIA_Machine/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MachineID,MachineNo,BatchNo,UserID,CompanyID,Created,Modified,Remarks,IsActive")] DIA_Machine dIA_Machine)
        {
            if (ModelState.IsValid)
            {
				dIA_Machine.Created = DateTime.Now;
				dIA_Machine.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					dIA_Machine.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.DIA_Machine.Add(dIA_Machine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Machine.CompanyID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Machine.UserID);
            return View(dIA_Machine);
        }

        // GET: DIA_Machine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Machine dIA_Machine = db.DIA_Machine.Find(id);
            if (dIA_Machine == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Machine.CompanyID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Machine.UserID);
            return View(dIA_Machine);
        }

        // POST: DIA_Machine/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MachineID,MachineNo,BatchNo,UserID,CompanyID,Created,Modified,Remarks,IsActive")] DIA_Machine dIA_Machine)
        {
            if (dIA_Machine.MachineID > 0)
            {
                if (dIA_Machine.Remarks == null || dIA_Machine.Remarks == "")
                {
                    ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Machine.CompanyID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Machine.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(dIA_Machine);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(dIA_Machine).State = EntityState.Modified;
				dIA_Machine.Modified =Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					dIA_Machine.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Machine.CompanyID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Machine.UserID);
            return View(dIA_Machine);
        }

        // GET: DIA_Machine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Machine dIA_Machine = db.DIA_Machine.Find(id);
            if (dIA_Machine == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Machine);
        }

        // POST: DIA_Machine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DIA_Machine dIA_Machine = db.DIA_Machine.Find(id);
            db.DIA_Machine.Remove(dIA_Machine);
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
