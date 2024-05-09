<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="HotelManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            justify-items: center;
            background: linear-gradient(90deg, #181a1e,#373a40,#484c56);
        }

        nav {
            border-bottom: solid rgba(255, 255, 255, 0.05);
        }

        footer {
            border-top: 1px solid rgba(255, 255, 255, 0.125);
        }

        .profile_card {
            height: 400px;
            width: 900px;
            border-radius: 25px;
            backdrop-filter: blur(25px) saturate(0%);
            background-color: rgba(56, 61, 68, 1);
            border-radius: 12px;
            border: 1px solid rgba(255, 255, 255, 0.125);
            color: white;
        }

        .profile_image {
            border-radius: 50%;
            height: 100px;
            width: 100px;
            margin-top: 20px;
            margin-left: 80px;
        }

        .profile_username {
            margin-left: 80px;
            font-size: 20px;
        }

        .nav-pills .nav-link {
            border-radius: 15px;
        }

        .nav-link:active {
            border-radius: 15px;
        }

        .details_panel {
            margin-top: 20px;
        }

        .form-control {
            border-radius: 25px;
        }

        .tab_pass {
            margin-top: 10px;
            width: 500px;
            margin-left: 50px;
        }

            .tab_pass input {
                background-color: transparent !important;
                color: white !important;
            }

                .tab_pass input:hover {
                    background-color: transparent !important;
                    color: white !important;
                }

                .tab_pass input:active {
                    background-color: transparent !important;
                    color: white !important;
                }

            .tab_pass label {
                background-color: transparent;
                padding-left: 30px !important;
                color: white !important;
            }

                .tab_pass label:active {
                    background-color: transparent;
                    padding-left: 30px !important;
                    color: white !important;
                }

                .tab_pass label:hover {
                    background-color: transparent;
                    padding-left: 30px !important;
                    color: white !important;
                }

                .tab_pass label::after {
                    background-color: transparent !important;
                    padding-left: 30px !important;
                    color: white !important;
                }

        .btn_updatepass {
            margin-top: 10px;
            margin-left: 0px;
            width: 300px;
            height: 50px;
            border-radius: 25px;
            border: 1px solid rgba(255, 255, 255, 0.125);
        }

        .col_profile {
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="section_container">
        <div class="card profile_card row">
            <div class="container col-3">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/Admin.jpg" CssClass="profile_image" />
                    <input type="file" id="fileloadprofile" runat="server" style="display: none;" />
                    <asp:Label ID="Label1" runat="server" Text="Username" CssClass="profile_username"></asp:Label>
                </div>
                <div class="col col_profile">
                    <div class="nav nav-pills flex-column">
                        <a class="nav-link active" href="#" data-bs-toggle="pill" data-bs-target="#profile" role="tab">
                            <i class="bi bi-person me-2"></i>
                            User Profile
                        </a>
                        <a class="nav-link" href="#" data-bs-toggle="pill" data-bs-target="#Changepassword" role="tab">
                            <i class="bi bi-gear me-2"></i>
                            Change Password
                        </a>
                        <a class="nav-link" href="#" data-bs-toggle="pill" data-bs-target="#order" role="tab">
                            <i class="bi bi-gear me-2"></i>
                            Order History
                        </a>
                    </div>
                </div>
            </div>
            <div class="container col-9 details_panel">
                <div class="tab-panel fade show" id="profile" role="tabpanel">
                    <div class="profile">
                    </div>

                </div>
                <div class="tab-panel fade tab_pass" id="Changepassword" role="tabpanel">
                    <div class="form-floating mb-4">
                        <asp:TextBox ID="txtoldpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                        <label for="txtoldpass">Old Password</label>
                        <asp:Label ID="lbloldpass" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="form-floating mb-4 col-6">
                            <asp:TextBox ID="txtnewpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            <label for="txtnewpass">New Password</label>
                            <asp:Label ID="lblnewpass" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-floating mb-4 col-6">
                            <asp:TextBox ID="txtconpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            <label for="txtconpass">Confirm Password</label>
                            <asp:Label ID="lblconpass" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div>
                        <asp:Button ID="btnuppass" runat="server" Text="Update Pasword" CssClass="btn btn-primary btn_updatepass" OnClick="btnuppass_Click" />
                    </div>
                </div>
                <div class="tab-panel fade" id="order" role="tabpanel">
                    <asp:GridView ID="GridView1" runat="server" DataSourceID="Booking"></asp:GridView>
                    <asp:SqlDataSource ID="Booking" runat="server"></asp:SqlDataSource>
                </div>
            </div>
<<<<<<< HEAD

=======
>>>>>>> 5f31a4cc2de2eb64e2b2cb596dcbe05ead0a2aae
        </div>
    </section>
</asp:Content>
