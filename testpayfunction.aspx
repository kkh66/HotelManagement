<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testpayfunction.aspx.cs" Inherits="HotelManagement.testpayfunction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://js.stripe.com/v3/"></script>
    <script>
        var stripe = Stripe('pk_test_51OrP6QJMhsB8qHbvjZwtZTq8vcXf5w3N1u6FsYiRK8o7FWQfrEhvOV4E0LTrhtIwKgxSoreFUskmLdi9gUHB3z2l003HweZzGM');
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtCardNumber" runat="server" PlaceHolder="Card Number"></asp:TextBox>
            <asp:TextBox ID="txtExpiryMonth" runat="server" PlaceHolder="Expiry Month"></asp:TextBox>
            <asp:TextBox ID="txtExpiryYear" runat="server" PlaceHolder="Expiry Year"></asp:TextBox>
            <asp:TextBox ID="txtCvc" runat="server" PlaceHolder="CVC"></asp:TextBox>
            <asp:Button ID="btnPay" runat="server" Text="Pay" OnClick="btnPay_Click" />
        </div>
    </form>
</body>
</html>
