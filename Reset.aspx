<%@ Page Title="" Language="C#" MasterPageFile="~/Customer.Master" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="HotelManagement.Reset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
        }

        .card_reset {
            width: 400px;
        }

            .card_reset .card-header {
                border: 0;
                padding: 0;
                background-color: transparent;
            }

            .card_reset .card-footer {
                border: 0;
                padding: 0;
                background-color: transparent;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card_reset flex-column text-center">
        <div class="card-body">
            <h3 class="card-header">Reset Password</h3>
            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
            <div class="form-floating mb-3" runat="server" id="divUsername" visible="true">
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                <label for="txtusername">Username</label>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </div>

            <div class="form-floating mb-3" runat="server" id="divEmail" visible="true">
                <asp:TextBox ID="txtmail" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                <label for="txtmail">User email</label>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </div>

            <div class="form-floating mb-3" runat="server" id="divOtp" visible="false">
                <asp:TextBox ID="txtOtp" runat="server" ClientIDMode="Static" placeholder="" CssClass="form-control"></asp:TextBox>
                <label for="txtOtp">Otp</label>
                <asp:Label ID="lblOtp" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="form-floating mb-3" runat="server" id="divPassword" visible="false">
                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                <label for="txtpassword">New Password</label>
                <asp:Label ID="lblpassword" runat="server" Text="Label"></asp:Label>
            </div>

            <div class="form-floating mb-3" runat="server" id="divConfirm" visible="false">
                <asp:TextBox ID="txtconfirm" runat="server" placeholder="" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                <label for="txtconfirm">Confirm Password</label>
                <asp:Label ID="lblconfirmpass" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="card-footer">
                <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" Visible="false" ClientIDMode="Static" CssClass="btn btn-primary" />
                <asp:Button ID="BtnSendOtp" runat="server" Text="SendOtp" OnClick="BtnSendOtp_Click" CssClass="btn btn-primary" ClientIDMode="Static" Visible="true" />
            </div>
        </div>
    </div>
</asp:Content>
