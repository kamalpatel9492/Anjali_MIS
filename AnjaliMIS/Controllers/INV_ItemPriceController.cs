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
    public class INV_ItemPriceController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_ItemPrice
        public ActionResult Index()
        {
            var iNV_ItemPrice = db.INV_ItemPrice.OrderByDescending(o=>o.Created).Include(i => i.INV_Item).Include(i => i.SYS_FinYear).Include(i => i.SEC_User);
            return View(iNV_ItemPrice.ToList());
        }

        // GET: INV_ItemPrice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemPrice iNV_ItemPrice = db.INV_ItemPrice.Find(id);
            if (iNV_ItemPrice == null)
            {
                return HttpNotFound();
            }
            return View(iNV_ItemPrice);
        }

        // GET: INV_ItemPrice/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            INV_ItemPrice _iNV_ItemPrice = new INV_ItemPrice();
            return View("Edit", _iNV_ItemPrice);
        }

        // POST: INV_ItemPrice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemPriceID,ItemID,PurchasePrice,UserID,Created,Modified,Remarks,FinYearID")] INV_ItemPrice iNV_ItemPrice)
        {
            #region Validation
            if (string.IsNullOrEmpty(iNV_ItemPrice.ItemID.ToString()))
            {
                ModelState.AddModelError("ItemID", "Item is required");
            }
            if (string.IsNullOrEmpty(iNV_ItemPrice.PurchasePrice.ToString()))
            {
                ModelState.AddModelError("PurchasePrice", "Purchase Price is required");
            }
            if (string.IsNullOrEmpty(iNV_ItemPrice.FinYearID.ToString()))
            {
                ModelState.AddModelError("FinYearID", "Fin Year is required");
            }

            #endregion Validation

            iNV_ItemPrice.Created = DateTime.Now;
            iNV_ItemPrice.Modified = DateTime.Now;
            if (Session["UserID"] != null)
            {
                iNV_ItemPrice.UserID = Convert.ToInt16(Session["UserID"].ToString());
            }
            if (ModelState.IsValid)
            {
                db.INV_ItemPrice.Add(iNV_ItemPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemPrice.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_ItemPrice.FinYearID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemPrice.UserID);
            return View("Edit", iNV_ItemPrice);
        }

        // GET: INV_ItemPrice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemPrice iNV_ItemPrice = db.INV_ItemPrice.Find(id);
            if (iNV_ItemPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemPrice.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_ItemPrice.FinYearID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemPrice.UserID);
            return View(iNV_ItemPrice);
        }

        // POST: INV_ItemPrice/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemPriceID,ItemID,PurchasePrice,UserID,Created,Modified,Remarks,FinYearID")] INV_ItemPrice iNV_ItemPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_ItemPrice).State = EntityState.Modified;
                iNV_ItemPrice.Created = Convert.ToDateTime(iNV_ItemPrice.Created);
                iNV_ItemPrice.Modified = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    iNV_ItemPrice.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemPrice.ItemID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", iNV_ItemPrice.FinYearID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemPrice.UserID);
            return View(iNV_ItemPrice);
        }

        // GET: INV_ItemPrice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemPrice iNV_ItemPrice = db.INV_ItemPrice.Find(id);
            if (iNV_ItemPrice == null)
            {
                return HttpNotFound();
            }
            return View(iNV_ItemPrice);
        }

        // POST: INV_ItemPrice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_ItemPrice iNV_ItemPrice = db.INV_ItemPrice.Find(id);
            db.INV_ItemPrice.Remove(iNV_ItemPrice);
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
