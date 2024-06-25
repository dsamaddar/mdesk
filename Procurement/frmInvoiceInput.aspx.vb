
Partial Class Procurement_frmInvoiceInput
    Inherits System.Web.UI.Page

    Dim SupplierData As New clsSupplierDataAccess()
    Dim InvoiceData As New clsInvoiceDataAccess()
    'Dim UserData As New clsUserDataAccess()
    Dim EmpData As New clsEmployeeInfoDataAccess()

    Protected Sub btnGenerateInvoice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGenerateInvoice.Click
        Try
            If drpSupplier.SelectedValue = "N\A" Then
                MessageBox("Select A Supplier First.")
                Exit Sub
            End If

            If drpApprover.SelectedValue = "N\A" Then
                MessageBox("Select Invoice Approver.")
                Exit Sub
            End If

            Dim Invoice As New clsInvoice()
            Dim folder As String = ""
            Dim Title As String = ""
            Dim DocExt As String = ""
            Dim DocFullName As String = ""
            Dim DocPrefix As String = ""
            Dim FileSize As Integer = 0
            Dim DocFileName As String = ""

            If flUpAttachment.HasFile Then

                If flUpAttachment.PostedFile.ContentType.Contains("pdf") Then
                    folder = Server.MapPath("~/Attachments/")

                    FileSize = flUpAttachment.PostedFile.ContentLength()
                    If FileSize > 524288000 Then
                        MessageBox("File size should be within 50MB")
                        Exit Sub
                    End If

                    DocPrefix = Title.Replace(" ", "")

                    DocExt = System.IO.Path.GetExtension(flUpAttachment.FileName)
                    DocFileName = "INV_" & DateTime.Now.ToString("ddMMyyHHmmss") & DocExt
                    DocFullName = folder & DocFileName
                    flUpAttachment.SaveAs(DocFullName)
                    Invoice.Attachment = DocFileName
                Else
                    MessageBox("Only PDF file type is allowed")
                    Exit Sub
                End If
            Else
                MessageBox("Attachment Is Mandatory!")
                Exit Sub
            End If

            Invoice.InvoiceNo = txtInvoiceNumber.Text
            Invoice.SupplierID = drpSupplier.SelectedValue
            Invoice.InvoiceDate = txtPurchaseDate.Text
            Invoice.InvoiceCost = txtInvoiceCost.Text
            Invoice.ApproverID = drpApprover.SelectedValue
            Invoice.EntryBy = Session("LoginUserID")

            Dim Check As Integer = InvoiceData.fnInsertInvoice(Invoice)

            If Check = 1 Then
                MessageBox("Invoice Inserted.")
                ClearInvoiceForm()
                ShowInvoiceDetails()
            Else
                MessageBox("Error Found.")
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
       
    End Sub

    Protected Sub ClearInvoiceForm()
        txtInvoiceNumber.Text = ""
        drpSupplier.SelectedIndex = -1
        txtPurchaseDate.Text = ""
        txtInvoiceCost.Text = ""
        drpApprover.SelectedIndex = -1

        grdInvoiceList.SelectedIndex = -1
        grdInvoiceList.DataSource = ""
        grdInvoiceList.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btnCancelInvoiceInput_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelInvoiceInput.Click
        ClearInvoiceForm()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "GenInv~") = 0 Then
            Response.Redirect("~\frmAILogin.aspx")
        End If

        If Not IsPostBack Then
            ShowSupplierInfo()
            ShowUserList()
            ShowInvoiceDetails()
        End If
    End Sub

    Protected Sub ShowInvoiceDetails()
        grdInvoiceList.DataSource = InvoiceData.fnGetDetailsInvoiceList()
        grdInvoiceList.DataBind()
    End Sub

    Protected Sub ShowSupplierInfo()
        drpSupplier.DataValueField = "SupplierID"
        drpSupplier.DataTextField = "SupplierName"
        drpSupplier.DataSource = SupplierData.fnGetSupplier()
        drpSupplier.DataBind()

        Dim A As New ListItem

        A.Text = "N\A"
        A.Value = "N\A"
        drpSupplier.Items.Insert(0, A)

    End Sub

    Protected Sub ShowUserList()
        drpApprover.DataTextField = "EmployeeName"
        drpApprover.DataValueField = "EmployeeID"
        drpApprover.DataSource = EmpData.fnGetActiveEmpList()
        drpApprover.DataBind()

        Dim A As New ListItem()

        A.Text = "N\A"
        A.Value = "N\A"

        drpApprover.Items.Insert(0, A)
    End Sub

    Protected Sub grdInvoiceList_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdInvoiceList.RowDeleting
        Try
            Dim lblInvoiceID As New Label
            lblInvoiceID = grdInvoiceList.Rows(e.RowIndex).FindControl("lblInvoiceID")

            Dim check As Integer = InvoiceData.fnDeleteInvoice(lblInvoiceID.Text)

            If check = 1 Then
                ClearInvoiceForm()
                ShowInvoiceDetails()
                MessageBox("Invoice Deleted Successfully.")
            End If

        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

End Class
