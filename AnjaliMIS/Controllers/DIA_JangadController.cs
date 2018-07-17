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
	public class DIA_JangadController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: DIA_Jangad
        public ActionResult Index()
        {
            var dIA_Jangad = db.DIA_Jangad.Include(d => d.ACC_Tax).Include(d => d.ACC_Tax1).Include(d => d.ACC_Tax2).Include(d => d.ACC_Tax3).Include(d => d.SYS_Company).Include(d => d.SYS_FinYear).Include(d => d.MST_Party).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_Jangad.ToList());
        }

        // GET: DIA_Jangad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Jangad dIA_Jangad = db.DIA_Jangad.Find(id);
            if (dIA_Jangad == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Jangad);
        }

        // GET: DIA_Jangad/Create
        public ActionResult Create()
        {
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.TDS = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			DIA_Jangad _dIA_Jangad = new DIA_Jangad();
			return View("Edit", _dIA_Jangad);
		}

        // POST: DIA_Jangad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JangadID,CompanyID,PartyID,Quantity,Weight,StatusID,UserID,Amount,RecivedAmount,PendingAmount,JangadNo,Created,Modified,Remarks,PricePerCarat,FinYearID,CGSTAmount,SGSTAmount,IGSTAmount,TDSAmount,IsActive,CGST,SGST,IGST,TDS,IsLocal,Casar,TotalAmount")] DIA_Jangad dIA_Jangad)
        {
            if (ModelState.IsValid)
            {
				dIA_Jangad.Created = DateTime.Now;
				dIA_Jangad.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					dIA_Jangad.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.DIA_Jangad.Add(dIA_Jangad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.SGST);
            ViewBag.TDS = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.TDS);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Jangad.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", dIA_Jangad.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", dIA_Jangad.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Jangad.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Jangad.UserID);
            return View(dIA_Jangad);
        }

        // GET: DIA_Jangad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Jangad dIA_Jangad = db.DIA_Jangad.Find(id);
            if (dIA_Jangad == null)
            {
                return HttpNotFound();
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.SGST);
            ViewBag.TDS = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.TDS);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Jangad.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", dIA_Jangad.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", dIA_Jangad.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Jangad.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Jangad.UserID);
            return View(dIA_Jangad);
        }

        // POST: DIA_Jangad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JangadID,CompanyID,PartyID,Quantity,Weight,StatusID,UserID,Amount,RecivedAmount,PendingAmount,JangadNo,Created,Modified,Remarks,PricePerCarat,FinYearID,CGSTAmount,SGSTAmount,IGSTAmount,TDSAmount,IsActive,CGST,SGST,IGST,TDS,IsLocal,Casar,TotalAmount")] DIA_Jangad dIA_Jangad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIA_Jangad).State = EntityState.Modified;
				dIA_Jangad.Modified = Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					dIA_Jangad.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.SGST);
            ViewBag.TDS = new SelectList(db.ACC_Tax, "TaxID", "Tax", dIA_Jangad.TDS);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", dIA_Jangad.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", dIA_Jangad.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", dIA_Jangad.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_Jangad.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_Jangad.UserID);
            return View(dIA_Jangad);
        }

        // GET: DIA_Jangad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_Jangad dIA_Jangad = db.DIA_Jangad.Find(id);
            if (dIA_Jangad == null)
            {
                return HttpNotFound();
            }
            return View(dIA_Jangad);
        }

        // POST: DIA_Jangad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DIA_Jangad dIA_Jangad = db.DIA_Jangad.Find(id);
            db.DIA_Jangad.Remove(dIA_Jangad);
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
