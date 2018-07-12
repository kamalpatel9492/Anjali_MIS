using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class MenuViewModal
    {
        public int ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string URL { get; set; }
        public string IconName { get; set; }
        public bool StrictlyStop { get; set; }
        public int Sequence { get; set; }
        public int UserID { get; set; }
    }
}