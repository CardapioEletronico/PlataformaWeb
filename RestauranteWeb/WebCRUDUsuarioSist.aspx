<%@ Page Async="true" Language="C#" MasterPageFile="~/AdminSistema.Master" AutoEventWireup="true" CodeBehind="WebCRUDUsuarioSist.aspx.cs" Inherits="RestauranteWeb.WebCRUDAdminRest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <br /><br />
        <table style="width:100%;">
                <tr style="float:left">
                    <td>
                        <asp:TextBox ID="textBoxUsuario" runat="server" PlaceHolder="Usuario" style="margin-top: 0px" CssClass="textbox"></asp:TextBox>
                        <asp:TextBox ID="textBoxSenha" runat="server" PlaceHolder="Senha" style="margin-top: 0px" TextMode="Password"></asp:TextBox>
                        <asp:DropDownList ID="Restaurantes" runat="server" AutoPostBack="True"></asp:DropDownList>
                        Administrador Restaurante: <asp:CheckBox CssClass="AdminRest" ID="AdminRest" runat="server" />
                        Garçom: <asp:CheckBox CssClass="checkbox" ID="Garçom" runat="server" />
                        Caixa: <asp:CheckBox CssClass="checkbox" ID="Caixa" runat="server" />
                        Gerente Pedidos: <asp:CheckBox CssClass="checkbox" ID="GerentePedidos" runat="server" />
                    </td>
                
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                            <asp:Button ID="btnSelect"  CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                            <asp:Button ID="btnInsert"  CssClass="ls-btn-primary" runat="server" Text="Inserir" OnClick="btnInsert_Click" />
                            <asp:Button ID="btnUpdate"  CssClass="ls-btn-primary" runat="server" Text="Alterar" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" CssClass="ls-btn-danger" runat="server" Text="Deletar" OnClick="btnDelete_Click" />
                        </td>
                </tr>

                <tr style="width:100%">
                    <td class="auto-style1">
                        <asp:Table  ID="Table1" cssClass="ls-table ls-bg-header" width="100%" BorderWidth="1px" BorderStyle="Ridge" runat="server" style="margin-left: 0px">
                        </asp:Table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
        </table>
    </div>
    <link href="css/locastyle.css" rel="stylesheet" type="text/css"/>
            <link href="css/style.css" rel="stylesheet" type="text/css" />
            <link href="css/StyleSheet1.css" rel="stylesheet" />
            <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
            <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
</asp:Content>

