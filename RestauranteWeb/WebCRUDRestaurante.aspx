<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebCRUDRestaurante.aspx.cs" Inherits="RestauranteWeb.WebForm1" Async="true" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 364px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
        <asp:TextBox ID="textBoxId" runat="server" style="margin-top: 0px">Id</asp:TextBox>
        <asp:TextBox ID="textBoxDesc" runat="server" style="margin-top: 0px">Descrição</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Select" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Insert" />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Update" />
        <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Delete" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
        <asp:Table ID="Table1" runat="server" style="margin-left: 0px">
        </asp:Table>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
