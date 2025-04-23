using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace EmploymentAgency.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader sdr;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ShowUserInfo();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void ShowUserInfo()
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"
                    SELECT 
                        ISNULL(Username, 'N/A') AS Username,
                        ISNULL(CAST(Name AS NVARCHAR(MAX)), 'N/A') AS Name,
                        ISNULL(Email, 'N/A') AS Email,
                        ISNULL(PhoneNumber, 'N/A') AS PhoneNumber,
                        ISNULL(Tenth, '') AS Tenth,
                        ISNULL(Twelveth, '') AS Twelveth,
                        ISNULL(Graduation, '') AS Graduation,
                        ISNULL(PostGraduation, '') AS PostGraduation,
                        ISNULL(Phd, '') AS Phd,
                        ISNULL(Work, '') AS Work,
                        ISNULL(Experience, '') AS Experience,
                        ISNULL(Address, '') AS Address,
                        ISNULL(City, '') AS City
                    FROM [User]
                    WHERE UserID = @UserID";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserID", Request.QueryString["id"]);
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows && sdr.Read())
                {
                    txtUsername.Text = sdr["Username"].ToString();
                    txtFullName.Text = sdr["Name"].ToString();
                    txtEmail.Text = sdr["Email"].ToString();
                    txtPhoneNumber.Text = sdr["PhoneNumber"].ToString();
                    txtTenth.Text = sdr["Tenth"].ToString();
                    txtTwelveth.Text = sdr["Twelveth"].ToString();
                    txtGraduation.Text = sdr["Graduation"].ToString();
                    txtPostGraduation.Text = sdr["PostGraduation"].ToString();
                    txtPhd.Text = sdr["Phd"].ToString();
                    txtWork.Text = sdr["Work"].ToString();
                    txtExperience.Text = sdr["Experience"].ToString();
                    txtAddress.Text = sdr["Address"].ToString();
                    ddlCity.SelectedValue = sdr["City"].ToString();
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "No record found!";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string concatQuery = string.Empty;
                    string filePath = string.Empty;
                    bool isValid = true;
                    con = new SqlConnection(str);

                    if (fuResume.HasFile)
                    {
                        if (Utils.IsValidExtensionForResume(fuResume.FileName))
                        {
                            concatQuery = "Resume = @Resume,";
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Please select a valid .doc, .docx, or .pdf file for the resume!";
                            lblMessage.CssClass = "alert alert-danger";
                            isValid = false;
                        }
                    }

                    string query = $@"
                        UPDATE [User]
                        SET 
                            Username = @Username,
                            Name = @Name,
                            Email = @Email,
                            PhoneNumber = @PhoneNumber,
                            Tenth = @Tenth,
                            TwelvethGrade = @TwelvethGrade,
                            GraduationGrade = @GraduationGrade,
                            PostGraduationGrade = @PostGraduationGrade,
                            Phd = @Phd,
                            WorksOn = @WorksOn,
                            Experience = @Experience,
                            {concatQuery}
                            Address = @Address,
                            City = @City
                        WHERE UserID = @UserID";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tenth", txtTenth.Text.Trim());
                    cmd.Parameters.AddWithValue("@TwelvethGrade", txtTwelveth.Text.Trim());
                    cmd.Parameters.AddWithValue("@GraduationGrade", txtGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorksOn", txtWork.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@City", ddlCity.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserID", Request.QueryString["id"]);

                    if (fuResume.HasFile && isValid)
                    {
                        Guid obj = Guid.NewGuid();
                        filePath = "Resumes/" + obj.ToString() + "_" + fuResume.FileName;
                        fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/") + obj.ToString() + "_" + fuResume.FileName);
                        cmd.Parameters.AddWithValue("@Resume", filePath);
                    }

                    if (isValid)
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Resume details updated successfully!";
                            lblMessage.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Failed to update the record. Please try again later.";
                            lblMessage.CssClass = "alert alert-danger";
                        }
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Invalid request. Please try relogging.";
                    lblMessage.CssClass = "alert alert-danger";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = $"<b>{txtUsername.Text.Trim()}</b> Username already exists! Please try a different one.";
                    lblMessage.CssClass = "alert alert-danger";
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
