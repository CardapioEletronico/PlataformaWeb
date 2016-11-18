<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/GerentePedidos.Master" AutoEventWireup="true" CodeBehind="WebGridPedidos.aspx.cs" Inherits="RestauranteWeb.WebGridPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView cssClass="ls-table ls-bg-header" ID="GridView1" runat="server" 
        AutoGenerateColumns="false" DataKeyNames="Id"
 OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
 OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum pedido foi feito.">
    <Columns>
        <asp:TemplateField HeaderText="Id" SortExpression="Id">
            <HeaderStyle />

            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" 
                Text='<%# Eval("Id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Quantidade" SortExpression="Disponivel">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" 
                Text='<%# Eval("Quantidade") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Hora" SortExpression="Numero">
            <HeaderStyle/>

            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" 
                Text='<%# Eval("Hora") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Produto" SortExpression="Numero">
            <HeaderStyle/>

            <ItemTemplate>
                <asp:Label ID="Label5" runat="server" 
                Text='<%# Eval("Produto.Descricao") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Situacao" SortExpression="Numero">

            <EditItemTemplate>
                <asp:TextBox ID="txtSituacao" runat="server" Text='<%# Eval("Situacao") %>'></asp:TextBox>
            </EditItemTemplate>

            <ItemTemplate>
                <asp:Label ID="Label4" runat="server" 
                Text='<%# Eval("Situacao") %>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

        <asp:CommandField ButtonType="Link" ShowEditButton="true" edittext="Atender" ShowDeleteButton="true" ItemStyle-Width="100"/>
    </Columns>
</asp:GridView>
    <link href="css/locastyle.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleSheet1.css" rel="stylesheet" />
    <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png" />
    <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png" />
</asp:Content>
