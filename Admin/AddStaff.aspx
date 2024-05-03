<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="HotelManagement.Admin.AddStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display:flex;
            justify-content: center;
        }

        .card_staff {
            width: 500px;
            height:600px;
            top:50px;
            border-radius:35px;
        }
        .card_staff h3{
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="card card_staff">
            <div class="card-body">
                <h3 class="card-title">Add Staff</h3>

            </div>
        </div>

</asp:Content>
