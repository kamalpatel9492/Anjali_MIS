using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;

namespace AnjaliMIS.Controllers
{
    public class INV_PurchaseOrderController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_PurchaseOrder
        public ActionResult Index()
        {
            var iNV_PurchaseOrder = db.INV_PurchaseOrder.Include(i => i.ACC_Tax).Include(i => i.ACC_Tax1).Include(i => i.ACC_Tax2).Include(i => i.SYS_Company).Include(i => i.SYS_FinYear).Include(i => i.MST_Party).Include(i => i.SYS_Status).Include(i => i.SEC_User);
            return View(iNV_PurchaseOrder.ToList());
        }

        // GET: INV_PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PurchaseOrder iNV_PurchaseOrder = db.INV_PurchaseOrder.Find(id);
            if (iNV_PurchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(iNV_PurchaseOrder);
        }

        // GET: INV_PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            return View();
        }

        // POST: INV_PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PurchaseOrderID,CompanyID,SellerPartyID,UserID,StatusID,Amount,PaidAmount,Created,Modified,Remarks,PendingAmount,Casar,PONo,PODate,FinYearID,IsLocal,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,TotalAmount")] INV_PurchaseOrder iNV_PurchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.INV_PurchaseOrder.Add(iNV_PurchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_PurchaseOrder.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_PurchaseOrder.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_PurchaseOrder.UserID);
            return View(iNV_PurchaseOrder);
        }

        // GET: INV_PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PurchaseOrder iNV_PurchaseOrder = db.INV_PurchaseOrder.Find(id);
            if (iNV_PurchaseOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_PurchaseOrder.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_PurchaseOrder.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_PurchaseOrder.UserID);
            return View(iNV_PurchaseOrder);
        }

        // POST: INV_PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderID,CompanyID,SellerPartyID,UserID,StatusID,Amount,PaidAmount,Created,Modified,Remarks,PendingAmount,Casar,PONo,PODate,FinYearID,IsLocal,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,TotalAmount")] INV_PurchaseOrder iNV_PurchaseOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_PurchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_PurchaseOrder.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_PurchaseOrder.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_PurchaseOrder.UserID);
            return View(iNV_PurchaseOrder);
        }

        // GET: INV_PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PurchaseOrder iNV_PurchaseOrder = db.INV_PurchaseOrder.Find(id);
            if (iNV_PurchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(iNV_PurchaseOrder);
        }

        // POST: INV_PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_PurchaseOrder iNV_PurchaseOrder = db.INV_PurchaseOrder.Find(id);
            db.INV_PurchaseOrder.Remove(iNV_PurchaseOrder);
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

        // GET: INV_Invoice
        public ActionResult CreatePurchaseOrder()
        {
            var model = new INV_PurchaseOrderViewModal();
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            //model.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.ToList();
            return View(model);
        }
    }
}
