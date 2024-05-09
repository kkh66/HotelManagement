<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="loginpage.aspx.cs" Inherits="HotelManagement.Staff.loginpage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: url("/img/Stafflogin.jpg");
            background-size: cover;
        }

        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            padding: 0;
        }

        footer {
            color: white;
        }

        .logo_use {
            color: white !important;
        }

            .logo_use:hover {
                color: wheat !important;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card login_staff">
        <div class="card-body">
            <h2>Login Staff</h2>
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
            <div class="form-floating mb-4 mt-5 custom_login_text">
                <asp:TextBox ID="txtuser" runat="server" CssClass="form-control " placeholder=""  ClientIDMode="Static"></asp:TextBox>
                <label for="txtuser">Username</label>
                <asp:Label ID="lbluser" runat="server" CssClass="text-danger" Text="" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="form-floating mb-4 custom_login_text">
                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=""  ClientIDMode="Static"></asp:TextBox>
                <label for="txtpassword">Password</label>
                <asp:Label ID="lblLogin" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
            </div>
            <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn_custom_login" OnClick="BtnLogin_Click" />
        </div>
    </div>

</asp:Content>
