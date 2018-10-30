using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnjaliMIS.ViewModals
{
    public class POPrintReportViewModal
    {
        public int PurchaseOrderID { get; set; }
        public string PONo { get; set; }
        public DateTime PODate { get; set; }
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyGSTIN { get; set; }
        public string Phone { get; set; }
        public string ComapanyCity { get; set; }
        public int SellerPartyID { get; set; }
        public string PartyName { get; set; }
        public string Mobile { get; set; }
        public string PartyGSTIN { get; set; }
        public string PartyAddress { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public decimal? CGSTPercentage { get; set; }
        public decimal? CGSTAmount { get; set; }
        public decimal? SGSTPercentage { get; set; }
        public decimal? SGSTAmount { get; set; }
        public decimal? IGSTPercentage { get; set; }
        public decimal? IGSTAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int OrderedQuantity { get; set; }
        public decimal PuchasePrice { get; set; }
    }
}