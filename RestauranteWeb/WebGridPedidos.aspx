<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/GerentePedidos.Master" AutoEventWireup="true" CodeBehind="WebGridPedidos.aspx.cs" Inherits="RestauranteWeb.WebGridPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header" ID="GridView1" runat="server"
        AutoGenerateColumns="false" DataKeyNames="Id"
        OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" OnRowCommand="GridView1_RowCommand1" EmptyDataText="Nenhum pedido foi feito.">
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
                <HeaderStyle />

                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server"
                        Text='<%# Eval("Hora") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Produto" SortExpression="Numero">
                <HeaderStyle />

                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server"
                        Text='<%# Eval("Produto.Descricao") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Situação" SortExpression="Numero">

                <EditItemTemplate>
                    <asp:TextBox ID="txtSituacao" runat="server" Text='<%# Eval("Situacao") %>'></asp:TextBox>
                </EditItemTemplate>

                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server"
                        Text='<%# Eval("Situacao").ToString() == "1" ? "Aberto" : "Atendido"  %>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>

            <%-- <asp:CommandField ButtonType="Link" ShowEditButton="true" edittext="Editar" ShowDeleteButton="true" ItemStyle-Width="100"/> --%>


            <asp:ButtonField ControlStyle-CssClass="ls-btn-primary" CommandName="AtenderPedido" Text="Atender" ButtonType="Button">
                <HeaderStyle Width="100px" />
            </asp:ButtonField>
            <asp:ButtonField ControlStyle-CssClass="ls-btn-danger" CommandName="CancelarPedido" Text="Cancelar" ButtonType="Button">
                <HeaderStyle Width="100px" />
            </asp:ButtonField>

        </Columns>
    </asp:GridView>
    <link href="css/locastyle.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleSheet1.css" rel="stylesheet" />
    <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png" />
    <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png" />
</asp:Content>
