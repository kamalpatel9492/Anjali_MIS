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
    public class INV_StockHistoryController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_StockHistory
        public ActionResult Index()
        {
            var iNV_StockHistory = db.INV_StockHistory.OrderByDescending(o=>o.Modified).Include(i => i.INV_Item).Include(i => i.SYS_FinYear).Include(i => i.SYS_OperationType).Include(i => i.SEC_User).OrderByDescending(O => O.Modified);
            return View(iNV_StockHistory.ToList());
        }

        // GET: INV_StockHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_StockHistory iNV_StockHistory = db.INV_StockHistory.Find(id);
            if (iNV_StockHistory == null)
            {
                return HttpNotFound();
            }
            return View(iNV_StockHistory);
        }

        // GET: INV_StockHistory/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.OperationTypeID = new SelectList(db.SYS_OperationType, "OperationTypeID", "OperationType");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            return View();
        }

        // POST: INV_StockHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockHistoryID,ItemID,OperationTypeID,ReferenceID,Quantity,UserID,Created,Modified,Remarks,FinYearID")] INV_StockHistory iNV_StockHistory)
        {
            if (ModelState.IsValid)
            {
                db.INV_StockHistory.Add(iNV_StockHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_StockHistory.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_StockHistory.FinYearID);
            ViewBag.OperationTypeID = new SelectList(db.SYS_OperationType, "OperationTypeID", "OperationType", iNV_StockHistory.OperationTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_StockHistory.UserID);
            return View(iNV_StockHistory);
        }

        // GET: INV_StockHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_StockHistory iNV_StockHistory = db.INV_StockHistory.Find(id);
            if (iNV_StockHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_StockHistory.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_StockHistory.FinYearID);
            ViewBag.OperationTypeID = new SelectList(db.SYS_OperationType, "OperationTypeID", "OperationType", iNV_StockHistory.OperationTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_StockHistory.UserID);
            return View(iNV_StockHistory);
        }

        // POST: INV_StockHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockHistoryID,ItemID,OperationTypeID,ReferenceID,Quantity,UserID,Created,Modified,Remarks,FinYearID")] INV_StockHistory iNV_StockHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_StockHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_StockHistory.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_StockHistory.FinYearID);
            ViewBag.OperationTypeID = new SelectList(db.SYS_OperationType, "OperationTypeID", "OperationType", iNV_StockHistory.OperationTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_StockHistory.UserID);
            return View(iNV_StockHistory);
        }

        // GET: INV_StockHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_StockHistory iNV_StockHistory = db.INV_StockHistory.Find(id);
            if (iNV_StockHistory == null)
            {
                return HttpNotFound();
            }
            return View(iNV_StockHistory);
        }

        // POST: INV_StockHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_StockHistory iNV_StockHistory = db.INV_StockHistory.Find(id);
            db.INV_StockHistory.Remove(iNV_StockHistory);
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
