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
    public class INV_InvoiceController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_Invoice
        public ActionResult Index()
        {
            var iNV_Invoice = db.INV_Invoice.Include(i => i.ACC_Tax).Include(i => i.ACC_Tax1).Include(i => i.ACC_Tax2).Include(i => i.SYS_Company).Include(i => i.SYS_FinYear).Include(i => i.MST_Party).Include(i => i.SYS_Status).Include(i => i.SEC_User);
            return View(iNV_Invoice.ToList());
        }

        // GET: INV_Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Invoice iNV_Invoice = db.INV_Invoice.Find(id);
            if (iNV_Invoice == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Invoice);
        }

        // GET: INV_Invoice/Create
        public ActionResult Create()
        {
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			INV_Invoice _iNV_Invoice = new INV_Invoice();
			return View("Edit", _iNV_Invoice);
		}

        // POST: INV_Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,CompanyID,PartyID,UserID,Amount,AmountReceived,StatusID,Created,Modified,Remarks,InvoiceDate,InvoiceNo,PONo,AmountPending,FinYearID,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,IsLocal,IsActive,Casar,TotalAmount")] INV_Invoice iNV_Invoice)
        {
            if (ModelState.IsValid)
            {
				iNV_Invoice.Created = DateTime.Now;
				iNV_Invoice.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					iNV_Invoice.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.INV_Invoice.Add(iNV_Invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Invoice.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_Invoice.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_Invoice.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_Invoice.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Invoice.UserID);
            return View(iNV_Invoice);
        }

        // GET: INV_Invoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Invoice iNV_Invoice = db.INV_Invoice.Find(id);
            if (iNV_Invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Invoice.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_Invoice.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_Invoice.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_Invoice.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Invoice.UserID);
            return View(iNV_Invoice);
        }

        // POST: INV_Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,CompanyID,PartyID,UserID,Amount,AmountReceived,StatusID,Created,Modified,Remarks,InvoiceDate,InvoiceNo,PONo,AmountPending,FinYearID,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,IsLocal,IsActive,Casar,TotalAmount")] INV_Invoice iNV_Invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_Invoice).State = EntityState.Modified;
				iNV_Invoice.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					iNV_Invoice.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_Invoice.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Invoice.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_Invoice.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_Invoice.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_Invoice.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Invoice.UserID);
            return View(iNV_Invoice);
        }

        // GET: INV_Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Invoice iNV_Invoice = db.INV_Invoice.Find(id);
            if (iNV_Invoice == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Invoice);
        }

        // POST: INV_Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_Invoice iNV_Invoice = db.INV_Invoice.Find(id);
            db.INV_Invoice.Remove(iNV_Invoice);
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
