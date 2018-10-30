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
    public partial class RPT_INV_PurchaseOrder_Print : System.Web.UI.Page
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

                if (Request.QueryString["PurchaseOrderID"] != null)
                {
                    ddlPurchaseOrderID.SelectedValue = Convert.ToInt32(Request.QueryString["PurchaseOrderID"]).ToString();
                    btnShow_Click(btnShow, EventArgs.Empty);
                }
            }
        }

        private void FillDropDownList()
        {
            List<INV_PurchaseOrder> INV_PurchaseOrder = new List<INV_PurchaseOrder>();
            INV_PurchaseOrder = db.INV_PurchaseOrder.ToList();
            DataTable dt = CommonConfig.ToDataTable(INV_PurchaseOrder);
            ddlPurchaseOrderID.DataSource = dt;
            ddlPurchaseOrderID.DataValueField = "PurchaseOrderID";
            ddlPurchaseOrderID.DataTextField = "PONo";
            ddlPurchaseOrderID.DataBind();
            ddlPurchaseOrderID.Items.Insert(0, new ListItem(" Select Purchse Order", "-99"));
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            SqlInt32 PurchaseOrderID = SqlInt32.Null;
            DataTable dtPO = new DataTable();
            List<POPrintReportViewModal> _POPrintReportViewModal = new List<POPrintReportViewModal>();

            if (ddlPurchaseOrderID.SelectedIndex > 0)
                PurchaseOrderID = Convert.ToInt32(ddlPurchaseOrderID.SelectedValue);

            using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
            {
                try
                {
                    var sql = "exec PP_INV_PurchaseOrder_SelectForPrint @PurchaseOrderID";

                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@PurchaseOrderID", PurchaseOrderID));

                    SqlParameter[] parameters = parameterList.ToArray();

                    dtPO = CommonConfig.ToDataTable(db.Database.SqlQuery<POPrintReportViewModal>(sql, parameters).ToList());
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
            rvReport.LocalReport.ReportPath = Server.MapPath("~/Report/RPT_INV_PurchaseOrder_Print.rdlc");
            rvReport.LocalReport.DataSources.Add(new ReportDataSource("PP_INV_PurchaseOrder_SelectForPrint", dtPO));

            rvReport.Visible = true;
            rvReport.LocalReport.DisplayName = "Purchase Order";
            rvReport.LocalReport.Refresh();
            rvReport.DataBind();
        }
    }
}