<%@ Page Language="VB" Theme="CommonSkin" MasterPageFile="~/AIMaster.master" AutoEventWireup="false"
    CodeFile="frmReportCurrentPrice.aspx.vb" Inherits="Report_frmReportCurrentPrice"
    Title=".:Help Desk:Current Price Report:." %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /*Calendar Control CSS*/.MyCalendarCss .ajax__calendar_container
        {
            background-color: #DEF1F4;
            border: solid 1px #77D5F7;
        }
        .MyCalendarCss .ajax__calendar_header
        {
            background-color: #ffffff;
            margin-bottom: 4px;
        }
        .MyCalendarCss .ajax__calendar_title, .MyCalendarCss .ajax__calendar_next, .MyCalendarCss .ajax__calendar_prev
        {
            color: #004080;
            padding-top: 3px;
        }
        .MyCalendarCss .ajax__calendar_body
        {
            background-color: #ffffff;
            border: solid 1px #77D5F7;
        }
        .MyCalendarCss .ajax__calendar_dayname
        {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
            color: #004080;
        }
        .MyCalendarCss .ajax__calendar_day
        {
            color: #004080;
            text-align: center;
        }
        .MyCalendarCss .ajax__calendar_hover .ajax__calendar_day, .MyCalendarCss .ajax__calendar_hover .ajax__calendar_month, .MyCalendarCss .ajax__calendar_hover .ajax__calendar_year, .MyCalendarCss .ajax__calendar_active
        {
            color: #004080;
            font-weight: bold;
            background-color: #DEF1F4;
        }
        .MyCalendarCss .ajax__calendar_today
        {
            font-weight: bold;
        }
        .MyCalendarCss .ajax__calendar_other, .MyCalendarCss .ajax__calendar_hover .ajax__calendar_today, .MyCalendarCss .ajax__calendar_hover .ajax__calendar_title
        {
            color: #bbbbbb;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%;">
        <tr align="center">
            <td>
                <asp:Panel ID="pnlLowBalanceRptParameter" runat="server" Width="100%" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr>
                            <td align="left">
                                <div class="widgettitle">
                                    Current Price Report</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnExportLowBalanceRpt" runat="server" CssClass="styled-button-1"
                                    Text="Export" />
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <div style="max-height: 500px; overflow: auto">
                                    <asp:GridView ID="grdLowBalanceReport" runat="server" CellPadding="4" ForeColor="#333333"
                                        GridLines="None" AutoGenerateColumns="False" CssClass="mGrid">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="ItemID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemID" runat="server" Text='<%# Bind("ItemID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ItemName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ItemCode">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ItemCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LastEntry">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLastEntry" runat="server" Text='<%# Bind("LastEntry") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UnitPrice">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
