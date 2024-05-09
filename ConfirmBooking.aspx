<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="ConfirmBooking.aspx.cs" Async="true" Inherits="HotelManagement.ConfirmBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://js.stripe.com/v3/"></script>

    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .separator {
            border-left: 5px solid #000;
            height: 100%;
            margin-left: 10px;
            margin-right: 10px;
            display: inline-block;
            vertical-align: top;
        }

        .StripeElement {
            height: 50px;
        }

        .card_booking_confirm {
            background-color: #d4d2cd;
        }

            .card_booking_confirm .card-header {
                background-color: transparent;
                border: 0;
                font-size: 40px;
                text-align: center;
            }

        .custom_using_pay {
            width: 400px;
        }

            .custom_using_pay h2 {
                font-weight: 300;
            }

            .custom_using_pay button {
                border: solid 1px;
                width: 300px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class=" d-flex justify-content-center align-content-center">

        <div class="custom_using_pay">
            <h2>COMPLETE BOOKING</h2>
            <h4><strong>PAYMENT INFORMATION</strong></h4>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" CssClass="selectpicker">
                <asp:ListItem Value="CreditCard" Selected="True">Credit Card</asp:ListItem>
                <asp:ListItem Value="PayPal">PayPal</asp:ListItem>
            </asp:DropDownList>

            <asp:Panel ID="pnlCreditCard" runat="server" Visible="True">
                <div class="card-body">
                    <h5 class="card-title">Credit Card Payment</h5>
                    <p class="card-text">Enter your credit card information here.</p>
                    <div>
                        <div id="card-element"></div>
                        <div id="card-errors" class="text-danger" role="alert"></div>
                        <hr />
                        <asp:Button ID="Btnpaystripe" runat="server" Text="Pay" OnClick="Btnpaystripe_Click" />
                        <input type="hidden" id="stripeTokenInput" name="stripeToken" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlPayPal" runat="server" Visible="False">
                <div class="card-body">
                    <h5 class="card-title">PayPal Payment</h5>
                    <p class="card-text">You will be redirected to PayPal to complete the payment.</p>
                    <asp:Label ID="lblpaypal" runat="server" Text=""></asp:Label>
                    <hr />
                    <asp:Button ID="BtnPaypal" runat="server" Text="Button" OnClick="BtnPaypal_Click" CssClass="" />
                </div>
            </asp:Panel>
        </div>
        <div class="separator"></div>
        <div class="card d-flex flex-column card_booking_confirm ">
            <div class="card-body d-flex flex-column">
                <div class="card-header">
                    <h3><i class="fa-solid fa-hotel"></i>Hotel | KNN</h3>
                    <strong>Your booking details</strong>
                    <hr />
                </div>
                <div>
                    <label>
                        Room type:<br />
                    </label>
                    <asp:Label ID="lblroomtype" runat="server" Text="Label"></asp:Label>
                </div>
                <asp:Label ID="lblcustomername" runat="server" Text="Label"></asp:Label><div class="row">
                    <div class="col-6">
                        <asp:Label ID="lblcheckin" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="col-6">
                        <label></label>
                        <asp:Label ID="lblcheckout" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>
                <hr />
                <label>Total Price: RM</label>
                <asp:Label ID="lbltotalprice" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="ReservedRoomNumber" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </div>
    <script>   
        document.addEventListener('DOMContentLoaded', function () {
            var stripe = Stripe('pk_test_51OrP6QJMhsB8qHbvjZwtZTq8vcXf5w3N1u6FsYiRK8o7FWQfrEhvOV4E0LTrhtIwKgxSoreFUskmLdi9gUHB3z2l003HweZzGM');
            var elements = stripe.elements();
            var card = elements.create('card');
            card.mount('#card-element');

            var form = document.getElementById('form1');
            form.addEventListener('submit', function (event) {
                event.preventDefault();

                stripe.createToken(card).then(function (result) {
                    if (result.error) {
                        var errorElement = document.getElementById('card-errors');
                        errorElement.textContent = result.error.message;
                    } else {
                        var tokenInput = document.getElementById('stripeTokenInput');
                        tokenInput.value = result.token.id;
                        __doPostBack('<%=Btnpaystripe.UniqueID%>', '');
                        
                    }
                });
            });
        });
    </script>
</asp:Content>
