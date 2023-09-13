
Partial Class Administration_frmInventoryItems
    Inherits System.Web.UI.Page

    Dim UnitTypeData As New clsUnitTypeDataAccess()
    Dim ItemData As New clsItemDataAccess()

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub ShowUnitTypeList()
        drpUnitType.DataTextField = "UnitType"
        drpUnitType.DataValueField = "UnitTypeID"
        drpUnitType.DataSource = UnitTypeData.fnGetUnitTypeList()
        drpUnitType.DataBind()
    End Sub

    Protected Sub GetInventoryItemDetails()
        grdAvailableItems.DataSource = ItemData.fnGetItemListDetails()
        grdAvailableItems.DataBind()
    End Sub

    Protected Sub btnCancelInputItemType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelInputItemType.Click
        ClearItemInput()
    End Sub

    Protected Sub ClearItemInput()
        txtItemCode.Text = ""
        txtItemName.Text = ""
        txtLowBalanceReport.Text = ""
        txtMaxRequisition.Text = "10000"
        drpUnitType.SelectedIndex = -1
        chkItemIsActive.Checked = False

        grdAvailableItems.SelectedIndex = -1

        btnAddItem.Visible = True
        btnUpdateInventoryItems.Visible = False
        hdFldItemID.Value = ""
        hdFldItemImage.Value = ""
        imgItemImage.ImageUrl = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim MenuIDs As String

        MenuIDs = Session("PermittedMenus")

        If InStr(MenuIDs, "MngItm~") = 0 Then
            Response.Redirect("~\frmAILogin.aspx")
        End If

        If Not IsPostBack Then
            ShowUnitTypeList()
            btnUpdateInventoryItems.Visible = False
            GetInventoryItemDetails()
        End If
    End Sub

    Protected Sub btnUpdateInventoryItems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateInventoryItems.Click
        Dim folder As String = ""
        Dim Title As String = ""
        Dim DocExt As String = ""
        Dim DocFullName As String = ""
        Dim DocPrefix As String = ""
        Dim FileSize As Integer = 0
        Dim DocFileName As String = ""

        Dim Items As New clsItems()
        Try
            Items.ItemID = hdFldItemID.Value
            Items.ItemName = txtItemName.Text
            Items.ItemCode = txtItemCode.Text
            Items.LowBalanceReport = txtLowBalanceReport.Text
            Items.MaxAllowableRequisition = txtMaxRequisition.Text
            Items.UnitTypeID = drpUnitType.SelectedValue
            If chkItemIsActive.Checked = True Then
                Items.IsActive = True
            Else
                Items.IsActive = False
            End If

            If flUpItemImage.HasFile Then

                folder = ConfigurationManager.AppSettings("item_img_storage")

                FileSize = flUpItemImage.PostedFile.ContentLength()
                If FileSize > 2097152 Then
                    MessageBox("File size should be within 2MB")
                    Exit Sub
                End If

                'DocPrefix = Title.Replace(" ", "")

                DocExt = System.IO.Path.GetExtension(flUpItemImage.FileName)
                DocFileName = "ITM_IMG_" & DateTime.Now.ToString("ddMMyyHHmmss") & DocExt
                'DocFullName = folder & DocFileName
                flUpItemImage.SaveAs(Server.MapPath(folder) & DocFileName)

                Items.ItemImage = DocFileName
            Else
                Items.ItemImage = hdFldItemImage.Value
            End If

            Items.EntryBy = Session("LoginUserID")

            Dim Check As Integer = ItemData.fnUpdateInventoryItems(Items)

            If Check = 1 Then
                MessageBox("Item Updated Successfully.")
                GetInventoryItemDetails()
                ClearItemInput()
            Else
                MessageBox("Error Found.")
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try

    End Sub

    Protected Sub grdAvailableItems_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAvailableItems.SelectedIndexChanged

        grdAvailableItems.Focus()
        btnAddItem.Visible = False
        btnUpdateInventoryItems.Visible = True

        Dim lblItemID As New System.Web.UI.WebControls.Label()
        Dim lblItemName As New System.Web.UI.WebControls.Label()
        Dim lblItemCode As New System.Web.UI.WebControls.Label()
        Dim lblUnitTypeID As New System.Web.UI.WebControls.Label()
        Dim lblLowBalanceReport As New System.Web.UI.WebControls.Label()
        Dim lblMaxAllowableRequisition, lblItemImage As New System.Web.UI.WebControls.Label()
        Dim lblIsActive As New System.Web.UI.WebControls.Label()

        lblItemID = grdAvailableItems.SelectedRow.FindControl("lblItemID")
        lblItemName = grdAvailableItems.SelectedRow.FindControl("lblItemName")
        lblItemCode = grdAvailableItems.SelectedRow.FindControl("lblItemCode")
        lblUnitTypeID = grdAvailableItems.SelectedRow.FindControl("lblUnitTypeID")
        lblLowBalanceReport = grdAvailableItems.SelectedRow.FindControl("lblLowBalanceReport")
        lblMaxAllowableRequisition = grdAvailableItems.SelectedRow.FindControl("lblMaxAllowableRequisition")
        lblItemImage = grdAvailableItems.SelectedRow.FindControl("lblItemImage")
        lblIsActive = grdAvailableItems.SelectedRow.FindControl("lblIsActive")

        hdFldItemID.Value = lblItemID.Text
        txtItemName.Text = lblItemName.Text
        txtItemCode.Text = lblItemCode.Text
        drpUnitType.SelectedValue = lblUnitTypeID.Text
        txtLowBalanceReport.Text = lblLowBalanceReport.Text
        txtMaxRequisition.Text = lblMaxAllowableRequisition.Text
        hdFldItemImage.Value = lblItemImage.Text
        imgItemImage.ImageUrl = ConfigurationManager.AppSettings("item_img_storage") & lblItemImage.Text

        If lblIsActive.Text = "Active" Then
            chkItemIsActive.Checked = True
        Else
            chkItemIsActive.Checked = False
        End If

    End Sub

    Protected Sub btnAddItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Dim folder As String = ""
        Dim Title As String = ""
        Dim DocExt As String = ""
        Dim DocFullName As String = ""
        Dim DocPrefix As String = ""
        Dim FileSize As Integer = 0
        Dim DocFileName As String = ""
        Dim Items As New clsItems()

        Try
            Items.ItemName = txtItemName.Text
            Items.ItemCode = txtItemCode.Text
            Items.LowBalanceReport = txtLowBalanceReport.Text
            Items.MaxAllowableRequisition = txtMaxRequisition.Text
            Items.UnitTypeID = drpUnitType.SelectedValue
            If chkItemIsActive.Checked = True Then
                Items.IsActive = True
            Else
                Items.IsActive = False
            End If

            If flUpItemImage.HasFile Then

                folder = ConfigurationManager.AppSettings("item_img_storage")

                FileSize = flUpItemImage.PostedFile.ContentLength()
                If FileSize > 2097152 Then
                    MessageBox("File size should be within 2MB")
                    Exit Sub
                End If

                'DocPrefix = Title.Replace(" ", "")

                DocExt = System.IO.Path.GetExtension(flUpItemImage.FileName)
                DocFileName = "ITM_IMG_" & DateTime.Now.ToString("ddMMyyHHmmss") & DocExt
                'DocFullName = folder & DocFileName
                flUpItemImage.SaveAs(Server.MapPath(folder) & DocFileName)

                Items.ItemImage = DocFileName
            Else
                Items.ItemImage = "na.jpg"
            End If


            Items.EntryBy = Session("LoginUserID")

            Dim Check As Integer = ItemData.fnInsertInventoryItems(Items)

            If Check = 1 Then
                MessageBox("Item Inserted Successfully.")
                GetInventoryItemDetails()
                ClearItemInput()
            Else
                MessageBox("Error Found.")
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
       
    End Sub

End Class
