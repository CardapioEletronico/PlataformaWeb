<%@ Page Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDFila.aspx.cs" Inherits="RestauranteWeb.WebCRUDFila" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width:100%;">
            <tr style="float:left">
                <td>
                    <asp:TextBox ID="textBoxDesc" required="required" runat="server" PlaceHolder="Descrição" style="margin-top: 0px"></asp:TextBox>
                    <asp:DropDownList cssClass="ls-dropdown" style="height: 33px;" ID="Cardapios" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>               
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSelect" CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                 </td>
                <td>
                    <asp:Button ID="btnInsert" CssClass="ls-btn-primary" runat="server" Text="Inserir" OnClick="btnInsert_Click" />
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    
                     <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header bg-customer-support col-md-12" ID="GridView1" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="Id" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum restaurante foi adicionado." 
                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
            
                                <asp:TemplateField HeaderText="Fila" SortExpression="Descricao" >
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Eval("Descricao") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Descricao") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cardápio" SortExpression="Numero">
                                    <HeaderStyle />

                                    <EditItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Eval("Cardapio_Id")%>' Visible ="false"></asp:Label>
                                        <asp:DropDownList ID="CardapiosSelect" runat = "server" AutoPostBack="true"/>              
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("Cardapio.Descricao") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Link" ItemStyle-CssClass="pull-right" ShowEditButton="true" edittext="Editar" ItemStyle-Width="100"/>
                                
                                 <%--<asp:CommandField ShowDeleteButton="true" ItemStyle-CssClass="pull-right" ButtonType="Link" deletetext="Excluir" ItemStyle-Width="100"/>--%>
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" 
                                        CommandArgument='<%# Eval("Id") %>' 
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


