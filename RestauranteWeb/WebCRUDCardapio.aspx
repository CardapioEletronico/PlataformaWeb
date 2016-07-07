<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="WebCRUDCardapio.aspx.cs" Inherits="RestauranteWeb.WebCRUDCardapio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 368px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
        <asp:TextBox ID="textBoxId" runat="server" style="margin-top: 0px">Id</asp:TextBox>
        <asp:TextBox ID="textBoxDesc" runat="server" style="margin-top: 0px">Descrição</asp:TextBox>
                    <asp:DropDownList ID="Restaurantes" runat="server">
                    </asp:DropDownList>
                    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
        <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" />
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
    </div>
    </form>
</body>
</html>
