<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ViewStaff.aspx.cs" Inherits="HotelManagement.Admin.ViewStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .wrapper {
            min-height: 100vh;
            background-color: black;
        }

        .wrapper {
            min-height: 100vh;
            background-color: black;
        }

        /* Style the entire GridView */
        .table_view_staff {
            background-image: linear-gradient(135deg, #3B2667 10%, #BC78EC 100%) !important;
            border-radius: 12px; /* Add rounded corners */
            overflow: hidden; /* Ensure content doesn't overflow rounded corners */
        }

            /* Style GridView headers */
            .table_view_staff th {
                background-color: transparent;
                color: white;
                font-weight: bold;
            }

            /* Style GridView rows */
            .table_view_staff tr {
                background-color: transparent;
            }

                /* Style selected GridView rows */
                .table_view_staff tr.selected {
                    background-color: #3B2667;
                    color: white;
                }

            /* Style GridView cells */
            .table_view_staff td {
                padding: 5px;
            }

            /* Style GridView footer */
            .table_view_staff tfoot {
                background-color: white;
            }

            /* Style GridView pager */
            .table_view_staff .pager {
                color: #3B2667;
                text-align: center;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container view_staff">
        <h2>Staff List</h2>
        <asp:GridView ID="gvStaff" runat="server" AutoGenerateColumns="False" CssClass="table table_view_staff" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Role" HeaderText="Role" />
                <asp:BoundField DataField="Age" HeaderText="Age" />
                <asp:BoundField DataField="Gender" HeaderText="Gender" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#editModal" onclick="setEditEmpID('<%# Eval("EmpID") %>')">Edit</button>
                        <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal" onclick="setDeleteEmpID('<%# Eval("EmpID") %>')">Delete</button>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FFF1D4" />
            <SortedAscendingHeaderStyle BackColor="#B95C30" />
            <SortedDescendingCellStyle BackColor="#F1E5CE" />
            <SortedDescendingHeaderStyle BackColor="#93451F" />
        </asp:GridView>
    </div>
    <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="editModalLabel">Edit Staff</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfEditEmpID" runat="server" />
                    <div class="form-group">
                        <label for="editUsername">Username</label>
                        <asp:TextBox ID="txtEditUsername" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="editName">Name</label>
                        <asp:TextBox ID="txtEditName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="editRole">Role</label>
                        <asp:TextBox ID="txtEditRole" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="editAge">Age</label>
                        <asp:TextBox ID="txtEditAge" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="editGender">Gender</label>
                        <asp:TextBox ID="txtEditGender" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="editPassword">New Password</label>
                        <asp:TextBox ID="txtEditPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Delete Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="deleteModalLabel">Delete Staff</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:HiddenField ID="hfDeleteEmpID" runat="server" />
                    Are you sure you want to delete this staff member?
                   
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDeleteConfirm" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDeleteConfirm_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
