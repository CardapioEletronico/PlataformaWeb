<%@ Page  Async="true" Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDProduto.aspx.cs" Inherits="RestauranteWeb.CRUDProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        function setUploadButtonState(x, y) {
            var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $("FileUpload1");

            var butao1 = document.getElementById("<%= btnInsert.ClientID %>");
            var butao4 = document.getElementById("<%= btnSelect.ClientID %>");

            var label = document.getElementById("<%= Label1.ClientID %>");
            
            butao1.disabled = true;
            butao4.disabled = true;

            if (y == '') {
                return false;
            }

            else {
                if (x < maxFileSize)
                {
                    label.innerHTML = '';
                    butao1.disabled = false;
                    butao4.disabled = false;
                    return true;
                }

                else
                {
                    label.innerHTML = '<div class="ls-alert-danger"> O tamanho máximo do arquivo é 4Mb. Por favor, selecione um que esteja no padrão solicitado. </div>';

                    return false;
                }
            }
        }

        $(function () {
            $('input[type=file]').bind('change', function () {
                setUploadButtonState(this.files[0].size, this.files[0]);
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
             <asp:Label CssClass="" ID="Label1" runat="server"/>
        <table style="width:100%;">
            <tr style="float:left" class="col-lg-12">
                <td class="col-lg-12">
                    <asp:TextBox required="required" cssClass="col-lg-6" style="margin-left:-30px" ID="textBoxNomeDescr" PlaceHolder="Nome" runat="server"></asp:TextBox>
                    <asp:TextBox required="required" cssClass="col-lg-6" ID="textBoxDesc" runat="server" PlaceHolder="Descrição" style="margin-top: 0px; margin-left: 10px"></asp:TextBox>
                </td>

                <td class="col-lg-12">
                    <asp:TextBox required="required" CssClass="col-lg-4" style="margin-left:-30px; margin-top:10px;" ID="textBoxPreco" PlaceHolder="Preço" runat="server"></asp:TextBox>
                    <label class="col-lg-1" style="margin-top:20px">Imagem: </label>
                    <asp:FileUpload CssClass="col-lg-6" style="margin-top:18px" accept=".png,.jpg,.jpeg,.gif" ID="FileUpload1" AutoPostBack="True" runat="server" Height="33" ValidateRequestMode="Inherit" />
                </td>

                <td class="col-lg-12">
                    <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="Deu ruim" ControlToValidate="FileUpload1" />
                    <label class="col-lg-1" style="margin-top:20px; margin-left:-43px">Cardápio: </label>
                    <asp:DropDownList CssClass="col-lg-5" ID="Cardapios" style="height: 33px; margin-top: 10px" runat="server" AutoPostBack="True"></asp:DropDownList>

                    <label class="col-lg-1" style="margin-top:20px">Fila: </label>
                    <asp:DropDownList CssClass="col-lg-5" ID="Filas" style="height: 33px; margin-top: -5px" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>

                <td class="auto-style1">&nbsp;</td>
            </tr>

            <tr>
                <td>
                   <asp:Button ID="btnInsert" CssClass="ls-btn-primary" runat="server" Text="Inserir Produto" OnClick="btnInsert_Click" />
                   <asp:Button ID="btnSelect" CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
                </td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <asp:GridView CssClass="ls-table ls-bg-header bg-customer-support col-md-12" ID="GridView1" runat="server"
                        AutoGenerateColumns="false" GridLines="Horizontal" DataKeyNames="Id" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
                        OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="Nenhum restaurante foi adicionado."
                        OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Produto" SortExpression="NomeDescricao">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNomeDescricao" runat="server" Text='<%# Eval("NomeDescricao") %>'></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server"
                                        Text='<%# Eval("NomeDescricao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescricao" runat="server" Text='<%# Eval("Descricao") %>'></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server"
                                        Text='<%# Eval("Descricao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Preço" SortExpression="Preco">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPreço" runat="server" Width="90px" Text='<%# Eval("Preco") %>'></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server"
                                        Text='<%# Eval("Preco","{0:n}") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cardápio" SortExpression="Preco">
                                <EditItemTemplate>
                                        <asp:Label ID="lblCardapio" runat="server" Text='<%# Eval("Cardapio_Id")%>' Visible = "false"></asp:Label>
                                        <asp:DropDownList ID="CardapioDrop" runat = "server" AutoPostBack="true"/>              
                                    </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server"
                                        Text='<%# Eval("Cardapio.Descricao") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fila" SortExpression="Preco">

                                <EditItemTemplate>
                                        <asp:Label ID="lblFila" runat="server" Text='<%# Eval("Fila_Id")%>' Visible = "false"></asp:Label>
                                        <asp:DropDownList ID="FilaDrop" runat = "server" AutoPostBack="true"/>              
                                    </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server"
                                        Text='<%# Eval("Fila.Descricao") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="150px"></HeaderStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Imagem">
                                <EditItemTemplate>
                                    <asp:FileUpload accept=".png,.jpg,.jpeg,.gif" ID="FileUpload2" AutoPostBack="True" runat="server" Height="33" ValidateRequestMode="Inherit" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                       <asp:Image ID="imgCover" runat="server" Height="100" Width="100" AlternateText="Image Cover" ImageUrl='<%# Eval("ArquivoFoto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                             
                            <asp:CommandField ButtonType="Link" ItemStyle-CssClass="pull-right" ShowEditButton="true" EditText="Editar" ItemStyle-Width="100">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1"
                                        CommandArgument='<%# Eval("Id") %>'
                                        CommandName="Deletar" runat="server">
                                        Excluir</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="75px"></HeaderStyle>
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

