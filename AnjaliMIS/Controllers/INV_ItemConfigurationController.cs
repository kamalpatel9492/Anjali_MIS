﻿using System;
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
			INV_ItemConfiguration _iNV_ItemConfiguration = new INV_ItemConfiguration();
			return View("Edit", _iNV_ItemConfiguration);
		}

        // POST: INV_ItemConfiguration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemConfigurationID,MainItemID,SubItemID,Qunatity,UserID,Created,Modified,Remarks")] INV_ItemConfiguration iNV_ItemConfiguration)
        {
            if (ModelState.IsValid)
            {
				iNV_ItemConfiguration.Created = DateTime.Now;
				iNV_ItemConfiguration.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.INV_ItemConfiguration.Add(iNV_ItemConfiguration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
            ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.SubItemID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
            return View(iNV_ItemConfiguration);
        }

        // GET: INV_ItemConfiguration/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
            ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.SubItemID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
            return View(iNV_ItemConfiguration);
        }

        // POST: INV_ItemConfiguration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemConfigurationID,MainItemID,SubItemID,Qunatity,UserID,Created,Modified,Remarks")] INV_ItemConfiguration iNV_ItemConfiguration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNV_ItemConfiguration).State = EntityState.Modified;
				iNV_ItemConfiguration.Modified = DateTime.Now;
				if (Session["UserID"] != null)
				{
					iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MainItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.MainItemID);
            ViewBag.SubItemID = new SelectList(db.INV_Item, "ItemID", "ItemName", iNV_ItemConfiguration.SubItemID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", iNV_ItemConfiguration.UserID);
            return View(iNV_ItemConfiguration);
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
    }
}