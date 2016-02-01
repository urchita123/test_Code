using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace SIS
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserLabel();

            if (GetUserType().Equals("Admin")){
                Session["UserLabel"] = lblUser.Text;
                Session["UserRole"] = "Admin panel";
                Response.Redirect("AdminPanel.aspx");
            }
            else
            {
                Session["UserLabel"] = lblUser.Text;
                Session["UserRole"] = "Student panel";
                Response.Redirect("StudentPanel.aspx");
            }

        }

        private void GetUserLabel()
        {
            if ((Convert.ToString(Session["RedirectSource"]).Equals("register")))
                lblUser.Text = "Hello " + Convert.ToString(Session["UserName"]) + " your id is " + Convert.ToInt32(Session["UserID"]);
            else
                lblUser.Text = "Hello " + Convert.ToString(Session["UserName"]);
        }

        private string GetUserType()
        {
            string CS = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetSISUserRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", Convert.ToString(Session["UserName"]));
                SqlParameter output = new SqlParameter();
                output.ParameterName = "@UserRole";
                output.DbType = System.Data.DbType.String;
                output.Size = 10;
                output.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(output);
                con.Open();
                cmd.ExecuteReader();
                return output.Value.ToString();
            }
        }        
    }
}