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
using AnjaliMIS.ViewModals;

namespace AnjaliMIS.Controllers
{
    [SessionTimeout]
    public class SEC_UserController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        // GET: SEC_User
        public ActionResult Index()
        {
            var sEC_User = db.SEC_User.Where(u => u.IsActive == true).OrderByDescending(o => o.Created).Include(s => s.EMP_Employee1).Include(s => s.SEC_User2);
            return View(sEC_User.ToList());
        }

        // GET: SEC_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_User sEC_User = db.SEC_User.Find(id);
            if (sEC_User == null)
            {
                return HttpNotFound();
            }
            return View(sEC_User);
        }

        // GET: SEC_User/Create
        public ActionResult Create()
        {
            SEC_UserAddEditModel model = new SEC_UserAddEditModel();
            model.sYS_ModuleList = (from t1 in db.SYS_Module
                                    select new SYS_ModuleModel()
                                    {
                                        ModuleID = t1.ModuleID,
                                        ModuleName = t1.ModuleName,
                                        IsSelected = false
                                    }).ToList();


            var Employees =
                     db.EMP_Employee
                       .Where(i => i.IsActive == true)
                       .Select(s => new
                       {
                           EmployeeID = s.EmployeeID,
                           EmployeeName = s.EMP_Department.DepartmentName + " - " + s.EMP_Designation.Designation + " - " + s.EmployeeName
                       })
                       .ToList();

            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "EmployeeName");
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName");
            //ViewBag.SYS_Module_List = new SelectList(db.SYS_Module, "ModuleID", "ModuleName");

            return View("Edit", model);
        }

        // POST: SEC_User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SEC_UserAddEditModel sEC_UserAddEditModel)
        {

            if (sEC_UserAddEditModel == null)
            {
                return HttpNotFound();
            }
            //if (ModelState.IsValid)
            if (sEC_UserAddEditModel != null)
            {
                if (sEC_UserAddEditModel.sEC_User != null)
                {
                    sEC_UserAddEditModel.sEC_User.Created = DateTime.Now;
                    sEC_UserAddEditModel.sEC_User.Modified = DateTime.Now;
                    sEC_UserAddEditModel.sEC_User.EmployeeID = sEC_UserAddEditModel.sEC_User.EmployeeID;
                    if (Session["UserID"] != null)
                    {
                        sEC_UserAddEditModel.sEC_User.CreatedByUserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    db.SEC_User.Add(sEC_UserAddEditModel.sEC_User);
                    db.SaveChanges();

                    List<SEC_UserPrivileges> newList_SEC_UserPrivileges = new List<SEC_UserPrivileges>();

                    foreach (var item in sEC_UserAddEditModel.sYS_ModuleList)
                    {
                        SEC_UserPrivileges new_SEC_UserPrivileges = new SEC_UserPrivileges();
                        if (item.IsSelected == true)
                        {
                            new_SEC_UserPrivileges.ModuleID = item.ModuleID;
                            new_SEC_UserPrivileges.UserID = sEC_UserAddEditModel.sEC_User.UserID;
                            new_SEC_UserPrivileges.CreatedByUserID = Convert.ToInt16(Session["UserID"].ToString());
                            new_SEC_UserPrivileges.Created = DateTime.Now;
                            new_SEC_UserPrivileges.Modified = DateTime.Now;
                            newList_SEC_UserPrivileges.Add(new_SEC_UserPrivileges);
                        }

                    }
                    if (newList_SEC_UserPrivileges.Count > 0)
                    {
                        db.SEC_UserPrivileges.AddRange(newList_SEC_UserPrivileges);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            var Employees =
                    db.EMP_Employee
                      .Where(i => i.IsActive == true)
                      .Select(s => new
                      {
                          EmployeeID = s.EmployeeID,
                          EmployeeName = s.EMP_Department.DepartmentName + " - " + s.EMP_Designation.Designation + " - " + s.EmployeeName
                      })
                      .ToList();

            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "EmployeeName");

            //ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_UserAddEditModel.sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_UserAddEditModel.sEC_User.CreatedByUserID);
            return View(sEC_UserAddEditModel);
        }

        // GET: SEC_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //SEC_User sEC_User = db.SEC_User.Find(id);
            SEC_UserAddEditModel sEC_UserAddEditModel = new SEC_UserAddEditModel();
            sEC_UserAddEditModel.sEC_User = db.SEC_User.Find(id);

            if (sEC_UserAddEditModel.sEC_User == null)
            {
                return HttpNotFound();
            }

            sEC_UserAddEditModel.sYS_ModuleList = (from t1 in db.SYS_Module
                                                   join t2 in db.SEC_UserPrivileges on
                                                   new
                                                   {
                                                       Key1 = t1.ModuleID,
                                                       Key2 = id.Value
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       Key1 = t2.ModuleID,
                                                       Key2 = t2.UserID
                                                   }
                                                   into sup
                                                   from t3 in sup.DefaultIfEmpty()
                                                   select new SYS_ModuleModel() { ModuleID = t1.ModuleID, ModuleName = t1.ModuleName, IsSelected = (t3.UserPrivilegesID > 0 ? true : false) }).ToList();
            var Employees =
                    db.EMP_Employee
                      .Where(i => i.IsActive == true)
                      .Select(s => new
                      {
                          EmployeeID = s.EmployeeID,
                          EmployeeName = s.EMP_Department.DepartmentName + " - " + s.EMP_Designation.Designation + " - " + s.EmployeeName
                      })
                      .ToList();

            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "EmployeeName", sEC_UserAddEditModel.sEC_User.EmployeeID);
            //ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_UserAddEditModel.sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_UserAddEditModel.sEC_User.CreatedByUserID);
            return View(sEC_UserAddEditModel);
        }

        // POST: SEC_User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SEC_UserAddEditModel sEC_UserAddEditModel)
        {
            if (sEC_UserAddEditModel == null)
            {
                //error handle
            }

            if (sEC_UserAddEditModel != null)
            {
                if (sEC_UserAddEditModel.sEC_User != null)
                {
                    var get_SEC_User = db.SEC_User.Where(e => e.UserID == sEC_UserAddEditModel.sEC_User.UserID).FirstOrDefault();
                    //get_SEC_User.EmployeeID = sEC_UserAddEditModel.sEC_User.EmployeeID;
                    get_SEC_User.UserName = sEC_UserAddEditModel.sEC_User.UserName;
                    get_SEC_User.Password = sEC_UserAddEditModel.sEC_User.Password;
                    get_SEC_User.IsAdmin = sEC_UserAddEditModel.sEC_User.IsAdmin;
                    get_SEC_User.IsActive = sEC_UserAddEditModel.sEC_User.IsActive;
                    get_SEC_User.Remarks = sEC_UserAddEditModel.sEC_User.Remarks;
                    get_SEC_User.Modified = DateTime.Now;
                    db.SaveChanges();

                    if (sEC_UserAddEditModel.sYS_ModuleList != null)
                    {
                        foreach (var moduleList in sEC_UserAddEditModel.sYS_ModuleList)
                        {
                            if (moduleList.IsSelected == true)
                            {
                                var checkExist = db.SEC_UserPrivileges.Any(usp => usp.UserID == get_SEC_User.UserID && usp.ModuleID == moduleList.ModuleID);
                                int userid = get_SEC_User.UserID;
                                if (checkExist == false)
                                {
                                    var newModuleAdd = new SEC_UserPrivileges()
                                    {
                                        ModuleID = moduleList.ModuleID,
                                        UserID = userid,
                                        CreatedByUserID = Convert.ToInt16(Session["UserID"].ToString()),
                                        Created = DateTime.Now,
                                        Modified = DateTime.Now
                                    };
                                    db.SEC_UserPrivileges.Add(newModuleAdd);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                    List<int> getAllSelectedModuleList = sEC_UserAddEditModel.sYS_ModuleList.Where(e => e.IsSelected == true).Select(e => e.ModuleID).ToList();
                    List<SEC_UserPrivileges> removeRange = db.SEC_UserPrivileges.Where(t => t.UserID == sEC_UserAddEditModel.sEC_User.UserID && !getAllSelectedModuleList.Contains(t.ModuleID)).Select(t => t).ToList();

                    db.SEC_UserPrivileges.RemoveRange(removeRange);
                    db.SaveChanges();
                }
            }
            var Employees =
                   db.EMP_Employee
                     .Where(i => i.IsActive == true)
                     .Select(s => new
                     {
                         EmployeeID = s.EmployeeID,
                         EmployeeName = s.EMP_Department.DepartmentName + " - " + s.EMP_Designation.Designation + " - " + s.EmployeeName
                     })
                     .ToList();

            ViewBag.EmployeeID = new SelectList(Employees, "EmployeeID", "EmployeeName", sEC_UserAddEditModel.sEC_User.EmployeeID);
            //ViewBag.EmployeeID = new SelectList(db.EMP_Employee, "EmployeeID", "EmployeeName", sEC_UserAddEditModel.sEC_User.EmployeeID);
            ViewBag.CreatedByUserID = new SelectList(db.SEC_User, "UserID", "UserName", sEC_UserAddEditModel.sEC_User.CreatedByUserID);

            return RedirectToAction("Index");
        }

        // GET: SEC_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SEC_User sEC_User = db.SEC_User.Find(id);
            if (sEC_User == null)
            {
                return HttpNotFound();
            }
            return View(sEC_User);
        }

        // POST: SEC_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SEC_User sEC_User = db.SEC_User.Find(id);
            try
            {
                List<SEC_UserPrivileges> sEC_UserPrivileges_List = db.SEC_UserPrivileges.Where(e => e.UserID == sEC_User.UserID).ToList();
                db.SEC_UserPrivileges.RemoveRange(sEC_UserPrivileges_List);
                db.SEC_User.Remove(sEC_User);
                db.Entry(sEC_User).State = EntityState.Modified;
                sEC_User.IsActive = false;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "You can not Delete this User.");
                //return RedirectToAction("Index");
                return View(sEC_User);
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
