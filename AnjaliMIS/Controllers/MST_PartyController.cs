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
	public class MST_PartyController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: MST_Party
        public ActionResult Index()
        {
            var mST_Party = db.MST_Party.Include(m => m.LOC_City).Include(m => m.LOC_Country).Include(m => m.LOC_State).Include(m => m.SYS_Company).Include(m => m.SYS_PartyType).Include(m => m.SEC_User);
            return View(mST_Party.ToList());
        }

        // GET: MST_Party/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MST_Party mST_Party = db.MST_Party.Find(id);
            if (mST_Party == null)
            {
                return HttpNotFound();
            }
            return View(mST_Party);
        }

        // GET: MST_Party/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName");
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName");
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.PartyTypeID = new SelectList(db.SYS_PartyType, "PartyTypeID", "PartyType");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
			MST_Party _mST_Party = new MST_Party();
			return View("Edit", _mST_Party);
		}

        // POST: MST_Party/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartyID,PartyName,Address,CountryID,StateID,CityID,ContactPerson,Mobile,Phone,Email,PartyTypeID,CompanyID,UserID,Description,Created,Modofied,Remarks,IsActive,PanNo,TanNo,AadharNo,GSTIN")] MST_Party mST_Party)
        {
            if (ModelState.IsValid)
            {
				mST_Party.Created = DateTime.Now;
				mST_Party.Modofied = DateTime.Now;
                mST_Party.CompanyID = 4;

                if (Session["UserID"] != null)
				{
					mST_Party.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.MST_Party.Add(mST_Party);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", mST_Party.CityID);
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName", mST_Party.CountryID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", mST_Party.StateID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", mST_Party.CompanyID);
            ViewBag.PartyTypeID = new SelectList(db.SYS_PartyType, "PartyTypeID", "PartyType", mST_Party.PartyTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", mST_Party.UserID);
            return View(mST_Party);
        }

        // GET: MST_Party/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MST_Party mST_Party = db.MST_Party.Find(id);
            if (mST_Party == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", mST_Party.CityID);
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName", mST_Party.CountryID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", mST_Party.StateID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", mST_Party.CompanyID);
            ViewBag.PartyTypeID = new SelectList(db.SYS_PartyType, "PartyTypeID", "PartyType", mST_Party.PartyTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", mST_Party.UserID);
            return View(mST_Party);
        }

        // POST: MST_Party/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartyID,PartyName,Address,CountryID,StateID,CityID,ContactPerson,Mobile,Phone,Email,PartyTypeID,CompanyID,UserID,Description,Created,Modofied,Remarks,IsActive,PanNo,TanNo,AadharNo,GSTIN")] MST_Party mST_Party)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mST_Party).State = EntityState.Modified;
				mST_Party.Modofied = Convert.ToDateTime(DateTime.Now);
				if (Session["UserID"] != null)
				{
					mST_Party.UserID = Convert.ToInt16(Session["UserID"].ToString());
				}
				db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", mST_Party.CityID);
            ViewBag.CountryID = new SelectList(db.LOC_Country, "CountryID", "CountryName", mST_Party.CountryID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", mST_Party.StateID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", mST_Party.CompanyID);
            ViewBag.PartyTypeID = new SelectList(db.SYS_PartyType, "PartyTypeID", "PartyType", mST_Party.PartyTypeID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", mST_Party.UserID);
            return View(mST_Party);
        }

        // GET: MST_Party/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MST_Party mST_Party = db.MST_Party.Find(id);
            if (mST_Party == null)
            {
                return HttpNotFound();
            }
            return View(mST_Party);
        }

        // POST: MST_Party/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MST_Party mST_Party = db.MST_Party.Find(id);
            db.MST_Party.Remove(mST_Party);
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
