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
    public class INV_ItemController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_Item
        public ActionResult Index()
        {
            var iNV_Item = db.INV_Item.Include(i => i.SYS_Company).Include(i => i.INV_Unit).Include(i => i.SEC_User);
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
            INV_Item _iNV_Item = new INV_Item();
            return View("Edit", _iNV_Item);
        }

        // POST: INV_Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,CompanyID,ItemName,UnitID,UserID,IsConfigurable,Quantity,MinStockLimit,Created,Modified,Remarks,RejectedQuantity")] INV_Item iNV_Item)
        {

            #region Validation
            if (string.IsNullOrEmpty(iNV_Item.ItemName))
            {
                ModelState.AddModelError("ItemName", "Item is required");
            }
            if (!string.IsNullOrEmpty(iNV_Item.ItemName))
            {
                if (db.INV_Item.Where(I => I.ItemName == iNV_Item.ItemName).Count() > 0)
                {
                    ModelState.AddModelError("ItemNameDuplicate", iNV_Item.ItemName + " Already added.");
                }
            }
            #endregion Validation

            if (ModelState.IsValid)
            {
                iNV_Item.Created = DateTime.Now;
                iNV_Item.Modified = DateTime.Now;
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
                _iNV_StockHistory.FinYearID = 2;
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
            if (iNV_Item == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
            return View(iNV_Item);
        }

        // POST: INV_Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,CompanyID,ItemName,UnitID,UserID,IsConfigurable,Quantity,MinStockLimit,Created,Modified,Remarks,RejectedQuantity")] INV_Item iNV_Item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_Item).State = EntityState.Modified;
                iNV_Item.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    iNV_Item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }

                db.SaveChanges();
                #region Update INV_StockHistory 
                INV_StockHistory _iNV_StockHistoryOld = db.INV_StockHistory.Where(IS => IS.ItemID == iNV_Item.ItemID).FirstOrDefault();
                _iNV_StockHistoryOld.ItemID = iNV_Item.ItemID;
                _iNV_StockHistoryOld.OperationTypeID = 6;
                _iNV_StockHistoryOld.Quantity = iNV_Item.Quantity;
                _iNV_StockHistoryOld.UserID = iNV_Item.UserID;
                _iNV_StockHistoryOld.FinYearID = 2;
                _iNV_StockHistoryOld.Modified = DateTime.Now;
                db.Entry(_iNV_StockHistoryOld).State = EntityState.Modified;
                db.SaveChanges();
                #endregion Update INV_StockHistory 
                return RedirectToAction("Index");
            }
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", iNV_Item.CompanyID);
            ViewBag.UnitID = new SelectList(db.INV_Unit, "UnitID", "Unit", iNV_Item.UnitID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_Item.UserID);
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
            db.INV_Item.Remove(iNV_Item);
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

