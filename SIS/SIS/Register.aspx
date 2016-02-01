<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Register.aspx.cs" Inherits="SIS.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Yourself</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" style="align-content: center" runat="server">
        <div style="background-color: gray">
                    <asp:Label ID="lblAlreadyAMember" runat="server" Text="Already a member?" Style="float: left;" ForeColor="Black"></asp:Label>
                        <asp:Button ID="btnSignIn" runat="server" Width="150px" Text="Sign in" OnClick="btnSignIn_Click" />
                    </div>
    <h1 style="margin-left: 500px">Register yourself</h1>
        <div style="margin-left: 450px">
            <br />
            <asp:Label ID="Label1" CssClass="csslbl" ForeColor="Black" runat="server" BorderColor="#CC3399" Text="Name" Width="150px"></asp:Label>
            <asp:TextBox ID="txtName" CssClass="csstxt" runat="server" ValidateRequestMode="Disabled"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please enter your name" ValidationGroup="AllValidator"></asp:RequiredFieldValidator>

            <br />
            <br />

            <asp:Label ID="Label2" CssClass="csslbl"  ForeColor="Black" runat="server" Text="User Name" Width="150px"></asp:Label>
            <asp:TextBox ID="txtUserName" CssClass="csstxt" runat="server" OnTextChanged="txtUserName_TextChanged" AutoPostBack="True"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Please select Username" ValidationGroup="AllValidator"></asp:RequiredFieldValidator>

            <asp:Label ID="lblUserExists" runat="server"></asp:Label>

            <br />
            <br />

            <asp:Label ID="Label11" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Email" Width="150px"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="csstxt" runat="server" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter your email" ValidationGroup="AllValidator"></asp:RequiredFieldValidator>
            <br />
            <br />

            <asp:Label ID="Label3" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Password" Width="150px"></asp:Label>
            <asp:TextBox ID="txtPassword" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ValidationGroup="AllValidator"></asp:RequiredFieldValidator>

            <br />
            <br />

            <asp:Label ID="Label4" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Confirm Password" Width="150px"></asp:Label>
            <asp:TextBox ID="txtConfirmPwd" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirmPwd" ErrorMessage="Confirm Password is required" ValidationGroup="AllValidator"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPwd" Display="Dynamic" ErrorMessage="Passwords Must match" ControlToCompare="txtPassword" ValidationGroup="AllValidator"></asp:CompareValidator>

            <br />
            <br />

            <asp:Label ID="Label10" CssClass="csslbl" ForeColor="Black" runat="server" BorderColor="#CC3399" Text="Gender" Width="150px"></asp:Label>
            <asp:RadioButton ID="rbtMale" runat="server" Text="Male" name="Gen" Width="75px" GroupName="Gender" Checked="True" />
            <asp:RadioButton ID="rbtFemale" runat="server" ForeColor="Black" name="Gen" Text="Female" Width="80px" GroupName="Gender" />

            <br />
            <br />

            <asp:Label ID="Label5" CssClass="csslbl"  ForeColor="Black" runat="server" BorderColor="#CC3399" Text="Contact" Width="150px"></asp:Label>
            <asp:TextBox ID="txtContact" CssClass="csstxt" runat="server" TextMode="Number"></asp:TextBox>

            
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label9" runat="server" CssClass="csslbl" ForeColor="Black" Text="Country" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" Width="155px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" CssClass="csslbl" ForeColor="Black" Text="State" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" Width="155px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" CssClass="csslbl"  ForeColor="Black" Text="City" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListCity" runat="server" Width="155px">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />

            <asp:Button ID="btnSubmit" OnClientClick="return validate();" runat="server" Text="Submit" Width="150px" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" Width="150px" OnClick="btnReset_Click" />
        </div>
    </form>
    <script type="text/javascript">
        var Name = document.getElementById('txtName');
        var UserName = document.getElementById('txtUserName');
        var Email = document.getElementById('txtEmail');
        var Password = document.getElementById('txtPassword');
        var CPassword = document.getElementById('txtConfirmPwd');
        var Contact = document.getElementById('txtContact');
        function validate() {

        var check = true;
            if (CPassword.value == "") {
                CPassword.style.border = "1px solid RED";
                CPassword.title = "Confirm Password cannot be blank"
                CPassword.focus();
                check = false;
            } else {
                CPassword.style.border = "1px solid Gray";
                CPassword.title = ""
            }
            if (Password.value != CPassword.value) {
                Password.style.border = "1px solid RED";
                Password.title = "Passwords Do not match"
                Password.focus();
                check = false;
            } else {
                Password.style.border = "1px Gray solid";
                Password.title = ""
            }
            if (Password.value == "") {
                Password.style.border = "1px solid RED";
                Password.title = "Password cannot be blank"
                Password.focus();
                check = false;
            } else {
                Password.style.border = "1px Gray solid";
                Password.title = ""
            }
            if (Email.value == "") {
                Email.style.border = "1px solid RED";
                Email.title = "Email cannot be blank"
                Email.focus();
                check = false;
            } else {
                Email.style.border = "1px Gray solid";
                Email.title = ""
            }
            if (UserName.value == "") {
                UserName.style.border = "1px solid RED";
                UserName.title = "Username cannot be Empty"
                UserName.focus();
                check = false;
            } else {
                UserName.style.border = "1px Gray solid";
                UserName.title = ""
            }
            if (Name.value == "") {
                Name.style.border = "1px solid RED";
                Name.title = "Name cannot be Empty"
                Name.focus();
                check = false;
            } else {
                Name.style.border = "1px Gray solid";
                Name.title = ""
            }
            
            return check;
        }
    </script>
</body>
</html>
