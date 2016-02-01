using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
namespace SIS
{
    public partial class AdminPanel : System.Web.UI.Page
    {
        static int SelectedExamID, NoOfQues, AddRowClickCount;
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                    Response.Redirect("LoginPage.aspx");
                AddRowClickCount = 1;
                SelectedExamID = 0;
                NoOfQues = 0;
                SetInitialRow();
                lblUser.Text = Convert.ToString(Session["UserLabel"]);
                //lblUserRole.Text = Convert.ToString(Session["UserRole"]);
                divStudentsList.Visible = false;
                divExams.Visible = false;
                divUploadCourses.Visible = false;
                divRegistered.Visible = false;
                divAddUser.Visible = false;

                ddlDropDownListCountry.DataSource = GetData("spGetCountries", null);
                ddlDropDownListCountry.DataTextField = "Name";
                ddlDropDownListCountry.DataValueField = "ID";
                ddlDropDownListCountry.DataBind();

                ListItem liCountry = new ListItem("Select Country", "-1");
                ddlDropDownListCountry.Items.Insert(0, liCountry);

                ListItem liState = new ListItem("Select State", "-1");
                ddlDropDownListState.Items.Insert(0, liState);

                ListItem liCity = new ListItem("Select City", "-1");
                ddlDropDownListCity.Items.Insert(0, liCity);
                ddlDropDownListState.Enabled = false;
                ddlDropDownListCity.Enabled = false;
            }
        }

        protected void btnStudentList_Click(object sender, EventArgs e)
        {
            divExams.Visible = false;
            divUploadCourses.Visible = false;
            divRegistered.Visible = false;
            divAddUser.Visible = false;
            divStudentsList.Visible = true;
            gvStudentList.DataSource = GetData("spGetSISStudents", null);
            gvStudentList.DataBind();
        }


        //Courses section

        protected void btnCourses_Click(object sender, EventArgs e)
        {
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divRegistered.Visible = false;
            divAddUser.Visible = false;
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divUploadCourses.Visible = true;
            gvShowUploadedFiles.DataSource = GetUploadedFiles();
            gvShowUploadedFiles.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divRegistered.Visible = false;
            divAddUser.Visible = false;
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divUploadCourses.Visible = true;
            //if (fuUploadCourses.FileName)
            //{

            //}

            string fname = fuUploadCourses.FileName;
            if (fuUploadCourses.HasFile)
            {
                fuUploadCourses.PostedFile.SaveAs(Server.MapPath("~/Data/") + fuUploadCourses.FileName);
            }
            gvShowUploadedFiles.DataSource = GetUploadedFiles();
            gvShowUploadedFiles.DataBind();
        }
        public DataTable GetUploadedFiles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            foreach (string str in Directory.GetFiles(Server.MapPath("~/Data")))
            {
                FileInfo fi = new FileInfo(str);
                dt.Rows.Add(fi.Name, fi.Length, fi.Extension);

            }
            return dt;
        }
        protected void gvShowUploadedFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                Response.AppendHeader("content-disposition", "filename=" + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/Data/") + e.CommandArgument);
                Response.End();
            }
        }
        protected void gvShowUploadedFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvShowUploadedFiles.Rows[e.RowIndex];
            LinkButton LinkButton1 = (LinkButton)row.FindControl("LinkButton1");
            System.IO.File.Delete(Server.MapPath("~/Data/") + LinkButton1.Text);
            // gvShowUploadedFiles.
            gvShowUploadedFiles.DataSource = GetUploadedFiles();
            gvShowUploadedFiles.DataBind();
            //gvShowUploadedFiles.DeleteRow(e.RowIndex);
        }

        //Getting data
        private DataSet GetData(string spName, SqlParameter spParam)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(spName, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Clear();
                if (spParam != null)
                {
                    da.SelectCommand.Parameters.Add(spParam);
                }
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        //Add user / Registration section
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divUploadCourses.Visible = false;
            divRegistered.Visible = false;
            divAddUser.Visible = true;
        }
        private void SetData(string spName)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(spName, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                if (rbtMale.Checked)
                {
                    cmd.Parameters.AddWithValue("@Gender", "Male");
                }
                if (rbtFemale.Checked)
                {
                    cmd.Parameters.AddWithValue("@Gender", "Female");
                }
             
                    cmd.Parameters.AddWithValue("@Role", "Student");


                cmd.Parameters.AddWithValue("@CountryId", ddlDropDownListCountry.SelectedValue);
                cmd.Parameters.AddWithValue("@StateId", ddlDropDownListState.SelectedValue);
                cmd.Parameters.AddWithValue("@CityId", ddlDropDownListCity.SelectedValue);
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@UserId";
                //Setting the database veriable type
                output.DbType = System.Data.DbType.Int32;
                //setting the veriable direction(input,output or input/output both)
                output.Direction = System.Data.ParameterDirection.Output;
                //adding the veriable to parameter list
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                string id = output.Value.ToString();
                lblUserIDName.Text = "User id is " + id + " and Username is " + txtUserName.Text;
                int useridgen = Convert.ToInt32(id);

                //return useridgen;


                output.Direction = System.Data.ParameterDirection.Input;
                output.Value = Int32.Parse(id);
                gvDispAddedUser.DataSource = GetData("spGetSISUserByID", output);
                gvDispAddedUser.DataBind();
            }
        }
        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlDropDownListState.SelectedIndex <1)
            {
                ddlDropDownListCity.Enabled = false;
            }
            else
            {
                ddlDropDownListCity.Enabled = true;
                SqlParameter p = new SqlParameter("@StateId", ddlDropDownListState.SelectedValue);

                ddlDropDownListCity.DataSource = GetData("spGetCitiessByStateId", p);

                ddlDropDownListCity.DataTextField = "Name";
                ddlDropDownListCity.DataValueField = "ID";
                ddlDropDownListCity.DataBind();
                ListItem liCity = new ListItem("Select City", "-1");
                ddlDropDownListCity.Items.Insert(0, liCity);
            }
        }
        protected void DropDownListCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDropDownListCountry.SelectedIndex < 1)
            {
                ddlDropDownListState.Enabled = false;
            }
            else
            {
                ddlDropDownListState.Enabled = true;
                SqlParameter p = new SqlParameter("@CountryId", ddlDropDownListCountry.SelectedValue);

                ddlDropDownListState.DataSource = GetData("spGetStatesByCountryId", p);
                ddlDropDownListState.DataTextField = "Name";
                ddlDropDownListState.DataValueField = "ID";
                ddlDropDownListState.DataBind();
                ListItem liState = new ListItem("Select State", "-1");
                ddlDropDownListState.Items.Insert(0, liState);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //SqlParameter pr = new SqlParameter();
            //pr.DbType = DbType.Int32;
            //pr.Direction = ParameterDirection.Input;
            //pr.ParameterName = "@UserID";
            SetData("spSISRegisterUserRtnUserId");
            divStudentsList.Visible = false;
            divExams.Visible = false;
            divUploadCourses.Visible = false;
            divRegistered.Visible = true;
            divAddUser.Visible = false;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            form1.Controls.Clear();
        }

        //Logout section
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }

        //Exams Section

        protected void btnExams_Click(object sender, EventArgs e)
        {
            divStudentsList.Visible = false;
            divUploadCourses.Visible = false;
            divRegistered.Visible = false;
            divAddUser.Visible = false;
            divStudentsList.Visible = false;
            divExams.Visible = true;
            divAddExamDetails.Visible = false;
            divExamsQues.Visible = false;
            divAddExamDetails.Visible = false;
            divAddExamDetailsBtn.Visible = true;
            divExamsList.Visible = false;
            divExamsQues.Visible = false;
        }

        //1. Adding exam
        protected void btnAddExamDetails_Click(object sender, EventArgs e)
        {
            divAddExamDetails.Visible = true;
            divExamsList.Visible = false;
            divExamsQues.Visible = false;
        }
        private int SetExamData()
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spStoreExamAndReturnExamID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamName", txtExamName.Text);
                cmd.Parameters.AddWithValue("@NoOfQue", txtNoOfQues.Text);
                cmd.Parameters.AddWithValue("@ExamTime", txtExamTime.Text);
                cmd.Parameters.AddWithValue("@ExamDate", txtExamDate.Text);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@ExamID";
                //Setting the database veriable type
                output.DbType = System.Data.DbType.Int32;
                //setting the veriable direction(input,output or input/output both)
                output.Direction = System.Data.ParameterDirection.Output;
                //adding the veriable to parameter list
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();//as it is insert query
                return Convert.ToInt32(output.Value);
            }

        }
        private int GetNoOfQues()
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetNoOFStoredQuesByExamID", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamID", SelectedExamID);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@NoOfQues";
                //Setting the database veriable type
                output.DbType = System.Data.DbType.Int32;
                //setting the veriable direction(input,output or input/output both)
                output.Direction = System.Data.ParameterDirection.Output;
                //adding the veriable to parameter list
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteReader();
                return Convert.ToInt32(output.Value);
            }
        }
        protected void btnSaveExamDetails_Click(object sender, EventArgs e)
        {
            SetExamData();
            SqlParameter spParam = new SqlParameter();
            spParam.ParameterName = "@All";
            spParam.DbType = DbType.Int32;
            spParam.Direction = ParameterDirection.Input;
            if(cbAllExams.Checked == true)
                spParam.Value = 1;          
            else
                spParam.Value = 0;
            gvExamsList.DataSource = GetData("spGetSISExamDetails", spParam);
            gvExamsList.DataBind();
            divAddExamDetails.Visible = false;
            divExamsQues.Visible = false;
            divAddExamDetails.Visible = false;
            divExamsList.Visible = true;
        }
        private int GetExamIdByName(String ExamName)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetExamIdByName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamName", ExamName);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@ExamID";
                //Setting the database veriable type
                output.DbType = System.Data.DbType.Int32;
                //setting the veriable direction(input,output or input/output both)
                output.Direction = System.Data.ParameterDirection.Output;
                //adding the veriable to parameter list
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();//as it is insert query
                return Convert.ToInt32(output.Value);
            }
            //GetExamIdByName

        }
        protected void gvExamsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ExamName = gvExamsList.SelectedRow.Cells[2].Text;
            NoOfQues = Convert.ToInt32(gvExamsList.SelectedRow.Cells[3].Text);
            lblSelectedExamName.BackColor = System.Drawing.Color.Gray;
            lblSelectedExamName.Text = "Selected exam name is " + ExamName;
            SelectedExamID = GetExamIdByName(ExamName);
            SqlParameter spParam = new SqlParameter();
            spParam.DbType = DbType.Int32;
            spParam.ParameterName = "@ExamID";
            spParam.Value = SelectedExamID;
            gvExamsList.DataSource = GetData("GetExamDetailsByID", spParam);
            gvExamsList.DataBind();


            SqlParameter spParam1 = new SqlParameter();
            spParam1.DbType = DbType.Int32;
            spParam1.ParameterName = "@ExamID";
            spParam1.Value = SelectedExamID;
            gvDispSavedQues.DataSource = GetData("spGetQuesByExamId", spParam1);
            gvDispSavedQues.DataBind();
            AddRowClickCount = GetNoOfQues();
            if (AddRowClickCount <= NoOfQues-1)
            {
                divExamsQues.Visible = true;
            }
            else
                lblQuesSaved.Text = NoOfQues+" questions were already added to exam "+ExamName;

        }
        protected void gvExamsList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int row = e.RowIndex;
            string CS = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spDelExamAndQues", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamID", GetExamIdByName(gvExamsList.Rows[e.RowIndex].Cells[2].Text));
                con.Open();
                cmd.ExecuteNonQuery();
                SqlParameter spParam = new SqlParameter();
                spParam.ParameterName = "@All";
                spParam.DbType = DbType.Int32;
                spParam.Direction = ParameterDirection.Input;
                if (cbAllExams.Checked == true)
                    spParam.Value = 1;
                else
                    spParam.Value = 0;
                gvExamsList.DataSource = GetData("spGetSISExamDetails", spParam);
                gvExamsList.DataBind();
            }
        }

        protected void btnViewExams_Click(object sender, EventArgs e)
        {
            divExams.Visible = true;
            divAddExamDetails.Visible = false;
            SqlParameter spParam = new SqlParameter();
            spParam.ParameterName = "@All";
            spParam.DbType = DbType.Int32;
            spParam.Direction = ParameterDirection.Input;
            if (cbAllExams.Checked == true)
                spParam.Value = 1;
            else
                spParam.Value = 0;
            gvExamsList.DataSource = GetData("spGetSISExamDetails", spParam);
            gvExamsList.DataBind();
            if (gvExamsList.Rows.Count == 0)
                lblNoExam.Text = "There are no exam to display";
            else
                lblNoExam.Text = "";
            divExamsList.Visible = true;
            divExamsQues.Visible = false;
            lblQuesSaved.Text = "";
            gvExamsQues.DataSource = null;
            gvExamsQues.DataBind();
        }
        //Functionality for storing Questions
        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("Column0", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));
            dt.Columns.Add(new DataColumn("Column3", typeof(string)));
            dt.Columns.Add(new DataColumn("Column4", typeof(string)));
            dt.Columns.Add(new DataColumn("Column5", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column6", typeof(Int32)));
            dr = dt.NewRow();
            dr["Column0"] = string.Empty;
            dr["Column1"] = string.Empty;
            dr["Column2"] = string.Empty;
            dr["Column3"] = string.Empty;
            dr["Column4"] = string.Empty;
            dr["Column5"] = string.Empty;
            // dr["Column6"] = 1;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();
            //Store the DataTable in ViewState
            ViewState["CurrentQuestions"] = dt;

            gvExamsQues.DataSource = dt;
            gvExamsQues.DataBind();
        }
        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentQuestions"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentQuestions"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box0 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[0].FindControl("txtQuestion");
                        TextBox box1 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[1].FindControl("txtopt1");
                        TextBox box2 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[2].FindControl("txtopt2");
                        TextBox box3 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[3].FindControl("txtopt3");
                        TextBox box4 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[4].FindControl("txtopt4");
                        TextBox box5 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[5].FindControl("txtcCorrectOption");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Column0"] = box0.Text;
                        dtCurrentTable.Rows[i - 1]["Column1"] = box1.Text;
                        dtCurrentTable.Rows[i - 1]["Column2"] = box2.Text;
                        dtCurrentTable.Rows[i - 1]["Column3"] = box3.Text;
                        dtCurrentTable.Rows[i - 1]["Column4"] = box4.Text;
                        dtCurrentTable.Rows[i - 1]["Column5"] = box5.Text;
                        //dtCurrentTable.Rows[i - 1]["Column6"] = 1;
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentQuestions"] = dtCurrentTable;

                    gvExamsQues.DataSource = dtCurrentTable;
                    gvExamsQues.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            //Set Previous Data on Postbacks
            SetPreviousData();
        }
        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentQuestions"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentQuestions"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box0 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[0].FindControl("txtQuestion");
                        TextBox box1 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[1].FindControl("txtopt1");
                        TextBox box2 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[2].FindControl("txtopt2");
                        TextBox box3 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[3].FindControl("txtopt3");
                        TextBox box4 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[4].FindControl("txtopt4");
                        TextBox box5 = (TextBox)gvExamsQues.Rows[rowIndex].Cells[5].FindControl("txtcCorrectOption");
                        box0.Text = dt.Rows[i]["Column0"].ToString();
                        box1.Text = dt.Rows[i]["Column1"].ToString();
                        box2.Text = dt.Rows[i]["Column2"].ToString();
                        box3.Text = dt.Rows[i]["Column3"].ToString();
                        box4.Text = dt.Rows[i]["Column4"].ToString();
                        box5.Text = dt.Rows[i]["Column5"].ToString();
                        rowIndex++;
                    }
                }
            }
        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddRowClickCount++;
            if (NoOfQues-1 <= AddRowClickCount)
            {
                AddNewRowToGrid();
                Button objbtn = gvExamsQues.FooterRow.FindControl("ButtonAdd") as Button;
                if(objbtn != null)
                    objbtn.Enabled = false;
                //objbtn.Visible = false;
            }
            else
            {
                AddNewRowToGrid();
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblQuesSaved.Text = "Questions saved";
                divExamsQues.Visible = false;
                AddNewRowToGrid();
                //SetPreviousData();
                if (ViewState["CurrentQuestions"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentQuestions"];

                    string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
                    int i = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (i != dt.Rows.Count)
                        {
                            using (SqlConnection con = new SqlConnection(cs))
                            {
                                SqlCommand cmd = new SqlCommand("spStoreQuestions", con);
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@Que", row["Column0"].ToString());
                                cmd.Parameters.AddWithValue("@Opt1", row["Column1"].ToString());
                                cmd.Parameters.AddWithValue("@Opt2", row["Column2"].ToString());
                                cmd.Parameters.AddWithValue("@Opt3", row["Column3"].ToString());
                                cmd.Parameters.AddWithValue("@Opt4", row["Column4"].ToString());
                                cmd.Parameters.AddWithValue("@Answer", row["Column5"].ToString());
                                cmd.Parameters.AddWithValue("@ExamID", SelectedExamID);
                                //SelectedExamID
                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                            i++;
                        }

                    }
                    SqlParameter spParam = new SqlParameter();
                    spParam.ParameterName = "@ExamID";
                    spParam.DbType = DbType.Int32;
                    spParam.Value = SelectedExamID;
                    gvDispSavedQues.DataSource = GetData("spGetQuesByExamId", spParam);
                    gvDispSavedQues.DataBind();
                    //gvDispSavedQues.Columns[0].Visible = false;
                    //gvDispSavedQues.Columns.RemoveAt(0);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Some Error Occured ";
            }

        }
        protected void gvExamsQues_SelectedIndexChanged(object sender, EventArgs e) { }
        protected void gvDispAddedUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void gvExamsList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            divExamsQues.Visible = false;
            divExamsList.Visible = true;
            lblQuesSaved.Text = "";
            gvExamsQues.DataSource = null;
            gvExamsQues.DataBind();
        }
        private int checkExamNameAvailability(string ExamName)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCheckExamNameAvailability", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ExamName", ExamName);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@Flag";
                //Setting the database veriable type
                output.DbType = System.Data.DbType.Int32;
                //setting the veriable direction(input,output or input/output both)
                output.Direction = System.Data.ParameterDirection.Output;
                //adding the veriable to parameter list
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();//as it is insert query
                int flag = Convert.ToInt32(output.Value);
                return (flag > 0) ? 1 : 0;

            }
        }
        protected void txtExamName_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(checkExamNameAvailability(txtExamName.Text)))
                lblExamExists.Text = "Exam already saved";
            else
                lblExamExists.Text = " ";
        }
        protected void btnAddQuesToExam_Click(object sender, EventArgs e)
        {
            SetInitialRow();
            divExamsQues.Visible = true;
        }

        protected void cbAllExams_CheckedChanged(object sender, EventArgs e)
        {
            SqlParameter spParam = new SqlParameter();
            spParam.ParameterName = "@All";
            spParam.DbType = DbType.Int32;
            spParam.Direction = ParameterDirection.Input;
            if (cbAllExams.Checked == true)
                spParam.Value = 1;
            else
                spParam.Value = 0;
            gvExamsList.DataSource = GetData("spGetSISExamDetails", spParam);
            gvExamsList.DataBind();
        }
    }
}