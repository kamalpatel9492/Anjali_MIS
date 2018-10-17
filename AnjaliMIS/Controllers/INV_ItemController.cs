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
    public class INV_ItemController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_Item
        public ActionResult Index()
        {
            var iNV_Item = db.INV_Item.Where(i => i.IsActive != false).Include(i => i.SYS_Company).Include(i => i.INV_Unit).Include(i => i.SEC_User).Include(i => i.INV_Category).OrderByDescending(o => o.Created);
            return View(iNV_Item.ToList());
        }

        // GET: INV_Item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Item iNV_Item = db.INV_Item.Find(id);
            if (iNV_Item == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Item);
        }

        // GET: INV_Item/Create
        public ActionResult Create()
        {
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            ViewBag.CategoryID = new SelectList(db.INV_Category, "CategoryID", "CategoryName");

            INV_Item _iNV_Item = new INV_Item();
            return View("Edit", _iNV_Item);
        }

        // POST: INV_Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,CompanyID,ItemName,UnitID,UserID,IsConfigurable,IsLock,Quantity,MinStockLimit,Created,Modified,Remarks,RejectedQuantity,CategoryID,ItemCode")] INV_Item iNV_Item)
        {

            #region Validation
            if (string.IsNullOrEmpty(iNV_Item.ItemName))
            {
                ModelState.AddModelError("ItemName", "Item is required");
            }
            if (!string.IsNullOrEmpty(iNV_Item.ItemName))
            {
                if (db.INV_Item.Where(I => I.ItemName == iNV_Item.ItemName && I.CategoryID == iNV_Item.CategoryID).Count() > 0)
                {
                    ModelState.AddModelError("ItemNameDuplicate", iNV_Item.ItemName + " Already added.");
                }
            }
            #endregion Validation

            if (ModelState.IsValid)
            {
                iNV_Item.Created = DateTime.Now;
                iNV_Item.Modified = DateTime.Now;
                iNV_Item.CompanyID = 4;

                #region Generate Item Code
                if (iNV_Item.CategoryID != null && iNV_Item.CategoryID > 0)
                {
                    Int32 NextNumber = db.INV_Item.Where(i => i.CategoryID == iNV_Item.CategoryID).Count() + 1;
                    String PrefixForCode = db.INV_Category.Where(i => i.CategoryID == iNV_Item.CategoryID).FirstOrDefault().CategoryShortName;
                    iNV_Item.ItemCode = PrefixForCode + "-" + NextNumber;
                }
                else
                {
                    Int32 NextNumber = db.INV_Item.Count() + 1;
                    String PrefixForCode = "ITM";
                    iNV_Item.ItemCode = PrefixForCode + "-" + NextNumber;
                }


                #endregion Generate Item Code

                if (Session["UserID"] != null)
                {
                    iNV_Item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.INV_Item.Add(iNV_Item);
                db.SaveChanges();

                #region Update INV_StockHistory 
                int newPK = iNV_Item.ItemID;
                INV_StockHistory _iNV_StockHistory = new INV_StockHistory();
                _iNV_StockHistory.ItemID = iNV_Item.ItemID;
                _iNV_StockHistory.OperationTypeID = 6;
                _iNV_StockHistory.Quantity = iNV_Item.Quantity;
                _iNV_StockHistory.UserID = iNV_Item.UserID;
                _iNV_StockHistory.FinYearID = CommonConfig.GetFinYearID();
                _iNV_StockHistory.Created = DateTime.Now;
                _iNV_StockHistory.Modified = DateTime.Now;
                db.INV_StockHistory.Add(_iNV_StockHistory);
                db.SaveChanges();
                #endregion Update INV_StockHistory 

                return RedirectToAction("Index");
            }

            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
            ViewBag.CategoryID = new SelectList(db.INV_Category, "CategoryID", "CategoryName", iNV_Item.CategoryID);
            return View("Edit", iNV_Item);
        }

        // GET: INV_Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Item iNV_Item = db.INV_Item.Find(id);
            iNV_Item.Remarks = "";
            if (iNV_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
            ViewBag.CategoryID = new SelectList(db.INV_Category, "CategoryID", "CategoryName", iNV_Item.CategoryID);
            return View(iNV_Item);
        }

        // POST: INV_Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,CompanyID,ItemName,UnitID,UserID,IsConfigurable,IsLock,Quantity,MinStockLimit,Created,Modified,Remarks,RejectedQuantity,CategoryID,ItemCode")] INV_Item iNV_Item)
        {
            if (iNV_Item.ItemID > 0)
            {
                if (iNV_Item.Remarks == null || iNV_Item.Remarks == "")
                {
                    ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
                    ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
                    ViewBag.CategoryID = new SelectList(db.INV_Category, "CategoryID", "CategoryName", iNV_Item.CategoryID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(iNV_Item);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(iNV_Item).State = EntityState.Modified;
                iNV_Item.Modified = DateTime.Now;
                iNV_Item.CompanyID = 4;
                if (Session["UserID"] != null)
                {
                    iNV_Item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                #region Generate Item Code

                if (iNV_Item.ItemID <= 0)
                {
                    if (iNV_Item.CategoryID != null && iNV_Item.CategoryID > 0)
                    {
                        Int32 NextNumber = db.INV_Item.Where(i => i.CategoryID == iNV_Item.CategoryID).Count() + 1;
                        String PrefixForCode = db.INV_Category.Where(i => i.CategoryID == iNV_Item.CategoryID).FirstOrDefault().CategoryShortName;
                        iNV_Item.ItemCode = PrefixForCode + "-" + NextNumber;
                    }
                    else
                    {
                        Int32 NextNumber = db.INV_Item.Count() + 1;
                        String PrefixForCode = "ITM";
                        iNV_Item.ItemCode = PrefixForCode + "-" + NextNumber;
                    }
                }

                #endregion Generate Item Code

                db.SaveChanges();
                #region Update INV_StockHistory 
                INV_StockHistory _iNV_StockHistoryOld = db.INV_StockHistory.Where(IS => IS.ItemID == iNV_Item.ItemID).FirstOrDefault();
                if (_iNV_StockHistoryOld != null)
                {
                    _iNV_StockHistoryOld.ItemID = iNV_Item.ItemID;
                    _iNV_StockHistoryOld.OperationTypeID = 6;
                    _iNV_StockHistoryOld.Quantity = iNV_Item.Quantity;
                    _iNV_StockHistoryOld.UserID = iNV_Item.UserID;
                    _iNV_StockHistoryOld.FinYearID = CommonConfig.GetFinYearID();
                    _iNV_StockHistoryOld.Modified = DateTime.Now;
                    db.Entry(_iNV_StockHistoryOld).State = EntityState.Modified;
                    db.SaveChanges();
                }
                #endregion Update INV_StockHistory 
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
            ViewBag.CategoryID = new SelectList(db.INV_Category, "CategoryID", "CategoryName", iNV_Item.CategoryID);
            return View(iNV_Item);
        }

        // GET: INV_Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_Item iNV_Item = db.INV_Item.Find(id);
            if (iNV_Item == null)
            {
                return HttpNotFound();
            }
            return View(iNV_Item);
        }

        // POST: INV_Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            INV_Item iNV_Item = db.INV_Item.Find(id);
            try
            {
                if (iNV_Item.IsLock)
                {
                    iNV_Item.IsActive = false;
                    db.Entry(iNV_Item).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    List<INV_StockHistory> StockHistory = db.INV_StockHistory.Where(i => i.ItemID == iNV_Item.ItemID).ToList();
                    db.INV_StockHistory.RemoveRange(StockHistory);
                    db.SaveChanges();
                    db.INV_Item.Remove(iNV_Item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You can not Delete this Item.");
                //return RedirectToAction("Index");
                return View(iNV_Item);
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
        // GET: INV_Item
        public ActionResult Dashboard()
        {
            var modal = new DashboardViewModal();
            modal.InvoiceCount = db.INV_Invoice.Count();
            modal.ItemCount = db.INV_Item.Count();
            modal.POCount = db.INV_PurchaseOrder.Count();
            return View(modal);
        }
    }
}

