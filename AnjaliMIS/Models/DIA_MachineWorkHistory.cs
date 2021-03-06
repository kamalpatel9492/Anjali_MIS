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
    
    public partial class DIA_MachineWorkHistory
    {
        public int MachineWorkingHistoryID { get; set; }
        public int MachineID { get; set; }
        public int CassettID { get; set; }
        public int JangadItemID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Remarks { get; set; }
    
        public virtual DIA_Cassett DIA_Cassett { get; set; }
        public virtual DIA_JangadItem DIA_JangadItem { get; set; }
        public virtual DIA_Machine DIA_Machine { get; set; }
        public virtual SEC_User SEC_User { get; set; }
    }
}
