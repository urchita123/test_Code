<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ButtonClicks.aspx.cs" Inherits="LearningJQuery.ButtonClicks" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="div1" runat="server">
        <h1>Hello BB</h1>
    </div>
        <br /><br /><br /><br />
        <div id="div2" runat="server">
            <h1>Bye Bye BB</h1>
        </div>
    </form>
</body>

<script src="Scripts/jquery-2.2.0.js"></script>
<script src="Scripts/jquery-2.2.0.intellisense.js"></script>
    <script>
        $(document).ready(function () {
            $('div1').hide(3000).delay(2000).show(3000);
        });
    </script>
</html>
