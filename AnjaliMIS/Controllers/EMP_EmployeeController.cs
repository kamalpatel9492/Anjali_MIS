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
using static AnjaliMIS.CommonConfig;

namespace AnjaliMIS.Controllers
{
    [SessionTimeout]
    public class EMP_EmployeeController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: EMP_Employee
        public ActionResult Index()
        {
            var eMP_Employee = db.EMP_Employee.OrderByDescending(o => o.JoiningDate).Include(e => e.ACC_Bank).Include(e => e.EMP_Department).Include(e => e.EMP_Designation).Include(e => e.LOC_City).Include(e => e.SYS_Company).Include(e => e.SYS_FinYear).Include(e => e.LOC_State).Include(e => e.SEC_User);
            return View(eMP_Employee.ToList());
        }

        // GET: EMP_Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Employee eMP_Employee = db.EMP_Employee.Find(id);
            if (eMP_Employee == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Employee);
        }

        // GET: EMP_Employee/Create
        public ActionResult Create()
        {

            ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName");
            ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName");
            ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation");
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName");
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName");
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear");
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName");
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName");
            EMP_Employee _eMP_Employee = new EMP_Employee();
            _eMP_Employee.DOB = DateTime.Now;
            _eMP_Employee.JoiningDate = DateTime.Now;
            _eMP_Employee.EndingDate = DateTime.Now;
            return View("Edit", _eMP_Employee);
        }
        public enum Gender
        {
            Male,
            Female
        }
        // POST: EMP_Employee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(EMP_Employee eMP_Employee)
        {
            if (ModelState.IsValid)
            {
                eMP_Employee.Created = DateTime.Now;
                eMP_Employee.Modified = DateTime.Now;
                eMP_Employee.CompanyID = 4;
                eMP_Employee.FinYearID = CommonConfig.GetFinYearID();
                HttpPostedFileBase photoProof = Request.Files["IDProofPhotoPath"];
                if (photoProof != null && photoProof.FileName != "")
                {
                    //create path to store in database
                    eMP_Employee.IDProofPhotoPath = "~/Images/" + photoProof.FileName;

                    //store image in folder
                    photoProof.SaveAs(Server.MapPath("~/Images") + "/" + photoProof.FileName);
                }
                HttpPostedFileBase photo = Request.Files["PhotoPath"];
                if (photo != null && photo.FileName != "")
                {
                    //create path to store in database
                    eMP_Employee.PhotoPath = "~/Images/" + photo.FileName;

                    //store image in folder
                    photo.SaveAs(Server.MapPath("~/Images") + "/" + photo.FileName);
                }

                if (Session["UserID"] != null)
                {
                    eMP_Employee.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                db.EMP_Employee.Add(eMP_Employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName", eMP_Employee.BankID);
            ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName", eMP_Employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation", eMP_Employee.DesignationID);
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", eMP_Employee.CityID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", eMP_Employee.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", eMP_Employee.FinYearID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", eMP_Employee.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Employee.UserID);
            return View(eMP_Employee);
        }

        // GET: EMP_Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Employee eMP_Employee = db.EMP_Employee.Find(id);
            if (eMP_Employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName", eMP_Employee.BankID);
            ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName", eMP_Employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation", eMP_Employee.DesignationID);
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", eMP_Employee.CityID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", eMP_Employee.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", eMP_Employee.FinYearID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", eMP_Employee.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Employee.UserID);
            return View(eMP_Employee);
        }

        // POST: EMP_Employee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EMP_Employee eMP_Employee)
        {

            if (eMP_Employee.EmployeeID > 0)
            {
                if (eMP_Employee.Remarks == null || eMP_Employee.Remarks == "")
                {
                    ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName", eMP_Employee.BankID);
                    ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName", eMP_Employee.DepartmentID);
                    ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation", eMP_Employee.DesignationID);
                    ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", eMP_Employee.CityID);
                    ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", eMP_Employee.CompanyID);
                    ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", eMP_Employee.FinYearID);
                    ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", eMP_Employee.StateID);
                    ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Employee.UserID);
                    ModelState.AddModelError("", "Enter Remarks");
                    return View(eMP_Employee);
                }
            }

            if (eMP_Employee.EmployeeName != null && eMP_Employee.Address != null)
            {
                if (!string.IsNullOrEmpty(eMP_Employee.EmployeeName))
                {
                    if (db.EMP_Employee.Where(I => I.EmployeeName == eMP_Employee.EmployeeName && I.Address == eMP_Employee.Address).Count() > 0)
                    {
                        var oldemployee = db.EMP_Employee.Where(I => I.EmployeeName == eMP_Employee.EmployeeName && I.Address == eMP_Employee.Address).FirstOrDefault();

                        if (oldemployee.EmployeeID != eMP_Employee.EmployeeID)
                        {
                            ModelState.AddModelError("EmployeeNameDuplicate", eMP_Employee.EmployeeName + " Already added.");
                            ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName", eMP_Employee.BankID);
                            ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName", eMP_Employee.DepartmentID);
                            ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation", eMP_Employee.DesignationID);
                            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", eMP_Employee.CityID);
                            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", eMP_Employee.CompanyID);
                            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", eMP_Employee.FinYearID);
                            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", eMP_Employee.StateID);
                            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Employee.UserID);
                            return View(eMP_Employee);
                        }
                    }
                }
            }


            //EMP_Employee OldEMP_Employee = db.EMP_Employee.Find(eMP_Employee.EmployeeID);

            if (ModelState.IsValid)
            {
                eMP_Employee.Modified = Convert.ToDateTime(DateTime.Now);
                eMP_Employee.CompanyID = 4;
                eMP_Employee.FinYearID = CommonConfig.GetFinYearID();
                HttpPostedFileBase photoProof = Request.Files["IDProofPhotoPath"];
                if (photoProof != null && photoProof.FileName != "")
                {
                    //create path to store in database
                    eMP_Employee.IDProofPhotoPath = "~/Images/" + photoProof.FileName;

                    //store image in folder
                    photoProof.SaveAs(Server.MapPath("~/Images") + "/" + photoProof.FileName);
                }
                else
                {
                    //eMP_Employee.IDProofPhotoPath = OldEMP_Employee.IDProofPhotoPath;
                }
                HttpPostedFileBase photo = Request.Files["PhotoPath"];
                if (photo != null && photo.FileName != "")
                {
                    //create path to store in database
                    eMP_Employee.PhotoPath = "~/Images/" + photo.FileName;

                    //store image in folder
                    photo.SaveAs(Server.MapPath("~/Images") + "/" + photo.FileName);
                }
                else
                {
                    //eMP_Employee.PhotoPath = OldEMP_Employee.PhotoPath;
                }
                if (Session["UserID"] != null)
                {
                    eMP_Employee.UserID = Convert.ToInt16(Session["UserID"].ToString());
                }
                if (eMP_Employee.EmployeeID > 0)
                {
                    db.Entry(eMP_Employee).State = EntityState.Modified;
                    eMP_Employee.Modified = Convert.ToDateTime(DateTime.Now);
                    EMP_Employee _OldEMP_Employee = db.EMP_Employee.Find(eMP_Employee.EmployeeID);
                    eMP_Employee.PhotoPath = _OldEMP_Employee.PhotoPath;
                    eMP_Employee.IDProofPhotoPath = _OldEMP_Employee.IDProofPhotoPath;
                }
                else
                {
                    eMP_Employee.Created = DateTime.Now;
                    eMP_Employee.Modified = DateTime.Now;
                    db.EMP_Employee.Add(eMP_Employee);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankID = new SelectList(db.ACC_Bank, "BankID", "BankName", eMP_Employee.BankID);
            ViewBag.DepartmentID = new SelectList(db.EMP_Department, "DepartmentID", "DepartmentName", eMP_Employee.DepartmentID);
            ViewBag.DesignationID = new SelectList(db.EMP_Designation, "DesignationID", "Designation", eMP_Employee.DesignationID);
            ViewBag.CityID = new SelectList(db.LOC_City, "CityID", "CityName", eMP_Employee.CityID);
            ViewBag.CompanyID = new SelectList(db.SYS_Company, "CompanyID", "CompanyName", eMP_Employee.CompanyID);
            ViewBag.FinYearID = new SelectList(db.SYS_FinYear, "FinYearID", "FinYear", eMP_Employee.FinYearID);
            ViewBag.StateID = new SelectList(db.LOC_State, "StateID", "StateName", eMP_Employee.StateID);
            ViewBag.UserID = new SelectList(db.SEC_User, "UserID", "UserName", eMP_Employee.UserID);
            return View(eMP_Employee);
        }

        // GET: EMP_Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMP_Employee eMP_Employee = db.EMP_Employee.Find(id);
            if (eMP_Employee == null)
            {
                return HttpNotFound();
            }
            return View(eMP_Employee);
        }

        // POST: EMP_Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            EMP_Employee eMP_Employee = db.EMP_Employee.Find(id);

            try
            {
                db.EMP_Employee.Remove(eMP_Employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You can not Delete this Employee.");
                //return RedirectToAction("Index");
                return View(eMP_Employee);
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
    }
}
