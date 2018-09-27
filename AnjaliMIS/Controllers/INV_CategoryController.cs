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
    public class INV_CategoryController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_Category
        public ActionResult Index()
        {
            var iNV_Category = db.INV_Category.Include(i => i.SEC_User);
            return View(iNV_Category.ToList());
        }

        // GET: INV_Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Category iNV_Category = db.INV_Category.Find(id);
            if (iNV_Category == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Category);
        }

        // GET: INV_Category/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            INV_Category iNV_Category = new INV_Category();
            return View("Edit", iNV_Category);
        }

        // POST: INV_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,CategoryShortName,Created,Modified,Remarks,UserID")] INV_Category iNV_Category)
        {
            if (ModelState.IsValid)
            {
                iNV_Category.Created = DateTime.Now;
                iNV_Category.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    iNV_Category.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.INV_Category.Add(iNV_Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Category.UserID);
            return View(iNV_Category);
        }

        // GET: INV_Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Category iNV_Category = db.INV_Category.Find(id);
            if (iNV_Category == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Category.UserID);
            return View(iNV_Category);
        }

        // POST: INV_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryShortName,Created,Modified,Remarks,UserID")] INV_Category iNV_Category)
        {
            if (iNV_Category.CategoryID > 0)
            {
                if (iNV_Category.Remarks == null || iNV_Category.Remarks == "")
                {
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Category.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(iNV_Category);
                }
            }
            if (ModelState.IsValid)
            {

                iNV_Category.Created = Convert.ToDateTime(iNV_Category.Created);
                iNV_Category.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    iNV_Category.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.Entry(iNV_Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Category.UserID);
            return View(iNV_Category);
        }

        // GET: INV_Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Category iNV_Category = db.INV_Category.Find(id);
            if (iNV_Category == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Category);
        }

        // POST: INV_Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_Category iNV_Category = db.INV_Category.Find(id);
            try
            {
                db.INV_Category.Remove(iNV_Category);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                
                ModelState.AddModelError("", "You can not Delete this Category.");
                //return RedirectToAction("Index");
                return View(iNV_Category);
            }
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
