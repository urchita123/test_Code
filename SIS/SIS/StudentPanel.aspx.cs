using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.IO;
using System.Configuration;

namespace SIS
{
    public partial class StudentPanel : System.Web.UI.Page
    {
        static int SelectedExamID, NoOfQues , i,QueId;
        static string Ans = null;
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            if (Session["UserName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                QueId=0;
                i = 0;
                lblUser.Text = Convert.ToString(Session["UserLabel"]);
                lblUserRole.Text = Convert.ToString(Session["UserRole"]);
                divCourses.Visible = false;
                divExams.Visible = false;
                divExamQues.Visible = false;

            }
        }
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
        protected void btnCourses_Click(object sender, EventArgs e)
        {
            divCourses.Visible = true;
            divExams.Visible = false;
            //DataTable dt 
            gvShowCourses.DataSource = GetUploadedFiles();
            gvShowCourses.DataBind();
        }
        private DataTable GetUploadedFiles()
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
        protected void gvShowCourses_RowCommand(object sender, GridViewCommandEventArgs e)
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
        protected void btmExams_Click(object sender, EventArgs e)
        {
            divCourses.Visible = false;
            divExams.Visible = true;
            divExamQues.Visible = false;
            btnStartExam.Visible = false;
            divExamCompleted.Visible = false;
            lblExamResults.Text = "";
            lblExamComplete.Text = "";
            gvExamsList.DataSource = GetData("spGetSISExamDetails", null);
            gvExamsList.DataBind();
        }
        private int GetExamIdByName(String ExamName)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("GetExamIdByName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ExamName", ExamName);
                SqlParameter output = new SqlParameter();
                output.ParameterName = "@ExamID";
                output.DbType = System.Data.DbType.Int32;
                output.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteNonQuery();
                return Convert.ToInt32(output.Value);
            }
        }
        protected void gvExamsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = 0;
            btnStartExam.Visible = true;
            divExamCompleted.Visible = false;
            string ExamName = gvExamsList.SelectedRow.Cells[1].Text;
            NoOfQues = Convert.ToInt32(gvExamsList.SelectedRow.Cells[2].Text);
            SelectedExamID = GetExamIdByName(ExamName);
            SqlParameter spParam = new SqlParameter();
            spParam.DbType = DbType.Int32;
            spParam.ParameterName = "@ExamID";
            spParam.Value = SelectedExamID;
            gvExamsList.DataSource = GetData("GetExamDetailsByID", spParam);
            gvExamsList.DataBind();
        }
        private void UncheckAll()
        {
            rbtOpt1.Checked = false;
            rbtOpt2.Checked = false;
            rbtOpt3.Checked = false;
            rbtOpt4.Checked = false;
        }
        private void SetExamAnswers(string Answer)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spStoreExamAnswers", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueID", QueId);
                cmd.Parameters.AddWithValue("@UserName", Convert.ToString(Session["UserName"]));
                cmd.Parameters.AddWithValue("@Answer", Answer);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void SetExamResults(string Answer)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spStoreExamResults", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueID", QueId);
                cmd.Parameters.AddWithValue("@UserName", Convert.ToString(Session["UserName"]));
                cmd.Parameters.AddWithValue("@UserAnswer", Answer);
                cmd.Parameters.AddWithValue("@ExamID",SelectedExamID);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private DataSet GetExamResults()
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter("spGetExamResultsByExamIDandUserName", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Clear();
                da.SelectCommand.Parameters.AddWithValue("@UserName", Convert.ToString(Session["UserName"]));
                da.SelectCommand.Parameters.AddWithValue("@ExamID", SelectedExamID);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }

        }
        private void ConductExam(int RowIndex)
        {
            divExamQues.Visible = true;
            SqlParameter spParam = new SqlParameter();
            spParam.DbType = DbType.Int32;
            spParam.ParameterName = "@ExamID";
            spParam.Value = SelectedExamID;
            DataSet ds = GetData("spGetQuesByExamId", spParam);
            DataRow dr = ds.Tables["Table"].Rows[RowIndex];
            lblQue.Text = Convert.ToString(dr["Question"]);
            rbtOpt1.Text = Convert.ToString(dr["Option 1"]);
            rbtOpt2.Text = Convert.ToString(dr["Option 2"]);
            rbtOpt3.Text = Convert.ToString(dr["Option 3"]);
            rbtOpt4.Text = Convert.ToString(dr["Option 4"]);
            QueId = Convert.ToInt32(dr["QuestionID"]);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            if(rbtOpt1.Checked)
            {
                Ans = rbtOpt1.Text;
            }
            else if (rbtOpt2.Checked)
            {
                Ans = rbtOpt2.Text;
            }
            else if (rbtOpt3.Checked)
            {
                Ans = rbtOpt3.Text;
            }
            else if (rbtOpt4.Checked)
            {
                Ans = rbtOpt4.Text;
            }
            SetExamAnswers(Ans);
            SetExamResults(Ans);
            if (i != NoOfQues-1)
            {
                ConductExam(++i);
                UncheckAll();
            }
            else
            {
                lblExamComplete.Text = "Exam Completed";
                //lblExamComplete.ForeColor = System.Drawing.Color.Green;
                lblExamResults.Text = "Student is " + Convert.ToString(Session["UserName"]) + " and Exam is " + gvExamsList.SelectedRow.Cells[1].Text;
                divExamCompleted.Visible = true;
                divExams.Visible = true;
                gvShowExamResults.DataSource = GetExamResults();
                gvShowExamResults.DataBind();
                TimerExamTimer.Enabled = false;
                divExamQues.Visible = false;
                //btnNext.Visible = false;
                //rbtOpt1.Visible = false;
                //rbtOpt2.Visible = false;
                //rbtOpt3.Visible = false;
                //rbtOpt4.Visible = false;
                //lblQue.Visible = false;
               
            }
        }

        protected void gvShowExamResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void gvShowExamResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Text = e.Row.Cells[3].Text;

                if (Text == "Correct")
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Blue;
                else if (Text == "Incorrect")
                    e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnStartExam_Click(object sender, EventArgs e)
        {
            btnStartExam.Visible = false;           
            TimerExamTimer.Interval = Convert.ToInt32(gvExamsList.SelectedRow.Cells[2].Text) * 60000;
            ConductExam(i);
        }
        protected void TimerExamTimer_Tick(object sender, EventArgs e)
        {
            TimerExamTimer.Enabled = false;
            divExamQues.Visible = false;
            divExamCompleted.Visible = true;
            lblExamComplete.Text = "Exam time finished!";
            lblExamResults.Text = "Student is " + Convert.ToString(Session["UserName"]) + " and Exam is " + gvExamsList.SelectedRow.Cells[1].Text;
            gvShowExamResults.DataSource = GetExamResults();
            gvShowExamResults.DataBind();
        }
    }
}