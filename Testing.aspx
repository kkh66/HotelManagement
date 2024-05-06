<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Testing.aspx.cs" Inherits="HotelManagement.Testing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="card card_reset flex-column text-center">
            <div class="card-body">
                <h3 class="card-header">Reset Password</h3>
                <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                <div class="form-floating mb-3" runat="server" id="divUsername" visible="true">
                    <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                    <label for="txtusername">Username</label>
                </div>

                <div class="form-floating mb-3" runat="server" id="divEmail" visible="true">
                    <asp:TextBox ID="txtmail" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                    <label for="txtmail">User email</label>
                </div>

                <div class="form-floating mb-3" runat="server" id="divOtp" visible="false">
                    <asp:TextBox ID="txtOtp" runat="server" Visible="false" ClientIDMode="Static"></asp:TextBox>
                    <label for="txtOtp">Otp</label>
                </div>
                <div class="form-floating mb-3" runat="server" id="divPassword" visible="false">
                    <asp:TextBox ID="txtpassword" runat="server" Visible="false" CssClass="form-control" placeholder="" ClientIDMode="Static"></asp:TextBox>
                    <label for="txtpassword">New Password</label>
                </div>

                <div class="form-floating mb-3" runat="server" id="divConfirm" visible="false">
                    <asp:TextBox ID="txtconfirm" runat="server" Visible="false" ClientIDMode="Static"></asp:TextBox>
                    <label for="txtconfirm">Confirm Password</label>
                </div>
                <div class="card-footer">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" Visible="false" ClientIDMode="Static" />
                    <asp:Button ID="BtnSendOtp" runat="server" Text="SendOtp" OnClick="BtnSendOtp_Click" CssClass="btn btn-primary" ClientIDMode="Static" Visible="true" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
