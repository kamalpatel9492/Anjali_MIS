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
    
    public partial class EMP_Designation
    {
        public EMP_Designation()
        {
            this.EMP_Employee = new HashSet<EMP_Employee>();
        }
    
        public int DesignationID { get; set; }
        public string Designation { get; set; }
        public string Remarks { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public int UserID { get; set; }
    
        public virtual SEC_User SEC_User { get; set; }
        public virtual ICollection<EMP_Employee> EMP_Employee { get; set; }
    }
}
