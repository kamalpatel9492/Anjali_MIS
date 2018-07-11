using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;

namespace AnjaliMIS.Controllers
{
    public class INV_UnitController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_Unit
        public ActionResult Index()
        {
            return View(db.INV_Unit.ToList());
        }

        // GET: INV_Unit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Unit iNV_Unit = db.INV_Unit.Find(id);
            if (iNV_Unit == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Unit);
        }

        // GET: INV_Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INV_Unit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UnitID,UserID,Unit,Remarks,Created,Modified")] INV_Unit iNV_Unit)
        {
            if (ModelState.IsValid)
            {
                db.INV_Unit.Add(iNV_Unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iNV_Unit);
        }

        // GET: INV_Unit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Unit iNV_Unit = db.INV_Unit.Find(id);
            if (iNV_Unit == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Unit);
        }

        // POST: INV_Unit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UnitID,UserID,Unit,Remarks,Created,Modified")] INV_Unit iNV_Unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_Unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iNV_Unit);
        }

        // GET: INV_Unit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Unit iNV_Unit = db.INV_Unit.Find(id);
            if (iNV_Unit == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Unit);
        }

        // POST: INV_Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_Unit iNV_Unit = db.INV_Unit.Find(id);
            db.INV_Unit.Remove(iNV_Unit);
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
