<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="HotelManagement.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            display: flex;
            justify-content: center;
            justify-items: center;
            background: linear-gradient(90deg, #181a1e,#373a40,#484c56);
        }

        nav {
            border-bottom: solid rgba(255, 255, 255, 0.05);
        }

        footer {
            border-top: 1px solid rgba(255, 255, 255, 0.125);
        }

        .profile_card {
            height: 400px;
            width: 900px;
            border-radius: 25px;
            backdrop-filter: blur(25px) saturate(0%);
            background-color: rgba(56, 61, 68, 1);
            border-radius: 12px;
            border: 1px solid rgba(255, 255, 255, 0.125);
            color: white;
        }

        .profile_image {
            border-radius: 50%;
            height: 100px;
            width: 100px;
            margin-top: 20px;
            margin-left: 80px;
        }

        .profile_username {
            margin-left: 80px;
            font-size: 20px;
        }

        .nav-pills .nav-link {
            border-radius: 15px;
        }

        .nav-link:active {
            border-radius: 15px;
        }

        .details_panel {
            margin-top: 20px;
        }

        .form-control {
            border-radius: 25px;
        }

        .tab_pass {
            margin-top: 10px;
            width: 500px;
            margin-left: 50px;
        }

            .tab_pass input {
                background-color: transparent !important;
                color: white !important;
            }

                .tab_pass input:hover {
                    background-color: transparent !important;
                    color: white !important;
                }

                .tab_pass input:active {
                    background-color: transparent !important;
                    color: white !important;
                }

            .tab_pass label {
                background-color: transparent;
                padding-left: 30px !important;
                color: white !important;
            }

                .tab_pass label:active {
                    background-color: transparent;
                    padding-left: 30px !important;
                    color: white !important;
                }

                .tab_pass label:hover {
                    background-color: transparent;
                    padding-left: 30px !important;
                    color: white !important;
                }

                .tab_pass label::after {
                    background-color: transparent !important;
                    padding-left: 30px !important;
                    color: white !important;
                }

        .btn_updatepass {
            margin-top: 10px;
            margin-left: 0px;
            width: 300px;
            height: 50px;
            border-radius: 25px;
            border: 1px solid rgba(255, 255, 255, 0.125);
        }

        .col_profile {
            margin-top: 20px;
        }

        .gview_order {
            overflow: auto;
            height: 350px;
            margin-right: 500px;
            width: 630px;
            transform: translateY(-230px);
        }

        .modal_review {
            color: black;
        }

        .profile_use {
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="section_container">
        <div class="card profile_card row">
            <div class="container col-3">
                <div>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/img/Admin.jpg" CssClass="profile_image" />
                </div>
                <div class="col col_profile">
                    <div class="nav nav-pills flex-column">
                        <a class="nav-link active" href="#" data-bs-toggle="pill" data-bs-target="#profile" role="tab">
                            <i class="bi bi-person me-2"></i>
                            User Profile
                        </a>
                        <a class="nav-link" href="#" data-bs-toggle="pill" data-bs-target="#Changepassword" role="tab">
                            <i class="bi bi-gear me-2"></i>
                            Change Password
                        </a>
                        <a class="nav-link" href="#" data-bs-toggle="pill" data-bs-target="#order" role="tab">
                            <i class="bi bi-gear me-2"></i>
                            Order History
                        </a>
                    </div>
                </div>
            </div>
            <div class=" col-9 ">
                <div class="container details_panel">
                    <div class="tab-panel fade tab_pass" id="Changepassword" role="tabpanel">
                        <div class="form-floating mb-4">
                            <asp:TextBox ID="txtoldpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                            <label for="txtoldpass">Old Password</label>
                            <asp:Label ID="lbloldpass" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="row">
                            <div class="form-floating mb-4 col-6">
                                <asp:TextBox ID="txtnewpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                <label for="txtnewpass">New Password</label>
                                <asp:Label ID="lblnewpass" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="form-floating mb-4 col-6">
                                <asp:TextBox ID="txtconpass" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                <label for="txtconpass">Confirm Password</label>
                                <asp:Label ID="lblconpass" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="btnuppass" runat="server" Text="Update Pasword" CssClass="btn btn-primary btn_updatepass" OnClick="btnuppass_Click" />
                        </div>
                    </div>
                    <div class="tab-panel fade collapse profile_use show" id="profile" role="tabpanel">
                        <div class="profile">
                            <div class="form-floating mb-4 col-6">
                                <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" placeholder="New Username"></asp:TextBox>
                                <label for="txtNewUsername">Username</label>
                            </div>
                            <div class="form-floating mb-4 col-6">
                                <asp:TextBox ID="txtNewEmail" runat="server" CssClass="form-control" placeholder="New Email"></asp:TextBox>
                                <label for="txtNewEmail">Email</label>
                            </div>
                            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" CssClass="btn btn-primary" OnClick="btnUpdateProfile_Click" />
                        </div>
                    </div>
                    <div class="tab-panel collapse fade gview_order" id="order" role="tabpanel" aria-labelledby="order-tab">
                        <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvOrderHistory_RowCommand" DataKeyNames="ReservationID" Width="673px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="ReservationID" HeaderText="Reservation ID" />
                                <asp:BoundField DataField="RoomType" HeaderText="Room Type" />
                                <asp:BoundField DataField="CheckIndate" HeaderText="Check In Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="CheckOutdate" HeaderText="Check Out Date" DataFormatString="{0:d}" />
                                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price" DataFormatString="Rm {0}" />
                                <asp:TemplateField HeaderText="Actions">
                                    <ItemTemplate>
                                        <asp:Button ID="btnShowReview" runat="server" Text="Show Review" CommandName="ShowReview" CommandArgument="<%# Container.DataItemIndex %>" />
                                        <button type="button" class="btn btn-primary" onclick="showReviewModal('<%# Eval("RoomType") %>', '<%# Eval("RoomId") %>')">Leave Review</button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </div>
                </div>
                <div class="container col-9 details_panel">
                </div>
            </div>
        </div>
        <div class="modal fade" id="paymentModal" tabindex="-1" aria-labelledby="paymentModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="paymentModalLabel">Payment Information</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p><strong>Payment Method:</strong> <span id="paymentMethod"></span></p>
                        <p><strong>Payment Amount:</strong> <span id="paymentAmount"></span></p>
                        <p><strong>Payment Status:</strong> <span id="paymentStatus"></span></p>
                        <p><strong>Reason:</strong> <span id="paymentReason"></span></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="reviewModal" tabindex="-1" aria-labelledby="reviewModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="reviewModalLabel">Leave a Review</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p><strong>Room Type:</strong> <span id="roomType"></span></p>
                        <div class="form-group">
                            <label for="txtReviewTitle">Title</label>
                            <asp:TextBox ID="txtReviewTitle" runat="server" CssClass="form-control" placeholder="Enter review title"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtReviewComment">Comment</label>
                            <asp:TextBox ID="txtReviewComment" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Enter your review"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <asp:Button ID="btnSubmitReview" runat="server" Text="Submit Review" CssClass="btn btn-primary" OnClick="btnSubmitReview_Click" />

                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hiddenRoomId" runat="server" />

    </section>
    <script>
        function showReviewModal(roomType, roomId) {
            document.getElementById('roomType').textContent = roomType;

            var reviewModal = new bootstrap.Modal(document.getElementById('reviewModal'));
            reviewModal.show();

            document.getElementById('<%= hiddenRoomId.ClientID %>').value = roomId;
        }

        function closeReviewModal() {
            var reviewModal = bootstrap.Modal.getInstance(document.getElementById('reviewModal'));
            reviewModal.hide();
        }
        function showReviewModal(roomType, roomId) {
            document.getElementById('roomType').textContent = roomType;

            var reviewModal = new bootstrap.Modal(document.getElementById('reviewModal'));
            reviewModal.show();

            document.getElementById('<%= hiddenRoomId.ClientID %>').value = roomId;
        }

        function closeReviewModal() {
            var reviewModal = bootstrap.Modal.getInstance(document.getElementById('reviewModal'));
            reviewModal.hide();
        }
        function showPaymentInfo(paymentMethod, paymentAmount, paymentStatus, reason) {
            document.getElementById('paymentMethod').textContent = paymentMethod;
            document.getElementById('paymentAmount').textContent = paymentAmount;
            document.getElementById('paymentStatus').textContent = paymentStatus;
            document.getElementById('paymentReason').textContent = reason;

            var paymentModal = new bootstrap.Modal(document.getElementById('paymentModal'));
            paymentModal.show();
        }

        $(document).ready(function () {
            $('.nav-link').on('click', function () {
                var target = $(this).data('bs-target');
                $('.tab-panel').removeClass('show');
                $(target).addClass('show');
            });
        });
    </script>
</asp:Content>
