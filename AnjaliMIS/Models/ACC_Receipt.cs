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
    
    public partial class ACC_Receipt
    {
        public int ReceiptID { get; set; }
        public int AccountHeadID { get; set; }
        public Nullable<int> ReferenceID { get; set; }
        public Nullable<int> PartyID { get; set; }
        public Nullable<decimal> ReceivedAmount { get; set; }
        public Nullable<int> PaymentModeID { get; set; }
        public Nullable<int> ReferenceNo { get; set; }
        public Nullable<int> BankID { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public int UserID { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public int FinYearID { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public bool IsLocal { get; set; }
        public bool IsActive { get; set; }
        public Nullable<decimal> TDS { get; set; }
    }
}
