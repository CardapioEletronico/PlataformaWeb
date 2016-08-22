<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="WebCRUDProduto.aspx.cs" Inherits="RestauranteWeb.CRUDProduto" %>

<!DOCTYPE html>

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
                    <asp:Label ID="Label2" runat="server" Text="Id"></asp:Label>
                <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="Preco"></asp:Label>
                    <asp:TextBox ID="textBoxPreco" runat="server"></asp:TextBox>
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    <asp:Label ID="Label4" runat="server" Text="Nome"></asp:Label>
                    <asp:TextBox ID="textBoxNomeDescr" runat="server"></asp:TextBox>
                    <asp:Label ID="Label5" runat="server" Text="Descricao"></asp:Label>
                <asp:TextBox ID="textBoxDesc" runat="server" PlaceHolder="Descrição" style="margin-top: 0px"></asp:TextBox>
                 <asp:DropDownList ID="Cardapios" runat="server" AutoPostBack="True">
                  </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
        <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
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
