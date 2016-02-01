<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="SIS.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Here</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body style=" margin: 0;
    position: absolute;
    top: 50%;
    left: 50%;
    margin-right: -50%;
    transform: translate(-50%, -50%)">
    <div style="width: 567px; margin: 12% 40%;">
        <asp:Label ID="lblInvalidUser" align="center" runat="server"></asp:Label>
        <form id="form1" runat="server" style="align-content: center">
            <div>
                <h1 align="center" style="width: 305px">Login </h1>
                <asp:Label ID="lblUserName" runat="server" Text="User Name" style="text-align:center;" Width="150px"></asp:Label><asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Username cannot be blank"></asp:RequiredFieldValidator>
                <br />
                <asp:Label ID="lblPassword" runat="server" Text="Password" style="text-align:center;" Width="150px"></asp:Label><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Password cannot be blank"></asp:RequiredFieldValidator>
                <br />
                <br />
                <asp:Button ID="btnLogin" runat="server" BorderColor="White" style="text-align:center;" BorderStyle="None" EnableTheming="False" Text="Login" Width="305px" OnClick="btnLogin_Click" />
                 <br /><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ForgetPassword.aspx" Target="_parent" Width="305px">Forget/Update Password</asp:HyperLink>
                <br /><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Register.aspx" Target="_parent" Width="305px">Sign Up</asp:HyperLink>
            </div>
        </form>
    </div>    
</body>
</html>
