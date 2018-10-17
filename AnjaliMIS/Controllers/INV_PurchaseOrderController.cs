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
    public class INV_PurchaseOrderController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_PurchaseOrder
        public ActionResult Index()
        {
            var iNV_PurchaseOrder = db.INV_PurchaseOrder.OrderByDescending(o => o.Created).Include(i => i.ACC_Tax).Include(i => i.ACC_Tax1).Include(i => i.ACC_Tax2).Include(i => i.SYS_Company).Include(i => i.SYS_FinYear).Include(i => i.MST_Party).Include(i => i.SYS_Status).Include(i => i.SEC_User);
            return View(iNV_PurchaseOrder.ToList());
        }

        // GET: INV_PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal;

            var POData = db.INV_PurchaseOrder.Find(id);
            _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal()
            {
                PurchaseOrderID = POData.PurchaseOrderID,
                CompanyID = POData.CompanyID,
                SellerPartyID = POData.SellerPartyID,
                PartyIDName = POData.MST_Party.PartyName,
                UserID = POData.UserID,
                Amount = POData.Amount,
                PaidAmount = POData.PaidAmount,
                StatusID = POData.StatusID,
                Created = POData.Created,
                Modified = POData.Modified,
                Remarks = POData.Remarks,
                PODate = POData.PODate,
                PONo = POData.PONo,
                FinYearID = CommonConfig.GetFinYearID(),
                CGST = POData.CGST,
                CGSTAmount = POData.CGSTAmount,
                SGST = POData.SGST,
                SGSTAmount = POData.SGSTAmount,
                IGST = POData.IGST,
                IGSTAmount = POData.IGSTAmount,
                IsLocal = POData.IsLocal,
                Casar = POData.Casar,
                TotalAmount = POData.TotalAmount
            };
            _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == id).ToList();
            if (_iNV_PurchaseOrderViewModal == null)
            {
                return HttpNotFound();
            }
            return View(_iNV_PurchaseOrderViewModal);
        }

        // GET: INV_PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
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
                iNV_PurchaseOrder.StatusID = CommonConfig.GetStatusPending();
                db.INV_PurchaseOrder.Add(iNV_PurchaseOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.SGST);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_PurchaseOrder.UserID);
            return View(iNV_PurchaseOrder);
        }

        // GET: INV_PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var POData = db.INV_PurchaseOrder.Find(id);
            if (POData != null)
            {
                _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal()
                {
                    PurchaseOrderID = POData.PurchaseOrderID,
                    CompanyID = POData.CompanyID,
                    SellerPartyID = POData.SellerPartyID,
                    PartyIDName = POData.MST_Party.PartyName,
                    UserID = POData.UserID,
                    Amount = POData.Amount,
                    PaidAmount = POData.PaidAmount,
                    StatusID = POData.StatusID,
                    Created = POData.Created,
                    Modified = POData.Modified,
                    Remarks = POData.Remarks,
                    PODate = POData.PODate,
                    PONo = POData.PONo,
                    FinYearID = CommonConfig.GetFinYearID(),
                    CGST = POData.CGST,
                    CGSTAmount = POData.CGSTAmount,
                    SGST = POData.SGST,
                    SGSTAmount = POData.SGSTAmount,
                    IGST = POData.IGST,
                    IGSTAmount = POData.IGSTAmount,
                    IsLocal = POData.IsLocal,
                    Casar = POData.Casar,
                    TotalAmount = POData.TotalAmount
                };
                _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == id).ToList();
                Boolean IsAnyReceived = _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems.Where(w => w.ReceivedQuantity > 0).Any();
                if (IsAnyReceived)
                {
                    ModelState.AddModelError("", "You can not Edit this PO As Item(s) received.");
                    ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.CGST);
                    ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.IGST);
                    ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.SGST);
                    ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", _iNV_PurchaseOrderViewModal.FinYearID);
                    ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", _iNV_PurchaseOrderViewModal.SellerPartyID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", _iNV_PurchaseOrderViewModal.UserID);
                    ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName", _iNV_PurchaseOrderViewModal.UserID);
                    return View("CreatePurchaseOrder", _iNV_PurchaseOrderViewModal);
                }
                if (_iNV_PurchaseOrderViewModal == null)
                {
                    return HttpNotFound();
                }

            }
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", _iNV_PurchaseOrderViewModal.SGST);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", _iNV_PurchaseOrderViewModal.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", _iNV_PurchaseOrderViewModal.SellerPartyID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", _iNV_PurchaseOrderViewModal.UserID);
            ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName", _iNV_PurchaseOrderViewModal.UserID);
            return View("CreatePurchaseOrder", _iNV_PurchaseOrderViewModal);
        }

        // POST: INV_PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PurchaseOrderID,CompanyID,SellerPartyID,UserID,StatusID,Amount,PaidAmount,Created,Modified,Remarks,PendingAmount,Casar,PONo,PODate,FinYearID,IsLocal,CGST,CGSTAmount,SGST,SGSTAmount,IGST,IGSTAmount,TotalAmount")] INV_PurchaseOrder iNV_PurchaseOrder)
        {
            if (iNV_PurchaseOrder.PurchaseOrderID > 0)
            {
                if (iNV_PurchaseOrder.Remarks == null || iNV_PurchaseOrder.Remarks == "")
                {
                    ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.CGST);
                    ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.IGST);
                    ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", iNV_PurchaseOrder.SGST);
                    ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
                    ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_PurchaseOrder.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(iNV_PurchaseOrder);
                }
            }
            if (ModelState.IsValid)
            {
                iNV_PurchaseOrder.StatusID = CommonConfig.GetStatusPending();
                db.Entry(iNV_PurchaseOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax", iNV_PurchaseOrder.CGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax", iNV_PurchaseOrder.IGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax", iNV_PurchaseOrder.SGST);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_PurchaseOrder.FinYearID);
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", iNV_PurchaseOrder.SellerPartyID);
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
            try
            {
                var poitem = db.INV_PurchaseOrderItem.Where(poi => poi.PurchaseOrderID == iNV_PurchaseOrder.PurchaseOrderID).ToList();
                db.INV_PurchaseOrderItem.RemoveRange(poitem);
                db.SaveChanges();

                var poHistory = db.INV_PurchaseOrderHistory.Where(poi => poi.PurchaseOrderID == iNV_PurchaseOrder.PurchaseOrderID).ToList();
                db.INV_PurchaseOrderHistory.RemoveRange(poHistory);
                db.SaveChanges();

                db.INV_PurchaseOrder.Remove(iNV_PurchaseOrder);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You can not Delete this PO.");
                return View(iNV_PurchaseOrder);
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

        // GET: INV_Invoice
        public ActionResult CreatePurchaseOrder()
        {
            var model = new INV_PurchaseOrderViewModal();
            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            model.IsLocal = true;
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
        public JsonResult AddPurchaseOrder(INV_PurchaseOrderViewModal iNV_PurchaseOrderViewModal)
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
                    if (iNV_PurchaseOrderViewModal.PurchaseOrderID > 0)
                    {
                        //var ToRemovedbINV_PurchaseOrderItem = db.INV_PurchaseOrderItem.Where(i => i.PurchaseOrderID == iNV_PurchaseOrderViewModal.PurchaseOrderID).ToList();
                        //db.INV_PurchaseOrderItem.RemoveRange(ToRemovedbINV_PurchaseOrderItem);
                        //var ToRemovedbINV_PurchaseOrderHistory = db.INV_PurchaseOrderHistory.Where(i => i.PurchaseOrderID == iNV_PurchaseOrderViewModal.PurchaseOrderID).ToList();
                        //db.INV_PurchaseOrderHistory.RemoveRange(ToRemovedbINV_PurchaseOrderHistory);
                        //var POtoremove = db.INV_PurchaseOrder.Where(i => i.PurchaseOrderID == iNV_PurchaseOrderViewModal.PurchaseOrderID);
                        //db.INV_PurchaseOrder.RemoveRange(POtoremove);
                        //db.SaveChanges();
                        int purchaseOrderID = iNV_PurchaseOrderViewModal.PurchaseOrderID;
                        foreach (var item in iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems)
                        {
                            var checkExist = db.INV_PurchaseOrderItem.Any(e => e.PurchaseOrderID == purchaseOrderID
                                                && e.ItemID == item.ItemID
                                                && e.OrderedQuantity == item.OrderedQuantity);
                            if (checkExist == false)
                            {
                                var newINV_PurchaseOrderItemAdd = new INV_PurchaseOrderItem()
                                {
                                    PurchaseOrderID = purchaseOrderID,
                                    ItemID = item.ItemID,
                                    OrderedQuantity = item.OrderedQuantity,
                                    ReceivedQuantity = 0,
                                    ReceviedDate = DateTime.Now,
                                    Created = DateTime.Now,
                                    Modified = DateTime.Now,
                                    Remarks = item.Remarks,
                                    PuchasePrice = item.PuchasePrice
                                };
                                db.INV_PurchaseOrderItem.Add(newINV_PurchaseOrderItemAdd);
                                db.SaveChanges();
                            }
                        }
                        iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems.ForEach(e =>
                        {
                            e.PurchaseOrderID = purchaseOrderID;
                            e.PurchaseOrderItemID = db.INV_PurchaseOrderItem.Where(t => t.PurchaseOrderID == purchaseOrderID && t.ItemID == e.ItemID && t.OrderedQuantity == e.OrderedQuantity).FirstOrDefault().PurchaseOrderItemID;
                        });
                        List<int> getAllList = iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems.Select(e => e.PurchaseOrderItemID).ToList();
                        List<INV_PurchaseOrderItem> removeRange = db.INV_PurchaseOrderItem.Where(t => t.PurchaseOrderID == purchaseOrderID && !getAllList.Contains(t.PurchaseOrderItemID)).Select(t => t).ToList();

                        db.INV_PurchaseOrderItem.RemoveRange(removeRange);
                        db.SaveChanges();

                        INV_PurchaseOrder get_purchaseOrder = db.INV_PurchaseOrder.Where(e => e.PurchaseOrderID == purchaseOrderID).FirstOrDefault();

                        INV_PurchaseOrderHistory Add_INV_PurchaseOrderHistory = new INV_PurchaseOrderHistory()
                        {
                            CompanyID = get_purchaseOrder.CompanyID,
                            SellerPartyID = get_purchaseOrder.SellerPartyID,
                            UserID = get_purchaseOrder.UserID,
                            StatusID = get_purchaseOrder.StatusID,
                            Amount = get_purchaseOrder.Amount,
                            PaidAmount = get_purchaseOrder.PaidAmount,
                            Created = get_purchaseOrder.Created,
                            Remarks = get_purchaseOrder.Remarks,
                            PendingAmount = get_purchaseOrder.PendingAmount,
                            Casar = get_purchaseOrder.Casar,
                            PONo = get_purchaseOrder.PONo,
                            PODate = get_purchaseOrder.PODate,
                            FinYearID = get_purchaseOrder.FinYearID,
                            IsLocal = get_purchaseOrder.IsLocal,
                            CGST = get_purchaseOrder.CGST,
                            CGSTAmount = get_purchaseOrder.CGSTAmount,
                            SGST = get_purchaseOrder.SGST,
                            SGSTAmount = get_purchaseOrder.SGSTAmount,
                            IGST = get_purchaseOrder.IGST,
                            IGSTAmount = get_purchaseOrder.IGSTAmount,
                            TotalAmount = get_purchaseOrder.TotalAmount,
                            Operation = "Purchase",
                            PurchaseOrderID = purchaseOrderID
                        };
                        db.INV_PurchaseOrderHistory.Add(Add_INV_PurchaseOrderHistory);
                        db.SaveChanges();

                        if (Session["UserID"] != null)
                        {
                            get_purchaseOrder.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        get_purchaseOrder.StatusID = 1;//ask to kamal
                        get_purchaseOrder.Amount = iNV_PurchaseOrderViewModal.Amount;
                        get_purchaseOrder.PaidAmount = iNV_PurchaseOrderViewModal.PaidAmount;
                        get_purchaseOrder.Modified = DateTime.Now;
                        get_purchaseOrder.PendingAmount = iNV_PurchaseOrderViewModal.PendingAmount;
                        get_purchaseOrder.Casar = iNV_PurchaseOrderViewModal.Casar;
                        get_purchaseOrder.CGST = iNV_PurchaseOrderViewModal.CGST;
                        get_purchaseOrder.CGSTAmount = iNV_PurchaseOrderViewModal.CGSTAmount;
                        get_purchaseOrder.SGST = iNV_PurchaseOrderViewModal.SGST;
                        get_purchaseOrder.SGSTAmount = iNV_PurchaseOrderViewModal.SGSTAmount;
                        get_purchaseOrder.IGST = iNV_PurchaseOrderViewModal.IGST;
                        get_purchaseOrder.IGSTAmount = iNV_PurchaseOrderViewModal.IGSTAmount;

                        db.SaveChanges();
                    }
                    else
                    {

                        INV_PurchaseOrder new_INV_PurchaseOrder = new INV_PurchaseOrder();
                        new_INV_PurchaseOrder.CompanyID = CommonConfig.GetCompanyID();
                        new_INV_PurchaseOrder.StatusID = CommonConfig.GetStatusPending();
                        new_INV_PurchaseOrder.FinYearID = CommonConfig.GetFinYearID();
                        new_INV_PurchaseOrder.SellerPartyID = iNV_PurchaseOrderViewModal.SellerPartyID;
                        if (Session["UserID"] != null)
                        {
                            new_INV_PurchaseOrder.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        new_INV_PurchaseOrder.Amount = iNV_PurchaseOrderViewModal.Amount;
                        new_INV_PurchaseOrder.PaidAmount = 0;
                        new_INV_PurchaseOrder.Created = DateTime.Now;
                        new_INV_PurchaseOrder.Modified = DateTime.Now;
                        new_INV_PurchaseOrder.Remarks = iNV_PurchaseOrderViewModal.Remarks;
                        new_INV_PurchaseOrder.PODate = DateTime.Now;

                        //Int32 TotalForMonth = db.INV_PurchaseOrder.Where(p => p.Created.Month == DateTime.Today.Month && p.Created.Year == DateTime.Today.Year).Count();
                        //Int32 NextCount = TotalForMonth + 1;
                        //Int32 _NewPONOtCount = Convert.ToInt32(DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + NextCount);

                        ////Change PONUmber Add PO-
                        //new_INV_PurchaseOrder.PONo = _NewPONOtCount.ToString();

                        #region Generate InvoiceNo
                        String _NewPONOtCount = CommonConfig.GetNextNumber("PO");
                        #endregion Generate InvoiceNo
                        new_INV_PurchaseOrder.PONo = _NewPONOtCount.ToString();


                        new_INV_PurchaseOrder.PendingAmount = Convert.ToInt32(iNV_PurchaseOrderViewModal.TotalAmount);
                        if (iNV_PurchaseOrderViewModal.CGST > 0)
                            new_INV_PurchaseOrder.CGST = iNV_PurchaseOrderViewModal.CGST;
                        new_INV_PurchaseOrder.CGSTAmount = iNV_PurchaseOrderViewModal.CGSTAmount;
                        if (iNV_PurchaseOrderViewModal.SGST > 0)
                            new_INV_PurchaseOrder.SGST = iNV_PurchaseOrderViewModal.SGST;
                        new_INV_PurchaseOrder.SGSTAmount = iNV_PurchaseOrderViewModal.SGSTAmount;
                        if (iNV_PurchaseOrderViewModal.IGST > 0)
                            new_INV_PurchaseOrder.IGST = iNV_PurchaseOrderViewModal.IGST;
                        new_INV_PurchaseOrder.IGSTAmount = iNV_PurchaseOrderViewModal.IGSTAmount;
                        new_INV_PurchaseOrder.IsLocal = iNV_PurchaseOrderViewModal.IsLocal;
                        new_INV_PurchaseOrder.Casar = 0;
                        new_INV_PurchaseOrder.TotalAmount = iNV_PurchaseOrderViewModal.TotalAmount;

                        db.INV_PurchaseOrder.Add(new_INV_PurchaseOrder);
                        db.SaveChanges();


                        #region INV_PurchaseOrderHistory
                        INV_PurchaseOrderHistory new_INV_PurchaseOrderHistory = new INV_PurchaseOrderHistory();
                        new_INV_PurchaseOrderHistory = new INV_PurchaseOrderHistory()
                        {
                            CompanyID = new_INV_PurchaseOrder.CompanyID,
                            SellerPartyID = new_INV_PurchaseOrder.SellerPartyID,
                            UserID = new_INV_PurchaseOrder.UserID,
                            StatusID = new_INV_PurchaseOrder.StatusID,
                            Amount = new_INV_PurchaseOrder.Amount,
                            Created = DateTime.Now,
                            PaidAmount = new_INV_PurchaseOrder.PaidAmount,
                            Remarks = new_INV_PurchaseOrder.Remarks,
                            PendingAmount = new_INV_PurchaseOrder.PendingAmount,
                            Casar = new_INV_PurchaseOrder.Casar,
                            PONo = new_INV_PurchaseOrder.PONo.ToString(),
                            PODate = new_INV_PurchaseOrder.PODate,
                            FinYearID = new_INV_PurchaseOrder.FinYearID,
                            IsLocal = new_INV_PurchaseOrder.IsLocal,
                            CGST = new_INV_PurchaseOrder.CGST,
                            CGSTAmount = new_INV_PurchaseOrder.CGSTAmount,
                            SGST = new_INV_PurchaseOrder.SGST,
                            SGSTAmount = new_INV_PurchaseOrder.SGSTAmount,
                            IGST = new_INV_PurchaseOrder.IGST,
                            IGSTAmount = new_INV_PurchaseOrder.IGSTAmount,
                            TotalAmount = new_INV_PurchaseOrder.TotalAmount,
                            PurchaseOrderID = new_INV_PurchaseOrder.PurchaseOrderID,
                            ACC_Tax = new_INV_PurchaseOrder.ACC_Tax,
                            ACC_Tax1 = new_INV_PurchaseOrder.ACC_Tax1,
                            ACC_Tax2 = new_INV_PurchaseOrder.ACC_Tax2,
                            SYS_Company = new_INV_PurchaseOrder.SYS_Company,
                            SYS_FinYear = new_INV_PurchaseOrder.SYS_FinYear,
                            MST_Party = new_INV_PurchaseOrder.MST_Party,
                            SYS_Status = new_INV_PurchaseOrder.SYS_Status,
                            SEC_User = new_INV_PurchaseOrder.SEC_User
                        };
                        new_INV_PurchaseOrderHistory.Operation = "Purchase";
                        db.INV_PurchaseOrderHistory.Add(new_INV_PurchaseOrderHistory);
                        db.SaveChanges();
                        #endregion INV_PurchaseOrderHistory


                        int newInvoiceId = new_INV_PurchaseOrder.PurchaseOrderID;
                        List<INV_PurchaseOrderItem> newINV_PurchaseOrderItem = new List<INV_PurchaseOrderItem>();
                        foreach (var item in iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems)
                        {
                            INV_PurchaseOrderItem new_INV_PurchaseOrderItem = new INV_PurchaseOrderItem();
                            new_INV_PurchaseOrderItem.PurchaseOrderItemID = item.PurchaseOrderItemID;
                            new_INV_PurchaseOrderItem.PurchaseOrderID = newInvoiceId;
                            new_INV_PurchaseOrderItem.ItemID = item.ItemID;
                            new_INV_PurchaseOrderItem.OrderedQuantity = item.OrderedQuantity;
                            new_INV_PurchaseOrderItem.ReceivedQuantity = 0;
                            new_INV_PurchaseOrderItem.Created = DateTime.Now;
                            new_INV_PurchaseOrderItem.Modified = DateTime.Now;
                            new_INV_PurchaseOrderItem.Remarks = item.Remarks;
                            new_INV_PurchaseOrderItem.PuchasePrice = item.PuchasePrice;

                            #region INV_ItemPrice
                            INV_ItemPrice _iNV_ItemPrice = new INV_ItemPrice();
                            _iNV_ItemPrice = db.INV_ItemPrice.Where(M => M.ItemID == item.ItemID & M.PurchasePrice == item.PuchasePrice).OrderByDescending(o => o.Created).FirstOrDefault();
                            if (_iNV_ItemPrice == null)
                            {
                                _iNV_ItemPrice = new INV_ItemPrice();
                                _iNV_ItemPrice.ItemID = item.ItemID;
                                _iNV_ItemPrice.PurchasePrice = Convert.ToDecimal(item.PuchasePrice);
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

                            newINV_PurchaseOrderItem.Add(new_INV_PurchaseOrderItem);
                        }
                        db.INV_PurchaseOrderItem.AddRange(newINV_PurchaseOrderItem);
                        db.SaveChanges();

                    }

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
            INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal;

            var POData = db.INV_PurchaseOrder.Find(id);
            _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal()
            {
                PurchaseOrderID = POData.PurchaseOrderID,
                CompanyID = POData.CompanyID,
                SellerPartyID = POData.SellerPartyID,
                PartyIDName = POData.MST_Party.PartyName,
                UserID = POData.UserID,
                Amount = POData.Amount,
                PaidAmount = POData.PaidAmount,
                StatusID = POData.StatusID,
                Created = POData.Created,
                Modified = POData.Modified,
                Remarks = POData.Remarks,
                PODate = POData.PODate,
                PONo = POData.PONo,
                FinYearID = CommonConfig.GetFinYearID(),
                CGST = POData.CGST,
                CGSTAmount = POData.CGSTAmount,
                SGST = POData.SGST,
                SGSTAmount = POData.SGSTAmount,
                IGST = POData.IGST,
                IGSTAmount = POData.IGSTAmount,
                IsLocal = POData.IsLocal,
                Casar = POData.Casar,
                TotalAmount = POData.TotalAmount
            };
            _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == id).ToList();
            if (_iNV_PurchaseOrderViewModal == null)
            {
                return HttpNotFound();
            }
            return View(_iNV_PurchaseOrderViewModal);
        }

        // GET: GRN
        public ActionResult POReturn(int id = -1)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal();
            if (id > 0)
            {
                var POData = db.INV_PurchaseOrder.Find(id);
                _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal()
                {
                    PurchaseOrderID = POData.PurchaseOrderID,
                    CompanyID = POData.CompanyID,
                    SellerPartyID = POData.SellerPartyID,
                    PartyIDName = POData.MST_Party.PartyName,
                    UserID = POData.UserID,
                    Amount = POData.Amount,
                    PaidAmount = POData.PaidAmount,
                    StatusID = POData.StatusID,
                    Created = POData.Created,
                    Modified = POData.Modified,
                    Remarks = POData.Remarks,
                    PODate = POData.PODate,
                    PONo = POData.PONo,
                    FinYearID = CommonConfig.GetFinYearID(),
                    CGST = POData.CGST,
                    CGSTAmount = POData.CGSTAmount,
                    SGST = POData.SGST,
                    SGSTAmount = POData.SGSTAmount,
                    IGST = POData.IGST,
                    IGSTAmount = POData.IGSTAmount,
                    IsLocal = POData.IsLocal,
                    Casar = POData.Casar,
                    TotalAmount = POData.TotalAmount
                };
                _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == id).ToList();
                _iNV_PurchaseOrderViewModal.INV_PurchaseOrderHistory = db.INV_PurchaseOrderHistory.Where(I => I.PurchaseOrderID == id).ToList();
                if (_iNV_PurchaseOrderViewModal == null)
                {
                    return HttpNotFound();
                }
            }

            ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
            ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
            ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            ViewData["errorPOReceive"] = TempData["errorPOReceive"];
            return View(_iNV_PurchaseOrderViewModal);
        }

        [HttpPost]
        public ActionResult POReturn(INV_PurchaseOrderViewModal iNV_PurchaseOrderViewModal)
        {
            try
            {
                if (iNV_PurchaseOrderViewModal == null)
                {
                    return RedirectToAction("Index");
                    //error meesage or expception handle
                }
                else if (iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems == null)
                {
                    //error meesage or expception handle
                    return RedirectToAction("Index");
                }
                else
                {
                    INV_PurchaseOrder new_INV_PurchaseOrder = new INV_PurchaseOrder();
                    List<INV_PurchaseOrderItem> itemsFromDB = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == iNV_PurchaseOrderViewModal.PurchaseOrderID).ToList();
                    if (Session["UserID"] != null)
                    {
                        new_INV_PurchaseOrder.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }

                    INV_PurchaseOrder _INV_PurchaseOrder = db.INV_PurchaseOrder.Find(iNV_PurchaseOrderViewModal.PurchaseOrderID);
                    string Err = "";
                    TempData["errorPOReceive"] = "";

                    foreach (var item in iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems)
                    {
                        INV_PurchaseOrderItem new_INV_PurchaseOrderItem = db.INV_PurchaseOrderItem.Find(item.PurchaseOrderItemID);
                        db.Entry(new_INV_PurchaseOrderItem).State = EntityState.Modified;
                        new_INV_PurchaseOrderItem.PurchaseOrderItemID = item.PurchaseOrderItemID;
                        if (new_INV_PurchaseOrderItem.ReceivedQuantity + item.ReceivedQuantity > new_INV_PurchaseOrderItem.OrderedQuantity)
                        {
                            INV_Item inv_Item = new INV_Item();
                            inv_Item = db.INV_Item.Where(i => i.ItemID == item.ItemID).FirstOrDefault();

                            if (Err == "")
                                Err = Err + "You can not receive more than ordered. " + inv_Item.ItemName;
                            else
                                Err = Err + ", You can not receive more than ordered. " + inv_Item.ItemName;


                        }
                        if ((new_INV_PurchaseOrderItem.ReceivedQuantity + item.ReceivedQuantity) == new_INV_PurchaseOrderItem.OrderedQuantity)
                        {
                            db.Entry(_INV_PurchaseOrder).State = EntityState.Modified;
                            _INV_PurchaseOrder.StatusID = 2;
                        }
                        new_INV_PurchaseOrderItem.ReceivedQuantity = new_INV_PurchaseOrderItem.ReceivedQuantity + item.ReceivedQuantity;
                        new_INV_PurchaseOrderItem.Modified = DateTime.Now;
                        new_INV_PurchaseOrderItem.Remarks = item.Remarks;
                        new_INV_PurchaseOrderItem.PuchasePrice = item.PuchasePrice;

                        #region INV_ItemPrice
                        INV_ItemPrice _iNV_ItemPrice = new INV_ItemPrice();
                        _iNV_ItemPrice = db.INV_ItemPrice.Where(M => M.ItemID == item.ItemID & M.PurchasePrice == item.PuchasePrice).OrderByDescending(o => o.Created).FirstOrDefault();
                        if (_iNV_ItemPrice == null)
                        {
                            _iNV_ItemPrice = new INV_ItemPrice();
                            _iNV_ItemPrice.ItemID = item.ItemID;
                            _iNV_ItemPrice.PurchasePrice = Convert.ToDecimal(item.PuchasePrice);
                            _iNV_ItemPrice.Created = DateTime.Now;
                            _iNV_ItemPrice.Modified = DateTime.Now;
                            _iNV_ItemPrice.FinYearID = CommonConfig.GetFinYearID();
                            if (Session["UserID"] != null)
                            {
                                _iNV_ItemPrice.UserID = Convert.ToInt16(Session["UserID"].ToString());
                            }
                            db.INV_ItemPrice.Add(_iNV_ItemPrice);
                        }
                        #endregion INV_ItemPrice

                        #region Item
                        INV_Item _INV_Item = new INV_Item();
                        _INV_Item = db.INV_Item.Where(i => i.ItemID == item.ItemID).FirstOrDefault();

                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 8;
                        new_INV_StockHistory.ReferenceID = iNV_PurchaseOrderViewModal.PONo;
                        new_INV_StockHistory.Quantity = item.ReceivedQuantity;
                        if (Session["UserID"] != null)
                        {
                            new_INV_StockHistory.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }
                        new_INV_StockHistory.Created = DateTime.Now;
                        new_INV_StockHistory.Modified = DateTime.Now;
                        new_INV_StockHistory.Remarks = "Return";
                        new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                        new_INV_StockHistory.IssueNumber = iNV_PurchaseOrderViewModal.PONo;
                        db.INV_StockHistory.Add(new_INV_StockHistory);

                        if (_INV_Item != null)
                        {
                            _INV_Item.Quantity = _INV_Item.Quantity + item.ReceivedQuantity;
                        }
                        #endregion Item
                    }
                    if (Err != "")
                    {

                        
                        #region GET PO Data
                        db.Dispose();
                        db = new DB_A157D8_AnjaliMISEntities1();
                        ViewBag.CGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "CGST"), "TaxID", "Tax");
                        ViewBag.IGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "IGST"), "TaxID", "Tax");
                        ViewBag.SGST = new SelectList(db.ACC_Tax.Where(a => a.TaxType == "SGST"), "TaxID", "Tax");
                        ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
                        ViewBag.SellerPartyID = new SelectList(db.MST_Party, "PartyID", "PartyName");
                        ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
                        ViewBag.ItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
                        ModelState.AddModelError("errorPOReceive", Err);
                        TempData["errorPOReceive"] = Err;
                        ViewData["errorPOReceive"] = TempData["errorPOReceive"];
                        INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal();

                        var POData = db.INV_PurchaseOrder.Find(iNV_PurchaseOrderViewModal.PurchaseOrderID);
                        _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal()
                        {
                            PurchaseOrderID = POData.PurchaseOrderID,
                            CompanyID = POData.CompanyID,
                            SellerPartyID = POData.SellerPartyID,
                            PartyIDName = POData.MST_Party.PartyName,
                            UserID = POData.UserID,
                            Amount = POData.Amount,
                            PaidAmount = POData.PaidAmount,
                            StatusID = POData.StatusID,
                            Created = POData.Created,
                            Modified = POData.Modified,
                            Remarks = POData.Remarks,
                            PODate = POData.PODate,
                            PONo = POData.PONo,
                            FinYearID = CommonConfig.GetFinYearID(),
                            CGST = POData.CGST,
                            CGSTAmount = POData.CGSTAmount,
                            SGST = POData.SGST,
                            SGSTAmount = POData.SGSTAmount,
                            IGST = POData.IGST,
                            IGSTAmount = POData.IGSTAmount,
                            IsLocal = POData.IsLocal,
                            Casar = POData.Casar,
                            TotalAmount = POData.TotalAmount
                        };
                        _iNV_PurchaseOrderViewModal.INV_PurchaseOrderItems = db.INV_PurchaseOrderItem.Where(I => I.PurchaseOrderID == POData.PurchaseOrderID).ToList();
                        _iNV_PurchaseOrderViewModal.INV_PurchaseOrderHistory = db.INV_PurchaseOrderHistory.Where(I => I.PurchaseOrderID == POData.PurchaseOrderID).ToList();
                        #endregion GET PO Data

                        return View(_iNV_PurchaseOrderViewModal);
                    }

                    if (_INV_PurchaseOrder != null)
                    {
                        if (iNV_PurchaseOrderViewModal.IsComplete)
                        {
                            db.Entry(_INV_PurchaseOrder).State = EntityState.Modified;
                            _INV_PurchaseOrder.StatusID = 2;
                            db.SaveChanges();
                        }

                        #region INV_PurchaseOrderHistory
                        INV_PurchaseOrderHistory new_INV_PurchaseOrderHistory = new INV_PurchaseOrderHistory();
                        new_INV_PurchaseOrderHistory = new INV_PurchaseOrderHistory()
                        {
                            CompanyID = _INV_PurchaseOrder.CompanyID,
                            SellerPartyID = _INV_PurchaseOrder.SellerPartyID,
                            UserID = _INV_PurchaseOrder.UserID,
                            StatusID = _INV_PurchaseOrder.StatusID,
                            Amount = _INV_PurchaseOrder.Amount,
                            PaidAmount = _INV_PurchaseOrder.PaidAmount,
                            Remarks = _INV_PurchaseOrder.Remarks,
                            PendingAmount = _INV_PurchaseOrder.PendingAmount,
                            Created = DateTime.Now,
                            Casar = _INV_PurchaseOrder.Casar,
                            PONo = _INV_PurchaseOrder.PONo,
                            PODate = _INV_PurchaseOrder.PODate,
                            FinYearID = _INV_PurchaseOrder.FinYearID,
                            IsLocal = _INV_PurchaseOrder.IsLocal,
                            CGST = _INV_PurchaseOrder.CGST,
                            CGSTAmount = _INV_PurchaseOrder.CGSTAmount,
                            SGST = _INV_PurchaseOrder.SGST,
                            SGSTAmount = _INV_PurchaseOrder.SGSTAmount,
                            IGST = _INV_PurchaseOrder.IGST,
                            IGSTAmount = _INV_PurchaseOrder.IGSTAmount,
                            TotalAmount = _INV_PurchaseOrder.TotalAmount,
                            PurchaseOrderID = _INV_PurchaseOrder.PurchaseOrderID,
                            ACC_Tax = _INV_PurchaseOrder.ACC_Tax,
                            ACC_Tax1 = _INV_PurchaseOrder.ACC_Tax1,
                            ACC_Tax2 = _INV_PurchaseOrder.ACC_Tax2,
                            SYS_Company = _INV_PurchaseOrder.SYS_Company,
                            SYS_FinYear = _INV_PurchaseOrder.SYS_FinYear,
                            MST_Party = _INV_PurchaseOrder.MST_Party,
                            SYS_Status = _INV_PurchaseOrder.SYS_Status,
                            SEC_User = _INV_PurchaseOrder.SEC_User
                        };
                        if (_INV_PurchaseOrder.StatusID == 2)
                        {
                            new_INV_PurchaseOrderHistory.Operation = "Complete";
                        }
                        else
                        {
                            new_INV_PurchaseOrderHistory.Operation = "Purchase";

                        }
                        db.INV_PurchaseOrderHistory.Add(new_INV_PurchaseOrderHistory);
                        #endregion INV_PurchaseOrderHistory
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        // GET: GRN
        public JsonResult POSearch(string PONumber = "")
        {
            Int32 PurchaseOrderID = -1;
            INV_PurchaseOrderViewModal _iNV_PurchaseOrderViewModal = new INV_PurchaseOrderViewModal();
            if (PONumber != "")
            {
                var _PurchaseOrder = db.INV_PurchaseOrder.Where(p => p.PONo == PONumber).FirstOrDefault();
                if (_PurchaseOrder != null)
                {
                    PurchaseOrderID = _PurchaseOrder.PurchaseOrderID;
                }

            }
            if (PurchaseOrderID > 0)
            {
                return Json(PurchaseOrderID, JsonRequestBehavior.AllowGet);
            }
            //return RedirectToAction("POReturn", new { id = PurchaseOrderID });

            return Json("Failure", JsonRequestBehavior.AllowGet);
        }

    }
}
