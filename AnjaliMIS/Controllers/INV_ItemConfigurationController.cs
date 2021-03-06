﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.ModelBinding;
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
            var iNV_ItemConfiguration = db.INV_ItemConfiguration.GroupBy(IC => IC.MainItemID)
                   .Select(IC => IC.FirstOrDefault()).Include(i => i.INV_Item).Include(i => i.SEC_User).ToList();

            //var iNV_ItemConfiguration = db.INV_ItemConfiguration.Distinct(i=>i.MainItemID).Include(i => i.INV_Item).Include(i => i.SEC_User);
            return View(iNV_ItemConfiguration);
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
            var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();

            ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName");
            ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            INV_ItemConfigurationViewModal _iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal();
            ViewData["errorConfig"] = TempData["errorConfig"];
            return View("Edit", _iNV_ItemConfigurationViewModal);
        }

        // POST: INV_ItemConfiguration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(INV_ItemConfiguration iNV_ItemConfiguration)
        {
            //if (db.INV_ItemConfiguration.Where(I => I.MainItemID == iNV_ItemConfiguration.MainItemID).Count() > 0)
            //{
            //    ModelState.AddModelError("ItemNameDuplicate", "Selected item is already configured.");
            //}

            if (iNV_ItemConfiguration.ItemConfigurationID > 0)
            {
                if (iNV_ItemConfiguration.Remarks == null || iNV_ItemConfiguration.Remarks == "")
                {
                    var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();
                    iNV_ItemConfiguration.INV_Items = db.INV_ItemConfiguration.Where(ic => ic.MainItemID == iNV_ItemConfiguration.MainItemID).ToList();
                    ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
                    ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(iNV_ItemConfiguration);
                }
            }

            if (iNV_ItemConfiguration.INV_Items != null)
            {
                if (ModelState.IsValid)
                {
                    iNV_ItemConfiguration.Modified = DateTime.Now;
                    if (Session["UserID"] != null)
                    {
                        iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    var ToRemovedbINV_ItemConfiguration = db.INV_ItemConfiguration.Where(i => i.MainItemID == iNV_ItemConfiguration.MainItemID).ToList();
                    db.INV_ItemConfiguration.RemoveRange(ToRemovedbINV_ItemConfiguration);
                    db.SaveChanges();
                    String Err = "";
                    List<INV_ItemConfiguration> newINV_ItemConfiguration = new List<INV_ItemConfiguration>();
                    foreach (var item in iNV_ItemConfiguration.INV_Items)
                    {
                        if (item.Qunatity <= 0)
                        {
                            INV_Item _Subitem = db.INV_Item.Find(item.SubItemID);
                            if (_Subitem != null)
                            {
                                if (Err == "")
                                    Err = "Enter Valid Quantity for " + _Subitem.ItemName;
                                else
                                    Err += ", Enter Valid Quantity for " + _Subitem.ItemName;
                            }

                        }
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
                    if (Err != "")
                    {
                        var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();

                        ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName");
                        ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
                        ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
                        TempData["errorConfig"] = Err;
                        return View(iNV_ItemConfiguration);
                    }

                    db.INV_ItemConfiguration.AddRange(newINV_ItemConfiguration);
                    db.SaveChanges();
                }
            }
            var itemsa =
                     db.INV_Item
                       .Where(i => i.IsLock == true && i.IsConfigurable == true)
                       .Select(s => new
                       {
                           ItemID = s.ItemID,
                           ItemName = s.ItemName + " - " + s.ItemCode
                       })
                       .ToList();

            ViewBag.MainItemID = new SelectList(itemsa, "ItemID", "ItemName");
            ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            INV_ItemConfigurationViewModal iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal();
            ViewData["errorConfig"] = TempData["errorConfig"];
            return View("Edit", iNV_ItemConfigurationViewModal);
        }

        // GET: INV_ItemConfiguration/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INV_ItemConfigurationViewModal iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal();
            INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(id);
            if (iNV_ItemConfiguration != null)
            {
                iNV_ItemConfiguration.Remarks = "";
                iNV_ItemConfigurationViewModal = new INV_ItemConfigurationViewModal()
                {
                    ItemConfigurationID = iNV_ItemConfiguration.ItemConfigurationID,
                    MainItemID = iNV_ItemConfiguration.MainItemID,
                    SubItemID = iNV_ItemConfiguration.SubItemID,
                    Qunatity = iNV_ItemConfiguration.Qunatity,
                    UserID = iNV_ItemConfiguration.UserID,
                    Created = iNV_ItemConfiguration.Created,
                    Modified = iNV_ItemConfiguration.Modified,
                    Remarks = iNV_ItemConfiguration.Remarks
                };
                iNV_ItemConfigurationViewModal.SubItems = db.INV_ItemConfiguration.Where(ic => ic.MainItemID == iNV_ItemConfiguration.MainItemID).ToList();
            }

            if (iNV_ItemConfigurationViewModal == null)
            {
                return HttpNotFound();
            }
            var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();

            ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
            ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
            return View(iNV_ItemConfigurationViewModal);
        }

        // POST: INV_ItemConfiguration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(INV_ItemConfigurationViewModal iNV_ItemConfigurationViewModal)
        {
            if (iNV_ItemConfigurationViewModal.ItemConfigurationID > 0)
            {
                if (iNV_ItemConfigurationViewModal.Remarks == null || iNV_ItemConfigurationViewModal.Remarks == "")
                {
                    var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();
                    iNV_ItemConfigurationViewModal.SubItems = db.INV_ItemConfiguration.Where(ic => ic.MainItemID == iNV_ItemConfigurationViewModal.MainItemID).ToList();
                    ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName", iNV_ItemConfigurationViewModal.MainItemID);
                    ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfigurationViewModal.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(iNV_ItemConfigurationViewModal);
                }
            }

            if (iNV_ItemConfigurationViewModal.SubItems.Count > 0)
            {
                if (ModelState.IsValid)
                {
                    INV_ItemConfiguration iNV_ItemConfiguration = db.INV_ItemConfiguration.Find(iNV_ItemConfigurationViewModal.ItemConfigurationID);

                    db.Entry(iNV_ItemConfiguration).State = EntityState.Modified;
                    iNV_ItemConfiguration.Modified = DateTime.Now;
                    if (Session["UserID"] != null)
                    {
                        iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    String Err = "";
                    TempData["errorIssue"] = "";
                    List<INV_ItemConfiguration> newINV_ItemConfiguration = new List<INV_ItemConfiguration>();
                    foreach (var item in iNV_ItemConfiguration.INV_Items)
                    {
                        if (item.Qunatity <= 0)
                        {
                            INV_Item _Subitem = db.INV_Item.Find(item.SubItemID);
                            if (_Subitem != null)
                            {
                                if (Err == "")
                                    Err = "Enter Valid Quantity for " + _Subitem.ItemName;
                                else
                                    Err += ", Enter Valid Quantity for " + _Subitem.ItemName;
                            }
                        }
                        INV_ItemConfiguration new_INV_InvoiceItem = new INV_ItemConfiguration();
                        new_INV_InvoiceItem.MainItemID = iNV_ItemConfiguration.MainItemID;
                        new_INV_InvoiceItem.SubItemID = item.SubItemID;
                        new_INV_InvoiceItem.Qunatity = item.Qunatity;
                        new_INV_InvoiceItem.Created = DateTime.Now;
                        new_INV_InvoiceItem.Modified = DateTime.Now;

                        INV_ItemConfiguration dbINV_ItemConfiguration = db.INV_ItemConfiguration.Where(i => i.MainItemID == item.MainItemID & i.SubItemID == item.SubItemID).FirstOrDefault();
                        if (dbINV_ItemConfiguration == null)
                        {
                            newINV_ItemConfiguration.Add(new_INV_InvoiceItem);
                        }
                    }
                    if (Err != "")
                    {
                        var items =
                      db.INV_Item
                        .Where(i => i.IsLock == true && i.IsConfigurable == true)
                        .Select(s => new
                        {
                            ItemID = s.ItemID,
                            ItemName = s.ItemName + " - " + s.ItemCode
                        })
                        .ToList();

                        ViewBag.MainItemID = new SelectList(items, "ItemID", "ItemName");
                        ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
                        ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
                        TempData["errorConfig"] = Err;
                        return View(iNV_ItemConfiguration);
                    }
                    db.INV_ItemConfiguration.AddRange(newINV_ItemConfiguration);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MainItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true && i.IsConfigurable == true), "ItemID", "ItemName");
            ViewBag.SubItemID = new SelectList(db.INV_Item.Where(i => i.IsLock == true), "ItemID", "ItemName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfigurationViewModal.UserID);
            return RedirectToAction("Index");
        }

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
            try
            {
                var LisIiNV_ItemConfiguration = db.INV_ItemConfiguration.Where(ic => ic.MainItemID == iNV_ItemConfiguration.MainItemID).ToList();
                db.INV_ItemConfiguration.RemoveRange(LisIiNV_ItemConfiguration);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You can not Delete this Employee.");
                return View(iNV_ItemConfiguration);
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

        public JsonResult RetrieveItemList()
        {
            try
            {
                var itemList = db.INV_Item.Where(w => w.IsLock == true).Select(e => new
                {
                    ItemID = e.ItemID,
                    ItemName = e.ItemName + " - " + e.ItemCode
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

        public JsonResult RetrieveItemSubList(int MainItemID)
        {

            try
            {
                var itemList = db.INV_Item.Where(w => w.IsLock == true & w.ItemID != MainItemID).Select(e => new
                {
                    ItemID = e.ItemID,
                    ItemName = e.ItemName + " - " + e.ItemCode
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
