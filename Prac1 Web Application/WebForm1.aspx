<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label5" runat="server" Text="INFS3204 Prac1 Freddy"></asp:Label>
        <div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Calculate" OnClick="Button1_Click" />
        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
             <asp:ListItem Text="+" Value="+" />
             <asp:ListItem Text="-" Value="-" />
             <asp:ListItem Text="*" Value="*" />
             <asp:ListItem Text="/" Value="/" />
        </asp:DropDownList>
        <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="= Base10: "></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server" text=" " ></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="= Base2: "></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server" Text=" "></asp:TextBox>
        <p>
            <asp:Button ID="Button2" runat="server" Text="Count" OnClick="Button2_Click" />
        </p>
        <asp:Label ID="Label3" runat="server" Text="Number of 0: "></asp:Label>
        <asp:TextBox ID="TextBox5" runat="server" Text=" "></asp:TextBox>
        <p>
            <asp:Label ID="Label4" runat="server" Text="Number of 1: "></asp:Label>
            <asp:TextBox ID="TextBox6" runat="server" Text=" "></asp:TextBox>
        </p>
    </form>
</body>
</html>
