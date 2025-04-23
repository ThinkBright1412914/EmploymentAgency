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
    public partial class Hiring : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        public string query;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string type = string.Empty;
                query = @"Insert into HiringOrganizations values(@Name,@Position,@City,@Email,@PANNo,@NoOfEmployee,@PhoneNo,@IsApproval,@CreateDate )";
                type = " Saved";
                DateTime time = DateTime.Now;

                con = new SqlConnection(str);
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", txtOrganizationName.Text.Trim());
                cmd.Parameters.AddWithValue("@Position", txtPositionFor.Text.Trim());
                cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text.Trim());
                cmd.Parameters.AddWithValue("@NoOfEmployee", txtNoOfEmployees.Text.Trim());
                cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@IsApproval", false);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@City", ddlCity.SelectedValue);
                cmd.Parameters.AddWithValue("@CreateDate", time.ToString("yyyy-MM-dd HH:mm:ss"));

                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Text = "Your form has been submitted successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    Clear();
                }
                else
                {
                    lblMsg.Text = "Cannot save record right now, please try after sometime..!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");

            }
            finally
            {
                con.Close();
            }

        }

        private void Clear()
        {
            txtOrganizationName.Text = string.Empty;
            txtNoOfEmployees.Text = string.Empty;
            txtPANNo.Text = string.Empty;
            txtPositionFor.Text = string.Empty;
            txtPhoneNumber.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlCity.ClearSelection();

        }
    
    }
}