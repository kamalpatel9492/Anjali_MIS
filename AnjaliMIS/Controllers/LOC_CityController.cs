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
    public class LOC_CityController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: LOC_City
        public ActionResult Index()
        {
            var lOC_City = db.LOC_City.Include(l => l.LOC_State).Include(l => l.SEC_User);
            return View(lOC_City.ToList());
        }

        // GET: LOC_City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_City lOC_City = db.LOC_City.Find(id);
            if (lOC_City == null)
            {
                return HttpNotFound();
            }
            return View(lOC_City);
        }

        // GET: LOC_City/Create
        public ActionResult Create()
        {
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            return View();
        }

        // POST: LOC_City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityID,StateID,CityName,Remarks,Created,Modified,UserID")] LOC_City lOC_City)
        {
            if (ModelState.IsValid)
            {
                db.LOC_City.Add(lOC_City);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", lOC_City.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_City.UserID);
            return View(lOC_City);
        }

        // GET: LOC_City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_City lOC_City = db.LOC_City.Find(id);
            if (lOC_City == null)
            {
                return HttpNotFound();
            }
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", lOC_City.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_City.UserID);
            return View(lOC_City);
        }

        // POST: LOC_City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityID,StateID,CityName,Remarks,Created,Modified,UserID")] LOC_City lOC_City)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOC_City).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", lOC_City.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_City.UserID);
            return View(lOC_City);
        }

        // GET: LOC_City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_City lOC_City = db.LOC_City.Find(id);
            if (lOC_City == null)
            {
                return HttpNotFound();
            }
            return View(lOC_City);
        }

        // POST: LOC_City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOC_City lOC_City = db.LOC_City.Find(id);
            db.LOC_City.Remove(lOC_City);
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
