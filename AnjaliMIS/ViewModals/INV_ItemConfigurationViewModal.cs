using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnjaliMIS.Models;

namespace AnjaliMIS.ViewModals
{
    public class INV_ItemConfigurationViewModal
    {
        public INV_ItemConfigurationViewModal()
        {
            SubItems = new List<INV_ItemConfiguration>();
        }
        public int ItemConfigurationID { get; set; }
        public int MainItemID { get; set; }
        public int SubItemID { get; set; }
        public int Qunatity { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }

        public List<INV_ItemConfiguration> SubItems { get; set; }

    }
}