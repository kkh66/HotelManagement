<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="PaymentSuccesfull.aspx.cs" Inherits="HotelManagement.PaymentSuccesfull" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #000;
            color: #fff;
        }

        .wrapper {
            height: 500px;
        }

        .container_pay_succesful {
            max-width: 600px;
            margin: 100px auto;
            background-color: #222;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 0 10px rgba(255, 255, 255, 0.1);
        }

            .container_pay_succesful h1 {
                color: #007bff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container_pay_succesful">
        <h1 class="text-center">Your payment was successful.</h1>
        <div class="alert alert-success alert-dismissible fade show success-message" role="alert">
            <strong>Booking Successful!</strong>
            <br />
            Your hotel room has been booked successfully.  
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
        <div class="text-center">
            <asp:Label ID="lblshowtime" runat="server" Text=""></asp:Label>
            <asp:HyperLink ID="linksucces" NavigateUrl="~/Home.aspx" runat="server" CssClass="btn btn-primary">Back Home</asp:HyperLink>
        </div>
    </div>
</asp:Content>
