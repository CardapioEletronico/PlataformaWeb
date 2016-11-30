<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/AdminSistema.Master" AutoEventWireup="true" CodeBehind="WebCRUDAdminSistema.aspx.cs" Inherits="RestauranteWeb.WebCRUDAdminSistema" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
    <br /><br />
        <table style="width:100%;">
                <tr style="float:left">
                    <td>
                        <asp:TextBox ID="textBoxUsuario" required="required" runat="server" PlaceHolder="Usuario" style="margin-top: 0px" CssClass="textbox"></asp:TextBox>
                        <asp:TextBox ID="textBoxSenha" required="required" runat="server" PlaceHolder="Senha" style="margin-top: 0px" TextMode="Password"></asp:TextBox>
                        

                    </td>
                
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnInsert" ValidationGroup="valGroup1" CssClass="ls-btn-primary" runat="server" Text="Inserir" OnClick="btnInsert_Click" />
                        <asp:Button ID="btnSelect"  CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>

                <tr style="width:100%">

                    <td class="auto-style1">
                        <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header bg-customer-support col-md-12" ID="GridView1" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="Usuario" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum Administrador foi adicionado." 
                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
            
                                <asp:TemplateField HeaderText="Usuário" SortExpression="Usuario" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUsuario" runat="server" Text='<%# Eval("Usuario") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Link" ItemStyle-CssClass="pull-right" ShowEditButton="true" edittext="Editar" ItemStyle-Width="100"/>
                                
                                 <%--<asp:CommandField ShowDeleteButton="true" ItemStyle-CssClass="pull-right" ButtonType="Link" deletetext="Excluir" ItemStyle-Width="100"/> --%>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" 
                                        CommandArgument='<%# Eval("Usuario") %>' 
                                        CommandName="Deletar" runat="server">
                                        Excluir</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField> 
        
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
