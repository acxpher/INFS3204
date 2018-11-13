<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="MVC3.Purchase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label7" runat="server" Text="Purchase Book"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Total Budget "></asp:Label>
        &nbsp;
        <asp:TextBox ID="Buget" runat="server"></asp:TextBox>
        <br />
        <br />

        <asp:Label ID="Label9" runat="server" Text="Book Number  "></asp:Label>
        <asp:TextBox ID="morebook" runat="server"></asp:TextBox>
        <asp:Label ID="Label10" runat="server" Text="Amount "></asp:Label>
        <asp:TextBox ID="moreamount" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button4" runat="server" Text="More" OnClick="MORE_Click" />
        <br />
        <asp:PlaceHolder ID="More" runat="server"></asp:PlaceHolder>
        <br />

        <asp:Button ID="Button5" runat="server" Text="Purchase" OnClick="P_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>


        <br />
       
    </div>
    </form>
</body>
</html>
