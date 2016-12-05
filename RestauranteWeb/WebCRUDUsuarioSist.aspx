<%@ Page Async="true" Language="C#" MasterPageFile="~/AdminSistema.Master" AutoEventWireup="true" CodeBehind="WebCRUDUsuarioSist.aspx.cs" Inherits="RestauranteWeb.WebCRUDAdminRest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div>
    <br /><br />
        <table style="width:100%;">
                <tr class="col-lg-12" style="float:left">
                    <td class="col-lg-12">
                        <asp:TextBox ID="textBoxUsuario" cssClass="col-lg-6" runat="server" PlaceHolder="Usuario" style="margin-left: -29px" ></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="valGroup1" style="margin-top: 10px" CssClass="col-lg-6" ID="RequiredFieldValidator2" runat="server" ErrorMessage="É necessário preencher o campo para inserir o Usuário" ControlToValidate="textBoxUsuario"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                
                <tr class="col-lg-12" style="float:left; margin-top:10px;">
                    <td class="col-lg-12">
                        <asp:TextBox ID="textBoxSenha" cssClass="col-lg-6" runat="server" PlaceHolder="Senha" style="margin-left: -29px" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="valGroup1" style="margin-top: 10px" CssClass="col-lg-6" ID="RequiredFieldValidator1" runat="server" ErrorMessage="É necessário preencher o campo para inserir o Usuário" ControlToValidate="textBoxSenha"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr class="col-lg-12" style="float:left; margin-top:10px;">
                    <td class="col-lg-12">
                        <asp:label cssClass="col-lg-6" runat="server" style="margin-left:-42px;"> Restaurante: </asp:label>
                    </td>

                    <td class="col-lg-12">
                        <asp:DropDownList cssClass="col-lg-6" ID="Restaurantes" runat="server" style=" margin-left:-29px; height:30px; font-family: Verdana" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>

                <tr class="col-lg-12" style="float:left; margin-top:10px;">
                    <td class="col-lg-12">
                        <asp:label cssClass="col-lg-6" runat="server" style="margin-left:-42px;"> Tipo Usuário: </asp:label>
                    </td>
                    <td><br /></td>
                    <td style="margin-top:30px; margin-left:-42px;">
                        Administrador Restaurante: <asp:CheckBox CssClass="AdminRest" ID="AdminRest" runat="server" style="margin-right:20px;" />
                        Garçom: <asp:CheckBox CssClass="checkbox" ID="Garçom" runat="server"  style="margin-right:20px;"/>
                        Caixa: <asp:CheckBox CssClass="checkbox" ID="Caixa" runat="server"  style="margin-right:20px;"/>
                        Gerente Pedidos: <asp:CheckBox CssClass="checkbox" ID="GerentePedidos" runat="server"  style="margin-right:20px;"/>
                    </td>
                </tr>
            
                <tr class="col-lg-12" style="float:left; margin-top:10px; margin-left:-17px;">
                    <td>
                        <asp:Button ID="btnInsert" ValidationGroup="valGroup1" style="margin-right:30px;" CssClass="ls-btn-primary" runat="server" Text="Inserir Usuário" OnClick="btnInsert_Click" /> 
                        
                        <asp:DropDownList ID="RestaurantesSelect" runat="server" style=" height:30px; font-family: Verdana" AutoPostBack="True"></asp:DropDownList>
                        <asp:Button ID="btnSelect"  CssClass="ls-btn-primary" runat="server" Text="Consultar Lista" OnClick="btnSelect_Click" />
                    </td>
                </tr>


                <tr style="width:100%">
                    <td class="auto-style1">

                        <asp:GridView GridLines="Horizontal" CssClass="ls-table ls-bg-header bg-customer-support col-md-12" ID="GridView1" runat="server"
                            AutoGenerateColumns="false" DataKeyNames="Usuario" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum restaurante foi adicionado." 
                                OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                            <Columns>
            
                                <asp:TemplateField HeaderText="Usuário" SortExpression="Descricao" ItemStyle-Width="200">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUsuario" runat="server" Text='<%# Eval("Usuario") %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server"
                                            Text='<%# Eval("Usuario") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Admin. Rest." SortExpression="Admin" >
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="AdminRestCheck" runat="server" Checked='<%# Convert.ToBoolean(Eval("AdminRest")) %>' />                                    
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server"
                                            Text='<%# Eval("AdminRest").ToString() == "True" ? "Sim" : "Não"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Garçom" SortExpression="Garcom" >
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="GarcomCheck" runat="server" Checked='<%# Convert.ToBoolean(Eval("Garcom")) %>' />                                    
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server"
                                            Text='<%# Eval("Garcom").ToString() == "True" ? "Sim" : "Não"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ger. Pedidos" SortExpression="Gerente" >
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="GerPedidosCheck" runat="server" Checked='<%# Convert.ToBoolean(Eval("GerentePedidos")) %>' />                                    
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server"
                                            Text='<%# Eval("GerentePedidos").ToString() == "True" ? "Sim" : "Não"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Caixa" SortExpression="Caixa" >
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="CaixaCheck" runat="server" Checked='<%# Convert.ToBoolean(Eval("Caixa")) %>' />                                    
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server"
                                            Text='<%# Eval("Caixa").ToString() == "True" ? "Sim" : "Não"  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Restaurante" SortExpression="Caixa" >
                                    <EditItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Eval("Restaurante_Id")%>' Visible = "false"></asp:Label>
                                        <asp:DropDownList ID="Restaurante" runat = "server" AutoPostBack="true"/>              
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server"
                                            Text='<%# Eval("Restaurante_Id")%>'></asp:Label>
                                    </ItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server"
                                            Text='<%# Eval("Restaurante.Descricao") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ButtonType="Link" ItemStyle-CssClass="pull-right" ShowEditButton="true" edittext="Editar" ItemStyle-Width="100">
                                    <HeaderStyle Width="100px"></HeaderStyle>

                                </asp:CommandField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1"
                                            CommandArgument='<%# Eval("Usuario") %>'
                                            CommandName="Delete" runat="server">
                                        Apagar</asp:LinkButton>
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
    <link href="css/locastyle.css" rel="stylesheet" type="text/css"/>
            <link href="css/style.css" rel="stylesheet" type="text/css" />
            <link href="css/StyleSheet1.css" rel="stylesheet" />
            <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
            <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
</asp:Content>

