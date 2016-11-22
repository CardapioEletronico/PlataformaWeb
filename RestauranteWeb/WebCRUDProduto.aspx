<%@ Page Async="true" Language="C#" MasterPageFile="~/RestAdm.Master" AutoEventWireup="true" CodeBehind="WebCRUDProduto.aspx.cs" Inherits="RestauranteWeb.CRUDProduto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function setUploadButtonState(x) {
            var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024
            var fileUpload = $("FileUpload1");
            

            console.log(x);


            if (fileUpload.val() == '') {
                return false;
            }
            else {
                if (x < maxFileSize) {
                    $('#button_fileUpload').prop('enabled', true);
                    return true;
                } else {
                    $('#lbl_uploadMessage').text('File too big !')
                    return false;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width:100%;">
            <tr style="float:left">
                <td>
                    <asp:TextBox ID="textBoxId" runat="server" PlaceHolder="Id" style="margin-top: 0px"></asp:TextBox>
                    <asp:TextBox ID="textBoxPreco" PlaceHolder="Preço" runat="server"></asp:TextBox>



                    <asp:FileUpload ID="FileUpload1" AutoPostBack="True" runat="server" onchange="alert(this.files[0].size); setUploadButtonState(this.files[0].size);" Height="33" ValidateRequestMode="Inherit" />
                    <asp:CustomValidator ID="customValidatorUpload" runat="server" ErrorMessage="Deu ruim" ControlToValidate="FileUpload1" ClientValidationFunction="setUploadButtonState();" />
                    <asp:Button ID="button_fileUpload" runat="server" Text="Upload File" OnClick="btnInsert_Click" Enabled="false" />
                    <asp:Label ID="lbl_uploadMessage" runat="server" Text="" />



                    <asp:TextBox ID="textBoxNomeDescr" PlaceHolder="Nome" runat="server"></asp:TextBox>
                    <asp:TextBox ID="textBoxDesc" runat="server" PlaceHolder="Descrição" style="margin-top: 0px"></asp:TextBox>
                    <asp:DropDownList ID="Cardapios" style="height: 33px;" runat="server" AutoPostBack="True"></asp:DropDownList>
                    <asp:DropDownList ID="Filas" style="height: 33px;" runat="server" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="btnSelect" CssClass="ls-btn-primary" runat="server" Text="Atualizar Lista" OnClick="btnSelect_Click" />
               
                    <asp:Button ID="btnInsert" CssClass="ls-btn-primary" runat="server" Text="Inserir" OnClick="btnInsert_Click" />
                   
                    <asp:Button ID="btnUpdate" CssClass="ls-btn-primary" runat="server" Text="Alterar" OnClick="btnUpdate_Click" />
                    
                    <asp:Button ID="btnDelete" CssClass="ls-btn-danger" runat="server" Text="Deletar" OnClick="btnDelete_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
        <asp:Table ID="Table1" cssClass="ls-table ls-bg-header" width="100%" BorderWidth="1px" BorderStyle="Ridge" runat="server" style="margin-left: 0px">
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

