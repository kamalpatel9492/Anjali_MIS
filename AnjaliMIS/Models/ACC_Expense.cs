//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnjaliMIS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ACC_Expense
    {
        public ACC_Expense()
        {
            this.ACC_ExpenseHistory = new HashSet<ACC_ExpenseHistory>();
        }
    
        public int ExpensesID { get; set; }
        public int AccountHeadID { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public int Amount { get; set; }
        public int PaymentModeID { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> BankID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public int FinYearID { get; set; }
        public bool IsLocal { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> CGST { get; set; }
        public Nullable<decimal> SGST { get; set; }
        public Nullable<decimal> IGST { get; set; }
        public Nullable<decimal> TDS { get; set; }
        public Nullable<int> PartyID { get; set; }
    
        public virtual ACC_AccountHead ACC_AccountHead { get; set; }
        public virtual ACC_Bank ACC_Bank { get; set; }
        public virtual SYS_FinYear SYS_FinYear { get; set; }
        public virtual ACC_PaymentMode ACC_PaymentMode { get; set; }
        public virtual SEC_User SEC_User { get; set; }
        public virtual ICollection<ACC_ExpenseHistory> ACC_ExpenseHistory { get; set; }
    }
}
