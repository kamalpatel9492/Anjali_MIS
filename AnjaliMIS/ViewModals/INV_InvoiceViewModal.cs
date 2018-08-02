using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AnjaliMIS.Models;

namespace AnjaliMIS.ViewModals
{
    public class INV_InvoiceViewModal
    {
        public INV_InvoiceViewModal()
        {
            INV_InvoiceItems = new List<INV_InvoiceItem>();
        }

        public int InvoiceID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> PartyID { get; set; }
        public Nullable<int> UserID { get; set; }
        public int Amount { get; set; }
        public int AmountReceived { get; set; }
        public int StatusID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public int InvoiceNo { get; set; }
        public int PONo { get; set; }
        public Nullable<int> AmountPending { get; set; }
        public int FinYearID { get; set; }
        public Nullable<int> CGST { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<int> SGST { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<int> IGST { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<decimal> Casar { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<int> ItemID { get; set; }
        //public Nullable<int> ItemPrice { get; set; }

        public List<INV_InvoiceItem> INV_InvoiceItems { get; set; }
    }
}