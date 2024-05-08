<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="HotelManagement.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            flex-direction: column;
            gap: 20px;
            min-height: 100vh;
        }

            .wrapper h2 {
                margin-top: 20px;
                margin-left: 50px;
                font-size: 50px;
            }

        .right_use {
            margin-top: 80px;
            margin-right: 50px;
        }

        .card_book {
            width: 700px;
            border: solid 2px;
            margin-left: 20px;
            border-radius: 25px;
        }

        .card_book {
            border: solid 2px;
            margin-left: 50px;
            border-radius: 13px;
            width: 700px;
        }

            .card_book img {
                width: 700px;
                border-radius: 13px;
                height: 400px;
                overflow: auto;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between">
        <div>
            <h2>
                <asp:Label ID="lblroomtype" runat="server" Text="Single room"></asp:Label></h2>
            <div class="card card_book">

                <div id="Carousel" class="carousel slide" data-bs-touch="false" data-bs-ride="carousel">
                    <div class="carousel-indicators">
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="0" class="active" aria-label="Slide 1"></button>
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="3" aria-label="Slide 4"></button>
                    </div>
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <asp:Image ID="img1" runat="server" alt="Slide 1" ImageUrl="~/img/Delexu.jpg" />
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100 img-fluid" src="img/carosel-2.png" alt="Slide 2">
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100 img-fluid" src="img/carosel-3.png" alt="Slide 3">
                        </div>
                        <div class="carousel-item">
                            <img class="d-block w-100 img-fluid" src="img/carosel-4.png" alt="Slide 4">
                        </div>
                    </div>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#Carousel" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#Carousel" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>
        </div>
        <div class="right_use">
            <div class="card">
                <asp:Label ID="lblpricepernight" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblroomcapacity" runat="server" Text="Label"></asp:Label>
            </div>
            <div class="card d-flex flex-column">
                <div class="card-body">
                    <asp:TextBox ID="txtcheckindate" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtcheckoutdate" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddlroom" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlroom_SelectedIndexChanged" DataSourceID="Room"></asp:DropDownList>
                    <asp:Button ID="btnbook" runat="server" Text="Booking" OnClick="btnbook_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="d-flex justify-content-between">
        <div class="card">
            test
        </div>
        <asp:Repeater ID="Repeater1" runat="server"></asp:Repeater>
    </div>


</asp:Content>
