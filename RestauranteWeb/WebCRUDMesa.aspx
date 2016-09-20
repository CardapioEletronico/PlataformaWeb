<%@ Page Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDMesa.aspx.cs" Inherits="RestauranteWeb.WebCRUDMesa" Async="true" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
            <table style="width:100%;">
                <tr style="float:left">
                    <td>
                        <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px">Id</asp:TextBox>
                        <asp:TextBox ID="textBoxNum" runat="server" PlaceHolder="Descrição" style="margin-top: 0px">Número</asp:TextBox>
                        <asp:DropDownList ID="Restaurantes" style="height: 33px;" runat="server" AutoPostBack="True"></asp:DropDownList>
                        <asp:Label ID="Label2" runat="server" Text="Disponível"></asp:Label>
                        <asp:RadioButtonList style="float:right" ID="CheckBoxList1" runat="server">
                            <asp:ListItem>Sim</asp:ListItem>
                            <asp:ListItem>Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSelect" CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                        <asp:Button ID="btnInsert" CssClass="ls-btn-primary" runat="server" Text="Inserir" OnClick="btnInsert_Click" />
                        <asp:Button ID="btnUpdate" CssClass="ls-btn-primary" runat="server" Text="Alterar" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnDelete" CssClass="ls-btn-danger" runat="server" Text="Deletar" OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Table ID="Table1" cssClass="ls-table ls-bg-header" width="100%" BorderWidth="1px" BorderStyle="Ridge" runat="server" style="margin-left: 0px">
                        </asp:Table>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <link href="css/locastyle.css" rel="stylesheet" type="text/css" />
        <link href="css/style.css" rel="stylesheet" type="text/css" />
        <link href="css/StyleSheet1.css" rel="stylesheet" />
        <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png" />
        <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png" />
    </asp:Content>