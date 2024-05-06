<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="HotelManagement.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="txtcheckindate" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtcheckoutdate" runat="server"></asp:TextBox>
    <asp:DropDownList ID="ddlroom" runat="server"></asp:DropDownList>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Button ID="btnbook" runat="server" Text="Booking" OnClick="btnbook_Click" />
</asp:Content>
