<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HotelManagement.Register" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="Style.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="Script.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/css/bootstrap-select.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/js/bootstrap-select.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/theme.min.css" />
    <script src="https://challenges.cloudflare.com/turnstile/v0/api.js" async defer></script>
    <style>
        body {
            background-image: url('/img/RegisterHotel.png');
            background-size: cover;
            min-height: 100vh;
        }

        .reg_card {
            backdrop-filter: blur(7px) saturate(180%);
            border-radius: 20px;
            background-color: rgba(17, 25, 40, 0.75);
            color: white;
            width: 500px;
            text-align: center;
        }

        .btn_regis {
            border-radius: 25px;
            background-color: rgb(83, 41, 156);
            border: 0;
        }

            .btn_regis:hover {
                border-radius: 25px;
                background-color: rgb(69, 32, 133) !important;
                border: 0;
            }

            .btn_regis:active {
                background-color: rgb(69, 32, 133) !important;
                border: 0;
            }

        .custom_regddl {
            margin-left: -50px;
        }

        .custom_regtext {
            background-color: transparent;
            width: 400px;
            margin: 30px;
        }

            .custom_regtext input {
                background-color: transparent !important;
                border-top: 0;
                border-left: 0;
                border-right: 0px;
                color: white !important;
                box-shadow: 0 !important;
            }

            .custom_regtext label {
                background-color: transparent !important;
                border-top: 0;
                border-left: 0;
                border-right: 0px;
                color: white !important;
            }

                .custom_regtext label::after {
                    background-color: transparent !important;
                    border-top: 0;
                    border-left: 0;
                    border-right: 0px;
                    color: white !important;
                }

        .custom_regddl {
            margin-left: -110px;
        }

            .custom_regddl button {
                background-color: transparent !important;
                color: white !important;
                width: 330px !important;
                text-align: center !important;
            }

            .custom_regddl .dropdown-menu {
                background-color: rgba(0,0,0,0.6) !important;
                width: 330px !important;
                color: white !important;
            }

                .custom_regddl .dropdown-menu .dropdown-item {
                    background-color: rgb(15, 6, 49,0.75);
                    width: 330px !important;
                    color: white !important;
                    backdrop-filter: blur(7px) saturate(180%);
                }

                    .custom_regddl .dropdown-menu .dropdown-item:hover {
                        background-color: rgb(32, 28, 81,0.5);
                        width: 330px !important;
                    }

        .loader {
            width: 40px;
            height: 26px;
            --c: no-repeat linear-gradient(#000 0 0);
            background: var(--c) 0 100%, var(--c) 50% 100%, var(--c) 100% 100%;
            background-size: 8px calc(100% - 4px);
            position: relative;
        }

            .loader:before {
                content: "";
                position: absolute;
                width: 8px;
                height: 8px;
                border-radius: 50%;
                background: #000;
                left: 0;
                top: 0;
                animation: l3-1 1.5s linear infinite alternate, l3-2 0.75s cubic-bezier(0,200,.8,200) infinite;
            }

        @keyframes l3-1 {
            100% {
                left: calc(100% - 8px)
            }
        }

        @keyframes l3-2 {
            100% {
                top: -0.1px
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center  vh-100">
            <div class="card reg_card ">
                <div class="card-body  d-flex flex-column">
                    <h2 class="card-header">Register Your Account</h2>
                    <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                    <div class="form-floating mb-2 mt-2 custom_regtext">
                        <asp:TextBox ID="txtcustomeruser" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                        <label for="txtcustomeruser">Username</label>
                        <asp:Label ID="lblcususe" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-2 mt-2 custom_regtext">
                        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder="" TextMode="Password"></asp:TextBox>
                        <label for="txtpassword">Password</label>
                        <asp:Label ID="lblpass" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-2 mt-2 custom_regtext">
                        <asp:TextBox ID="txtconfirmpass" runat="server" CssClass="form-control" placeholder="" TextMode="Password"></asp:TextBox>
                        <label for="txtconfirmpass">Confirm Password</label>
                        <asp:Label ID="lblconfitmpass" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-2 mt-2 custom_regtext">
                        <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" placeholder="" TextMode="email"></asp:TextBox>
                        <label for="txtemail">Email</label>
                        <asp:Label ID="lblmail" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    </div>
                    <div class="mb-2 mt-2 custom_regddl">
                        <asp:Label ID="lblgender" runat="server" Text="Gender :" AssociatedControlID="ddlgender" class="dropdown"></asp:Label>
                        <asp:DropDownList ID="ddlgender" runat="server" CssClass="selectpicker">
                            <asp:ListItem Value="0">--Select Gender--</asp:ListItem>
                            <asp:ListItem Value="1">Male</asp:ListItem>
                            <asp:ListItem Value="2">Female</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-floating mb-2 mt-2 custom_regtext">
                        <asp:TextBox ID="txtDateofBirth" runat="server" CssClass="form-control flatpickr-input" placeholder="" ClientIDMode="Static"></asp:TextBox>
                        <label for="txtDateofBirth">Date of Birth</label>
                        <asp:Label ID="lblDateofBirth" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="cf-turnstile mb-2 mt-2" id="cf-turnstile" data-sitekey="0x4AAAAAAAWJ3SUUSyP1g1Ad" data-callback="javascriptCallback" data-theme="auto"></div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary btn_regis" />
                </div>
            </div>
        </div>

    </form>
</body>
</html>
