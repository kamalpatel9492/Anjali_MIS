using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class InvoicePrintReportViewModal
    {
        public int InvoiceID { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int PONo { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyGSTIN { get; set; }
        public string Phone { get; set; }
        public string ComapanyCity { get; set; }
        public int PartyID { get; set; }
        public string PartyName { get; set; }
        public string Mobile { get; set; }
        public string PartyGSTIN { get; set; }
        public string PartyAddress { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public decimal? CGST_Percentage { get; set; }
        public decimal? CGSTAmount { get; set; }
        public decimal? SGST_Percentage { get; set; }
        public decimal? SGSTAmount { get; set; }
        public decimal? IGST_Percentage { get; set; }
        public decimal? IGSTAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public int PricePerUnit { get; set; }
    }
}