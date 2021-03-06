﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class ItemStockReportViewModal
    {
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int UnitID { get; set; }
        public string Unit { get; set; }
        public bool IsConfigurable { get; set; }
        public int? Quantity { get; set; }
        public int? MinStockLimit { get; set; }
        public int? RejectedQuantity { get; set; }
    }
}