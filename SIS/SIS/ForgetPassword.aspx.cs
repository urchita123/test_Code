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
namespace SIS
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Request.QueryString["UserName"];
            txtUserName.ReadOnly = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spSISUpdatePassword", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                cmd.Parameters.AddWithValue("@UserPassword", txtPassword.Text);
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                    lblUserPasswordUpdated.Text = "User Password updated";
            }
        }

        private int checkUserNameAvailability(string UserName)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCheckUserNameAvailability", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserName", UserName);
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

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(checkUserNameAvailability(txtUserName.Text)))
                lblUserExists.Text = "";
            else
                lblUserExists.Text = "User does not exists";
        }
    }
}