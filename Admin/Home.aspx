<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" EnableEventValidation="true" CodeBehind="Home.aspx.cs" Inherits="HotelManagement.Admin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            min-height: 100vh;
            display: flex;
            justify-content: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Reservation List</h1>
        <asp:GridView ID="gvReservationList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="ReservationID">
            <Columns>
                <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="CheckOutDate" HeaderText="Check-Out Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="RM {0:}" />
                <asp:TemplateField HeaderText="Check-In/Out">
                    <ItemTemplate>
                        <asp:Button ID="btnCheckIn" runat="server" CssClass='<%# Eval("Checkinout", "{0}") == "0" ? "btn btn-primary" : "btn disabled btn-danger" %>' Text="Check-In" OnClick="btnCheckIn_Click" />
                        <asp:Button ID="btnCheckOut" runat="server" CssClass='<%# Eval("Checkinout", "{0}") == "1" ? "btn btn-primary" : "btn disabled btn-danger" %>' Text="Check-Out" OnClick="btnCheckOut_Click" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>
