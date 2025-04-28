using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace EmploymentAgency.User
{
    public partial class Register : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string userImg = string.Empty;
                con = new SqlConnection(str);

                if (img.HasFile)
                {
                    using (System.IO.BinaryReader br = new System.IO.BinaryReader(img.PostedFile.InputStream))
                    {
                        byte[] bytes = br.ReadBytes(img.PostedFile.ContentLength);
                        userImg = Convert.ToBase64String(bytes);
                    }
                }

                string query = @"INSERT INTO [User] (Username, Password, Name, Address, Email, PhoneNumber, City , Image) 
                 VALUES (@Username, @Password, @Name, @Address, @Email, @PhoneNumber, @City , @Image)";

                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddesss.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@City", ddlCity.SelectedValue);
                cmd.Parameters.AddWithValue("@Image", userImg);

                con.Open();
                int r = cmd.ExecuteNonQuery();

                if (r > 0)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Registered Successfully!";
                    lblMessage.CssClass = "alert alert-success";
                    Response.Redirect("Login.aspx?msg=success", false);
                    CLear();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Cannot save record right now, plpease try after sometime..!";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Voilation of UNIQUE KEY constraint"))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "<b>" + txtUsername.Text.Trim() + "</b>Username already exists!, try new one...!";
                    lblMessage.CssClass = "alert alert-danger";
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "')</script>");
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


        private void CLear()
        {
            txtUsername.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtFullName.Text = String.Empty;
            txtAddesss.Text = String.Empty;
            txtPhoneNumber.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtConfirmPassword.Text = String.Empty;
            ddlCity.ClearSelection();
        }
    }
}