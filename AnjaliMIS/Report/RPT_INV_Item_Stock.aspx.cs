using AnjaliMIS.Models;
using AnjaliMIS.ViewModals;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnjaliMIS.Report
{
    public partial class RPT_INV_Item_Stock : System.Web.UI.Page
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
            }
        }

        private void FillDropDownList()
        {
            List<INV_Category> Category = new List<INV_Category>();
            Category = db.INV_Category.ToList();
            DataTable dt = CommonConfig.ToDataTable(Category);
            ddlCategoryID.DataSource = dt;
            ddlCategoryID.DataValueField = "CategoryID";
            ddlCategoryID.DataTextField = "CategoryName";
            ddlCategoryID.DataBind();
            ddlCategoryID.Items.Insert(0, new ListItem(" Select Category", "-99"));
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            SqlInt32 CategoryID = SqlInt32.Null;
            String CategoryName = String.Empty;
            DataTable dtItem = new DataTable();
            List<ItemStockReportViewModal> _ItemStockReportViewModal = new List<ItemStockReportViewModal>();

            if (ddlCategoryID.SelectedIndex > 0)
            {
                CategoryID = Convert.ToInt32(ddlCategoryID.SelectedValue);
                CategoryName = ddlCategoryID.SelectedItem.Text;
            }

            using (DB_A157D8_AnjaliMISEntities1 db = new DB_A157D8_AnjaliMISEntities1())
            {
                try
                {
                    var sql = "exec PP_INV_Item_SelectItemStockReportByCategoryID @CategoryID,@CompanyID";

                    List<SqlParameter> parameterList = new List<SqlParameter>();
                    parameterList.Add(new SqlParameter("@CategoryID", CategoryID));
                    parameterList.Add(new SqlParameter("@CompanyID", CommonConfig.GetCompanyID()));

                    SqlParameter[] parameters = parameterList.ToArray();

                    dtItem = CommonConfig.ToDataTable(db.Database.SqlQuery<ItemStockReportViewModal>(sql, parameters).ToList());

                    //_ItemStockReportViewModal = data1.Select(dataElement => new ItemStockReportViewModal()
                    //{
                    //    ModuleID = dataElement.ModuleID,
                    //    ModuleName = dataElement.ModuleName,
                    //    Controller = dataElement.Controller,
                    //    Action = dataElement.Action,
                    //    URL = dataElement.URL,
                    //    IconName = dataElement.IconName,
                    //    StrictlyStop = dataElement.StrictlyStop,
                    //    Sequence = dataElement.Sequence,
                    //    UserID = dataElement.UserID
                    //}).ToList();
                }
                catch (Exception ex)
                {
                    //SaveErrorLog("JobRepository", "GetJobListByPaging", ex);

                }
            }

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DB_A157D8_AnjaliMISEntities1"].ConnectionString))
            //{
            //    using (SqlCommand cmd = new SqlCommand("PP_INV_Item_SelectItemStockReportByCategoryID", con))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryID;
            //        cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CommonConfig.GetCompanyID();

            //        con.Open();
            //        SqlDataReader dr = cmd.ExecuteReader();
            //        dtItem.Load(dr);
            //    }
            //}


            rvReport.Reset();

            rvReport.LocalReport.DataSources.Clear();
            rvReport.ProcessingMode = ProcessingMode.Local;
            rvReport.LocalReport.EnableExternalImages = true;
            rvReport.LocalReport.ReportPath = Server.MapPath("~/Report/RPT_INV_Item_Stock.rdlc");
            rvReport.LocalReport.DataSources.Add(new ReportDataSource("PP_INV_Item_SelectItemStockReportByCategoryID", dtItem));

            rvReport.Visible = true;
            rvReport.LocalReport.DisplayName = "Item Stock Report";
            rvReport.LocalReport.Refresh();
            rvReport.DataBind();
        }
    }
}