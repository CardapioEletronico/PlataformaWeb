<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/GerentePedidos.Master" AutoEventWireup="true" CodeBehind="WebPedidosAbertos.aspx.cs" Inherits="RestauranteWeb.WebPedidosAbertos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


            <div>
                <table style="width:100%;">
                    <tr style="float:left">
                        <td>
                            <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px"></asp:TextBox>
                            <!--<asp:DropDownList CssClass="ls-dropdown-nav" ID="Restaurantes" runat="server" AutoPostBack="True"></asp:DropDownList>-->
                        </td>
                    
                        <td class="auto-style1">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnAtender"  CssClass="ls-btn-primary" runat="server" Text="Atender Pedido"/>
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
