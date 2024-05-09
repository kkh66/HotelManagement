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
                margin-left: 100px;
                font-size: 50px;
            }

        .right_use {
            margin-top: 80px;
            margin-right: 50px;
        }

        .card_book {
            border: solid 1px;
            margin-left: 100px;
            border-radius: 25px;
            width: 800px;
            height: 450px;
        }

            .card_book .carousel-item {
                padding-top: 10px;
                padding-left: 10px;
                border-radius: 25px;
                padding-right: 10px;
                padding-bottom: 10px;
            }

            .card_book img {
                border-radius: 25px;
                height: 430px;
                overflow: auto;
            }

        .card_price {
            border: 0;
            margin-left: -190px;
            margin-right: 80px;
        }

            .card_price .card-body {
                margin-left: 20px;
                text-align: center;
                height: 50px;
                width: 300px;
                padding-top: 5px;
                border: solid 1px;
                border-radius: 25px;
            }

        .card_capacity {
            border: 0;
            margin-left: -190px;
            margin-right: 80px;
        }

            .card_capacity .card-body {
                margin-left: 20px;
                text-align: center;
                height: 50px;
                width: 300px;
                padding-top: 5px;
                border: solid 1px;
                border-radius: 25px;
            }

        .card_checkinout {
            border: 0;
            margin-left: -170px;
            margin-right: 120px;
        }

            .card_checkinout .card-body {
                border: solid 1px;
                border-radius: 12px;
            }

        .btn_bok_room {
            margin-left: 50px;
            width: 200px;
            border-radius: 10px;
        }

        .card_viewreview {
            width: 360px;
            height: 200px;
            overflow: auto;
            margin-right: 160px;
            margin-bottom:50px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="d-flex justify-content-between">
        <div>
            <h2 class="mb-2">
                <asp:Label ID="lblroomtype" runat="server" Text="Single room"></asp:Label></h2>
            <div class="card card_book mt-2">

                <div id="Carousel" class="carousel slide" data-bs-touch="false" data-bs-ride="carousel">
                    <div class="carousel-indicators">
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="0" class="active" aria-label="Slide 1"></button>
                        <button type="button" data-bs-target="#Carousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    </div>
                    <div class="carousel-inner">
                        <div class="carousel-item active">
                            <asp:Image ID="img1" runat="server" alt="Slide 1" ImageUrl="~/img/Delexu.jpg" />
                        </div>
                        <div class="carousel-item">
                            <asp:Image ID="img2" runat="server" alt="Slide 2" ImageUrl="~/img/DeluxeBath.jpg" />
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
        <div class="right_use  d-flex flex-column">
            <div class="card card_price">
                <div class="card-body mb-3">
                    <h3>
                        <asp:Label ID="lblpricepernight" runat="server" Text="RM100"></asp:Label>/Per night</h3>
                </div>
            </div>
            <div class="card card_capacity mb-3">
                <div class="card-body">
                    <h3>Max Capacity:
                <asp:Label ID="lblroomcapacity" runat="server" Text="5"></asp:Label></h3>
                </div>
            </div>
            <div class="card card_checkinout">
                <div class="card-body">
                    <h3>Book Your Appointment</h3>
                    <div class="form-floating mt-2 mb-3">
                        <asp:TextBox ID="txtcheckindate" runat="server" placeholder="" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <label for="txtcheckindate">Check in Date</label>
                        <asp:Label ID="lblcheckin" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtcheckoutdate" runat="server" placeholder="" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                        <label for="txtcheckoutdate">Check out Date</label>
                        <asp:Label ID="lblcheckout" runat="server" Text="" CssClass="text-danger"></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:Label ID="lblroom" runat="server" Text="Room Type :" class="dropdown"></asp:Label>
                        <asp:DropDownList ID="ddlroom" runat="server" AutoPostBack="True" CssClass="selectpicker" OnSelectedIndexChanged="ddlroom_SelectedIndexChanged" DataSourceID="Room" DataTextField="RoomType" DataValueField="RoomId"></asp:DropDownList>
                        <asp:SqlDataSource ID="Room" runat="server" ConnectionString="<%$ ConnectionStrings:Room1 %>" ProviderName="<%$ ConnectionStrings:Room1.ProviderName %>" SelectCommand="SELECT [RoomId], [RoomType] FROM [Room]"></asp:SqlDataSource>
                    </div>
                    <asp:Button ID="btnbook" runat="server" Text="Booking" OnClick="btnbook_Click" CssClass="btn btn-primary btn_bok_room" />
                </div>
            </div>
        </div>
    </div>
    <div class="d-flex justify-content-between">
        <div class="card">
            test
        </div>
        <div class="card card_viewreview">
            <asp:Repeater ID="rptReviews" runat="server">
                <HeaderTemplate>
                    <div class="">
                </HeaderTemplate>
                <ItemTemplate>

                    <p>Customer: <%# Eval("Username") %></p>
                    <h3>review title:<%# Eval("Title") %></h3>
                    <p>review <%# Eval("Comment") %></p>
                    <hr />
                </ItemTemplate>
                <FooterTemplate></div></FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
