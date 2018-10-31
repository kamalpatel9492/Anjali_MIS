using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnjaliMIS.Report
{
    public partial class RPT_INV_Invoice_Print : System.Web.UI.Page
    {
        DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region 11.1 Check User Login
                if (Session["UserID"] == null)
                    Response.Redirect("~/Home/Index");
                #endregion 11.1 Check User Login

                FillDropDownList();

                if (Request.QueryString["InvoiceID"] != null)
                {
                    ddlInvoiceID.SelectedValue = Convert.ToInt32(Request.QueryString["InvoiceID"]).ToString();
                    btnShow_Click(btnShow, EventArgs.Empty);
                }
            }
        }


        private void FillDropDownList()
        {
            List<INV_Invoice> INV_Invoice = new List<INV_Invoice>();
            INV_Invoice = db.INV_Invoice.ToList();
            DataTable dt = CommonConfig.ToDataTable(INV_Invoice);
            ddlInvoiceID.DataSource = dt;
            ddlInvoiceID.DataValueField = "InvoiceID";
            ddlInvoiceID.DataTextField = "InvoiceNo";
            ddlInvoiceID.DataBind();
            ddlInvoiceID.Items.Insert(0, new ListItem(" Select Invoice", "-99"));
        }


        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (ddlInvoiceID.SelectedIndex > 1)
            {
                SqlInt32 InvoiceID = SqlInt32.Null;
                DataTable dtInvoice = new DataTable();
                List<InvoicePrintReportViewModal> _POPrintReportViewModal = new List<InvoicePrintReportViewModal>();

                if (ddlInvoiceID.SelectedIndex > 0)
                    InvoiceID = Convert.ToInt32(ddlInvoiceID.SelectedValue);

                using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
                {
                    try
                    {
                        var sql = "exec PP_INV_Invoice_SelectForPrint @InvoiceID";

                        List<SqlParameter> parameterList = new List<SqlParameter>();
                        parameterList.Add(new SqlParameter("@InvoiceID", InvoiceID));

                        SqlParameter[] parameters = parameterList.ToArray();

                        dtInvoice = CommonConfig.ToDataTable(db.Database.SqlQuery<InvoicePrintReportViewModal>(sql, parameters).ToList());
                    }
                    catch (Exception ex)
                    {
                        //SaveErrorLog("JobRepository", "GetJobListByPaging", ex);

                    }
                }
                rvReport.Reset();

                rvReport.LocalReport.DataSources.Clear();
                rvReport.ProcessingMode = ProcessingMode.Local;
                rvReport.LocalReport.EnableExternalImages = true;
                rvReport.LocalReport.ReportPath = Server.MapPath("~/Report/RPT_INV_Invoice_Print.rdlc");
                rvReport.LocalReport.DataSources.Add(new ReportDataSource("PP_INV_Invoice_SelectForPrint", dtInvoice));

                rvReport.Visible = true;
                rvReport.LocalReport.DisplayName = "Purchase Order";
                rvReport.LocalReport.Refresh();
                rvReport.DataBind();
            }
        }
    }
}