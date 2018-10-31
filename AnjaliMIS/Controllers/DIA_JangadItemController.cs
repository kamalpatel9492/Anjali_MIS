using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
    public class DIA_JangadItemController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: DIA_JangadItem
        public ActionResult Index()
        {
            //var _DIA_Jangad = new SelectList(db.DIA_Jangad.ToList(), "JangadID", "JangadNo");
            var _DIA_Jangad = db.DIA_Jangad.OrderByDescending(j=>j.Created).ToList();
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            //var dIA_JangadItem = db.DIA_JangadItem.Include(d => d.DIA_Cassett).Include(d => d.DIA_Jangad).Include(d => d.SYS_PolishingStage).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            //return View(dIA_JangadItem.Where(w => w.PolishingStageID == 1).ToList());
            return View(_DIA_Jangad);
        }

        // GET: DIA_JangadItem/Details/5
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

        // GET: DIA_JangadItem/Create
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

        public ActionResult CreateNew()
        {
            //var _DIA_Jangad = db.DIA_Jangad.ToList();
            DIA_Jangad modal = new DIA_Jangad();
            ViewBag.PartyID = new SelectList(db.MST_Party, "PartyID", "PartyName", modal.PartyID);
            ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", modal.StatusID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", modal.UserID);
            ViewBag.CGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", modal.CGST);
            ViewBag.SGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", modal.SGST);
            ViewBag.IGST = new SelectList(db.ACC_Tax, "TaxID", "Tax", modal.IGST);
            //ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;
            modal.IsLocal = true;
            return View(modal);
        }

        [HttpPost]
        public JsonResult AddJangad(DIA_JangadViewModal dIA_JangadViewModal)
        {
            try
            {
                if (dIA_JangadViewModal == null)
                {
                    //error meesage or expception handle
                }
                else if (dIA_JangadViewModal.DIA_JangadItems == null)
                {
                    //error meesage or expception handle
                }
                else
                {
                    if (Session["UserID"] != null)
                    {
                        dIA_JangadViewModal.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    //dIA_JangadViewModal;
                    if (dIA_JangadViewModal.JangadID > 0)
                    {
                        //edit time logic
                    }
                    else
                    {
                        DIA_Jangad new_DIA_Jangad = new DIA_Jangad();
                        new_DIA_Jangad.CompanyID = CommonConfig.GetCompanyID();
                        new_DIA_Jangad.PartyID = dIA_JangadViewModal.PartyID;
                        new_DIA_Jangad.Weight = dIA_JangadViewModal.Weight;
                        new_DIA_Jangad.StatusID = 1;//ask to kamal
                        new_DIA_Jangad.UserID = dIA_JangadViewModal.UserID;
                        new_DIA_Jangad.Amount = 0;//dIA_JangadViewModal.Amount;
                        new_DIA_Jangad.RecivedAmount = 0;//dIA_JangadViewModal.RecivedAmount;

                        new_DIA_Jangad.JangadNo = dIA_JangadViewModal.JangadNo; ;//ask to kamal
                        new_DIA_Jangad.Created = DateTime.Now;
                        new_DIA_Jangad.Modified = DateTime.Now;
                        new_DIA_Jangad.Remarks = dIA_JangadViewModal.Remarks;

                        new_DIA_Jangad.PricePerCarat = dIA_JangadViewModal.PricePerCarat;
                        new_DIA_Jangad.FinYearID = CommonConfig.GetFinYearID();
                        new_DIA_Jangad.CGSTAmount = dIA_JangadViewModal.CGSTAmount;
                        new_DIA_Jangad.SGSTAmount = dIA_JangadViewModal.SGSTAmount;
                        new_DIA_Jangad.IGSTAmount = dIA_JangadViewModal.IGSTAmount;
                        new_DIA_Jangad.TDSAmount = dIA_JangadViewModal.TDSAmount;
                        new_DIA_Jangad.IsActive = true;

                        if (dIA_JangadViewModal.CGST > 0)
                            new_DIA_Jangad.CGST = dIA_JangadViewModal.CGST;
                        else
                            new_DIA_Jangad.CGST = null;

                        if (dIA_JangadViewModal.SGST > 0)
                            new_DIA_Jangad.SGST = dIA_JangadViewModal.SGST;
                        else
                            new_DIA_Jangad.SGST = null;

                        if (dIA_JangadViewModal.IGST > 0)
                            new_DIA_Jangad.IGST = dIA_JangadViewModal.IGST;
                        else
                            new_DIA_Jangad.IGST = null;

                        if (dIA_JangadViewModal.TDS > 0)
                            new_DIA_Jangad.TDS = dIA_JangadViewModal.TDS;
                        else
                            new_DIA_Jangad.TDS = null;

                        new_DIA_Jangad.IsLocal = dIA_JangadViewModal.IsLocal;
                        new_DIA_Jangad.Casar = 0;//ask to kamal
                        //start default value set
                        new_DIA_Jangad.Quantity = dIA_JangadViewModal.Quantity;
                        new_DIA_Jangad.PricePerCarat = 0;
                        new_DIA_Jangad.QTYByThan = 0;
                        new_DIA_Jangad.Rate = 0;
                        //end
                        if (dIA_JangadViewModal.IsRatePerCarat == true)
                        {
                            new_DIA_Jangad.IsRatePerCarat = dIA_JangadViewModal.IsRatePerCarat;
                            new_DIA_Jangad.Quantity = dIA_JangadViewModal.Quantity;//ask to kamal
                            new_DIA_Jangad.PricePerCarat = dIA_JangadViewModal.PricePerCarat;//ask to kamal
                            new_DIA_Jangad.TotalAmount = dIA_JangadViewModal.TotalAmount;
                            new_DIA_Jangad.PendingAmount = Convert.ToInt32(dIA_JangadViewModal.TotalAmount);
                        }
                        if (dIA_JangadViewModal.IsRatePerThan == true)
                        {
                            new_DIA_Jangad.IsRatePerThan = dIA_JangadViewModal.IsRatePerThan;
                            new_DIA_Jangad.QTYByThan = dIA_JangadViewModal.QTYByThan;//ask to kamal
                            new_DIA_Jangad.Rate = dIA_JangadViewModal.Rate;
                            new_DIA_Jangad.TotalAmount = dIA_JangadViewModal.TotalAmount;
                            new_DIA_Jangad.PendingAmount = Convert.ToInt32(dIA_JangadViewModal.TotalAmount);

                        }
                        new_DIA_Jangad.JangadCode = dIA_JangadViewModal.JangadCode;//ask to kamal//common ma funcation ready te use karvanu

                        db.DIA_Jangad.Add(new_DIA_Jangad);
                        db.SaveChanges();

                        if (dIA_JangadViewModal.DIA_JangadItems.Count() > 0)
                        {
                            List<DIA_JangadItem> newList_DIA_JangadItems = new List<DIA_JangadItem>();
                            DIA_JangadItem new_DIA_JangadItem;
                            foreach (var item in dIA_JangadViewModal.DIA_JangadItems)
                            {
                                new_DIA_JangadItem = new DIA_JangadItem();
                                new_DIA_JangadItem.JangadID = new_DIA_Jangad.JangadID;
                                new_DIA_JangadItem.Weight = item.Weight;
                                new_DIA_JangadItem.Dia = item.Dia;
                                new_DIA_JangadItem.PavalionAngle = item.PavalionAngle;
                                new_DIA_JangadItem.CrownAngle = item.CrownAngle;
                                new_DIA_JangadItem.CrownHeight = item.CrownHeight;
                                new_DIA_JangadItem.Girdle = item.Girdle;
                                new_DIA_JangadItem.Para1 = item.Para1;
                                new_DIA_JangadItem.Para2 = item.Para2;
                                new_DIA_JangadItem.Para3 = item.Para3;
                                new_DIA_JangadItem.PavalionORCrown = "1";//ask to kamal
                                new_DIA_JangadItem.UserID = dIA_JangadViewModal.UserID;
                                new_DIA_JangadItem.StatusID = CommonConfig.GetStatusPending();
                                new_DIA_JangadItem.Created = DateTime.Now;
                                new_DIA_JangadItem.Remarks = item.Remarks;
                                new_DIA_JangadItem.RWeight = item.RWeight;
                                new_DIA_JangadItem.PWeight = item.PWeight;
                                new_DIA_JangadItem.Culet = item.Culet;//ask to kamal
                                new_DIA_JangadItem.ji_table = item.ji_table;//ask to kamal
                                new_DIA_JangadItem.JangadItemCode = item.JangadItemCode;//ask to kamal

                                new_DIA_JangadItem.PolishingStageID = 1;//Jangad Fresh
                                newList_DIA_JangadItems.Add(new_DIA_JangadItem);
                            }
                            db.DIA_JangadItem.AddRange(newList_DIA_JangadItems);
                            db.SaveChanges();
                        }
                    }
                }
                return Json("Sucess", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        // POST: DIA_JangadItem/Create
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
                dIA_JangadItem.PolishingStageID = 1;
                dIA_JangadItem.StatusID = 1;

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

        // GET: DIA_JangadItem/Edit/5
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

        // POST: DIA_JangadItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JangadItemID,JangadID,Weight,Size,PavalionAngle,CrownAngle,CrownWeight,Girdle,Para1,Para2,Para3,CassettsID,PolishingStageID,PavalionORCrown,UserID,StatusID,QCRemarks,Created,Completed,Remarks,PhysicalReceived,PhysicalReceivedDateTime,PhysicalSend,PhysicalSendDateTime,Delivered,DeliveredDateTime")] DIA_JangadItem dIA_JangadItem)
        {
            if (dIA_JangadItem.JangadItemID > 0)
            {
                if (dIA_JangadItem.Remarks == null || dIA_JangadItem.Remarks == "")
                {
                    ViewBag.CassettsID = new SelectList(db.DIA_Cassett, "CassettsID", "Remarks", dIA_JangadItem.CassettsID);
                    ViewBag.JangadID = new SelectList(db.DIA_Jangad, "JangadID", "Remarks", dIA_JangadItem.JangadID);
                    ViewBag.PolishingStageID = new SelectList(db.SYS_PolishingStage, "PolishingStageID", "SatgeName", dIA_JangadItem.PolishingStageID);
                    ViewBag.StatusID = new SelectList(db.SYS_Status, "StatusID", "StatusName", dIA_JangadItem.StatusID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", dIA_JangadItem.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(dIA_JangadItem);
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(dIA_JangadItem).State = EntityState.Modified;
                if (Session["UserID"] != null)
                {
                    dIA_JangadItem.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                dIA_JangadItem.PolishingStageID = 1;
                dIA_JangadItem.StatusID = 1;
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

        // GET: DIA_JangadItem/Delete/5
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

        // POST: DIA_JangadItem/Delete/5
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

        // GET: DIA_JangadItem
        public ActionResult IndexPolishingFresh()
        {
            var _DIA_Jangad = new SelectList(db.DIA_Jangad.ToList(), "JangadID", "JangadNo");
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            var dIA_JangadItem = db.DIA_JangadItem.Include(d => d.DIA_Cassett).Include(d => d.DIA_Jangad).Include(d => d.SYS_PolishingStage).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_JangadItem.ToList());
        }

        // GET: DIA_JangadItem/Forward/5
        public ActionResult Forward(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Where(e => e.JangadID == id).FirstOrDefault();
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

        public JsonResult RetrievePolishingStageList()
        {
            try
            {
                var polishingStageList = db.SYS_PolishingStage.Select(e => new
                {
                    PolishingStageID = e.PolishingStageID,
                    SatgeName = e.SatgeName
                }).ToList();

                if (polishingStageList.Count > 0)
                {
                    return Json(polishingStageList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PhysicalSend(int jangadID, int jangadItemID)
        {
            try
            {
                if (jangadItemID == 0)
                {
                    var getJangadItem = db.DIA_JangadItem.Where(e => e.JangadID == jangadID).ToList();
                    if (getJangadItem.Count > 0)
                    {
                        foreach (var item in getJangadItem)
                        {
                            item.PhysicalSend = true;
                            item.PhysicalSendDateTime = DateTime.Now;
                        }
                        db.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("failure", JsonRequestBehavior.AllowGet);
                    }
                }
                else 
                {
                    var getJangadItem = db.DIA_JangadItem.Where(e => e.JangadID == jangadID && e.JangadItemID== jangadItemID).FirstOrDefault();
                    if (getJangadItem != null)
                    {
                        getJangadItem.PhysicalSend = true;
                        getJangadItem.PhysicalSendDateTime = DateTime.Now;

                        db.SaveChanges();
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("failure", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw;
            }           
        }

        [HttpPost]
        public JsonResult SaveJangadForward(int? jangadID, int? polishingStageID, string remarks, List<DIA_JangadItem> jangadItemList, string jangadForwordTypeCompleteOrPartial)
        {
            try
            {
                List<DIA_JangadItem> _OldDIA_JangadItem = new List<DIA_JangadItem>();
                _OldDIA_JangadItem = db.DIA_JangadItem.Where(e => e.JangadID == jangadID.Value && e.PolishingStageID == 1).ToList();

                if (jangadForwordTypeCompleteOrPartial == "Complete")
                {
                    foreach (var item in _OldDIA_JangadItem)
                    {
                        item.PolishingStageID = polishingStageID.Value;
                        item.StatusID = 1;
                        item.Remarks = remarks;
                    }
                    db.SaveChanges();
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else if (jangadForwordTypeCompleteOrPartial == "Partial")
                {
                    foreach (var item in _OldDIA_JangadItem)
                    {
                        var a = jangadItemList.Where(e => e.JangadItemID == item.JangadItemID).FirstOrDefault();
                        if (a != null)
                        {
                            item.PolishingStageID = a.PolishingStageID == 0 ? item.PolishingStageID : a.PolishingStageID;
                            item.StatusID = 1;
                            item.Remarks = a.Remarks;
                        }
                    }

                    db.SaveChanges();
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }


        // POST: DIA_JangadItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forward([Bind(Include = "JangadItemID,PolishingStageID")] DIA_JangadItem dIA_JangadItem)
        {

            DIA_JangadItem _OldDIA_JangadItem = db.DIA_JangadItem.Find(dIA_JangadItem.JangadItemID);
            _OldDIA_JangadItem.PolishingStageID = dIA_JangadItem.PolishingStageID;
            if (ModelState.IsValid)
            {
                db.Entry(_OldDIA_JangadItem).State = EntityState.Modified;
                if (Session["UserID"] != null)
                {
                    _OldDIA_JangadItem.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                //dIA_JangadItem.PolishingStageID = 1;
                _OldDIA_JangadItem.StatusID = 1;
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

        [HttpPost]
        // GET: GRN
        public JsonResult RetrieveJangadItemList(int jangadID)
        {
            try
            {
                if (jangadID != 0)
                {
                    var jangadItemList = db.DIA_JangadItem.Where(e => e.JangadID == jangadID && e.PolishingStageID == 1).Select(e => new
                    {
                        JangadID = e.JangadID,
                        JangadItemID = e.JangadItemID
                    }).ToList();
                    if (jangadItemList.Count > 0)
                    {
                        return Json(jangadItemList, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        #region Planning
        // GET: DIA_JangadItem/IndexPlanning
        public ActionResult IndexPlanning()
        {
            //var _DIA_Jangad = db.DIA_Jangad.Where(r=>r.DIA_JangadItem).OrderByDescending(j => j.Created).ToList();
            List<int> jangadidList = db.DIA_JangadItem.Where(e => e.PolishingStageID == (int)CommonConfig.PolishingStage.Planning).Select(e => e.JangadID).ToList();
            var _DIA_Jangad = db.DIA_Jangad.Where(e => jangadidList.Contains(e.JangadID)).ToList();
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            return View(_DIA_Jangad);
        }

        public ActionResult ForwardPlanning(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Find(id);
            DIA_JangadItem dIA_JangadItem = db.DIA_JangadItem.Where(e => e.JangadID == id).FirstOrDefault();
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
        #endregion Planning

        #region Fixing
        // GET: DIA_JangadItem/IndexPlanning
        public ActionResult IndexFixing()
        {
            var _DIA_Jangad = new SelectList(db.DIA_Jangad.ToList(), "JangadID", "JangadNo");
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            var dIA_JangadItem = db.DIA_JangadItem.Include(d => d.DIA_Cassett).Include(d => d.DIA_Jangad).Include(d => d.SYS_PolishingStage).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_JangadItem.Where(w => w.PolishingStageID == 3).ToList());
        }
        #endregion Fixing

        #region Fixing
        // GET: DIA_JangadItem/IndexPlanning
        public ActionResult IndexSetup()
        {
            var _DIA_Jangad = new SelectList(db.DIA_Jangad.ToList(), "JangadID", "JangadNo");
            ViewData["DIA_Jangad_SelectListItem"] = _DIA_Jangad;

            var dIA_JangadItem = db.DIA_JangadItem.Include(d => d.DIA_Cassett).Include(d => d.DIA_Jangad).Include(d => d.SYS_PolishingStage).Include(d => d.SYS_Status).Include(d => d.SEC_User);
            return View(dIA_JangadItem.Where(w => w.PolishingStageID == 4).ToList());
        }
        #endregion Fixing



        // GET: DIA_JangadItem/Edit/5
        public ActionResult ABC()
        {
            DIA_JangadItem dIA_JangadItem = new DIA_JangadItem();
            return View(dIA_JangadItem);
        }
        public DIA_JangadViewModal ListDIA_JangadViewModal = new DIA_JangadViewModal();
        public JsonResult UploadCollectionFile()
        {
            var tempobj = new List<object>();
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpFileCollectionBase file = Request.Files;
                    if (file != null)
                    {
                        var filecount = file.Count;

                        for (int f = 0; f < filecount; f++)
                        {
                            if ((file != null) && (file.Count > 0))
                            {
                                byte[] fileBytes = new byte[Request.Files[f].ContentLength];
                                var fileBytess = new byte[Request.Files[f].ContentLength];
                                var data = Request.Files[f].InputStream.Read(fileBytess, 0, fileBytess.Length);

                                #region Directory 
                                string subPath = "~/Upload/Jangad"; // your code goes here
                                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                                if (!exists)
                                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                                string pathtosave = Server.MapPath(subPath) + "\\" + file[f].FileName;
                                file[f].SaveAs(pathtosave);
                                #endregion Directory

                                string text1 = System.IO.File.ReadAllText(pathtosave);
                                text1.Replace(System.Environment.NewLine, string.Empty);
                                string[] split = text1.Split('\t');
                                DIA_JangadItem newDIA_JangadItem = new DIA_JangadItem();
                                newDIA_JangadItem.Dia = Convert.ToDecimal(split[5]);
                                newDIA_JangadItem.RWeight = Convert.ToDecimal(split[3]);
                                newDIA_JangadItem.PWeight = Convert.ToDecimal(split[4]);
                                newDIA_JangadItem.PavalionAngle = Convert.ToDecimal(split[6]);
                                newDIA_JangadItem.CrownAngle = Convert.ToDecimal(split[7]);
                                newDIA_JangadItem.CrownHeight = Convert.ToDecimal(split[8]);
                                split[9] = split[9].Replace("M: ", "");
                                newDIA_JangadItem.Girdle = Convert.ToDecimal(split[9]);
                                newDIA_JangadItem.Culet = Convert.ToDecimal(split[10]);
                                newDIA_JangadItem.ji_table = Convert.ToDecimal(split[11]);
                                newDIA_JangadItem.JangadItemCode = Request.Files[f].FileName;
                                ListDIA_JangadViewModal.DIA_JangadItems.Add(newDIA_JangadItem);

                            }
                        }

                        ViewData["UploadData"] = ListDIA_JangadViewModal;
                        return Json(ListDIA_JangadViewModal, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

    }
}
