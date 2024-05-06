<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ViewRoom.aspx.cs" Inherits="HotelManagement.Admin.ViewRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function setEditRoomId(roomId) {
            document.getElementById('<%= hfEditRoomId.ClientID %>').value = roomId;
        }

        function setDeleteRoomId(roomId) {
            document.getElementById('<%= hfDeleteRoomId.ClientID %>').value = roomId;
        }
        function showEditModal() {
            var editModal = new bootstrap.Modal(document.getElementById('editModal'));
            editModal.show();
        }
    </script>
    <style>
        body {
            background-color: black;
        }

        .nav_custom {
            background-color: transparent;
        }

        .btn_offcanvas {
            background-color: transparent;
            border: 1px solid white;
            color: white;
        }

            .btn_offcanvas:hover {
                background-color: gray;
                border: 1px solid white;
                color: white;
            }

            .btn_offcanvas:active {
                background-color: gray !important;
                border: 1px solid white;
                color: white;
            }

        footer {
            background-color: transparent;
        }

        .wrapper {
            display: flex;
            min-height: 100vh;
            color: white;
        }

        .card_roomview {
            width: 550px;
            background-color: rgba(255, 255, 255, 0.2);
            backdrop-filter: blur(25px) saturate(200%);
            border: 1px solid rgba(255, 255, 255, 0.125);
            border-radius: 12px;
            color: white;
        }

            .card_roomview .card-body {
                padding-top: 10px;
                padding-bottom: 10px;
            }

            .card_roomview .carousel-item {
                height: 300px !important;
                object-fit: cover !important;
                overflow: hidden;
            }

        .view_room_img {
            height: 300px;
            width: 515px;
            border-radius: 12px !important;
            padding-top: 10px;
            padding-left: 3px;
        }

        .font {
            margin-top: 20px;
            height: 100px;
        }

        .modal_edit_room {
        }

            .modal_edit_room .modal-body {
                width: 400px !important;
                padding-left: 48px;
            }

            .modal_edit_room .form-floating {
                width: 400px;
            }

            .modal_edit_room .form-control {
                background-color: transparent;
                color: white;
            }

            .modal_edit_room label {
                background-color: transparent !important;
                color: white !important;
            }

                .modal_edit_room label:active {
                    background-color: transparent !important;
                    color: white !important;
                }

                .modal_edit_room label:hover {
                    background-color: transparent !important;
                    color: white !important;
                }

                .modal_edit_room label::after {
                    background-color: transparent !important;
                    color: white !important;
                }

            .modal_edit_room input:active {
                background-color: transparent !important;
            }

            .modal_edit_room input:hover {
                background-color: transparent;
            }

            .modal_edit_room .modal-header {
                text-align: center;
                padding-bottom: 10px;
            }

            .modal_edit_room .modal-content {
                backdrop-filter: blur(16px) saturate(180%);
                background-color: rgba(17, 25, 40, 0.75);
                border-radius: 12px;
            }

            .modal_edit_room .modal-footer {
                padding-top: 5px;
                padding-bottom: 5px;
            }

        .btn_editroom {
            background-color: dodgerblue !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex justify-content-center flex-wrap">
        <h1 class="font">Room List</h1>
        <div class="row">
            <asp:Repeater ID="rptRooms" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 mb-4 card_use_room">
                        <div class="card card_roomview">
                            <div class="card-body">
                                <div id="carouselRoom<%# Eval("RoomId") %>" class="carousel slide" data-ride="carousel">
                                    <div id="Carousel<%# Eval("RoomId") %>" class="carousel slide" data-bs-touch="false" data-bs-ride="carousel">
                                        <div class="carousel-indicators">
                                            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="0" class="active" aria-label="Slide 1"></button>
                                            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="1" aria-label="Slide 2"></button>
                                            <button type="button" data-bs-target="#Carousel" data-bs-slide-to="2" aria-label="Slide 3"></button>
                                        </div>
                                        <div class="carousel-inner">
                                            <div class="carousel-item active">
                                                <asp:Image ID="imgroom1" runat="server" ImageUrl="~/img/Delexu.jpg" alt="Slide 1" CssClass="view_room_img" />
                                            </div>
                                            <div class="carousel-item">
                                                <asp:Image ID="imgroom2" runat="server" ImageUrl="~/img/DeluxeBath.jpg" alt="Slide 2" CssClass="view_room_img" />
                                            </div>
                                            <div class="carousel-item">
                                                <asp:Image ID="imgroom3" runat="server" ImageUrl="~/img/DeluxeRoom.jpg" alt="Slide 3" CssClass="view_room_img" />
                                            </div>
                                        </div>
                                        <button class="carousel-control-prev" type="button" data-bs-target="#Carousel<%# Eval("RoomId") %>" data-bs-slide="prev">
                                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                            <span class="visually-hidden">Previous</span>
                                        </button>
                                        <button class="carousel-control-next" type="button" data-bs-target="#Carousel<%# Eval("RoomId") %>" data-bs-slide="next">
                                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                            <span class="visually-hidden">Next</span>
                                        </button>
                                    </div>
                                </div>
                                <h2 class="mt-2"><%# Eval("RoomType") %> (Room left <%# Eval("RoomNumber") %>)</h2>
                                <p class="mt-2 mb-0">Description: <%# Eval("Description") %></p>
                                <p class="mb-0">Capacity: <%# Eval("Capacity") %></p>
                                <div class="d-flex justify-content-between align-items-center">
                                    <div>
                                        <p>Price per Night: Rm <%# Eval("PricePerNight") %></p>
                                    </div>
                                    <div class="custom_view_button_all">
                                        <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" onclick="setDeleteRoomId(<%# Eval("RoomId") %>)">Delete</button>
                                        <asp:Button ID="btnCalledit" runat="server" Text="Edit" CommandArgument='<%# Eval("RoomId") %>' OnClick="btnCalledit_Click" OnClientClick="showEditModal(); return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <!-- Edit the room -->
    <div class="modal fade modal_edit_room" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content d-flex flex-column">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Edit Room</h5>
                </div>
                <div class="modal-body">
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtEditRoomType" runat="server" CssClass="form-control" placeholder="" />
                        <label for="txtEditRoomType">Room Type</label>
                        <asp:Label ID="lblRoomtype" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtCapacity" runat="server" CssClass="form-control" placeholder="" />
                        <label for="txtCapacity">Capacity</label>
                        <asp:Label ID="lblCapacity" runat="server" Text=""></asp:Label>

                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="" />
                        <label for="txtDescription">Description</label>
                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtPricePerNight" runat="server" CssClass="form-control" placeholder="" />
                        <label for="txtPricePerNight">Price Per Night</label>
                        <asp:Label ID="lblPricePerNight" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-floating mb-3">
                        <asp:TextBox ID="txtRoomNumber" runat="server" CssClass="form-control" placeholder="" />
                        <label for="txtRoomNumber">Room Number</label>
                        <asp:Label ID="lblRoomNumber" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:HiddenField ID="hfEditRoomId" runat="server" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-primary btn_editroom" OnClick="btnSaveChanges_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Delete the room -->
    <div class="modal fade custom_viewdeletemodal" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Delete Room</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>
                        Are you sure you want to delete this room?<asp:HiddenField ID="hfDeleteRoomId" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
