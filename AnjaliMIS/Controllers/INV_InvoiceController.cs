﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;
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

        // GET: INV_Invoice
        public ActionResult CreateInvoice()
        {
            var model = new INV_InvoiceViewModal();
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            //ViewBag.ItemPrice = new SelectList(db.INV_ItemPrice, "ItemPriceID", "PurchasePrice");
            return View(model);
        }

        public JsonResult DropDownItemPrice(int ItemID)
        {
            try
            {
                decimal itemPrice = db.INV_ItemPrice.Where(e => e.ItemID == ItemID).FirstOrDefault().PurchasePrice;

                return Json(itemPrice, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddInvoice(INV_InvoiceViewModal inv_InvoiceViewModal)
        {
            try
            {
                if (inv_InvoiceViewModal == null)
                {
                    //error meesage or expception handle
                }
                else if (inv_InvoiceViewModal.INV_InvoiceItems == null)
                {
                    //error meesage or expception handle
                }
                else
                {
                    INV_Invoice new_INV_Invoice = new INV_Invoice();
                    new_INV_Invoice.CompanyID = inv_InvoiceViewModal.CompanyID;
                    new_INV_Invoice.PartyID = inv_InvoiceViewModal.PartyID;
                    if (Session["UserID"] != null)
                    {
                        new_INV_Invoice.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }

                    new_INV_Invoice.Amount = inv_InvoiceViewModal.Amount;
                    new_INV_Invoice.AmountReceived = inv_InvoiceViewModal.AmountReceived;
                    new_INV_Invoice.StatusID = inv_InvoiceViewModal.StatusID;
                    new_INV_Invoice.Created = DateTime.Now;
                    new_INV_Invoice.Modified = DateTime.Now;
                    new_INV_Invoice.Remarks = inv_InvoiceViewModal.Remarks;
                    new_INV_Invoice.InvoiceDate = DateTime.Now;
                    new_INV_Invoice.InvoiceNo = inv_InvoiceViewModal.InvoiceNo;
                    new_INV_Invoice.PONo = inv_InvoiceViewModal.PONo;
                    new_INV_Invoice.AmountPending = inv_InvoiceViewModal.AmountPending;
                    new_INV_Invoice.FinYearID = inv_InvoiceViewModal.FinYearID;
                    new_INV_Invoice.CGST = inv_InvoiceViewModal.CGST;
                    new_INV_Invoice.CGSTAmount = inv_InvoiceViewModal.CGSTAmount;
                    new_INV_Invoice.SGST = inv_InvoiceViewModal.SGST;
                    new_INV_Invoice.SGSTAmount = inv_InvoiceViewModal.SGSTAmount;
                    new_INV_Invoice.IGST = inv_InvoiceViewModal.IGST;
                    new_INV_Invoice.IGSTAmount = inv_InvoiceViewModal.IGSTAmount;
                    new_INV_Invoice.IsLocal = inv_InvoiceViewModal.IsLocal;
                    new_INV_Invoice.IsActive = true;
                    new_INV_Invoice.Casar = inv_InvoiceViewModal.Casar;
                    new_INV_Invoice.TotalAmount = inv_InvoiceViewModal.TotalAmount;

                    db.INV_Invoice.Add(new_INV_Invoice);
                    db.SaveChanges();

                    int newInvoiceId = new_INV_Invoice.InvoiceID;
                    List<INV_InvoiceItem> newList_INV_InvoiceItem = new List<INV_InvoiceItem>();
                    foreach (var item in inv_InvoiceViewModal.INV_InvoiceItems)
                    {
                        INV_InvoiceItem new_INV_InvoiceItem = new INV_InvoiceItem();
                        new_INV_InvoiceItem.InvoiceItemID = item.InvoiceItemID;
                        new_INV_InvoiceItem.InvoiceID = newInvoiceId;
                        new_INV_InvoiceItem.ItemID = item.ItemID;
                        new_INV_InvoiceItem.Quantity = item.Quantity;
                        new_INV_InvoiceItem.Created = DateTime.Now;
                        new_INV_InvoiceItem.Modified = DateTime.Now;
                        new_INV_InvoiceItem.Remarks = item.Remarks;
                        new_INV_InvoiceItem.PricePerUnit = item.PricePerUnit;

                        newList_INV_InvoiceItem.Add(new_INV_InvoiceItem);
                    }
                    db.INV_InvoiceItem.AddRange(newList_INV_InvoiceItem);
                    db.SaveChanges();

                }

                return Json("Sucess", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }
    }
}
