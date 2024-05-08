<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="PaymentFail.aspx.cs" Inherits="HotelManagement.PaymentFail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #000;
            color: #fff;
        }

        .wrapper {
            min-height: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container_payfail">
        <h1 class="text-center">Payment Failed</h1>
        <p class="text-center">We're sorry, but there was an issue processing your payment.</p>
        <p class="text-center">Please try again or contact customer support.</p>
        <div class="text-center">
            <a href="Home.aspx" class="btn">Return to Home</a>
        </div>
    </div>
</asp:Content>
