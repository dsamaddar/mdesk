﻿<%@ Page Language="VB" MasterPageFile="~/AIMaster.master" AutoEventWireup="false"
    CodeFile="frmReportBalanceAdjHist.aspx.vb" Inherits="Report_frmReportBalanceAdjHist"
    Theme="CommonSkin" Title=".:mDesk: Balance Adj Hist:." %>

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
        <tr>
            <td>
                <asp:Panel ID="pnlRequisitionReportParameter" runat="server" Width="100%" SkinID="pnlInner">
                    <table style="width: 100%;">
                        <tr align="left">
                            <td colspan="5">
                                <div class="widgettitle">
                                    Balance Adjustment History<asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                </div>
                            </td>
                        </tr>
                        <tr align="left">
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                &nbsp;
                            </td>
                            <td class="label">
                                Branch/Warehouse
                            </td>
                            <td>
                                <asp:DropDownList ID="drpWareHouseList" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td class="label">
                                Item
                            </td>
                            <td>
                                <asp:DropDownList ID="drpInventoryItems" runat="server" CssClass="InputTxtBox" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                &nbsp;
                            </td>
                            <td class="label">
                                Date From
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequisitionDateFrom" runat="server" CssClass="InputTxtBox" Width="200px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtRequisitionDateFrom_CalendarExtender0" runat="server"
                                    CssClass="MyCalendarCss" Enabled="True" TargetControlID="txtRequisitionDateFrom">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="label">
                                To
                            </td>
                            <td>
                                <asp:TextBox ID="txtRequisitionDateTo" runat="server" CssClass="InputTxtBox" Width="200px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtRequisitionDateTo_CalendarExtender0" runat="server"
                                    CssClass="MyCalendarCss" Enabled="True" TargetControlID="txtRequisitionDateTo">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                            </td>
                            <td>
                            </td>
                            <td class="label">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                            </td>
                            <td>
                                <asp:Button ID="btnShowReport" runat="server" CssClass="styled-button-1" Text="Show Report" />
                                &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="styled-button-1" Text="Cancel" />
                            </td>
                            <td>
                                <asp:Button ID="btnExport" runat="server" CssClass="styled-button-1" Text="Export" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                            </td>
                            <td class="label">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlBalAdjHist" runat="server" SkinID="pnlInner">
                    <div>
                        <asp:GridView ID="grdBalAdjHist" runat="server" AutoGenerateColumns="True" CellPadding="4"
                            ForeColor="#333333" GridLines="None" CssClass="mGrid">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
