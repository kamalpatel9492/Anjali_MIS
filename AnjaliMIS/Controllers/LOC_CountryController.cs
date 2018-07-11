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
    public class LOC_CountryController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: LOC_Country
        public ActionResult Index()
        {
            var lOC_Country = db.LOC_Country.Include(l => l.SEC_User);
            return View(lOC_Country.ToList());
        }

        // GET: LOC_Country/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_Country lOC_Country = db.LOC_Country.Find(id);
            if (lOC_Country == null)
            {
                return HttpNotFound();
            }
            return View(lOC_Country);
        }

        // GET: LOC_Country/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            LOC_Country lOC_Country = new LOC_Country();
            return View("Edit", lOC_Country);
        }
        // POST: LOC_Country/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryID,CountryName,Remarks,Created,Modified,UserID")] LOC_Country lOC_Country)
        {
            lOC_Country.Created = DateTime.Now;
            lOC_Country.Modified = DateTime.Now;
            if (Session["UserID"] != null)
            {
                lOC_Country.UserID = Convert.ToInt16(Session["UserID"].ToString());
            }
            if (ModelState.IsValid)
            {
                db.LOC_Country.Add(lOC_Country);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_Country.UserID);
            return View();
        }

        // GET: LOC_Country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_Country lOC_Country = db.LOC_Country.Find(id);
            if (lOC_Country == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_Country.UserID);
            return View(lOC_Country);
        }

        // POST: LOC_Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryID,CountryName,Remarks,Created,Modified,UserID")] LOC_Country lOC_Country)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOC_Country).State = EntityState.Modified;
                lOC_Country.Modified = DateTime.Now;
                lOC_Country.Created = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    lOC_Country.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", lOC_Country.UserID);
            return View(lOC_Country);
        }

        // GET: LOC_Country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOC_Country lOC_Country = db.LOC_Country.Find(id);
            if (lOC_Country == null)
            {
                return HttpNotFound();
            }
            return View(lOC_Country);
        }

        // POST: LOC_Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOC_Country lOC_Country = db.LOC_Country.Find(id);
            db.LOC_Country.Remove(lOC_Country);
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
