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
    public class INV_ItemIssueReturnController : Controller
    {
        private DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();
        // GET: INV_ItemIssueReturn
        public ActionResult Index()
        {
            var model = new INV_InvoiceViewModal();
            return View();
        }

        public ActionResult Issue()
        {
            List<INV_IssueReturn> model = new List<INV_IssueReturn>();
            model = db.INV_IssueReturn.Where(e => e.IssueReturnNo.Contains("IS")).OrderByDescending(o => o.Modified).ToList();
            return View(model);
        }

        public ActionResult AddIssueItem()
        {
            var model = new INV_IssueReturnViewModal();
            ViewBag.IssueReturnToUserID = new SelectList(db.SEC_User, "UserID", "UserName", model.IssueReturnToUserID);
            ViewData["error"] = TempData["error"];
            return View(model);
        }

        public ActionResult Return()
        {
            List<INV_IssueReturn> model = new List<INV_IssueReturn>();
            model = db.INV_IssueReturn.Where(e => e.IssueReturnNo.Contains("RN")).OrderByDescending(o => o.Modified).ToList();
            return View(model);
        }

        public ActionResult AddReturnItem()
        {
            var model = new INV_InvoiceViewModal();

            return View();
        }

        public JsonResult RetrieveUserList()
        {
            try
            {
                var userList = db.SEC_User.Select(e => new
                {
                    UserID = e.UserID,
                    UserName = e.UserName
                }).ToList();

                if (userList.Count > 0)
                {
                    return Json(userList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        public JsonResult RetrieveItemListForAssemble()
        {
            try
            {
                var itemList = db.INV_Item.Where(w => w.IsLock == true && w.IsConfigurable == true).Select(e => new
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

        [HttpPost]
        public JsonResult CheckIssueNumber(string issuenumber)
        {
            try
            {
                bool existOrNot = db.INV_StockHistory.Where(e => e.IssueNumber == issuenumber).Any();

                return Json(existOrNot, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Retrieve_INV_ItemConfiguration_List(INV_ItemConfiguration iNV_ItemConfiguration)
        {
            try
            {
                if (iNV_ItemConfiguration == null)
                {
                    //erroor
                }
                if (iNV_ItemConfiguration != null)
                {
                    if (iNV_ItemConfiguration.RetrieveType == true)
                    {
                        var itemConfigurationList = db.INV_ItemConfiguration.Where(e => e.MainItemID == iNV_ItemConfiguration.MainItemID).OrderByDescending(e => e.ItemConfigurationID).Select(e => new
                        {
                            SubItemID = e.SubItemID,
                            ItemName = db.INV_Item.Where(i => i.ItemID == e.MainItemID).FirstOrDefault().ItemName,
                            Qunatity = e.Qunatity
                        }).FirstOrDefault();


                        return Json(itemConfigurationList, JsonRequestBehavior.AllowGet);
                    }
                    else if (iNV_ItemConfiguration.RetrieveType == false)
                    {
                        var itemConfigurationList = db.INV_ItemConfiguration.Where(e => e.MainItemID == iNV_ItemConfiguration.MainItemID).OrderByDescending(e => e.ItemConfigurationID).Select(e => new
                        {
                            SubItemID = e.SubItemID,
                            ItemName = db.INV_Item.Where(i => i.ItemID == e.SubItemID).FirstOrDefault().ItemName,
                            Qunatity = e.Qunatity,
                            MainItemID = e.MainItemID
                        }).ToList();


                        return Json(itemConfigurationList, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveForUse(INV_IssueReturnViewModal iNV_IssueReturnViewModal)
        {
            try
            {
                if (iNV_IssueReturnViewModal.IssueReturnItems.Count <= 0)
                {
                    TempData["error"] = "Select Item.";
                    return View(iNV_IssueReturnViewModal);
                }

                #region Generate IssueReturnNo
                String _NewIssueReturnNo = CommonConfig.GetNextNumber("Issue");
                #endregion Generate IssueReturnNo

                INV_IssueReturn iNV_IssueReturn = new INV_IssueReturn();
                iNV_IssueReturn.CompanyID = CommonConfig.GetCompanyID();
                iNV_IssueReturn.IssueReturnToUserID = iNV_IssueReturnViewModal.IssueReturnToUserID;
                iNV_IssueReturn.Created = DateTime.Now;
                iNV_IssueReturn.Modified = DateTime.Now;
                iNV_IssueReturn.Remarks = "Issue";
                iNV_IssueReturn.IssueReturnDate = DateTime.Now;
                iNV_IssueReturn.IssueReturnNo = _NewIssueReturnNo;
                iNV_IssueReturn.FinYearID = CommonConfig.GetFinYearID();

                db.INV_IssueReturn.Add(iNV_IssueReturn);
                db.SaveChanges();

                string Err = "";
                if (iNV_IssueReturnViewModal.IssueReturnItems != null)
                {
                    foreach (var item in iNV_IssueReturnViewModal.IssueReturnItems)
                    {
                        INV_Item inv_Item = new INV_Item();
                        inv_Item = db.INV_Item.Where(i => i.ItemID == item.ItemID).FirstOrDefault();
                        if (inv_Item.Quantity - item.Quantity < 0)
                        {
                            if (Err == "")
                                Err = Err + "Check Stock for " + inv_Item.ItemName;
                            else
                                Err = Err + ", Check Stock for " + inv_Item.ItemName;
                        }

                        if (Session["UserID"] != null)
                        {
                            item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 8;
                        new_INV_StockHistory.ReferenceID = _NewIssueReturnNo;
                        new_INV_StockHistory.Quantity = item.Quantity;
                        new_INV_StockHistory.UserID = item.UserID;
                        new_INV_StockHistory.Created = DateTime.Now;
                        new_INV_StockHistory.Modified = DateTime.Now;
                        new_INV_StockHistory.Remarks = "Issue";
                        new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                        new_INV_StockHistory.IssueNumber = _NewIssueReturnNo;
                        db.INV_StockHistory.Add(new_INV_StockHistory);

                        inv_Item.Quantity = inv_Item.Quantity - item.Quantity;
                    }
                    if (Err != "")
                    {
                        TempData["error"] = Err;
                        return View(iNV_IssueReturnViewModal);
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveForAssmeble(INV_IssueReturnViewModal iNV_IssueReturnViewModal)
        {
            try
            {

                if (iNV_IssueReturnViewModal.IssueReturnItems.Count <= 0)
                {
                    TempData["error"] = "Select Item.";
                    return View(iNV_IssueReturnViewModal);
                }

                #region Generate IssueReturnNo
                String _NewIssueReturnNo = CommonConfig.GetNextNumber("Issue");
                #endregion Generate IssueReturnNo

                INV_IssueReturn iNV_IssueReturn = new INV_IssueReturn();
                iNV_IssueReturn.CompanyID = CommonConfig.GetCompanyID();
                iNV_IssueReturn.IssueReturnToUserID = iNV_IssueReturnViewModal.IssueReturnToUserID;
                iNV_IssueReturn.Created = DateTime.Now;
                iNV_IssueReturn.Modified = DateTime.Now;
                iNV_IssueReturn.Remarks = "Issue for Assemble for Use";
                iNV_IssueReturn.IssueReturnDate = DateTime.Now;
                iNV_IssueReturn.IssueReturnNo = _NewIssueReturnNo;
                iNV_IssueReturn.FinYearID = CommonConfig.GetFinYearID();

                db.INV_IssueReturn.Add(iNV_IssueReturn);
                db.SaveChanges();

                string Err = "";
                if (iNV_IssueReturnViewModal.IssueReturnItems != null)
                {
                    foreach (var item in iNV_IssueReturnViewModal.IssueReturnItems)
                    {
                        INV_Item inv_Item = new INV_Item();
                        inv_Item = db.INV_Item.Where(i => i.ItemID == item.ItemID).FirstOrDefault();
                        if (inv_Item.Quantity - item.Quantity < 0)
                        {
                            if (Err == "")
                                Err = Err + "Check Stock for " + inv_Item.ItemName;
                            else
                                Err = Err + ", Check Stock for " + inv_Item.ItemName;
                        }

                        if (Session["UserID"] != null)
                        {
                            item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 8;
                        new_INV_StockHistory.ReferenceID = _NewIssueReturnNo;
                        new_INV_StockHistory.Quantity = item.Quantity;
                        new_INV_StockHistory.UserID = item.UserID;
                        new_INV_StockHistory.Created = DateTime.Now;
                        new_INV_StockHistory.Modified = DateTime.Now;
                        new_INV_StockHistory.Remarks = "Issue";
                        new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                        new_INV_StockHistory.IssueNumber = _NewIssueReturnNo;
                        db.INV_StockHistory.Add(new_INV_StockHistory);

                        inv_Item.Quantity = inv_Item.Quantity - item.Quantity;
                    }
                    if (Err != "")
                    {
                        TempData["error"] = Err;
                        return View(iNV_IssueReturnViewModal);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveMainItem(INV_ItemConfiguration iNV_ItemConfiguration)
        {
            try
            {
                if (iNV_ItemConfiguration != null)
                {
                    //erroor handle
                }
                if (iNV_ItemConfiguration != null)
                {
                    if (Session["UserID"] != null)
                    {
                        iNV_ItemConfiguration.UserID = Convert.ToInt16(Session["UserID"].ToString());
                    }
                    var inv_Item = db.INV_Item.Where(e => e.ItemID == iNV_ItemConfiguration.MainItemID).FirstOrDefault();

                    string getIssueLastNumber;

                    if (inv_Item != null)
                    {
                        var getIssueLast = db.INV_StockHistory.Where(e => e.Remarks == "Return").OrderByDescending(e => e.StockHistoryID).FirstOrDefault();
                        if (getIssueLast == null)
                        {
                            getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                            #region Check No
                            INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                            if (_INV_StockHistoryNo != null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                            }
                            #endregion Check No
                        }
                        else
                        {
                            var a = getIssueLast.IssueNumber;
                            if (a == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                #region Check No
                                INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                if (_INV_StockHistoryNo != null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                                }
                                #endregion Check No
                            }
                            else
                            {
                                //a = "278201801";
                                a = a.ToString();
                                string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                //bool aaa = a.Contains(delimiters);
                                string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                #region Check No
                                INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                if (_INV_StockHistoryNo != null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 2);
                                }
                                #endregion Check No
                            }
                        }
                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = inv_Item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 7;
                        new_INV_StockHistory.ReferenceID = getIssueLastNumber;
                        new_INV_StockHistory.Quantity = inv_Item.Quantity;
                        new_INV_StockHistory.UserID = iNV_ItemConfiguration.UserID;
                        new_INV_StockHistory.Created = DateTime.Now;
                        new_INV_StockHistory.Modified = DateTime.Now;
                        new_INV_StockHistory.Remarks = "Return";
                        new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                        new_INV_StockHistory.ReturnNumber = getIssueLastNumber;
                        db.INV_StockHistory.Add(new_INV_StockHistory);
                        db.SaveChanges();

                        inv_Item.Quantity = inv_Item.Quantity + iNV_ItemConfiguration.Qunatity;
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSubItem(List<INV_ItemConfiguration> iNV_ItemConfiguration_List)
        {
            try
            {
                if (iNV_ItemConfiguration_List != null)
                {
                    //erroor handle
                }
                if (iNV_ItemConfiguration_List != null)
                {
                    foreach (var item in iNV_ItemConfiguration_List)
                    {
                        if (Session["UserID"] != null)
                        {
                            item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        var inv_Item = db.INV_Item.Where(e => e.ItemID == item.SubItemID).FirstOrDefault();

                        string getIssueLastNumber;

                        if (inv_Item != null)
                        {
                            var getIssueLast = db.INV_StockHistory.Where(e => e.Remarks == "Return").OrderByDescending(e => e.StockHistoryID).FirstOrDefault();
                            if (getIssueLast == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                #region Check No
                                INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                if (_INV_StockHistoryNo != null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                                }
                                #endregion Check No
                            }
                            else
                            {
                                var a = getIssueLast.IssueNumber;
                                if (a == null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                    #region Check No
                                    INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                    if (_INV_StockHistoryNo != null)
                                    {
                                        getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                                    }
                                    #endregion Check No
                                }
                                else
                                {
                                    //a = "278201801";
                                    a = a.ToString();
                                    string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                    //bool aaa = a.Contains(delimiters);
                                    string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                    #region Check No
                                    INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                    if (_INV_StockHistoryNo != null)
                                    {
                                        getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 2);
                                    }
                                    #endregion Check No
                                }
                            }
                            INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                            new_INV_StockHistory.ItemID = inv_Item.ItemID;
                            new_INV_StockHistory.OperationTypeID = 7;
                            new_INV_StockHistory.ReferenceID = getIssueLastNumber;
                            new_INV_StockHistory.Quantity = inv_Item.Quantity;
                            new_INV_StockHistory.UserID = item.UserID;
                            new_INV_StockHistory.Created = DateTime.Now;
                            new_INV_StockHistory.Modified = DateTime.Now;
                            new_INV_StockHistory.Remarks = "Return";
                            new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                            new_INV_StockHistory.ReturnNumber = getIssueLastNumber;
                            db.INV_StockHistory.Add(new_INV_StockHistory);
                            db.SaveChanges();

                            inv_Item.Quantity = inv_Item.Quantity + item.Qunatity;
                            db.SaveChanges();
                        }
                    }

                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult RertunNewItem(List<INV_StockHistory> iNV_StockHistory_List)
        {
            try
            {
                if (iNV_StockHistory_List == null)
                {
                    //erroor handle
                }
                if (iNV_StockHistory_List != null)
                {
                    foreach (var item in iNV_StockHistory_List)
                    {
                        if (Session["UserID"] != null)
                        {
                            item.UserID = Convert.ToInt16(Session["UserID"].ToString());
                        }

                        var inv_Item = db.INV_Item.Where(e => e.ItemID == item.ItemID).FirstOrDefault();

                        string getIssueLastNumber;

                        if (inv_Item != null)
                        {
                            var getIssueLast = db.INV_StockHistory.Where(e => e.OperationTypeID == 8).OrderByDescending(e => e.StockHistoryID).FirstOrDefault();
                            if (getIssueLast == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                #region Check No
                                INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                if (_INV_StockHistoryNo != null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                                }
                                #endregion Check No
                            }
                            else
                            {
                                var a = getIssueLast.ReturnNumber;
                                if (a == null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                    #region Check No
                                    INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                    if (_INV_StockHistoryNo != null)
                                    {
                                        getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "02";
                                    }
                                    #endregion Check No
                                }
                                else
                                {
                                    //a = "278201801";
                                    a = a.ToString();
                                    string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                    //bool aaa = a.Contains(delimiters);
                                    string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                    #region Check No
                                    INV_StockHistory _INV_StockHistoryNo = db.INV_StockHistory.Where(w => w.IssueNumber == getIssueLastNumber).FirstOrDefault();
                                    if (_INV_StockHistoryNo != null)
                                    {
                                        getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 2);
                                    }
                                    #endregion Check No
                                }
                            }
                            INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                            new_INV_StockHistory.ItemID = inv_Item.ItemID;
                            new_INV_StockHistory.OperationTypeID = 9;
                            new_INV_StockHistory.ReferenceID = getIssueLastNumber;
                            new_INV_StockHistory.Quantity = inv_Item.Quantity;
                            new_INV_StockHistory.UserID = item.UserID;
                            new_INV_StockHistory.Created = DateTime.Now;
                            new_INV_StockHistory.Modified = DateTime.Now;
                            new_INV_StockHistory.Remarks = item.Remarks;
                            new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                            new_INV_StockHistory.ReturnNumber = getIssueLastNumber;
                            db.INV_StockHistory.Add(new_INV_StockHistory);
                            db.SaveChanges();

                            inv_Item.Quantity = inv_Item.Quantity + item.Quantity;
                            db.SaveChanges();
                        }
                    }

                }

            }
            catch (Exception exception)
            {
                //exception handiling
            }
            return Json("failure", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RetrieveItemFromIssueNumber(string issueNumer)
        {
            try
            {
                var itemList = db.INV_StockHistory.Where(e => e.IssueNumber == issueNumer).Select(e => new
                {
                    ItemID = e.ItemID,
                    ItemName = e.INV_Item.ItemName,
                    Quantity = e.Quantity,
                    Remarks = e.Remarks
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