<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="ConfirmBooking.aspx.cs" Inherits="HotelManagement.ConfirmBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged">
                <asp:ListItem Value="CreditCard" Selected="True">Credit Card</asp:ListItem>
                <asp:ListItem Value="PayPal">PayPal</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="ms-4">
            <asp:Panel ID="pnlCreditCard" runat="server" Visible="True">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">Credit Card Payment</h5>
                        <p class="card-text">Enter your credit card information here.</p>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPayPal" runat="server" Visible="False">
                <div class="card" style="width: 18rem;">
                    <div class="card-body">
                        <h5 class="card-title">PayPal Payment</h5>
                        <p class="card-text">You will be redirected to PayPal to complete the payment.</p>
                        <asp:Label ID="lblpaypal" runat="server" Text="Label"></asp:Label>

                    </div>
                </div>
            </asp:Panel>
        </div>
        <asp:Button ID="BtnPaypal" runat="server" Text="Button" OnClick="BtnPaypal_Click" />
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
</asp:Content>
