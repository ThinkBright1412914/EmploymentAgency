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
	public partial class Contact : System.Web.UI.Page
	{
		SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"Insert into Contact Values(@Name,@Email,@Subject,@Message)";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", name.Value.Trim());
                cmd.Parameters.AddWithValue("@Email", email.Value.Trim());
                cmd.Parameters.AddWithValue("@Subject", subject.Value.Trim());
                cmd.Parameters.AddWithValue("@Message", message.Value.Trim());
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if(r >0)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text ="Thanks for reaching out will look into your query!";
                    lblMessage.CssClass = "alert alert-success";
                    CLear();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Cannot save record right now, plpease try after sometime..!";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                con.Close();
            }

        }

        private void CLear()
        {
            name.Value = string.Empty;
            email.Value = string.Empty;
            subject.Value = string.Empty;
            message.Value = string.Empty;
        }
    }
}