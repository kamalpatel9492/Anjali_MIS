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
    public class EMP_DepartmentController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: EMP_Department
        public ActionResult Index()
        {
            var eMP_Department = db.EMP_Department.Include(e => e.SEC_User);
            return View(eMP_Department.ToList());
        }

        // GET: EMP_Department/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Department eMP_Department = db.EMP_Department.Find(id);
            if (eMP_Department == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Department);
        }

        // GET: EMP_Department/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            EMP_Department eMP_Department = new EMP_Department();
            return View("Edit", eMP_Department);
        }

        // POST: EMP_Department/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName,Remarks,Created,Modified,UserID,IsActive")] EMP_Department eMP_Department)
        {
            eMP_Department.Created = DateTime.Now;
            eMP_Department.Modified = DateTime.Now;
            if (Session["UserID"] != null)
            {
                eMP_Department.UserID = Convert.ToInt16(Session["UserID"].ToString());
            }
            if (ModelState.IsValid)
            {
                db.EMP_Department.Add(eMP_Department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Department.UserID);
            return View(eMP_Department);
        }

        // GET: EMP_Department/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Department eMP_Department = db.EMP_Department.Find(id);
            if (eMP_Department == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Department.UserID);
            return View(eMP_Department);
        }

        // POST: EMP_Department/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName,Remarks,Created,Modified,UserID,IsActive")] EMP_Department eMP_Department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMP_Department).State = EntityState.Modified;
                eMP_Department.Created = Convert.ToDateTime(eMP_Department.Created);
                eMP_Department.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    eMP_Department.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Department.UserID);
            return View(eMP_Department);
        }

        // GET: EMP_Department/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Department eMP_Department = db.EMP_Department.Find(id);
            if (eMP_Department == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Department);
        }

        // POST: EMP_Department/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMP_Department eMP_Department = db.EMP_Department.Find(id);
            db.EMP_Department.Remove(eMP_Department);
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
