<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true" CodeBehind="ViewRepair.aspx.cs" Inherits="Client_Portal.ViewRepair" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentPlaceHolder" runat="server">
<table width="100%">
        <tr>
            <td width="150px" style="padding: 0px 10px;" valign="top">
                <h1>
                    Menu</h1>
                <a href="/portal/DisplayRepairs.aspx">View Repairs</a><br />
                <a href="/portal/DisplayLots.aspx">View Lots</a><br />
                <a href="/portal/ChangePassword.aspx">Change Password</a><br />
                <a href="/portal/Logout.aspx">Logout</a>
            </td>
            <td align="center" valign="top">
                <h1 id="pageTitle" runat="server">Work Order 12345</h1>
                    <table width="100%">
                    <tr>
                    <td align="left"><h2>Lot Information</h2></td>
                    <td align="left"><h2>Appointment Information</h2></td>
                    </tr>
                    <tr><td align="left" id="pageLotInfo" runat="server"></td><td align="left" id="cellIncompleteRepairs" runat="server"></td></tr>
                    <tr><td align="left"><h2>Owner Information</h2></td><td align="left"><h2>Notes</h2></td></tr>
                    <tr><td align="left" id="pageOwnerInfo" runat="server"></td><td align="left" id="pageNotes" runat="server"></td></tr>
                    <tr><td align="left" colspan="2"><h2>Repair Action(s)</h2></td></tr>
                    <tr><td align="left" id="pageRepairActions" runat="server" colspan="2"></td></tr>
                    </table>
            </td>
        </tr>
    </table>
</asp:Content>
