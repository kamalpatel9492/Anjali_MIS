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
	public class SYS_PartyTypeController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: SYS_PartyType
        public ActionResult Index()
        {
            return View(db.SYS_PartyType.ToList());
        }

        // GET: SYS_PartyType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SYS_PartyType sYS_PartyType = db.SYS_PartyType.Find(id);
            if (sYS_PartyType == null)
            {
                return HttpNotFound();
            }
            return View(sYS_PartyType);
        }

        // GET: SYS_PartyType/Create
        public ActionResult Create()
        {
			SYS_PartyType sYS_PartyType = new SYS_PartyType();
			return View("Edit", sYS_PartyType);
		}

        // POST: SYS_PartyType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyTypeID,PartyType,Remarks,Created,Modified,UserID")] SYS_PartyType sYS_PartyType)
        {
            if (ModelState.IsValid)
            {
				sYS_PartyType.Created = DateTime.Now;
				sYS_PartyType.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					sYS_PartyType.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SYS_PartyType.Add(sYS_PartyType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sYS_PartyType);
        }

        // GET: SYS_PartyType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SYS_PartyType sYS_PartyType = db.SYS_PartyType.Find(id);
            if (sYS_PartyType == null)
            {
                return HttpNotFound();
            }
            return View(sYS_PartyType);
        }

        // POST: SYS_PartyType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartyTypeID,PartyType,Remarks,Created,Modified,UserID")] SYS_PartyType sYS_PartyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sYS_PartyType).State = EntityState.Modified;
				sYS_PartyType.Modified = Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					sYS_PartyType.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sYS_PartyType);
        }

        // GET: SYS_PartyType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SYS_PartyType sYS_PartyType = db.SYS_PartyType.Find(id);
            if (sYS_PartyType == null)
            {
                return HttpNotFound();
            }
            return View(sYS_PartyType);
        }

        // POST: SYS_PartyType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SYS_PartyType sYS_PartyType = db.SYS_PartyType.Find(id);
            db.SYS_PartyType.Remove(sYS_PartyType);
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
