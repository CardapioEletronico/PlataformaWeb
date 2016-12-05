<%@ Page Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDMesa.aspx.cs" Inherits="RestauranteWeb.WebCRUDMesa" Async="true" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
            <table style="width:100%;">
                <tr style="float:left">
                    <td>
                        <asp:TextBox required="required" ID="textBoxNum" runat="server" PlaceHolder="Número" style="margin-top: 0px"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text="Disponível: "></asp:Label>
                        <asp:RadioButtonList style="float:right" ID="CheckBoxList1" runat="server">
                            <asp:ListItem Selected="True">Sim</asp:ListItem>
                            <asp:ListItem>Não</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSelect" CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                        <asp:Button ID="btnInsert" CssClass="ls-btn-primary" runat="server" Text="Inserir Mesa" OnClick="btnInsert_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header bg-customer-support col-md-12" ID="GridView1" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="Id" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum restaurante foi adicionado."
                            OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>

                                <asp:TemplateField HeaderText="Número" SortExpression="Descricao">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNumero" runat="server" Text='<%# Eval("Numero") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Numero") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Disponível" SortExpression="Descricao">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Convert.ToBoolean(Eval("Disponivel")) == true ? "Livre" : "Ocupada"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:CommandField ShowDeleteButton="true" ItemStyle-CssClass="pull-right" ButtonType="Link" deletetext="Excluir" ItemStyle-Width="100"/>--%>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ItemStyle-Width="100px" ID="LinkButton2"
                                            CommandArgument='<%# Eval("Id") %>'
                                            CommandName="Situacao" runat="server">
                                        Mudar Disponibilidade</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200px"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:CommandField ItemStyle-Width="100px" ButtonType="Link" ShowEditButton="true" EditText="Editar" >
                                    <HeaderStyle Width="100px"></HeaderStyle>

                                </asp:CommandField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ItemStyle-Width="100px" ID="LinkButton1"
                                            CommandArgument='<%# Eval("Id") %>'
                                            CommandName="Deletar" runat="server">
                                        Excluir</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
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