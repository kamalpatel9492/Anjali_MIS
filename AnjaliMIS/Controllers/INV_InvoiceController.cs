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
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
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

            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", iNV_Invoice.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", iNV_Invoice.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", iNV_Invoice.SGST);
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
            INV_InvoiceViewModal iNV_InvoiceViewModal = new INV_InvoiceViewModal();
            var inv_Invoice = db.INV_Invoice.Find(id);
            iNV_InvoiceViewModal = new INV_InvoiceViewModal()
            {
                InvoiceID = inv_Invoice.InvoiceID,
                CompanyID = inv_Invoice.CompanyID,
                PartyID = inv_Invoice.PartyID,
                UserID = inv_Invoice.UserID,
                Amount = inv_Invoice.Amount,
                AmountReceived = inv_Invoice.AmountReceived,
                StatusID = inv_Invoice.StatusID,
                Created = inv_Invoice.Created,
                Modified = inv_Invoice.Modified,
                Remarks = inv_Invoice.Remarks,
                InvoiceDate = inv_Invoice.InvoiceDate,
                InvoiceNo = inv_Invoice.InvoiceNo,
                PONo = inv_Invoice.PONo,
                AmountPending = inv_Invoice.AmountPending,
                FinYearID = CommonConfig.GetFinYearID(),
                CGST = inv_Invoice.CGST,
                CGSTAmount = inv_Invoice.CGSTAmount,
                SGST = inv_Invoice.SGST,
                SGSTAmount = inv_Invoice.SGSTAmount,
                IGST = inv_Invoice.IGST,
                IGSTAmount = inv_Invoice.IGSTAmount,
                IsLocal = inv_Invoice.IsLocal,
                IsActive = inv_Invoice.IsActive,
                Casar = inv_Invoice.Casar,
                TotalAmount = inv_Invoice.TotalAmount
            };
            iNV_InvoiceViewModal.INV_InvoiceItems = db.INV_InvoiceItem.Where(i => i.InvoiceID == id).ToList();
            if (iNV_InvoiceViewModal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", iNV_InvoiceViewModal.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", iNV_InvoiceViewModal.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", iNV_InvoiceViewModal.SGST);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_InvoiceViewModal.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_InvoiceViewModal.FinYearID);
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_InvoiceViewModal.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", iNV_InvoiceViewModal.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_InvoiceViewModal.UserID);
            return View("CreateInvoice", iNV_InvoiceViewModal);
        }

        // POST: INV_Invoice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,UserID,Amount,AmountReceived,StatusID,Created,Modified,Remarks,InvoiceDate,InvoiceNo,PONo,AmountPending,FinYearID,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,IsLocal,IsActive,Casar,TotalAmount,NewAmountPending,NewAmountReceived")] INV_Invoice iNV_Invoice)
        {
            if (ModelState.IsValid)
            {
                INV_Invoice getInvoiceData = db.INV_Invoice.Where(e => e.InvoiceID == iNV_Invoice.InvoiceID).FirstOrDefault();

                INV_InvoiceHistory add_INV_InvoiceHistory = new INV_InvoiceHistory();
                add_INV_InvoiceHistory.CompanyID = getInvoiceData.CompanyID;
                add_INV_InvoiceHistory.PartyID = getInvoiceData.PartyID;
                add_INV_InvoiceHistory.UserID = getInvoiceData.CompanyID;
                add_INV_InvoiceHistory.Amount = getInvoiceData.Amount;
                add_INV_InvoiceHistory.AmountReceived = getInvoiceData.AmountReceived;
                add_INV_InvoiceHistory.StatusID = getInvoiceData.StatusID;
                add_INV_InvoiceHistory.Created = getInvoiceData.Created;
                add_INV_InvoiceHistory.Remarks = getInvoiceData.Remarks;
                add_INV_InvoiceHistory.InvoiceDate = getInvoiceData.InvoiceDate;
                add_INV_InvoiceHistory.InvoiceNo = getInvoiceData.InvoiceNo;
                add_INV_InvoiceHistory.PONo = getInvoiceData.PONo;
                add_INV_InvoiceHistory.AmountPending = getInvoiceData.AmountPending;
                add_INV_InvoiceHistory.FinYearID = CommonConfig.GetFinYearID();
                add_INV_InvoiceHistory.CGST = getInvoiceData.CGST;
                add_INV_InvoiceHistory.CGSTAmount = getInvoiceData.CGSTAmount;
                add_INV_InvoiceHistory.SGST = getInvoiceData.SGST;
                add_INV_InvoiceHistory.SGSTAmount = getInvoiceData.SGSTAmount;
                add_INV_InvoiceHistory.IGST = getInvoiceData.IGST;
                add_INV_InvoiceHistory.IGSTAmount = getInvoiceData.IGSTAmount;
                add_INV_InvoiceHistory.IsLocal = getInvoiceData.IsLocal;
                add_INV_InvoiceHistory.IsActive = true;
                add_INV_InvoiceHistory.Casar = getInvoiceData.Casar;
                add_INV_InvoiceHistory.TotalAmount = getInvoiceData.TotalAmount;
                add_INV_InvoiceHistory.Operation = "Operation";
                add_INV_InvoiceHistory.InvoiceID = getInvoiceData.InvoiceID;

                db.INV_InvoiceHistory.Add(add_INV_InvoiceHistory);
                //db.SaveChanges();
                //db.Entry(iNV_Invoice).State = EntityState.Modified;
                if (Session["UserID"] != null)
                {
                    getInvoiceData.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    getInvoiceData.Modified = DateTime.Now;
                    getInvoiceData.AmountReceived = iNV_Invoice.NewAmountReceived;
                    getInvoiceData.AmountPending = getInvoiceData.AmountPending - iNV_Invoice.NewAmountReceived;

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", iNV_Invoice.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", iNV_Invoice.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", iNV_Invoice.SGST);
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
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            //ViewBag.ItemPrice = new SelectList(db.INV_ItemPrice, "ItemPriceID", "PurchasePrice");
            return View(model);
        }

        public JsonResult DropDownItemPrice(int ItemID)
        {
            try
            {
                decimal itemPrice = db.INV_ItemPrice.Where(e => e.ItemID == ItemID).OrderByDescending(o => o.Created).FirstOrDefault().PurchasePrice;

                return Json(itemPrice, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return Json("0", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetrieveSGST()
        {
            try
            {
                var sgst = db.ACC_Tax.Where(e => e.IsActive == true && e.TaxType == "SGST").Select(e => new
                {
                    TaxID = e.TaxID,
                    Tax = e.Tax,
                    Percentage = e.Percentage
                }).ToList();

                if (sgst.Count > 0)
                {
                    return Json(sgst, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetrieveCGST()
        {
            try
            {
                var cgst = db.ACC_Tax.Where(e => e.IsActive == true && e.TaxType == "CGST").Select(e => new
                {
                    TaxID = e.TaxID,
                    Tax = e.Tax,
                    Percentage = e.Percentage
                }).ToList();

                if (cgst.Count > 0)
                {
                    return Json(cgst, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }
        public JsonResult RetrieveIGST()
        {
            try
            {
                var igst = db.ACC_Tax.Where(e => e.IsActive == true && e.TaxType == "IGST").Select(e => new
                {
                    TaxID = e.TaxID,
                    Tax = e.Tax,
                    Percentage = e.Percentage
                }).ToList();

                if (igst.Count > 0)
                {
                    return Json(igst, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetrieveItemList()
        {
            try
            {
                var itemList = db.INV_Item.Where(w => w.IsLock == true).Select(e => new
                {
                    ItemID = e.ItemID,
                    ItemName = e.ItemName
                }).OrderBy(i=>i.ItemName).ToList();

                if (itemList.Count > 0)
                {
                    return Json(itemList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetrieveItemListIsConfigurable()
        {
            try
            {
                var itemList = db.INV_Item.Where(w => w.IsLock == true && w.IsConfigurable == true).Select(e => new
                {
                    ItemID = e.ItemID,
                    ItemName = e.ItemName
                }).ToList();

                if (itemList.Count > 0)
                {
                    return Json(itemList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
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
                    new_INV_Invoice.CompanyID = 4;
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

                    Int32 TotalForMonth = db.INV_Invoice.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year).Count();
                    Int32 NextCount = TotalForMonth + 1;
                    Int32 _NewInvoiceNo = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);
                    //Change Number Add -INV
                    new_INV_Invoice.InvoiceNo = _NewInvoiceNo.ToString();

                    new_INV_Invoice.InvoiceNo = inv_InvoiceViewModal.InvoiceNo;
                    new_INV_Invoice.PONo = inv_InvoiceViewModal.PONo;
                    new_INV_Invoice.AmountPending = inv_InvoiceViewModal.AmountPending;
                    new_INV_Invoice.FinYearID = CommonConfig.GetFinYearID();
                    new_INV_Invoice.CGST = inv_InvoiceViewModal.CGST == 0 ? null : inv_InvoiceViewModal.CGST;
                    new_INV_Invoice.CGSTAmount = inv_InvoiceViewModal.CGST == 0 ? null : inv_InvoiceViewModal.CGSTAmount;
                    new_INV_Invoice.SGST = inv_InvoiceViewModal.SGST == 0 ? null : inv_InvoiceViewModal.SGST;
                    new_INV_Invoice.SGSTAmount = inv_InvoiceViewModal.SGST == 0 ? null : inv_InvoiceViewModal.SGSTAmount;
                    new_INV_Invoice.IGST = inv_InvoiceViewModal.IGST == 0 ? null : inv_InvoiceViewModal.IGST;
                    new_INV_Invoice.IGSTAmount = inv_InvoiceViewModal.IGST == 0 ? null : inv_InvoiceViewModal.IGSTAmount;
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

                        #region INV_ItemPrice
                        INV_ItemPrice _iNV_ItemPrice = new INV_ItemPrice();
                        _iNV_ItemPrice = db.INV_ItemPrice.Where(M => M.ItemID == item.ItemID & M.PurchasePrice == item.PricePerUnit).OrderByDescending(o => o.Created).FirstOrDefault();
                        if (_iNV_ItemPrice == null)
                        {
                            _iNV_ItemPrice = new INV_ItemPrice();
                            _iNV_ItemPrice.ItemID = item.ItemID;
                            _iNV_ItemPrice.PurchasePrice = item.PricePerUnit;
                            _iNV_ItemPrice.Created = DateTime.Now;
                            _iNV_ItemPrice.Modified = DateTime.Now;
                            _iNV_ItemPrice.FinYearID = CommonConfig.GetFinYearID();
                            if (Session["UserID"] != null)
                            {
                                _iNV_ItemPrice.UserID = Convert.ToInt16(Session["UserID"].ToString());
                            }
                            db.INV_ItemPrice.Add(_iNV_ItemPrice);
                            db.SaveChanges();
                        }
                        #endregion INV_ItemPrice

                        INV_Item _INV_Item = new INV_Item();
                        _INV_Item = db.INV_Item.Where(i => i.ItemID == item.ItemID).FirstOrDefault();
                        if (_INV_Item != null)
                        {
                            if (_INV_Item.Quantity - item.Quantity < 0)
                            {
                                ModelState.AddModelError("", "Check Stock..");
                                return Json("failure", JsonRequestBehavior.AllowGet);
                            }
                            _INV_Item.Quantity = _INV_Item.Quantity - item.Quantity;
                            db.SaveChanges();
                        }

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
                FinYearID = CommonConfig.GetFinYearID(),
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
