<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HotelManagement.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .carousel-inner img {
            height: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Carousel" class="carousel slide" data-bs-touch="false" data-bs-ride="carousel">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="0" class="active" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="3" aria-label="Slide 4"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img class="d-block w-100 img-fluid" src="img/carosel-1.png" alt="Slide 1">
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
        <button class="carousel-control-prev" type="button" data-bs-target="#Carousel" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#Carousel" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>

    <section class="about" id="about">
        <div class="section_container about_container">
            <div class="about_grid">
                <div class="about_image">
                    <img src="img/about-1.jpg" alt="about" />
                </div>
                <div class="about_card">
                    <span><i class="fa-solid fa-people-group logo_use"></i></span>
                    <h4>Nice Environment</h4>
                    <p>
                        We are proud to be environmentally conscious and socially responsible. 
                        Through sustainable practices and community engagement initiatives, 
                        we contribute to a better future for generations to come.
                        Unlocking Excellence And Ensures Your Perfect Stay.
                    </p>
                </div>
                <div class="about_image">
                    <img src="img/about-2.jpg" alt="about" />
                </div>
                <div class="about_card">
                    <span><i class="fa-solid fa-calendar logo_use"></i></span>
                    <h4>Luxury Room</h4>
                    <p>Experience Unrivaled Luxury at Our Exquisite Luxury Rooms</p>
                </div>
            </div>
            <div class="about_content">
                <p class="section_subheader">ABOUT US</p>
                <h2 class="section_header">Discover Our Underground</h2>
                <p class="section_description">
                    Welcome to a hidden realm of extraordinary accommodations where
            luxury, comfort, and adventure converge. Our underground hotels
            offer an unparalleled escape from the ordinary, inviting you to
            explore a subterranean world of wonders.
                </p>
                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-primary card_btn_use" NavigateUrl="~/BookingPage.aspx">Book Now</asp:HyperLink>
            </div>
        </div>
    </section>

    <section class="room_container" id="room">
        <p class="section_subheader">ROOMS</p>
        <h2 class="section_header">Our Room Type</h2>
        <div class="room_grid">
            <div class="room_card">
                <img src="img/Delexu.jpg" alt="room" />
                <div class="room_card_details">
                    <div>
                        <h4>Deluxe Suite</h4>
                        <p>Well-appointed rooms designed for guests who desire a more.</p>
                    </div>
                    <a href="BookingPage.aspx" class="btn btn-primary" style="background-color: #44484f; color: azure;">Rm399<span>/night</span></a>
                </div>
            </div>
            <div class="room_card">
                <img src="img/famaily.jpg" alt="room" />
                <div class="room_card_details">
                    <div>
                        <h4>Family Suite</h4>
                        <p>Consist of multiple rooms and a common living area.</p>
                    </div>
                    <a href="BookingPage.aspx" class="btn btn-primary" style="background-color: #44484f; color: azure;">Rm599<span>/night</span></a>
                </div>
            </div>
            <div class="room__card">
                <img src="img/Luxurysuite.jpg" alt="room" />
                <div class="room_card_details">
                    <div>
                        <h4>Luxury Penthouse</h4>
                        <p>
                            Top-tier accommodations usually on the highest floors of a
        hotel.
                        </p>
                    </div>
                    <a href="BookingPage.aspx" class="btn btn-primary" style="background-color: #44484f; color: azure;">Rm799<span>/night</span></a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
