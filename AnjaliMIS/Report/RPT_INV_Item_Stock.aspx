<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RPT_INV_Item_Stock.aspx.cs" Inherits="AnjaliMIS.Report.RPT_INV_Item_Stock" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Stock Report</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList runat="server" ID="ddlCategoryID"></asp:DropDownList>
        <br />
        <asp:Button runat="server" ID="btnShow" OnClick="btnShow_Click" Text="Show" />

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer ID="rvReport" runat="server" Height="1000px" Width="100%" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
