using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;

namespace AnjaliMIS.Controllers
{
    public class PartialViewMenuController : Controller
    {
        // GET: PartialViewMenu
        public ActionResult _MainMenu()
        {
            DB_A157D8_AnjaliMISEntities1 usersEntities = new DB_A157D8_AnjaliMISEntities1();
            List<MenuViewModal> _MenuViewModal = new List<MenuViewModal>();
            var data = usersEntities.PR_SEC_UserPrivileges_SelectByUserID(Convert.ToInt32(Session["UserID"].ToString())).ToList();

            _MenuViewModal = data.Select(dataElement => new MenuViewModal()
            {
                ModuleID = dataElement.ModuleID,
                ModuleName = dataElement.ModuleName,
                Controller = dataElement.Controller,
                Action = dataElement.Action,
                URL = dataElement.URL,
                IconName = dataElement.IconName,
                StrictlyStop = dataElement.StrictlyStop,
                Sequence = dataElement.Sequence,
                UserID = dataElement.UserID
            }).ToList();
            return PartialView("_MainMenu", _MenuViewModal);
            //return PartialView(_MenuViewModal);
        }
    }
}