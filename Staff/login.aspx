<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HotelManagement.Staff.Login" %>

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

        .login_staff {
            width: 500px;
            height: 400px;
            backdrop-filter: blur(10px) saturate(180%);
            background-color: rgba(17, 25, 40, 0.75);
            border-radius: 12px;
            border: 1px solid rgba(255, 255, 255, 0.125);
            color: white;
            text-align: center;
        }

        .btn_custom_login {
            width: 300px;
            border-radius: 25px;
        }

        .custom_login_text {
            width: 450px !important;
            margin-left: 15px !important;
        }

            .custom_login_text input {
                background-color: transparent !important;
                color: white !important;
            }

                .custom_login_text input:hover {
                    background-color: transparent !important;
                    color: white !important;
                }

                .custom_login_text input:active {
                    background-color: transparent !important;
                    color: white !important;
                }

            .custom_login_text label {
                background-color: transparent !important;
                color: white !important;
            }

                .custom_login_text label::after {
                    background-color: transparent !important;
                    color: white !important;
                }

                .custom_login_text label:hover {
                    background-color: transparent !important;
                    color: white !important;
                }

                .custom_login_text label:active {
                    background-color: transparent !important;
                    color: white !important;
                }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card login_staff">
        <div class="card-body">
            <h2>Login Staff</h2>
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
            <div class="form-floating mb-4 mt-5 custom_login_text">
                <asp:TextBox ID="txtuser" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtuser">Username</label>
                <asp:Label ID="lbluser" runat="server" CssClass="text-danger" Text="" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="form-floating mb-4 custom_login_text">
                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtpassword">Password</label>
                <asp:Label ID="lblLogin" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
                <div class="form-floating mb-5 mt-5 custom_login_text">
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                    <label for="txtuser">Username</label>
                    <asp:Label ID="Label1" runat="server" CssClass="text-danger" Text=""></asp:Label>
                </div>
                <div class="form-floating mb-5 custom_login_text">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" CssClass="form-control" placeholder=""></asp:TextBox>
                    <label for="txtpassword">Password</label>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
                <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="btn btn-primary btn_custom_login" OnClick="BtnLogin_Click" />
            </div>
        </div>
    </div>
</asp:Content>
