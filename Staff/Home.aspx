<%@ Page Title="" Language="C#" MasterPageFile="~/Staff/Staff.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HotelManagement.Staff.Home" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Reservation List</h1>
        <asp:GridView ID="gvReservationList" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                <asp:BoundField DataField="CheckInDate" HeaderText="Check-In Date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="CheckOutDate" HeaderText="Check-Out Date" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="" />
                <asp:BoundField DataField="ReservedRoomNumber" HeaderText="Reserved Room Number" />
                <asp:BoundField DataField="Pax" HeaderText="Pax" />
                <asp:TemplateField HeaderText="Check-In/Out">
                    <ItemTemplate>
                        <asp:Button ID="btnCheckInOut" runat="server" CssClass="btn btn-primary" Text="Check-In" OnClick="btnCheckInOut_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
