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
    
    public partial class ACC_AccountHead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACC_AccountHead()
        {
            this.ACC_Expense = new HashSet<ACC_Expense>();
            this.ACC_ExpenseHistory = new HashSet<ACC_ExpenseHistory>();
        }
    
        public int AccountHeadID { get; set; }
        public string TitleName { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_Expense> ACC_Expense { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_ExpenseHistory> ACC_ExpenseHistory { get; set; }
    }
}
