<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPanel.aspx.cs" Inherits="SIS.StudentPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIS Student</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="frm" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                <div runat="server" align="center">
                    <asp:Button ID="btnCourses" runat="server" Width="150px" Text="Courses" OnClick="btnCourses_Click" /><asp:Button ID="btmExams" runat="server" Width="150px" Text="Exams" OnClick="btmExams_Click" />
                </div>
                <div id="divCourses" align="center" runat="server">
                    <asp:GridView ID="gvShowCourses" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvShowCourses_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="File">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("File") %>' CommandName="Download" Text='<%# Eval("File") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Size" HeaderText="Size of file in bytes" />
                            <asp:BoundField DataField="Type" HeaderText="File Type" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#DCDCDC" Font-Bold="True" ForeColor="#000000" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <div id="divExams" align="center" runat="server">
                    <div id="divExamsList" runat="server">
                        <asp:GridView ID="gvExamsList" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" OnSelectedIndexChanged="gvExamsList_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField AccessibleHeaderText="Add" ShowSelectButton="True" />
                            </Columns>
                            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                            <HeaderStyle BackColor="#DCDCDC" Font-Bold="True" ForeColor="#000000" />
                            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                            <SelectedRowStyle BackColor="#191919" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#594B9C" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#33276A" />
                        </asp:GridView>
                        <asp:Button ID="btnStartExam" runat="server" OnClick="btnStartExam_Click" Text="Start Exam" Visible="false" />
                    </div>
                    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                    <asp:Timer ID="TimerExamTimer" runat="server" OnTick="TimerExamTimer_Tick"></asp:Timer>
                    <div id="divExamQues" runat="server">
                        <table>
                            <tr>
                                <th colspan="2">
                                    <asp:Label ID="lblQue" runat="server" Text=" "></asp:Label></th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:RadioButton ID="rbtOpt1" runat="server" GroupName="Answer" />
                                </th>
                                <th>
                                    <asp:RadioButton ID="rbtOpt2" runat="server" GroupName="Answer" />
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:RadioButton ID="rbtOpt3" runat="server" GroupName="Answer" />
                                </th>
                                <th>
                                    <asp:RadioButton ID="rbtOpt4" runat="server" GroupName="Answer" />
                                </th>
                            </tr>
                            <tr>
                                <th colspan="2">
                                    <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                                </th>
                            </tr>
                        </table>
                    </div>
                    <div id="divExamCompleted" align="center" runat="server">
                        <asp:Label ID="lblExamComplete" BackColor="#DCDCDC" Width="250px" runat="server"></asp:Label><br />
                        <br />
                        <asp:Label ID="lblExamResults" BackColor="#DCDCDC" Width="250px " runat="server"></asp:Label><br />
                        <br />
                        <asp:GridView ID="gvShowExamResults" runat="server" OnRowDataBound="gvShowExamResults_RowDataBound"
                            OnPageIndexChanging="gvShowExamResults_PageIndexChanging" CellPadding="3" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle BackColor="#DCDCDC" Font-Bold="True" ForeColor="#000000" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#191919" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

</body>
</html>
