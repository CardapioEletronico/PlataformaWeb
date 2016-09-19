<%@ Page Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDMesa.aspx.cs" Inherits="RestauranteWeb.WebCRUDMesa" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <br /><br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style1">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
        <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px">Id</asp:TextBox>
        <asp:TextBox ID="textBoxNum" runat="server" PlaceHolder="Descrição" style="margin-top: 0px">Número</asp:TextBox>
                    <asp:DropDownList ID="Restaurantes" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Disponível"></asp:Label>
                    <asp:RadioButtonList ID="CheckBoxList1" runat="server" BorderStyle="Dashed">
                        <asp:ListItem>Sim</asp:ListItem>
                        <asp:ListItem>Não</asp:ListItem>
                    </asp:RadioButtonList>
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
        <asp:Table ID="Table1" cssClass="ls-table ls-bg-header" width="100%" BorderWidth="1px" BorderStyle="Ridge" runat="server" style="margin-left: 0px">
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
