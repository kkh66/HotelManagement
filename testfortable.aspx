<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testfortable.aspx.cs" Inherits="HotelManagement.testfortable" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .edit-button-style {
            background-color: #3A9B70;
            color: #fff;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            padding: 8px 12px;
            transition: background-color 0.3s ease;
        }

            .edit-button-style:hover {
                background-color: #4CAF50;
            }

        .titleHeading {
            background-color: white;
            padding: 10px 0px 10px 10px;
            box-shadow: 0 0 5px 0.5px #D3D3D3;
            width: 95%;
            display: flex;
            border-radius: 3px;
            align-items: center;
        }

            .titleHeading h2 {
                margin-right: 20px;
            }

            .titleHeading .addNewStaffLink {
                border: 1px solid grey;
                padding: 5px 10px;
                font-size: 14px;
                border-radius: 3px;
                background-color: #45ffc1;
                text-decoration: none;
                color: black;
                cursor: pointer;
                transition: all 0.3s ease;
                border: none;
                margin-right: 20px;
            }

                .titleHeading .addNewStaffLink:hover {
                    transform: translateY(-5px);
                }

        .mainContent {
            background-color: white;
            padding-top: 10px;
            margin-top: 20px;
            box-shadow: 0 0 5px 0.5px #D3D3D3;
            padding-bottom: 50px;
            width: 95%;
            border-radius: 3px;
        }

        .row1 {
            width: 100%;
        }

        tr, td {
            padding: 12px 15px;
            text-align: center;
        }

        /Staff Table/

        .row1 h2 {
            padding-top: 10px;
            padding-left: 10px;
        }

        .staffTable {
            border-collapse: collapse;
            margin: 25px auto;
            font-size: 0.9em;
            font-family: sans-serif;
            min-width: 400px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
            border: none;
            width: 90%;
        }

            .staffTable tr {
                border-bottom: 1px solid #dddddd;
            }

            .staffTable th, .staffTable tr, .staffTable td {
                padding: 12px 15px;
                border: none;
            }

            .staffTable tbody tr:first-child {
                background-color: #ffc145;
                color: black;
                text-align: center;
                border: none;
            }


            .staffTable tr:last-of-type {
                border-bottom: 2px solid #ffc145;
            }



        .sessionLabelFailed {
            color: red;
            font-weight: bold;
            font-size: 14px;
        }

        .sessionLabelSuccess {
            color: green;
            font-weight: bold;
            font-size: 14px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="titleHeading">
            <h2>Admin</h2>
            <asp:HyperLink ID="HyperLink1" CssClass="addNewStaffLink" runat="server" NavigateUrl="~/admin/addAdmin.aspx">Add New</asp:HyperLink>

        </div>
        <div class="mainContent">
            <div class="row1">

                <table id="GridView1" class="staffTable" style="width: 80%;">
                    <tr>
                        <td>ID</td>
                        <td>Name</td>
                        <td>E-mail</td>
                        <td>Phone Number</td>
                        <td>Action</td>
                    </tr>
                    <tr>
                        <td>2300</td>
                        <td>Tey Junlin</td>
                        <td>teyjunlin@gmail.com</td>
                        <td>018-3130911</td>
                        <td>
                            <asp:Button class="edit-button-style" runat="server" Text="Edit" OnClick="EditBtn_Click" /></td>

                    </tr>
                    <tr>
                        <td>2300</td>
                        <td>Tey Junlin</td>
                        <td>teyjunlin@gmail.com</td>
                        <td>018-3130911</td>
                        <td>
                            <asp:Button class="edit-button-style" runat="server" Text="Edit" OnClick="EditBtn_Click" /></td>

                    </tr>
                    <tr>
                        <td>2300</td>
                        <td>Tey Junlin</td>
                        <td>teyjunlin@gmail.com</td>
                        <td>018-3130911</td>
                        <td>
                            <asp:Button class="edit-button-style" runat="server" Text="Edit" OnClick="EditBtn_Click" /></td>

                    </tr>
                    <tr>
                        <td>2300</td>
                        <td>Tey Junlin</td>
                        <td>teyjunlin@gmail.com</td>
                        <td>018-3130911</td>
                        <td>
                            <asp:Button class="edit-button-style" runat="server" Text="Edit" OnClick="EditBtn_Click" /></td>

                    </tr>

                </table>

                <br />

            </div>
        </div>


    </form>
</body>
</html>
