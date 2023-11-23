<%@ Page Language="VB" Theme="CommonSkin" MasterPageFile="~/AIMaster.master" AutoEventWireup="false"
    CodeFile="frmReportBalanceTransferHistory.aspx.vb" Inherits="Report_frmBalanceTransferHistory"
    Title=".AI:Balance Transfer History:." %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr align="center">
            <td>
                <asp:Panel ID="pnlBalanceTransferHistory" runat="server" Width="100%" 
                    SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                <div class="widgettitle">
                                    Low Balance Report</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnExportLowBalanceRpt" runat="server" 
                                    CssClass="styled-button-1" Text="Export" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <div style="max-height: 500px; overflow: auto">
                                    <asp:GridView ID="grdBalanceTransferHistory" runat="server" 
                                        AutoGenerateColumns="False" CssClass="mGrid">
                                        <Columns>
                                            <asp:BoundField DataField="InvoiceNo" HeaderText="InvoiceNo" />
                                            <asp:BoundField DataField="InvoiceDate" HeaderText="InvoiceDate" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item" />
                                            <asp:BoundField DataField="src" HeaderText="Source" />
                                            <asp:BoundField DataField="dst" HeaderText="Destination" />
                                            <asp:BoundField DataField="AdjustedBalance" HeaderText="AdjustedBalance" />
                                            <asp:BoundField DataField="EntryDate" HeaderText="Date" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr align="center">
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
