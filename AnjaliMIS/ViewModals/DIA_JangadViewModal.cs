using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnjaliMIS.Models;

namespace AnjaliMIS.ViewModals
{
    public class DIA_JangadViewModal
    {

        public DIA_JangadViewModal()
        {
            DIA_JangadItems = new List<DIA_JangadItem>();
            DIA_JangadHistory = new List<DIA_JangadHistory>();
        }


        public int JangadID { get; set; }
        public int CompanyID { get; set; }
        public int PartyID { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public int StatusID { get; set; }
        public int UserID { get; set; }
        public int Amount { get; set; }
        public Nullable<int> RecivedAmount { get; set; }
        public Nullable<int> PendingAmount { get; set; }
        public int JangadNo { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public decimal PricePerCarat { get; set; }
        public int FinYearID { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public Nullable<decimal> TDSAmount { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> CGST { get; set; }
        public Nullable<int> SGST { get; set; }
        public Nullable<int> IGST { get; set; }
        public Nullable<int> TDS { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public Nullable<decimal> Casar { get; set; }
        public decimal TotalAmount { get; set; }

        public List<DIA_JangadItem> DIA_JangadItems { get; set; }
        public List<DIA_JangadHistory> DIA_JangadHistory { get; set; }


       
        public string JangadCode { get; set; }
        public Nullable<int> QTYByThan { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<bool> IsRatePerThan { get; set; }
        public Nullable<bool> IsRatePerCarat { get; set; }

        public virtual ACC_Tax ACC_Tax { get; set; }
        public virtual ACC_Tax ACC_Tax1 { get; set; }
        public virtual ACC_Tax ACC_Tax2 { get; set; }
        public virtual ACC_Tax ACC_Tax3 { get; set; }
        public virtual SYS_Company SYS_Company { get; set; }
        public virtual SYS_FinYear SYS_FinYear { get; set; }
        public virtual MST_Party MST_Party { get; set; }
        public virtual SYS_Status SYS_Status { get; set; }
        public virtual SEC_User SEC_User { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_JangadItem> DIA_JangadItem { get; set; }
    }
}