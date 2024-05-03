<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HotelManagement.Register" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Style.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/momentjs/latest/moment.min.js"></script>
    <script src="https://challenges.cloudflare.com/turnstile/v0/api.js" async defer></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/css/bootstrap-select.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.14.0-beta3/dist/js/bootstrap-select.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/theme.min.css" integrity="sha512-hbs/7O+vqWZS49DulqH1n2lVtu63t3c3MTAn0oYMINS5aT8eIAbJGDXgLt6IxDHcWyzVTgf9XyzZ9iWyVQ7mCQ==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.13.2/themes/base/jquery-ui.min.css" integrity="sha512-ELV+xyi8IhEApPS/pSj66+Jiw+sOT1Mqkzlh8ExXihe4zfqbWkxPRi8wptXIO9g73FSlhmquFlUOuMSoXz5IRw==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <style>
        .ui-datepicker {
            z-index: 2 !important;
        }

        .select-picker {
            z-index: 3 !important;
        }
    </style>
</head>
<body class="vh-100 body_register">
    <form id="form1" runat="server">
        <<section class="section_container">
    <div class="row justify-content-center">
        <div id="register_all" class="col-12 col-md-5 mt-4">
            <div class="card" style="border-radius: 15px;">
                <div class="card-body page-content">
                    <h3 class="text-center mb-4">Register Your account</h3>
                    <div>
                        <div>
                            <asp:Label ID="lblRegError" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="txtRegUserName" runat="server" CssClass="form-control" placeholder="" />
                            <label for="txtRegUserName">User Name</label>
                            <asp:Label ID="checkname" runat="server" Text="" CssClass="text-danger"></asp:Label>

                        </div>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="txtRegPassword" runat="server" CssClass="form-control" placeholder="" TextMode="Password" />
                            <label for="txtRegPassword">Password</label>
                            <asp:Label ID="checkregpassword" runat="server" Text="" CssClass="text-danger"></asp:Label>

                        </div>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" placeholder="" TextMode="Password" />
                            <label for="txtConfirmPassword">Confirm Password</label>
                            <asp:Label ID="checkregconfpassword" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="txtRegMail" runat="server" CssClass="form-control" placeholder="" />
                            <label for="txtRegMail">Email</label>
                            <asp:Label ID="checkRegmail" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <asp:Label ID="Gender" runat="server" Text="Gender" AssociatedControlID="DdlGender" class="dropdown"></asp:Label>
                            <asp:DropDownList ID="DdlGender" runat="server" CssClass="selectpicker" aria-label="Select Children">
                                <asp:ListItem Selected="True">--Selected Gender--</asp:ListItem>
                                <asp:ListItem>Male</asp:ListItem>
                                <asp:ListItem>Female</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="CheckRegGen" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="form-floating mb-2">
                            <asp:TextBox ID="RegDateofBirth" runat="server" CssClass="form-control" ClientIDMode="Static" placeholder="" />
                            <label for="RegDateofBirth">Date of birth</label>
                            <asp:Label ID="checkRegDob" runat="server" Text="" CssClass="text-danger"></asp:Label>
                        </div>
                        <div class="mb-2">
                            <div class="cf-turnstile" id="cf-turnstile" data-sitekey="0x4AAAAAAAWJ3SUUSyP1g1Ad" data-callback="javascriptCallback" data-theme="auto"></div>
                        </div>
                        <div class="mb-2">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-primary card_btn_use" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</section>
<script type="text/javascript">   
    //calender for age
    $(document).ready(function () {
        $('#RegDateofBirth').datepicker({
            dateFormat: "yy/mm/dd",
            changeYear: true,
            changeMonth: true,
            yearRange: "1900:2006",
        });
        $('#RegDateofBirth').datepicker("setDate", new Date("2000-01-01"));
    });

    //Remove the error message after 2 seconds
    function removeErrorMessage() {
        $('#lblRegError, #checkname, #checkregpassword, #checkregconfpassword, #checkRegmail, #CheckRegGen, #checkRegDob').remove();
    }
    setTimeout(removeErrorMessage, 3000);
</script>
    </form>
</body>
</html>
