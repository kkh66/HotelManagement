<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="HotelManagement.Admin.AddStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/css/bootstrap-select.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/js/bootstrap-select.min.js"></script>
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            background-color: black;
            min-height: 100vh;
        }

        .form-control {
            width: 100%;
            border-left: 0px;
            border-right: 0px;
            border-top: 0px;
            background-color: transparent;
            border-radius: 15px !important;
            color: white !important;
        }

        .btn-light {
            background-color: transparent !important;
            color: white !important;
            border-radius: 25px;
        }

        .form-floating > .form-control-plaintext ~ label::after, .form-floating > .form-control:focus ~ label::after, .form-floating > .form-control:not(:placeholder-shown) ~ label::after, .form-floating > .form-select ~ label::after {
            color: white !important;
            background-color: transparent !important;
        }

        .form-floating > .form-control-plaintext ~ label, .form-floating > .form-control:focus ~ label, .form-floating > .form-control:not(:placeholder-shown) ~ label, .form-floating > .form-select ~ label {
            color: white !important;
        }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card_staff">
        <div class="card-body d-flex flex-column">
            <h3 class="card-title">Add Staff</h3>
            <asp:Label ID="lblerror" runat="server" Text="" CssClass="text-danger"></asp:Label>
            <div class="form-floating mb-3 mt-3 text_staff">
                <asp:TextBox ID="txtStaffUserName" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static" />
                <label for="txtStaffUserName">User Name</label>
                <asp:Label ID="lblwarnuser" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="form-floating mb-3 text_staff">
                <asp:TextBox ID="txtStaffName" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static" />
                <label for="txtStaffName">Name</label>
                <asp:Label ID="lblwarnname" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="form-floating mb-3 text_staff">
                <asp:TextBox ID="txtstaffage" runat="server" CssClass="form-control" placeholder="" ClientIDMode="Static" />
                <label for="txtstaffage">Age</label>
                <asp:Label ID="lblwarnage" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="form-floating mb-3 text_staff">
                <asp:TextBox ID="txtstaffpass" runat="server" CssClass="form-control" placeholder="" TextMode="Password" ClientIDMode="Static" />
                <label for="txtstaffpass">Password</label>
                <asp:Label ID="lblwarnpass" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
            </div>
            <div class="row text_staff">
                <div class="col-md-6">
                    <asp:Label ID="lblstaffGen" runat="server" Text="Gender" AssociatedControlID="ddlStaffgen" class="dropdown textintgen mb-3" ClientIDMode="Static"></asp:Label>
                    <asp:DropDownList ID="ddlStaffgen" runat="server" CssClass="selectpicker custom-dropdown" ClientIDMode="Static">
                        <asp:ListItem Selected="True">--Selected Gender--</asp:ListItem>
                        <asp:ListItem>Male</asp:ListItem>
                        <asp:ListItem>Female</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblWarnGen" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
                </div>
                <div class="col-md-6">
                    <asp:Label ID="lblRole" runat="server" Text="Role" AssociatedControlID="DdlRole" class="dropdown textintrole mb-3" ClientIDMode="Static"></asp:Label>
                    <asp:DropDownList ID="DdlRole" runat="server" CssClass="selectpicker custom-dropdown">
                        <asp:ListItem Selected="True">--Selected Role--</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>Staff</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblWarnRole" runat="server" Text="" CssClass="text-danger" ClientIDMode="Static"></asp:Label>
                </div>
            </div>
            <div class="card-footer">
                <asp:Button ID="btnaddstaff" runat="server" Text="Add New Staff" CssClass="btn staffbtn mt-4" OnClick="btnaddstaff_Click" />
            </div>
        </div>
    </div>
</asp:Content>
