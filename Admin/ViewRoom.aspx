<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ViewRoom.aspx.cs" Inherits="HotelManagement.Admin.ViewRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function setEditRoomId(roomId) {
            document.getElementById('<%= hfEditRoomId.ClientID %>').value = roomId;
        }

        function setDeleteRoomId(roomId) {
            document.getElementById('<%= hfDeleteRoomId.ClientID %>').value = roomId;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Room List</h1>
        <asp:Repeater ID="rptRooms" runat="server">
            <ItemTemplate>
                <div class="row">
                    <div class="col-md-6">

                        <div class="card mb-3">
                            <div class="card-header">
                                <%# Eval("RoomType") %> (Room <%# Eval("RoomNumber") %>)                     
                            </div>
                            <div class="card-body">
                                <div id="carouselRoom<%# Eval("RoomId") %>" class="carousel slide" data-ride="carousel">
                                    <!-- Carousel content -->
                                </div>
                                <p class="mt-3"><%# Eval("Description") %></p>
                                <p>Capacity: <%# Eval("Capacity") %></p>
                                <p>Price per Night: <%# Eval("PricePerNight", "{0:C}") %></p>
                                <div class="row">
                                    <div class="col-md-6">
                                        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editModal" onclick="setEditRoomId(<%# Eval("RoomId") %>)">Edit</button>
                                        <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" onclick="setDeleteRoomId(<%# Eval("RoomId") %>)">Delete</button>
                                    </div>
                                    <div class="col-md-6">
                                        <!-- Rectangle content -->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Edit Modal -->
        <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editModalLabel">Edit Room</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <!-- Add your ASP.NET edit form controls here -->
                        <asp:HiddenField ID="hfEditRoomId" runat="server" />
                        <!-- Other form controls -->
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCloseEdit" runat="server" Text="Close" CssClass="btn btn-secondary" data-dismiss="modal" />
                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />
                    </div>
                </div>
            </div>
        </div>

        <!-- Delete Modal -->
        <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Delete Room</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Are you sure you want to delete this room?
                           
                        <asp:HiddenField ID="hfDeleteRoomId" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="btn btn-secondary" data-dismiss="modal" />
                        <asp:Button ID="btnConfirmDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnConfirmDelete_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
