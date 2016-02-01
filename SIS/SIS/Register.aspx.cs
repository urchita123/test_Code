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
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        private DataSet GetData(string spName, SqlParameter spParam)
        {
            string cs = ConfigurationManager.ConnectionStrings["BBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter(spName, con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (spParam != null)
                {
                    da.SelectCommand.Parameters.Add(spParam);
                }
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
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
                    cmd.Parameters.AddWithValue("@Gender", "Female" );
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
                cmd.ExecuteNonQuery();//as it is insert query
                string id = output.Value.ToString();
                Session["UserID"] =Convert.ToInt32(id);
                Session["UserName"] = txtUserName.Text;
                Session["RedirectSource"] = "register";
                Response.Redirect("Main.aspx");
            }
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDropDownListState.SelectedIndex == 0)
            {

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
            if (ddlDropDownListCountry.SelectedIndex == 0)
            {

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
            Response.Write("Submit Button clicked");
            SetData("spSISRegisterUserRtnUserId");
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            form1.Controls.Clear();
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
                lblUserExists.Text = "Username already taken";
            else
                lblUserExists.Text = "Username available";
        }

        protected void btnSignIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }
      
    }
}