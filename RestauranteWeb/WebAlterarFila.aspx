<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/GerentePedidos.Master" AutoEventWireup="true" CodeBehind="WebAlterarFila.aspx.cs" Inherits="RestauranteWeb.WebAlterarFila" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            
            
            <asp:DropDownList CssClass="ls-dropdown-nav" ID="Filas" runat="server" AutoPostBack="true" Width="200"></asp:DropDownList>
            <asp:Button ID="btnTrocar"  CssClass="ls-btn-primary" runat="server" Text="Trocar Fila" OnClick="btnTrocar_Click" />
            Fila atual: <asp:Label ID="FilaLabel" runat="server" Text=""></asp:Label>      


            <link href="css/locastyle.css" rel="stylesheet" type="text/css"/>
            <link href="css/style.css" rel="stylesheet" type="text/css" />
            <link href="css/StyleSheet1.css" rel="stylesheet" />
            <link rel="icon" sizes="192x192" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
            <link rel="apple-touch-icon" href="/locawebstyle/assets/images/ico-boilerplate.png"/>
</asp:Content>
