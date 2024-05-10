<%@ Page Title="" Language="C#" MasterPageFile="~/Book.Master" AutoEventWireup="true" Async="true" CodeBehind="401page.aspx.cs" Inherits="HotelManagement._401page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row text-center">
            <div class="col-md-12">
                <div class="error-template">
                    <div class="row">
                        <h1><i class="fa-solid fa-ban fa-2xl"></i>Oops!</h1>
                    </div>
                    <br />
                    <h2>401 Unauthorized</h2>
                    <br />
                    <div class="error-details">
                        Please login to access this page.
                    </div>
                    <br />
                    <div class="error-actions">
                        <a href="Home.aspx" class="btn btn-primary btn_error"><span class="glyphicon glyphicon-home"></span>
                            Take Me Home </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
