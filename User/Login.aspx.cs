using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmploymentAgency.User
{
	public partial class Login : System.Web.UI.Page
	{
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        string username, password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLoginType.SelectedValue == "Admin")
                {
                    username = ConfigurationManager.AppSettings["username"];
                    password = ConfigurationManager.AppSettings["password"];
                    if (username == txtUsername.Text.Trim() && password == txtPassword.Text.Trim())
                    {
                        Session["admin"] = username;
                        Response.Redirect("../Admin/Dashboard.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("Admin");
                    }
                }
                else
                {
                    con = new SqlConnection(str);
                    string query = @"Select * from [User] where Username=@Username and Password= @Password";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                    con.Open();
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        Session["user"] = sdr["Username"].ToString();
                        Session["userid"] = sdr["UserId"].ToString();
                        Session["email"] = sdr["Email"].ToString();
                        Response.Redirect("Default.aspx", false);
                    }
                    else
                    {
                        showErrorMsg("User");
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                con.Close();
            }
        }

        private void showErrorMsg(string userType)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "<b>" + userType + "</b> Credentials are incorrect...!";
            lblMessage.CssClass = "alert alert-danger";
        }
    }
}