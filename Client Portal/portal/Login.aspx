<%@ Page Title="Portal Login - Ragno Electric" Language="C#" MasterPageFile="~/Portal.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Client_Portal.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pageContentPlaceHolder" runat="server">
    <form action="Login.aspx" method="post">
    <table style="margin:auto auto;">
    <tr><td colspan="2" align="center"><h1>Portal Login</h1></td></tr>
        <tr>
            <td>
                Email Address:
            </td>
            <td>
                <input type="text" name="txtEmail" />
            </td>
        </tr>
        <tr>
            <td>
                Password:
            </td>
            <td>
                <input type="password" name="txtPass" />
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
    </table>
    </form>
</asp:Content>
