//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnjaliMIS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DIA_Jangad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIA_Jangad()
        {
            this.DIA_JangadHistory = new HashSet<DIA_JangadHistory>();
            this.DIA_JangadItem = new HashSet<DIA_JangadItem>();
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
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<decimal> TDS { get; set; }
        public Nullable<bool> IsLocal { get; set; }
        public Nullable<decimal> Casar { get; set; }
        public decimal TotalAmount { get; set; }
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
        public virtual ICollection<DIA_JangadHistory> DIA_JangadHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_JangadItem> DIA_JangadItem { get; set; }
    }
}
