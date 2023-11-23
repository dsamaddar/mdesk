Imports System.IO
Imports System.IO.StreamWriter

Partial Class Report_frmBalanceTransferHistory
    Inherits System.Web.UI.Page

    Dim ItemData As New clsItemDataAccess()
    Dim WarehouseItemData As New clsWarehouseItemDataAccess()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "RptlowBal~") = 0 Then
            Response.Redirect("~\frmAILogin.aspx")
        End If

        If Not IsPostBack Then
            GetLowBalanceItemListInfo(Session("LoginUserID"))
        End If
    End Sub

    Protected Sub GetLowBalanceItemListInfo(ByVal UserID As String)
        grdBalanceTransferHistory.DataSource = WarehouseItemData.fnBalanceTransferHistory(UserID)
        grdBalanceTransferHistory.DataBind()

    End Sub

    Protected Sub btnExportLowBalanceRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportLowBalanceRpt.Click
        ExportToExcel(grdBalanceTransferHistory)
    End Sub

    Protected Sub ExportToExcel(ByVal gridview As System.Web.UI.WebControls.GridView)
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()

            Page.Response.AddHeader("content-disposition", "attachment;filename=LowBalanceReport.xls")
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

    Protected Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

End Class
