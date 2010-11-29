<%@ Page Title="Portal Home - Ragno Electric" Language="C#" MasterPageFile="~/Portal.Master"
    AutoEventWireup="true" CodeBehind="DisplayRepairs.aspx.cs" Inherits="Client_Portal.DisplayRepairs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentPlaceHolder" runat="server">
    <table width="100%">
        <tr>
            <td width="150px" style="padding: 0px 10px;" valign="top">
                <h1>
                    Menu</h1>
                <a href="/portal/DisplayRepairs.aspx">View Repairs</a><br />
                <!--<a href="/portal/DisplayLots.aspx">View Lots</a><br />-->
                <a href="/portal/ChangePassword.aspx">Change Password</a><br />
                <a href="/portal/Logout.aspx">Logout</a>
            </td>
            <td align="center" valign="top">
                <h1>
                    Repairs</h1>
                    <table width="100%">
                    <tr><td align="left"><h2>Upcoming Repairs</h2></td></tr>
                    <tr><td align="left" id="cellUpcomingRepairs" runat="server"></td></tr>
                    <tr><td align="left"><h2>Incompleted Repairs</h2></td></tr>
                    <tr><td align="left" id="cellIncompleteRepairs" runat="server"></td></tr>
                    <tr><td align="left"><h2>Completed Repairs</h2></td></tr>
                    <tr><td align="left" id="cellCompletedRepairs" runat="server"></td></tr></table>
            </td>
        </tr>
    </table>
</asp:Content>
