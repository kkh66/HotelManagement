<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HotelManagement.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="Style.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.bundle.min.js"></script>
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
            width:500px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center  vh-100">
            <div class="card reg_card ">
                <div class="card-body  d-flex flex-column">
                    <h2 class="card-header">Register Your Account</h2>
                    <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtcustomeruser" runat="server"></asp:TextBox>
                    <asp:Label ID="lblcususe" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
                    <asp:Label ID="labelpass" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtconfirmpass" runat="server"></asp:TextBox>
                    <asp:Label ID="labelconfitmpass" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
                    <asp:Label ID="labelmail" runat="server" Text="Label"></asp:Label>
                    <asp:DropDownList ID="ddlgender" runat="server">
                        <asp:ListItem>--Select Gender--</asp:ListItem>
                        <asp:ListItem Value="0">Male</asp:ListItem>
                        <asp:ListItem Value="1">Female</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblgender" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtDateofBirth" runat="server"></asp:TextBox>
                    <asp:Label ID="lblDateofBirth" runat="server" Text="Label"></asp:Label>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
