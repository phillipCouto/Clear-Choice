<%@ Page Title="" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true"
    CodeBehind="ViewRepair.aspx.cs" Inherits="Client_Portal.ViewRepair" %>

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
                <h1 id="pageTitle" runat="server">
                    Work Order 12345</h1>
                <table width="100%">
                    <tr>
                        <td align="left" width="40%">
                            <h2>
                                Lot Information</h2>
                        </td>
                        <td align="left">
                            <h2>
                                Appointment Information</h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                            <table>
                                <tr>
                                    <td class="columnLabel">
                                        <b>Lot Number:</b>
                                    </td>
                                    <td id="tdLotNum" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="columnLabel">
                                        <b>Address:</b>
                                    </td>
                                    <td id="tdLotAddress" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="columnLabel">
                                        <b>Model:</b>
                                    </td>
                                    <td id="tdLotModel" runat="server">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="columnLabel">
                                        <b>Work Order:</b>
                                    </td>
                                    <td id="tdtLotWorkOrder" runat="server">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="left" valign="top">
                        <table>
                        <tr><td class="columnLabel"><b>Date:</b></td><td id="tdApptDate" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Window:</b></td> <td id="tdApptWindow" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Requested By:</b></td><td id="tdApptRequested" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Inspection Pass Date:</b></td><td id="tdApptInspection" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Source Code:</b></td><td id="tdApptSourceCode" runat="server"></td></tr>
                        </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <h2>
                                Owner Information</h2>
                        </td>
                        <td align="left">
                            <h2>
                                Notes</h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">
                        <table>
                        <tr><td class="columnLabel"><b>Name:</b></td><td id="tdClientName" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Phone:</b></td><td id="tdClientPhone" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Alt Phone:</b></td><td id="tdClientAltPhone" runat="server"></td></tr>
                        <tr><td class="columnLabel"><b>Email:</b></td><td id="tdClientEmail" runat="server"></td></tr>
                        </table>
                        </td>
                        <td align="left" valign="top">
                            <div id="pageNotes" runat="server" class="pageNotes"></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <h2>
                                Repair Action(s)</h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" id="pageRepairActions" runat="server" colspan="2">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
