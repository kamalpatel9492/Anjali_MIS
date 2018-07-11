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
    
    public partial class ACC_Bank
    {
        public ACC_Bank()
        {
            this.ACC_Expense = new HashSet<ACC_Expense>();
            this.ACC_ExpenseHistory = new HashSet<ACC_ExpenseHistory>();
            this.EMP_Employee = new HashSet<EMP_Employee>();
        }
    
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string BankShortName { get; set; }
        public string Remarks { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public int UserID { get; set; }
    
        public virtual ICollection<ACC_Expense> ACC_Expense { get; set; }
        public virtual ICollection<ACC_ExpenseHistory> ACC_ExpenseHistory { get; set; }
        public virtual ICollection<EMP_Employee> EMP_Employee { get; set; }
    }
}
