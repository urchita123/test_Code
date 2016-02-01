<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterWithVaLIDATION.aspx.cs" Inherits="SIS.RegisterWithVaLIDATION" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Yourself</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <h1 style="margin-left: 500px">Register yourself</h1>
    <form id="form1" style="align-content:center" runat="server">
    <div style="margin-left: 450px" >
         <br />
        <asp:Label ID="Label1" CssClass="csslbl" runat="server"  Text="Name" Width="150px"></asp:Label>
        <asp:TextBox ID="txtName" CssClass="csstxt" runat="server" ValidateRequestMode="Disabled"></asp:TextBox>

         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please enter your name" ValidationGroup="AllValid"></asp:RequiredFieldValidator>

        <br /> <br />

        <asp:Label ID="Label2" CssClass="csslbl"  runat="server" Text="User Name" Width="150px"></asp:Label>
        <asp:TextBox ID="txtUserName" CssClass="csstxt" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Please select Username" ValidationGroup="AllValid"></asp:RequiredFieldValidator>
         <br />
         <br />

        <asp:Label ID="Label11" CssClass="csslbl"  runat="server" Text="Email" Width="150px"></asp:Label>
        <asp:TextBox ID="txtEmail" CssClass="csstxt" runat="server" TextMode="Email"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please Enter your email" ValidationGroup="AllValid"></asp:RequiredFieldValidator>
        <br /> <br />

        <asp:Label ID="Label3" CssClass="csslbl" runat="server" Text="Password" Width="150px"></asp:Label>
        <asp:TextBox ID="txtPassword" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>

         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ValidationGroup="AllValid"></asp:RequiredFieldValidator>

        <br /> <br />

        <asp:Label ID="Label4" CssClass="csslbl" runat="server" Text="Confirm Password" Width="150px"></asp:Label>
        <asp:TextBox ID="txtConfirmPwd" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>

         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirmPwd" ErrorMessage="Confirm Password is required" ValidationGroup="AllValid"></asp:RequiredFieldValidator>
         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPwd" Display="Dynamic" ErrorMessage="Passwords Must match" ControlToCompare="txtPassword" ValidationGroup="AllValid"></asp:CompareValidator>

        <br /> <br />
    
        <asp:Label ID="Label10" CssClass="csslbl" runat="server" BorderColor="#CC3399" Text="Gender" Width="150px"></asp:Label>
        <asp:RadioButton ID="rbtMale" runat="server" Text="Male" Width="75px" GroupName="Gender" Checked="True" />
        <asp:RadioButton ID="rbtFemale" runat="server" ForeColor="Black" Text="Female" Width="80px" GroupName="Gender" />

        <br /> <br />
    
        <asp:Label ID="Label5" CssClass="csslbl" runat="server" BorderColor="#CC3399" Text="Contact" Width="150px"></asp:Label>
        <asp:TextBox ID="txtContact" CssClass="csstxt" runat="server" TextMode="Phone"></asp:TextBox>
 
         <br />

                 <asp:Label ID="Label9" runat="server" CssClass="csslbl" Text="Country" Width="150px"></asp:Label>
                 <asp:DropDownList ID="ddlDropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" width="155px">
                 </asp:DropDownList>
                 <br />
                 <br />
                 <asp:Label ID="Label7" runat="server" CssClass="csslbl" Text="State" Width="150px"></asp:Label>
                 <asp:DropDownList ID="ddlDropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" width="155px">
                 </asp:DropDownList>
                 <br />
                 <br />
                 <asp:Label ID="Label8" runat="server" CssClass="csslbl" Text="City" Width="150px"></asp:Label>
                 <asp:DropDownList ID="ddlDropDownListCity" runat="server" width="155px">
                 </asp:DropDownList>

        
        <br /> <br />

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="150px" OnClick="btnSubmit_Click" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" Width="150px" OnClick="btnReset_Click" />
    </div>
    </form>
</body>
</html>
