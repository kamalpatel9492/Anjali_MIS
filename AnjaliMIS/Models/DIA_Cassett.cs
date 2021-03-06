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
    
    public partial class DIA_Cassett
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DIA_Cassett()
        {
            this.DIA_AssignCassettToMachine = new HashSet<DIA_AssignCassettToMachine>();
            this.DIA_JangadItem = new HashSet<DIA_JangadItem>();
            this.DIA_MachineWorkHistory = new HashSet<DIA_MachineWorkHistory>();
        }
    
        public int CassettsID { get; set; }
        public int CassettsNo { get; set; }
        public int Capacity { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> StatusID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_AssignCassettToMachine> DIA_AssignCassettToMachine { get; set; }
        public virtual SYS_Company SYS_Company { get; set; }
        public virtual SYS_Status SYS_Status { get; set; }
        public virtual SEC_User SEC_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_JangadItem> DIA_JangadItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DIA_MachineWorkHistory> DIA_MachineWorkHistory { get; set; }
    }
}
