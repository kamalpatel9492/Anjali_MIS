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
            List<INV_StockHistory> model = new List<INV_StockHistory>();
            model = db.INV_StockHistory.Where(e => e.OperationTypeID == 8).OrderByDescending(o => o.Modified).ToList();

            return View(model);
        }

        public ActionResult AddIssueItem()
        {
            var model = new INV_InvoiceViewModal();
            return View();
        }

        public ActionResult Return()
        {
            List<INV_StockHistory> model = new List<INV_StockHistory>();
            model = db.INV_StockHistory.Where(e => e.OperationTypeID == 9).OrderByDescending(o => o.Modified).ToList();
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

        [HttpPost]
        public JsonResult CheckIssueNumber(string issuenumber)
        {
            try
            {
                bool existOrNot = db.INV_StockHistory.Where(e=>e.IssueNumber== issuenumber).Any();

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
        public JsonResult SaveForUse(INV_ItemConfiguration iNV_ItemConfiguration)
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

                        var getIssueLast = db.INV_StockHistory.Where(e => e.Remarks == "Issue").OrderByDescending(e => e.StockHistoryID).FirstOrDefault();
                        if (getIssueLast == null)
                        {
                            getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                        }
                        else
                        {
                            var a = getIssueLast.IssueNumber;
                            if (a == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                            }
                            else
                            {
                                //a = "278201801";
                                a = a.ToString();
                                string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                //bool aaa = a.Contains(delimiters);
                                string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                            }
                        }

                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = inv_Item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 8;
                        new_INV_StockHistory.ReferenceID = Convert.ToInt32(getIssueLastNumber); 
                        new_INV_StockHistory.Quantity = inv_Item.Quantity;
                        new_INV_StockHistory.UserID = iNV_ItemConfiguration.UserID;
                        new_INV_StockHistory.Created = DateTime.Now;
                        new_INV_StockHistory.Modified = DateTime.Now;
                        new_INV_StockHistory.Remarks = "Issue";
                        new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                        new_INV_StockHistory.IssueNumber = getIssueLastNumber;
                        db.INV_StockHistory.Add(new_INV_StockHistory);
                        db.SaveChanges();

                        inv_Item.Quantity = inv_Item.Quantity - iNV_ItemConfiguration.Qunatity;
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
        public JsonResult SaveForAssmeble(List<INV_ItemConfiguration> iNV_ItemConfiguration_List)
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
                            var getIssueLast = db.INV_StockHistory.Where(e => e.Remarks == "Issue").OrderByDescending(e => e.StockHistoryID).FirstOrDefault();
                            if (getIssueLast == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                            }
                            else
                            {
                                var a = getIssueLast.IssueNumber;
                                if (a == null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                }
                                else
                                {
                                    //a = "278201801";
                                    a = a.ToString();
                                    string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                    //bool aaa = a.Contains(delimiters);
                                    string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                }
                            }

                            INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                            new_INV_StockHistory.ItemID = inv_Item.ItemID;
                            new_INV_StockHistory.OperationTypeID = 8;
                            new_INV_StockHistory.ReferenceID = Convert.ToInt32(getIssueLastNumber);
                            new_INV_StockHistory.Quantity = inv_Item.Quantity;
                            new_INV_StockHistory.UserID = item.UserID;
                            new_INV_StockHistory.Created = DateTime.Now;
                            new_INV_StockHistory.Modified = DateTime.Now;
                            new_INV_StockHistory.Remarks = "Issue";
                            new_INV_StockHistory.FinYearID = CommonConfig.GetFinYearID();

                            new_INV_StockHistory.IssueNumber = getIssueLastNumber;
                            db.INV_StockHistory.Add(new_INV_StockHistory);
                            db.SaveChanges();

                            inv_Item.Quantity = inv_Item.Quantity - item.Qunatity;
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
                        }
                        else
                        {
                            var a = getIssueLast.IssueNumber;
                            if (a == null)
                            {
                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                            }
                            else
                            {
                                //a = "278201801";
                                a = a.ToString();
                                string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                //bool aaa = a.Contains(delimiters);
                                string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                            }
                        }
                        INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                        new_INV_StockHistory.ItemID = inv_Item.ItemID;
                        new_INV_StockHistory.OperationTypeID = 7;
                        new_INV_StockHistory.ReferenceID = Convert.ToInt32(getIssueLastNumber);
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
                            }
                            else
                            {
                                var a = getIssueLast.IssueNumber;
                                if (a == null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                }
                                else
                                {
                                    //a = "278201801";
                                    a = a.ToString();
                                    string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                    //bool aaa = a.Contains(delimiters);
                                    string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                }
                            }
                            INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                            new_INV_StockHistory.ItemID = inv_Item.ItemID;
                            new_INV_StockHistory.OperationTypeID = 7;
                            new_INV_StockHistory.ReferenceID = Convert.ToInt32(getIssueLastNumber);
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
                if (iNV_StockHistory_List != null)
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
                            }
                            else
                            {
                                var a = getIssueLast.ReturnNumber;
                                if (a == null)
                                {
                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + "01";
                                }
                                else
                                {
                                    //a = "278201801";
                                    a = a.ToString();
                                    string delimiters = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                                    //bool aaa = a.Contains(delimiters);
                                    string[] newstring = a.Split(new[] { delimiters }, StringSplitOptions.None);

                                    getIssueLastNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + (Convert.ToInt32(newstring[1]) + 1);
                                }
                            }
                            INV_StockHistory new_INV_StockHistory = new INV_StockHistory();
                            new_INV_StockHistory.ItemID = inv_Item.ItemID;
                            new_INV_StockHistory.OperationTypeID = 9;
                            new_INV_StockHistory.ReferenceID = Convert.ToInt32(item.IssueNumber);
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
        

    }


}