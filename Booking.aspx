<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="HotelManagement.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            min-height: 100vh;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblroomtype" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblroomcapacity" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblpricepernight" runat="server" Text="Label"></asp:Label>
    <asp:TextBox ID="txtcheckindate" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtcheckoutdate" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlroom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlroom_SelectedIndexChanged"></asp:DropDownList>
    <asp:Button ID="btnbook" runat="server" Text="Booking" OnClick="btnbook_Click" />
</asp:Content>
