<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormLogins.aspx.cs" Inherits="RestauranteWeb.WebFormLogins" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/LoginAdminSist.aspx">Login Admin Sistema</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LoginAdmRest.aspx">Login Admin Restaurante</asp:HyperLink>
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/LoginGerentePedido.aspx">Login Gerente Pedidos</asp:HyperLink>
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="#">Login Garçom</asp:HyperLink>
        
    </div>
    </form>
</body>
</html>
