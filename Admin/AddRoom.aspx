<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="HotelManagement.Admin.AddRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper{
            display: flex;
            justify-content: center;
            min-height: 100vh;
        }
        .card_addroom{

        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card_addroom">
        <div class="card-body">
            <h3>Add Room</h3>
        </div>
    </div>
</asp:Content>
