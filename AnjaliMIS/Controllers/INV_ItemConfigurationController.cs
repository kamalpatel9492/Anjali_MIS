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
    public class INV_ItemConfigurationController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: INV_ItemConfiguration
        public ActionResult Index()
        {
            var iNV_ItemConfiguration = db.INV_ItemConfiguration.Include(i => i.INV_Item).Include(i => i.INV_Item1).Include(i => i.SEC_User);
            return View(iNV_ItemConfiguration.ToList());
        }

        // GET: INV_ItemConfiguration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(id);
            if (iNV_ItemConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(iNV_ItemConfiguration);
        }

        // GET: INV_ItemConfiguration/Create
        public ActionResult Create()
        {
            ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            INV_ItemConfigurationViewModal _iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal();
            return View("Edit", _iNV_ItemConfigurationViewModal);
        }

        // POST: INV_ItemConfiguration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(INV_ItemConfiguration iNV_ItemConfiguration)
        {
            if (iNV_ItemConfiguration.INV_Items != null)
            {
                if (ModelState.IsValid)
                {
                    iNV_ItemConfiguration.Modified = DateTime.Now;
                    if (Session["UserID"] != null)
                    {
                        iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    List<INV_ItemConfiguration> newINV_ItemConfiguration = new List<INV_ItemConfiguration>();
                    foreach (var item in iNV_ItemConfiguration.INV_Items)
                    {
                        INV_ItemConfiguration new_INV_InvoiceItem = new INV_ItemConfiguration();
                        new_INV_InvoiceItem.MainItemID = iNV_ItemConfiguration.MainItemID;
                        new_INV_InvoiceItem.SubItemID = item.SubItemID;
                        new_INV_InvoiceItem.Qunatity = item.Qunatity;
                        new_INV_InvoiceItem.Created = DateTime.Now;
                        new_INV_InvoiceItem.Modified = DateTime.Now;
                        new_INV_InvoiceItem.UserID = iNV_ItemConfiguration.UserID;
                        new_INV_InvoiceItem.Remarks = iNV_ItemConfiguration.Remarks;

                        newINV_ItemConfiguration.Add(new_INV_InvoiceItem);
                    }
                    db.INV_ItemConfiguration.AddRange(newINV_ItemConfiguration);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: INV_ItemConfiguration/Edit/5
        //    public ActionResult Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //        }
        //        INV_ItemConfigurationViewModal iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal();
        //        INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(id);
        //        iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal()
        //        {
        //            ItemConfigurationID = iNV_ItemConfiguration.ItemConfigurationID,
        //            MainItemID = iNV_ItemConfiguration.MainItemID,
        //            SubItemID = iNV_ItemConfiguration.SubItemID,
        //            Qunatity = iNV_ItemConfiguration.Qunatity,
        //            UserID = iNV_ItemConfiguration.UserID,
        //            Created = iNV_ItemConfiguration.Created,
        //            Modified = iNV_ItemConfiguration.Modified,
        //            Remarks = iNV_ItemConfiguration.Remarks
        //        };
        //        //iNV_ItemConfigurationViewModal.INV_Items = db.INV_ItemConfiguration.Where()

        //        if (iNV_ItemConfigurationViewModal == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
        //        ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.SubItemID);
        //        ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
        //        return View(iNV_ItemConfigurationViewModal);
        //    }

        //    // POST: INV_ItemConfiguration/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit([Bind(Include = "ItemConfigurationID,MainItemID,SubItemID,Qunatity,UserID,Created,Modified,Remarks")] INV_ItemConfiguration iNV_ItemConfiguration)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(iNV_ItemConfiguration).State = EntityState.Modified;
        //            iNV_ItemConfiguration.Modified = DateTime.Now;
        //            if (Session["UserID"] != null)
        //            {
        //                iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
        //            }
        //List<INV_ItemConfiguration> newINV_ItemConfiguration = new List<INV_ItemConfiguration>();
        //foreach (var item in iNV_ItemConfiguration.INV_Items)
        //{
        //	INV_ItemConfiguration new_INV_InvoiceItem = new INV_ItemConfiguration();
        //	new_INV_InvoiceItem.MainItemID = iNV_ItemConfiguration.MainItemID;
        //	new_INV_InvoiceItem.SubItemID = item.SubItemID;
        //	new_INV_InvoiceItem.Qunatity = item.Qunatity;
        //	new_INV_InvoiceItem.Created = DateTime.Now;
        //	new_INV_InvoiceItem.Modified = DateTime.Now;

        //	newINV_ItemConfiguration.Add(new_INV_InvoiceItem);
        //}
        //db.INV_ItemConfiguration.AddRange(newINV_ItemConfiguration);
        //db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
        //        ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.SubItemID);
        //        ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
        //        return View(iNV_ItemConfiguration);
        //    }

        // GET: INV_ItemConfiguration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(id);
            if (iNV_ItemConfiguration == null)
            {
                return HttpNotFound();
            }
            return View(iNV_ItemConfiguration);
        }

        // POST: INV_ItemConfiguration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(id);
            db.INV_ItemConfiguration.Remove(iNV_ItemConfiguration);
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

        public JsonResult RetrieveItemList()
        {
            try
            {
                var itemList = db.INV_Item.Select(e => new
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
    }
}
