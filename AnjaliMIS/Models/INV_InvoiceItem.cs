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
    
    public partial class INV_InvoiceItem
    {
        public int InvoiceItemID { get; set; }
        public int InvoiceID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string Remarks { get; set; }
        public int PricePerUnit { get; set; }
    
        public virtual INV_Invoice INV_Invoice { get; set; }
        public virtual INV_Item INV_Item { get; set; }
    }
}
