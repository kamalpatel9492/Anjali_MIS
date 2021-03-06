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
    
    public partial class SEC_User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SEC_User()
        {
            this.ACC_Expense = new HashSet<ACC_Expense>();
            this.ACC_ExpenseHistory = new HashSet<ACC_ExpenseHistory>();
            this.DIA_AssignCassettToMachine = new HashSet<DIA_AssignCassettToMachine>();
            this.DIA_Cassett = new HashSet<DIA_Cassett>();
            this.DIA_Jangad = new HashSet<DIA_Jangad>();
            this.DIA_JangadHistory = new HashSet<DIA_JangadHistory>();
            this.DIA_JangadItem = new HashSet<DIA_JangadItem>();
            this.DIA_Machine = new HashSet<DIA_Machine>();
            this.DIA_MachineMaintenance = new HashSet<DIA_MachineMaintenance>();
            this.DIA_MachineWorkHistory = new HashSet<DIA_MachineWorkHistory>();
            this.EMP_Department = new HashSet<EMP_Department>();
            this.EMP_Designation = new HashSet<EMP_Designation>();
            this.EMP_Employee = new HashSet<EMP_Employee>();
            this.EMP_EmployeeAttendance = new HashSet<EMP_EmployeeAttendance>();
            this.INV_Category = new HashSet<INV_Category>();
            this.INV_Invoice = new HashSet<INV_Invoice>();
            this.INV_InvoiceHistory = new HashSet<INV_InvoiceHistory>();
            this.INV_IssueReturn = new HashSet<INV_IssueReturn>();
            this.INV_IssueReturn1 = new HashSet<INV_IssueReturn>();
            this.INV_Item = new HashSet<INV_Item>();
            this.INV_ItemConfiguration = new HashSet<INV_ItemConfiguration>();
            this.INV_ItemPrice = new HashSet<INV_ItemPrice>();
            this.INV_PurchaseOrder = new HashSet<INV_PurchaseOrder>();
            this.INV_PurchaseOrderHistory = new HashSet<INV_PurchaseOrderHistory>();
            this.INV_StockHistory = new HashSet<INV_StockHistory>();
            this.LOC_City = new HashSet<LOC_City>();
            this.LOC_Country = new HashSet<LOC_Country>();
            this.LOC_State = new HashSet<LOC_State>();
            this.LOG_PageVisited = new HashSet<LOG_PageVisited>();
            this.MST_Party = new HashSet<MST_Party>();
            this.SEC_LoginHistory = new HashSet<SEC_LoginHistory>();
            this.SEC_User1 = new HashSet<SEC_User>();
            this.SEC_UserPrivileges = new HashSet<SEC_UserPrivileges>();
            this.SEC_UserPrivileges1 = new HashSet<SEC_UserPrivileges>();
            this.SYS_Company = new HashSet<SYS_Company>();
            this.SYS_FinYear = new HashSet<SYS_FinYear>();
        }
    
        public int UserID { get; set; }
        public Nullable<int> CreatedByUserID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_Expense> ACC_Expense { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACC_ExpenseHistory> ACC_ExpenseHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_AssignCassettToMachine> DIA_AssignCassettToMachine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_Cassett> DIA_Cassett { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_Jangad> DIA_Jangad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_JangadHistory> DIA_JangadHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_JangadItem> DIA_JangadItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_Machine> DIA_Machine { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_MachineMaintenance> DIA_MachineMaintenance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_MachineWorkHistory> DIA_MachineWorkHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Department> EMP_Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Designation> EMP_Designation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_Employee> EMP_Employee { get; set; }
        public virtual EMP_Employee EMP_Employee1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EMP_EmployeeAttendance> EMP_EmployeeAttendance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_Category> INV_Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_Invoice> INV_Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_InvoiceHistory> INV_InvoiceHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_IssueReturn> INV_IssueReturn { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_IssueReturn> INV_IssueReturn1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_Item> INV_Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_ItemConfiguration> INV_ItemConfiguration { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_ItemPrice> INV_ItemPrice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_PurchaseOrder> INV_PurchaseOrder { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_PurchaseOrderHistory> INV_PurchaseOrderHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INV_StockHistory> INV_StockHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOC_City> LOC_City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOC_Country> LOC_Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOC_State> LOC_State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOG_PageVisited> LOG_PageVisited { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MST_Party> MST_Party { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEC_LoginHistory> SEC_LoginHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEC_User> SEC_User1 { get; set; }
        public virtual SEC_User SEC_User2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEC_UserPrivileges> SEC_UserPrivileges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEC_UserPrivileges> SEC_UserPrivileges1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_Company> SYS_Company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SYS_FinYear> SYS_FinYear { get; set; }
    }
}
