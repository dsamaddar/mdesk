Imports System.IO
Imports System.IO.StreamWriter

Partial Class Report_frmReportCurrentPrice
    Inherits System.Web.UI.Page

    Dim ItemData As New clsItemDataAccess()
    Dim UserData As New clsUserDataAccess()
    Dim DepartmentData As New clsDepartmentDataAccess()
    Dim BranchData As New clsBranchDataAccess()
    Dim ItemRequisitionData As New clsItemRequisitionDataAccess()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "RptCurrentPrice~") = 0 Then
            Response.Redirect("~\frmAILogin.aspx")
        End If

        If Not IsPostBack Then
            GetCurrentPrice()
        End If
    End Sub

 
    Protected Sub GetCurrentPrice()
        grdLowBalanceReport.DataSource = ItemData.fnGetCurrentPrice()
        grdLowBalanceReport.DataBind()
    End Sub

    Protected Sub btnExportLowBalanceRpt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportLowBalanceRpt.Click
        ExportToExcel(grdLowBalanceReport)
    End Sub

    Protected Sub ExportToExcel(ByVal gridview As System.Web.UI.WebControls.GridView)
        Try
            Dim sw As New StringWriter()
            Dim hw As New System.Web.UI.HtmlTextWriter(sw)
            Dim frm As HtmlForm = New HtmlForm()

            Page.Response.AddHeader("content-disposition", "attachment;filename=CurrentPrice.xls")
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
