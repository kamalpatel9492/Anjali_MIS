using AnjaliMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class INV_IssueReturnViewModal
    {
        public INV_IssueReturnViewModal()
        {
            IssueReturnItems = new List<INV_StockHistory>();
        }

        public int IssueReturnID { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> IssueReturnByUserID { get; set; }
        public Nullable<int> IssueReturnToUserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> IssueReturnDate { get; set; }
        public string IssueReturnNo { get; set; }
        public int FinYearID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsRejected { get; set; }

        public List<INV_StockHistory> IssueReturnItems { get; set; }

    }
}