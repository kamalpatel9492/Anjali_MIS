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
    public class LOC_StateController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: LOC_State
        public ActionResult Index()
        {
            var lOC_State = db.LOC_State.Include(l => l.LOC_Country).Include(l => l.SEC_User);
            return View(lOC_State.ToList());
        }

        // GET: LOC_State/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_State lOC_State = db.LOC_State.Find(id);
            if (lOC_State == null)
            {
                return HttpNotFound();
            }
            return View(lOC_State);
        }

        // GET: LOC_State/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            LOC_State _lOC_State = new LOC_State();
            return View("Edit", _lOC_State);
        }

        // GET: LOC_State/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_State lOC_State = db.LOC_State.Find(id);
            if (lOC_State == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName", lOC_State.CountryID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_State.UserID);
            return View(lOC_State);
        }

        // POST: LOC_State/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StateID,CountryID,StateName,Remarks,Created,Modified,UserID")] LOC_State lOC_State)
        {
            lOC_State.Created = Convert.ToDateTime(lOC_State.Created);
            lOC_State.Modified = DateTime.Now;
            if (Session["UserID"] != null)
            {
                lOC_State.UserID = Convert.ToInt16(Session["UserID"].ToString());
            }

            if (ModelState.IsValid)
            {
                db.Entry(lOC_State).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName", lOC_State.CountryID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_State.UserID);
            return View(lOC_State);
        }

        // GET: LOC_State/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_State lOC_State = db.LOC_State.Find(id);
            if (lOC_State == null)
            {
                return HttpNotFound();
            }
            return View(lOC_State);
        }

        // POST: LOC_State/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOC_State lOC_State = db.LOC_State.Find(id);
            db.LOC_State.Remove(lOC_State);
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
