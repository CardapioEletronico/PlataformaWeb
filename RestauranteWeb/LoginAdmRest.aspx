﻿<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="LoginAdmRest.aspx.cs" Inherits="RestauranteWeb.AdmRest.LoginAdmRest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        @import url(https://fonts.googleapis.com/css?family=Roboto:300);
            .login-page {
              width: 360px;
              padding: 8% 0 0;
              margin: auto;
            }
            .form {
              position: relative;
              z-index: 1;
              background: #FFFFFF;
              max-width: 360px;
              margin: 0 auto 100px;
              padding: 45px;
              text-align: center;
              box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
            }
            .form input {
              font-family: "Roboto", sans-serif;
              outline: 0;
              background: #f2f2f2;
              width: 100%;
              border: 0;
              margin: 0 0 15px;
              padding: 15px;
              box-sizing: border-box;
              font-size: 14px;
            }
            .form button {
              font-family: "Roboto", sans-serif;
              text-transform: uppercase;
              outline: 0;
              background: #4CAF50;
              width: 100%;
              border: 0;
              padding: 15px;
              color: #FFFFFF;
              font-size: 14px;
              -webkit-transition: all 0.3 ease;
              transition: all 0.3 ease;
              cursor: pointer;
            }
            .form button:hover,.form button:active,.form button:focus {
              background: #43A047;
            }
            .form .message {
              margin: 15px 0 0;
              color: #b3b3b3;
              font-size: 12px;
            }
            .form .message a {
              color: #4CAF50;
              text-decoration: none;
            }
            .container {
              position: relative;
              z-index: 1;
              max-width: 300px;
              margin: 0 auto;
            }
            .container:before, .container:after {
              content: "";
              display: block;
              clear: both;
            }
            .container .info {
              margin: 50px auto;
              text-align: center;
            }
            .container .info h1 {
              margin: 0 0 15px;
              padding: 0;
              font-size: 36px;
              font-weight: 300;
              color: #1a1a1a;
            }
            .container .info span {
              color: #4d4d4d;
              font-size: 12px;
            }
            .container .info span a {
              color: #000000;
              text-decoration: none;
            }
            .container .info span .fa {
              color: #EF3B3A;
            }
            body {
              background: #76b852; /* fallback for old browsers */
              background: -webkit-linear-gradient(right, #76b852, #8DC26F);
              background: -moz-linear-gradient(right, #76b852, #8DC26F);
              background: -o-linear-gradient(right, #76b852, #8DC26F);
              background: linear-gradient(to left, #76b852, #8DC26F);
              font-family: "Roboto", sans-serif;
              -webkit-font-smoothing: antialiased;
              -moz-osx-font-smoothing: grayscale;      
            }

            #logo{
                margin-bottom:100px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="login-page">
          <div class="form">
                <a style="text-decoration: none;" href="WebFormLogins.aspx"><asp:Label ID="Label1" CssClass="logo" runat="server" Text="LOGO" Font-Names="verdana"></asp:Label></a>
              <br />
                <asp:TextBox ID="TextBoxUsuario" placeholder="Usuário" runat="server" CssClass="ls-login-bg-user ls-field-lg"></asp:TextBox>
                <asp:TextBox ID="TextBoxSenha"  placeholder="Senha" runat="server" CssClass="ls-login-bg-password ls-field-lg" TextMode="Password"></asp:TextBox>
                <button runat="server" onServerClick="login_click">login</button>
          </div>
        </div>
    </form>
</body>
</html>
