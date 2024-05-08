<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="ConfirmBooking.aspx.cs" Inherits="HotelManagement.ConfirmBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .separator {
            border-left: 4px solid #000;
            height: 100%;
            margin-left: 10px;
            margin-right: 10px;
            display: inline-block;
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" d-flex justify-content-center align-content-center">
        <div>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged">
                <asp:ListItem Value="CreditCard" Selected="True">Credit Card</asp:ListItem>
                <asp:ListItem Value="PayPal">PayPal</asp:ListItem>
            </asp:DropDownList>
            <asp:Panel ID="pnlCreditCard" runat="server" Visible="True">
                <div class="card-body">
                    <h5 class="card-title">Credit Card Payment</h5>
                    <p class="card-text">Enter your credit card information here.</p>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPayPal" runat="server" Visible="False">

                <div class="card-body">
                    <h5 class="card-title">PayPal Payment</h5>
                    <p class="card-text">You will be redirected to PayPal to complete the payment.</p>
                    <asp:Label ID="lblpaypal" runat="server" Text=""></asp:Label>
                    <asp:Button ID="BtnPaypal" runat="server" Text="Button" OnClick="BtnPaypal_Click" CssClass="" />
                </div>
            </asp:Panel>
        </div>
        <div class="separator"></div>
        <div class="card d-flex flex-column">
            <div class="card-body">
                <h2 class="card-header">Your booking details</h2>
                <asp:Label ID="lblroomtype" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblcustomername" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblcheckin" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblcheckout" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lbltotalprice" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="ReservedRoomNumber" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
