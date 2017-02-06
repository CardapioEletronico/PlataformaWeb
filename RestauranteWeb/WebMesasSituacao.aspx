<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Caixa.Master" AutoEventWireup="true" CodeBehind="WebMesasSituacao.aspx.cs" Inherits="RestauranteWeb.WebMesasSituacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
                <table style="width:100%;">

                    <tr style="width:100%">
                        <td class="auto-style1">
                            <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header" ID="GridView1" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="Id"
                             OnRowCommand="GridView1_RowCommand1" EmptyDataText="Nenhuma mesa está aberta.">
                            <Columns>
    
                                <asp:TemplateField HeaderText="Mesa" SortExpression="Disponivel">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Numero") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Valor Total" SortExpression="Disponivel">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Pedido.ValorTotal", "{0:n}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:ButtonField ControlStyle-CssClass="ls-btn-primary" CommandName="EncerrarMesa" Text="Encerrar Mesa" ButtonType="Button">
                                    <HeaderStyle Width="150px" />
                                </asp:ButtonField>

                            </Columns>
                        </asp:GridView>
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
