<%@ Page Async="true" Language="C#" MasterPageFile="~/RestAdm.Master"  AutoEventWireup="true" CodeBehind="WebCRUDCardapio.aspx.cs" Inherits="RestauranteWeb.WebCRUDCardapio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div>
                <table style="width:100%;">
                    <tr style="float:left">
                        <td>
                            <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px"></asp:TextBox>
                            <asp:TextBox ID="textBoxDesc" runat="server" PlaceHolder="Descrição" style="margin-top: 0px"></asp:TextBox>
                            <asp:DropDownList CssClass="ls-dropdown-nav" ID="Restaurantes" runat="server" AutoPostBack="True"></asp:DropDownList>
                            <div data-ls-module="dropdown" class="ls-dropdown">
                              <a href="#" class="ls-btn-primary">Users</a>
                              <ul class="ls-dropdown-nav">
                                  <li><a href="#">Kieran Whitaker</a></li>
                                  <li><a href="#">Casey Hoyle</a></li>
                                  <li><a href="#">Nadia Baker</a></li>
                                  <li><a href="#">Calum Perkins</a></li>
                                  <li><a href="#">Harrison McAllister</a></li>
                              </ul>
                            </div>
                        </td>
                    
                        <td class="auto-style1">&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSelect" runat="server" Text="Select" OnClick="btnSelect_Click" />
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                        </td>
                    </tr>

                    <tr style="width:100%">
                        <td class="auto-style1">
                            <asp:Table ID="Table1" runat="server" style="margin-left: 0px" BorderWidth="1px" BorderStyle="Ridge">

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


