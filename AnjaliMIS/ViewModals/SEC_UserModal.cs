using AnjaliMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class SEC_UserAddEditModel
    {
        public SEC_User sEC_User { get; set; }

        public List<SYS_ModuleModel> sYS_ModuleList { get; set; }
    }

    public class SYS_ModuleModel
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public bool IsSelected { get; set; }
    }
}