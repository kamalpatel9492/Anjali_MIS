using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnjaliMIS.Models;
using System.ComponentModel.DataAnnotations;

namespace AnjaliMIS.ViewModals
{
    public class INV_PurchaseOrderViewModal
    {
        public INV_PurchaseOrderViewModal()
        {
            INV_PurchaseOrderItems = new List<INV_PurchaseOrderItem>();
            INV_PurchaseOrderHistory = new List<INV_PurchaseOrderHistory>();
        }

        public int PurchaseOrderID { get; set; }
        public int CompanyID { get; set; }
        public int SellerPartyID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> StatusID { get; set; }
        public int Amount { get; set; }
        public int PaidAmount { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> PendingAmount { get; set; }
        public Nullable<int> Casar { get; set; }
        public string PONo { get; set; }
        public Nullable<System.DateTime> PODate { get; set; }
        public int FinYearID { get; set; }
        public bool IsLocal { get; set; }
        public Nullable<int> CGST { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<int> SGST { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<int> IGST { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<int> ItemID { get; set; }
        public List<INV_PurchaseOrderItem> INV_PurchaseOrderItems { get; set; }
        public string PartyIDName { get; set; }
        public bool IsComplete { get; set; }
        public List<INV_PurchaseOrderHistory> INV_PurchaseOrderHistory { get; set; }

    }
}