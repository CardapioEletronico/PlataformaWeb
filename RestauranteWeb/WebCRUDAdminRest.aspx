<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="WebCRUDAdminRest.aspx.cs" Inherits="RestauranteWeb.WebCRUDAdminRest" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CRUD Administrador Restaurante</title>
    <style type="text/css">
        body{
            background-color: cadetblue;
        }
        .textbox{
            width: 500px;
        }
        .auto-style1{
            padding-left:100px;
        }
        </style>

    <link rel="stylesheet" href="~/assets/css/bootstrap.min.css"/>

    <!-- Optional theme -->
    <link rel="stylesheet" href="~/assets/css/bootstrap-theme.min.css"/>

    <!-- Latest compiled and minified JavaScript -->
    <script src="~/assets/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="formCRUDAdminRest" runat="server">
    <div>
    <br /><br />
        <table style="width:100%;">
                <tr class="formulario">
                <td class="auto-style1">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                    <td class="input-group">
                        <asp:TextBox ID="textBoxUsuario" runat="server" PlaceHolder="Usuario" style="margin-top: 0px" CssClass="textbox"></asp:TextBox>
                        <asp:TextBox ID="textBoxSenha" runat="server" PlaceHolder="Senha" style="margin-top: 0px" TextMode="Password"></asp:TextBox>
                        <asp:DropDownList ID="Restaurantes" runat="server" AutoPostBack="True"></asp:DropDownList>
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
