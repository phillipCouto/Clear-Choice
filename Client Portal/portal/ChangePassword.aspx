<%@ Page Title="Change Password - Ragno Electric" Language="C#" MasterPageFile="~/Portal.Master"
    AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Client_Portal.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentPlaceHolder" runat="server">
    <form action="ChangePassword.aspx" method="post">
    <table style="margin: auto auto;">
        <tr>
            <td colspan="2" align="center">
                <h1>
                    Change Password</h1>
            </td>
        </tr>
        <tr>
            <td align="right">
                Current Password:
            </td>
            <td>
                <input type="password" name="txtCurrentPass" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Password:
            </td>
            <td>
                <input type="password" name="txtPassOne" />
            </td>
        </tr>
        <tr>
            <td align="right">
                Retype Password:
            </td>
            <td>
                <input type="password" name="txtPassTwo" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <input type="submit" value="Login" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="ErrorMsg" runat="server" CssClass="ErrorMsgs"></asp:Label>
            </td>
        </tr>
         <tr>
            <td colspan="2" align="center" style="font-size:12px;">
                Password must contain a Capital Letter, atleast one character, and atleast one number with a size between 6 and 16 characters long.
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
