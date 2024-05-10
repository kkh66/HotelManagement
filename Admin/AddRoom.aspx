<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddRoom.aspx.cs" Inherits="HotelManagement.Admin.AddRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            min-height: 100vh;
        }

        .card_addroom {
            margin-top: 50px;
            margin-bottom: 50px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card card_addroom">
        <div class="card-body">
            <h3>Add Room</h3>
            <asp:Label ID="lbltext" runat="server" Text="" CssClass="text-danger"></asp:Label>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtroomid" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtroomid">Room ID</label>
                <asp:Label ID="Label1" runat="server" Text="" CssClass="text-danger"></asp:Label>
            </div>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtroomtype" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtroomtype">Room type</label>
                <asp:Label ID="Label2" runat="server" Text="" CssClass="text-danger"></asp:Label>

            </div>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtcapacity" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtcapacity">Capacity</label>
                <asp:Label ID="Label3" runat="server" Text="" CssClass="text-danger"></asp:Label>

            </div>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtdescription" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtdescription">Description</label>
                <asp:Label ID="Label4" runat="server" Text="" CssClass="text-danger"></asp:Label>

            </div>
            <div class="form-floating mb-3">
                <asp:TextBox ID="txtPricepernight" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                <label for="txtPricepernight">Price Per Night</label>
                <asp:Label ID="Label5" runat="server" Text="" CssClass="text-danger"></asp:Label>

            </div>
            <div class="mb-3">
                Room Image:
                <asp:FileUpload ID="fileRoomImage" runat="server" />
            </div>
            <div class="mb-3">
                Room Environment:
                <asp:FileUpload ID="fileRoomEnvironment" runat="server" />
            </div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
