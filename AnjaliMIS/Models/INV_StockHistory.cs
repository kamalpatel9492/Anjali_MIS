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
    
    public partial class INV_StockHistory
    {
        public int StockHistoryID { get; set; }
        public int ItemID { get; set; }
        public int OperationTypeID { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public int Quantity { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public int FinYearID { get; set; }
        public string IssueNumber { get; set; }
        public string ReturnNumber { get; set; }
    
        public virtual INV_Item INV_Item { get; set; }
        public virtual SYS_FinYear SYS_FinYear { get; set; }
        public virtual SYS_OperationType SYS_OperationType { get; set; }
        public virtual SEC_User SEC_User { get; set; }
    }
}
