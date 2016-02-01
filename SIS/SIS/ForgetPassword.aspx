<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPassword.aspx.cs" Inherits="SIS.ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <h1 align="center">Reset or Update your Password</h1>
    <form id="form1" runat="server">
        <div align="center">
            <table>
                <tr>
                    <th><asp:Label ID="lblUsername" runat="server" Text="Username" Width="150px"></asp:Label></th>
                    <th><asp:TextBox ID="txtUserName" runat="server" Width="150px" AutoPostBack="True" OnTextChanged="txtUserName_TextChanged"></asp:TextBox></th>
                    <th><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Username cannot be blank"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblUserExists" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <th><asp:Label ID="lblPassword" runat="server" Text="Password" Width="150px"></asp:Label></th>
                    <th><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox></th>
                    <th><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Password cannot be blank"></asp:RequiredFieldValidator></th>
                </tr>
                <tr>
                    <th><asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" Width="150px"></asp:Label></th>
                    <th><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox></th>
                    <th><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Please Confirm password"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic" ErrorMessage="Passwords must match"></asp:CompareValidator></th>
                </tr>
                <tr>
                    <th><asp:Button ID="btnUpdate" runat="server" Text="Update" Width="150px" OnClick="btnUpdate_Click" /></th>
                    <th colspan="2"><asp:Label ID="lblUserPasswordUpdated" runat="server"></asp:Label></th>
                </tr>
                <tr>
                    <asp:HyperLink ID="hlLoginPage" runat="server" NavigateUrl="~/LoginPage.aspx">Login</asp:HyperLink>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
