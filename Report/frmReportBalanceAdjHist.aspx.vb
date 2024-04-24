Imports System.IO

Partial Class Report_frmReportBalanceAdjHist
    Inherits System.Web.UI.Page

    Dim WarehouseData As New clsWarehouseDataAccess()
    Dim ItemData As New clsItemDataAccess()

    Protected Sub GetWarehouseList(ByVal EmployeeID As String)
        drpWareHouseList.DataTextField = "WarehouseName"
        drpWareHouseList.DataValueField = "WarehouseID"
        drpWareHouseList.DataSource = WarehouseData.fnGetWarehouseListByEmp(EmployeeID)
        drpWareHouseList.DataBind()
    End Sub

    Protected Sub ShowItemList()
        drpInventoryItems.DataTextField = "ItemName"
        drpInventoryItems.DataValueField = "ItemID"
        drpInventoryItems.DataSource = ItemData.fnGetItemList()
        drpInventoryItems.DataBind()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "RptBalAdjHist~") = 0 Then
            Response.Redirect("~\frmAILogin.aspx")
        End If

        If Not IsPostBack Then
            GetWarehouseList(Session("EmployeeID"))
            ShowItemList()
            txtRequisitionDateFrom.Text = Now.Date
            txtRequisitionDateTo.Text = DateAdd(DateInterval.Day, 1, Now.Date)
        End If
    End Sub

    Protected Sub btnShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowReport.Click
        Try
            grdBalAdjHist.DataSource = WarehouseData.fnGetItemBalanceAdjHist(drpWareHouseList.SelectedValue, drpInventoryItems.SelectedValue, Convert.ToDateTime(txtRequisitionDateFrom.Text), Convert.ToDateTime(txtRequisitionDateTo.Text))
            grdBalAdjHist.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ExportToExcel(ByVal gridview As System.Web.UI.WebControls.GridView)
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()

            Page.Response.AddHeader("content-disposition", "attachment;filename=BalanceAdjHistory.xls")
            Page.Response.ContentType = "application/vnd.ms-excel"
            Page.Response.Charset = ""
            Page.EnableViewState = False
            frm.Attributes("runat") = "server"
            Controls.Add(frm)
            frm.Controls.Add(gridview)
            frm.RenderControl(hw)
            Response.Write(sw.ToString())
            Response.End()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            ExportToExcel(grdBalAdjHist)
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub
End Class
