using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SIS
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e){}
        private int ValidateUser(string UserName, string UserPassword)
        {
            string CS = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spUserValidation", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
                //Setting up an out parameter
                SqlParameter output = new SqlParameter();
                //Binding the name used in the sp to the new veriable created
                output.ParameterName = "@flag";
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            lblInvalidUser.Text = " ";
            if (Convert.ToBoolean(ValidateUser(txtUserName.Text, txtPassword.Text)))
            {
                Session["UserName"] = txtUserName.Text;
                Session["RedirectSource"] = "login";
                Response.Redirect("Main.aspx");
            }
            else
            {
                lblInvalidUser.Text = "Username and Password do not match";
                lblInvalidUser.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnForgetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgetPassword.aspx");
        }
    }
}