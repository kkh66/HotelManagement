<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HotelManagement.Admin.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-image: linear-gradient(6deg, #0b0b88, #3d10a8, #6216ca, #861dec) !important;
        }

        footer {
            background-color: transparent;
        }

        .nav_custom {
            background-color: transparent !important;
        }

        .btn_offcanvas {
            background-color: rgba(128, 0, 128, 0.1) !important;
            color: white;
            border-color: transparent;
        }

            .btn_offcanvas:hover {
                background-color: rgba(128, 0, 128, 0.1) !important;
                color: white;
                border-color: transparent;
            }

            .btn_offcanvas:active {
                background-color: rgba(128, 0, 128, 0.1) !important;
                color: white;
                border-color: transparent;
            }

            .btn_offcanvas:focus {
                background-color: rgba(128, 0, 128, 0.1) !important;
                color: white;
                border-color: transparent;
            }

        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            padding: 0;
        }

        .ddl_useritem {
            background: rgba(255, 255, 255, 0.3);
            color: white !important;
            margin-top: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card_login">
        <div class="card-body d-flex flex-column full-height">
            <h3 class="mb-4">Login</h3>
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
            <div class="mb-4 form-floating text_login">
                <asp:TextBox ID="txtloginuser" runat="server" ClientIDMode="Static" placeholder="" CssClass="form-control"></asp:TextBox>
                <label for="txtloginuser">UserName</label>
                <asp:Label ID="lbluser" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="mb-4 form-floating text_login">
                <asp:TextBox ID="txtloginpassword" runat="server" TextMode="Password" placeholder="" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                <label for="txtloginpassword">Password</label>
                <asp:Label ID="lblpassword" runat="server" Text="Label"></asp:Label>
            </div>
            <asp:Button ID="btnlogin" runat="server" Text="Login" OnClick="btnlogin_Click" CssClass="btn btn-primary btn_login" />
        </div>
    </div>
</asp:Content>
