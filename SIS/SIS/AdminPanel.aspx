<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="SIS.AdminPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIS Admin</title>
<link href="style.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: gray; height: 37px; >
            <%--<asp:Label ID="lblUserRole" runat="server" Style="float: left;" ForeColor="Black"></asp:Label>--%>
            <asp:Label ID="lblUser" runat="server" style="font-family: Merriweather, Georgia, serif; font:600; " ForeColor="Black"></asp:Label>
            <div style="float: right;">
                <asp:Button ID="btnLogout" runat="server" Width="150px" Text="Logout" OnClick="btnLogout_Click" />
            </div>

        </div>
        <br />
        <div align="center">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </div>
        <br />
        <div>
            <table>
                <tr>
                    <th style="text-align:center">
                        <asp:Button ID="btnAddUser" runat="server" Width="150px" Text="Add User" OnClick="btnAddUser_Click" />
                        <asp:Button ID="btnStudentList" runat="server" Width="150px" Text="View student list" OnClick="btnStudentList_Click" />
                        <asp:Button ID="btnCourses" OnClick="btnCourses_Click" runat="server" Width="150px" Text="Upload Courses" />
                        <asp:Button ID="btnExams" runat="server" Width="150px" Text="Exams" OnClick="btnExams_Click" />
                    </th>
                </tr>
            </table>
                
        </div>
        <div id="divStudentsList" align="center" runat="server">
            <asp:GridView ID="gvStudentList" BackColor="Gray" runat="server" CellPadding="4" GridLines="None" ForeColor="Gray">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div id="divUploadCourses" align="center" runat="server">
            <asp:FileUpload ID="fuUploadCourses" runat="server" accept=".pdf" /><asp:Button ID="Button1" runat="server" Text="Upload" OnClick="Button1_Click" />
            <asp:GridView ID="gvShowUploadedFiles" BackColor="Gray" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" OnRowCommand="gvShowUploadedFiles_RowCommand" OnRowDeleting="gvShowUploadedFiles_RowDeleting" ForeColor="Black" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="File">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("File") %>' CommandName="Download" Text='<%# Eval("File") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Size" HeaderText="Size in bytes" />
                    <asp:BoundField DataField="Type" HeaderText="File Type" />
                    <asp:CommandField CancelText="" EditText="" InsertText="" InsertVisible="False" NewText="" SelectText="" ShowCancelButton="False" ShowDeleteButton="True" ShowSelectButton="True" UpdateText="" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div id="divExams" align="center" runat="server">
            <div id="divAddExamDetailsBtn" runat="server" align="center">
                <asp:Button ID="btnAddExamDetails" runat="server" Text="Create new exam" OnClick="btnAddExamDetails_Click" />
                <asp:Button ID="btnViewExams" runat="server" Text="Exams List" OnClick="btnViewExams_Click" />
                <br />
            </div>
            <div id="divAddExamDetails" runat="server">
                <asp:Label ID="lblExamName" runat="server" Text="Exam Name" Width="150px"></asp:Label><asp:TextBox ID="txtExamName" runat="server" Width="150px" AutoPostBack="True" OnTextChanged="txtExamName_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="Exam name is required" ControlToValidate="txtExamName" ValidationGroup="AllValidation"></asp:RequiredFieldValidator>
                <asp:Label ID="lblExamExists" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblQuestionNo" runat="server" Text="No of Ques" Width="150px"></asp:Label><asp:TextBox ID="txtNoOfQues" runat="server" Width="150px" TextMode="Number"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNoOfQues" Display="Dynamic" Type="Integer" ErrorMessage="Must be a number between 1 to 100" MaximumValue="100" MinimumValue="1" SetFocusOnError="True" ValidationGroup="AllValidation"></asp:RangeValidator>
                <br />
                <asp:Label ID="lblExamTime" runat="server" Text="Time in Minutes" Width="150px"></asp:Label><asp:TextBox ID="txtExamTime" runat="server" Width="150px" TextMode="Number"></asp:TextBox>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtExamTime" Display="Dynamic" Type="Integer" ErrorMessage="Must be between 1 to 180" MaximumValue="180" MinimumValue="1" ValidationGroup="AllValidation"></asp:RangeValidator>
                <br />
                <asp:Label ID="lblExamDate" runat="server" Width="150px" Text="Exam Date"></asp:Label>
                <asp:TextBox ID="txtExamDate" Width="150px" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnSaveExamDetails" runat="server" Text="Save Exam" ValidationGroup="AllValidation" OnClick="btnSaveExamDetails_Click" />
            </div>
            <div id="divExamsList" runat="server" align="center">
                <asp:Label ID="lblNoExam" BackColor="#DCDCDC" runat="server"></asp:Label>
                <asp:CheckBox ID="cbAllExams" runat="server" Text="Show all Exams" OnCheckedChanged="cbAllExams_CheckedChanged" AutoPostBack="True" />
                <asp:GridView BackColor="Gray" ID="gvExamsList" runat="server" BorderColor="Gray" OnRowCommand="gvExamsList_RowCommand" CellPadding="4" GridLines="None" OnRowDeleting="gvExamsList_RowDeleting" OnSelectedIndexChanged="gvExamsList_SelectedIndexChanged" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:CommandField AccessibleHeaderText="Add" ShowSelectButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <br />
                <asp:Label ID="lblQuesSaved" runat="server" BackColor="Gray" Width="300px" Text=""></asp:Label>
                <br />
                <asp:GridView ID="gvDispSavedQues" BackColor="Gray" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" BorderColor="Gray">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Question" HeaderText="Question" />
                        <asp:BoundField DataField="Option 1" HeaderText="Option 1" />
                        <asp:BoundField DataField="Option 2" HeaderText="Option 2" />
                        <asp:BoundField DataField="Option 3" HeaderText="Option 3" />
                        <asp:BoundField DataField="Option 4" HeaderText="Option 4" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
            <div id="divExamsQues" runat="server">
                <asp:Label ID="lblSelectedExamName" Width="300px" runat="server"></asp:Label>
                <br />
                <asp:Button ID="btnAddQuesToExam" runat="server" Text="Add Questions" OnClick="btnAddQuesToExam_Click" />
                <asp:GridView ID="gvExamsQues" runat="server" ShowFooter="True" BackColor="Gray" BorderColor="Gray" CellPadding="4" GridLines="None" AutoGenerateColumns="False" OnSelectedIndexChanged="gvExamsQues_SelectedIndexChanged" ForeColor="#333333">

                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                    <Columns>

                        <asp:TemplateField HeaderText="Question">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuestion" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Left" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonSave" OnClick="ButtonSave_Click" runat="server" Text="Save Questions" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Option 1">
                            <ItemTemplate>
                                <asp:TextBox ID="txtopt1" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonCancel" runat="server" OnClick="ButtonCancel_Click" Text="Cancel Adding Quesion" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Option 2">
                            <ItemTemplate>
                                <asp:TextBox ID="txtopt2" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Option 3">
                            <ItemTemplate>
                                <asp:TextBox ID="txtopt3" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Option 4">
                            <ItemTemplate>
                                <asp:TextBox ID="txtopt4" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Correct Answer">
                            <ItemTemplate>
                                <asp:TextBox ID="txtcCorrectOption" runat="server"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
                            <FooterTemplate>
                                <asp:Button ID="ButtonAdd" OnClick="ButtonAdd_Click" runat="server" Text="Add New Quesion" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="Gray" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="Gray" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
        <div id="divRegistered" align="center" runat="server">
            <asp:Label ID="lblUserIDName" runat="server" Text=""></asp:Label>
            <asp:GridView ID="gvDispAddedUser" runat="server" CellPadding="4" ForeColor="#333333" BackColor="Gray" GridLines="None" OnRowCommand="gvDispAddedUser_RowCommand">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div id="divAddUser" align="center" runat="server">
            <h1>Register yourself</h1>
            <br />
            <asp:Label ID="Label1" CssClass="csslbl" ForeColor="Black" runat="server" BorderColor="Gray" Text="Name" Width="150px"></asp:Label>
            <asp:TextBox ID="txtName" CssClass="csstxt" runat="server" ValidateRequestMode="Disabled" ValidationGroup="AllValidatior"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Please enter your name" ValidationGroup="AllValidatior"></asp:RequiredFieldValidator>

            <br />
            <br />

            <asp:Label ID="Label2" CssClass="csslbl"  ForeColor="Black" runat="server" Text="User Name" Width="150px"></asp:Label>
            <asp:TextBox ID="txtUserName" CssClass="csstxt" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Please select Username" ValidationGroup="AllValidatior"></asp:RequiredFieldValidator>
            <br />
            <br />

            <asp:Label ID="Label11" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Email" Width="150px"></asp:Label>
            <asp:TextBox ID="txtEmail" CssClass="csstxt" runat="server" TextMode="Email"></asp:TextBox>

            <br />
            <br />
            <asp:Label ID="Label3" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Password" Width="150px"></asp:Label>
            <asp:TextBox ID="txtPassword" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>



            <br />
            <br />

            <asp:Label ID="Label4" CssClass="csslbl"  ForeColor="Black" runat="server" Text="Confirm Password" Width="150px"></asp:Label>
            <asp:TextBox ID="txtConfirmPwd" CssClass="csstxt" runat="server" TextMode="Password"></asp:TextBox>

            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtConfirmPwd" Display="Dynamic" ErrorMessage="Passwords Must match" ControlToCompare="txtPassword" ValidationGroup="AllValidatior"></asp:CompareValidator>

            <br />
            <br />

            <asp:Label ID="Label10" CssClass="csslbl"  ForeColor="Black" runat="server" BorderColor="#CC3399" Text="Gender" Width="150px"></asp:Label>
            <asp:RadioButton ID="rbtMale" runat="server" Text="Male" Width="75px" Checked="true" GroupName="Gender" />
            <asp:RadioButton ID="rbtFemale" runat="server" ForeColor="Black" Text="Female" Width="80px" GroupName="Gender" />

            <br />
            <br />

            <asp:Label ID="Label5" CssClass="csslbl"  ForeColor="Black" runat="server" BorderColor="#CC3399" Text="Contact No" Width="150px"></asp:Label>
            <asp:TextBox ID="txtContact" CssClass="csstxt" runat="server" Text="91" TextMode="Phone"></asp:TextBox>

            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="Label9" runat="server" CssClass="csslbl"  ForeColor="Black" Text="Country" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged" Width="150px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" CssClass="csslbl"  ForeColor="Black" Text="State" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListState_SelectedIndexChanged" Width="150px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" CssClass="csslbl"  ForeColor="Black" Text="City" Width="150px"></asp:Label>
                    <asp:DropDownList ID="ddlDropDownListCity" runat="server" Width="150px">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>

            <br />
            <br />

            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="150px" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Reset" Width="150px" OnClick="btnReset_Click" />
        </div>
    </form>

    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript">
        var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/;
        var Name = document.getElementById('txtName');
        var UserName = document.getElementById('txtUserName');
        var Email = document.getElementById('txtEmail');
        var Password = document.getElementById('txtPassword');
        var CPassword = document.getElementById('txtConfirmPwd');
        var Contact = document.getElementById('txtContact');

        $(function () {
            $("#txtExamDate").datepicker({
                minDate: 0, maxDate: "+1Y", changeMonth: true,
                changeYear: true
            });
        });

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
