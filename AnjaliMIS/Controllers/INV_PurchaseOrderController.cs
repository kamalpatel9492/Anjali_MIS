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
        public JsonResult AddInvoice(INV_PurchaseOrderViewModal iNV_PurchaseOrderViewModal)
        {
            try
            {
                if (iNV_PurchaseOrderViewModal == null)
                {
                    //error meesage or expception handle
                }
                else if (iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems == null)
                {
                    //error meesage or expception handle
                }
                else
                {
                    INV_Invoice new_INV_Invoice = new INV_Invoice();
                    new_INV_Invoice.CompanyID = iNV_PurchaseOrderViewModal.CompanyID;
                    new_INV_Invoice.PartyID = iNV_PurchaseOrderViewModal.PartyID;
                    if (Session["UserID"] != null)
                    {
                        new_INV_Invoice.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }

                    new_INV_Invoice.Amount = iNV_PurchaseOrderViewModal.Amount;
                    new_INV_Invoice.AmountReceived = iNV_PurchaseOrderViewModal.AmountReceived;
                    new_INV_Invoice.StatusID = Convert.ToInt32(iNV_PurchaseOrderViewModal.StatusID);
                    new_INV_Invoice.Created = DateTime.Now;
                    new_INV_Invoice.Modified = DateTime.Now;
                    new_INV_Invoice.Remarks = iNV_PurchaseOrderViewModal.Remarks;
                    new_INV_Invoice.InvoiceDate = DateTime.Now;
                    new_INV_Invoice.InvoiceNo = iNV_PurchaseOrderViewModal.InvoiceNo;
                    new_INV_Invoice.PONo = iNV_PurchaseOrderViewModal.PONo;
                    new_INV_Invoice.AmountPending = iNV_PurchaseOrderViewModal.AmountPending;
                    new_INV_Invoice.FinYearID = iNV_PurchaseOrderViewModal.FinYearID;
                    new_INV_Invoice.CGST = iNV_PurchaseOrderViewModal.CGST;
                    new_INV_Invoice.CGSTAmount = iNV_PurchaseOrderViewModal.CGSTAmount;
                    new_INV_Invoice.SGST = iNV_PurchaseOrderViewModal.SGST;
                    new_INV_Invoice.SGSTAmount = iNV_PurchaseOrderViewModal.SGSTAmount;
                    new_INV_Invoice.IGST = iNV_PurchaseOrderViewModal.IGST;
                    new_INV_Invoice.IGSTAmount = iNV_PurchaseOrderViewModal.IGSTAmount;
                    new_INV_Invoice.IsLocal = iNV_PurchaseOrderViewModal.IsLocal;
                    new_INV_Invoice.IsActive = true;
                    new_INV_Invoice.Casar = iNV_PurchaseOrderViewModal.Casar;
                    new_INV_Invoice.TotalAmount = iNV_PurchaseOrderViewModal.TotalAmount;

                    db.INV_Invoice.Add(new_INV_Invoice);
                    db.SaveChanges();

                    int newInvoiceId = new_INV_Invoice.InvoiceID;
                    List<INV_PurchaseOrderItem> newINV_PurchaseOrderItem = new List<INV_PurchaseOrderItem>();
                    foreach (var item in iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems)
                    {
                        INV_InvoiceItem new_INV_InvoiceItem = new INV_InvoiceItem();
                        new_INV_InvoiceItem.InvoiceItemID = item.PurchaseOrderItemID;
                        new_INV_InvoiceItem.InvoiceID = newInvoiceId;
                        new_INV_InvoiceItem.ItemID = item.ItemID;
                        new_INV_InvoiceItem.Quantity = item.OrderedQuantity;
                        new_INV_InvoiceItem.Quantity = item.ReceivedQuantity;
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

        // GET: INV_Invoice/Print/5
        public ActionResult Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_InvoiceViewModal _iNV_InvoiceViewModal;

            var InvoiceData = db.INV_Invoice.Find(id);
            _iNV_InvoiceViewModal = new INV_InvoiceViewModal()
            {
                InvoiceID = InvoiceData.InvoiceID,
                CompanyID = InvoiceData.CompanyID,
                PartyID = InvoiceData.PartyID,
                PartyIDName = InvoiceData.MST_Party.PartyName,
                UserID = InvoiceData.UserID,
                Amount = InvoiceData.Amount,
                AmountReceived = InvoiceData.AmountReceived,
                StatusID = InvoiceData.StatusID,
                Created = InvoiceData.Created,
                Modified = InvoiceData.Modified,
                Remarks = InvoiceData.Remarks,
                InvoiceDate = InvoiceData.InvoiceDate,
                InvoiceNo = InvoiceData.InvoiceNo,
                PONo = InvoiceData.PONo,
                AmountPending = InvoiceData.AmountPending,
                FinYearID = InvoiceData.FinYearID,
                CGST = InvoiceData.CGST,
                CGSTAmount = InvoiceData.CGSTAmount,
                SGST = InvoiceData.SGST,
                SGSTAmount = InvoiceData.SGSTAmount,
                IGST = InvoiceData.IGST,
                IGSTAmount = InvoiceData.IGSTAmount,
                IsLocal = InvoiceData.IsLocal,
                IsActive = InvoiceData.IsActive,
                Casar = InvoiceData.Casar,
                TotalAmount = InvoiceData.TotalAmount
            };
            _iNV_InvoiceViewModal.INV_InvoiceItems = db.INV_InvoiceItem.Where(I => I.InvoiceID == id).ToList();
            if (_iNV_InvoiceViewModal == null)
            {
                return HttpNotFound();
            }
            return View(_iNV_InvoiceViewModal);
        }
    }
}
