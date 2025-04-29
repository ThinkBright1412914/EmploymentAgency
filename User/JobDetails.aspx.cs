using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmploymentAgency.Helper;

namespace EmploymentAgency.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        public string jobTitle = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                showJobDetails();
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No extra code needed here
        }

        private void showJobDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(str))
                {
                    string query = @"SELECT * FROM Jobs WHERE JobID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataList1.DataSource = dt;
                        DataList1.DataBind();
                        jobTitle = dt.Rows[0]["Title"].ToString();
                    }
                    else
                    {
                        Response.Redirect("JobListing.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ApplyJob")
            {
                if (Session["user"] != null)
                {
                    HiddenField hfJobID = e.Item.FindControl("hfJobID") as HiddenField;
                    if (hfJobID != null)
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(str))
                            {
                                con.Open();
                                SqlCommand checkCmd = new SqlCommand("SELECT Resume FROM [User] WHERE UserID = @UserID", con);
                                checkCmd.Parameters.AddWithValue("@UserID", Session["userId"]);
                                object resumeResult = checkCmd.ExecuteScalar();

                                if (resumeResult == null || string.IsNullOrEmpty(resumeResult.ToString()))
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Please upload your resume in your profile before applying for a job.";
                                    lblMsg.CssClass = "alert alert-warning";
                                    return;
                                }

                                string query = @"INSERT INTO AppliedJobs (JobID, UserID) VALUES (@JobID, @UserID)";
                                SqlCommand cmd = new SqlCommand(query, con);
                                cmd.Parameters.AddWithValue("@JobID", hfJobID.Value);
                                cmd.Parameters.AddWithValue("@UserID", Session["userId"]);

                                int r = cmd.ExecuteNonQuery();
                                if (r > 0)
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Job Applied Successfully.";
                                    lblMsg.CssClass = "alert alert-success";

                                    string email = Session["email"].ToString();
                                    string name = Session["user"].ToString();
                                    EmailService.SendEmail(
                                        email,
                                        "Job Confirmation",
                                        $"Dear {name},<br><br>You have successfully applied for the job.<br><br>Best regards,<br>Job Finder."
                                    );

                                    DataList1.DataBind(); 
                                }
                                else
                                {
                                    lblMsg.Visible = true;
                                    lblMsg.Text = "Cannot apply for the job. Please try again later.";
                                    lblMsg.CssClass = "alert alert-danger";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                        }
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }


        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (Session["user"] != null)
                {
                    LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                    HiddenField hfJobID = e.Item.FindControl("hfJobID") as HiddenField;

                    if (btnApplyJob != null && hfJobID != null)
                    {
                        string jobId = hfJobID.Value;

                        if (isApplied(jobId))
                        {
                            btnApplyJob.Enabled = false;
                            btnApplyJob.Text = "Applied";
                            btnApplyJob.CssClass = "btn disabled"; // Optional: Make it greyed out
                        }
                        else
                        {
                            btnApplyJob.Enabled = true;
                            btnApplyJob.Text = "Apply Now";
                        }
                    }
                }
            }
        }

        private bool isApplied(string jobId)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string query = @"SELECT COUNT(*) FROM AppliedJobs WHERE UserId = @UserId AND JobID = @JobID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
                cmd.Parameters.AddWithValue("@JobID", jobId);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        protected string GetImageUrl(object url)
        {
            if (url == null || string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                return ResolveUrl("~/Images/No_Image.png");
            }
            return ResolveUrl($"~/Admin/{url.ToString()}");
        }
    }
}
