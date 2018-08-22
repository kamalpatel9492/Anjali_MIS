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
    public class DIA_PolishingFresh : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: DIA_PolishingFresh
        public ActionResult Index()
        {
            var _DIA_Jangad = new SelectList(db.DIA_Jangad.ToList(), "JangadID", "JangadNo");
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            var dIA_JangadItem = db.DIA_JangadItem.Include(d => d.DIA_Cassett).Include(d => d.DIA_Jangad).Include(d => d.SYS_PolishingStage).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_JangadItem.ToList());
        }

        // GET: DIA_PolishingFresh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            if (dIA_JangadItem == null)
            {
                return HttpNotFound();
            }
            return View(dIA_JangadItem);
        }

        // GET: DIA_PolishingFresh/Create
        public ActionResult Create()
        {
            ViewBag.CassettsID = new SelectList(db.DIA_Cassett, "CassettsID", "Remarks");
            ViewBag.JangadID = new SelectList(db.DIA_Jangad, "JangadID", "Remarks");
            ViewBag.PolishingStageID = new SelectList(db.SYS_PolishingStage, "PolishingStageID", "SatgeName");
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            DIA_JangadItem _dIA_JangadItem = new DIA_JangadItem();
            return View("Edit", _dIA_JangadItem);
        }

        // POST: DIA_PolishingFresh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JangadItemID,JangadID,Weight,Size,PavalionAngle,CrownAngle,CrownWeight,Girdle,Para1,Para2,Para3,CassettsID,PolishingStageID,PavalionORCrown,UserID,StatusID,QCRemarks,Created,Completed,Remarks,PhysicalReceived,PhysicalReceivedDateTime,PhysicalSend,PhysicalSendDateTime,Delivered,DeliveredDateTime")] DIA_JangadItem dIA_JangadItem)
        {
            if (ModelState.IsValid)
            {
                dIA_JangadItem.Created = DateTime.Now;
                if (Session["UserID"] != null)
                {
                    dIA_JangadItem.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.DIA_JangadItem.Add(dIA_JangadItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CassettsID = new SelectList(db.DIA_Cassett, "CassettsID", "Remarks", dIA_JangadItem.CassettsID);
            ViewBag.JangadID = new SelectList(db.DIA_Jangad, "JangadID", "Remarks", dIA_JangadItem.JangadID);
            ViewBag.PolishingStageID = new SelectList(db.SYS_PolishingStage, "PolishingStageID", "SatgeName", dIA_JangadItem.PolishingStageID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_JangadItem.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_JangadItem.UserID);
            return View(dIA_JangadItem);
        }

        // GET: DIA_PolishingFresh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            if (dIA_JangadItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CassettsID = new SelectList(db.DIA_Cassett, "CassettsID", "Remarks", dIA_JangadItem.CassettsID);
            ViewBag.JangadID = new SelectList(db.DIA_Jangad, "JangadID", "Remarks", dIA_JangadItem.JangadID);
            ViewBag.PolishingStageID = new SelectList(db.SYS_PolishingStage, "PolishingStageID", "SatgeName", dIA_JangadItem.PolishingStageID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_JangadItem.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_JangadItem.UserID);
            return View(dIA_JangadItem);
        }

        // POST: DIA_PolishingFresh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JangadItemID,JangadID,Weight,Size,PavalionAngle,CrownAngle,CrownWeight,Girdle,Para1,Para2,Para3,CassettsID,PolishingStageID,PavalionORCrown,UserID,StatusID,QCRemarks,Created,Completed,Remarks,PhysicalReceived,PhysicalReceivedDateTime,PhysicalSend,PhysicalSendDateTime,Delivered,DeliveredDateTime")] DIA_JangadItem dIA_JangadItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIA_JangadItem).State = EntityState.Modified;
                if (Session["UserID"] != null)
                {
                    dIA_JangadItem.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CassettsID = new SelectList(db.DIA_Cassett, "CassettsID", "Remarks", dIA_JangadItem.CassettsID);
            ViewBag.JangadID = new SelectList(db.DIA_Jangad, "JangadID", "Remarks", dIA_JangadItem.JangadID);
            ViewBag.PolishingStageID = new SelectList(db.SYS_PolishingStage, "PolishingStageID", "SatgeName", dIA_JangadItem.PolishingStageID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_JangadItem.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_JangadItem.UserID);
            return View(dIA_JangadItem);
        }

        // GET: DIA_PolishingFresh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            if (dIA_JangadItem == null)
            {
                return HttpNotFound();
            }
            return View(dIA_JangadItem);
        }

        // POST: DIA_PolishingFresh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            db.DIA_JangadItem.Remove(dIA_JangadItem);
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
