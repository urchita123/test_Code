using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
namespace SIS
{
    public partial class ResetForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("sistestinglanet@gmail.com", "ASD!@#asd123");
                //client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = true;
                
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("sistestinglanet@gmail.com", "SIS");
                msg.To.Add(new MailAddress(GetEmailByUserName()));
                msg.IsBodyHtml = true;
                msg.Body = "Click the link below to reset your password:<br /> <a href=\"localhost:51804/ForgetPassword.aspx?UserName=" + txtUserName.Text + "\">Reset password</a>";
                msg.BodyEncoding = Encoding.UTF8;
                client.Send(msg);
            }
            catch(Exception ex){
                Response.Write("could not send email - Error occured");
            }

        }
        private string GetEmailByUserName()
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spGetEmailByUserName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                SqlParameter spParam = new SqlParameter();
                spParam.ParameterName = "@Email";
                spParam.DbType = System.Data.DbType.String;
                spParam.Direction = System.Data.ParameterDirection.Output;
                spParam.Size = 50;
                cmd.Parameters.Add(spParam);
                con.Open();
                cmd.ExecuteNonQuery();
            
                Response.Write(Convert.ToString(spParam.Value));
                return Convert.ToString(spParam.Value);
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

        protected void txtUserName_TextChanged1(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(checkUserNameAvailability(txtUserName.Text)))
                lblUserExists.Text = "";
            else
                lblUserExists.Text = "User does not exists";

        }
    }
}