<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetForgetPassword.aspx.cs" Inherits="SIS.ResetForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="center">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
                            </th>
                            <th>
                                <asp:TextBox ID="txtUserName" runat="server" AutoPostBack="True" OnTextChanged="txtUserName_TextChanged1"></asp:TextBox>
                            </th>
                            <th>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="UserNane cannot be blank"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblUserExists" runat="server"></asp:Label>
                            </th>
                            <tr>
                                <th colspan="2">

                                    <asp:Button ID="btnSubmit" runat="server" Text="Send Reset Link" OnClick="btnSubmit_Click" />
                                </th>
                                <th></th>
                            </tr>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
