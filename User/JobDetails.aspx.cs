using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmploymentAgency.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;
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
            // Ensure no redundant logic is executed here
        }

        private void showJobDetails()
        {
            try
            {
                con = new SqlConnection(str);
                string query = @"SELECT * FROM Jobs WHERE JobID = @id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Request.QueryString["id"]);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
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
                    try
                    {
                        con = new SqlConnection(str);
                        string query = @"INSERT INTO AppliedJobs (JobID, UserID) VALUES (@JobID, @UserID)";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@JobID", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserID", Session["userid"]);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        lblMsg.Visible = true;
                        lblMsg.Text = r > 0 ? "Job Applied Successfully" : "Cannot apply for the job. Please try again later.";
                        lblMsg.CssClass = r > 0 ? "alert alert-success" : "alert alert-danger";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                    finally
                    {
                        con.Close();
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
            if (Session["user"] != null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                if (btnApplyJob != null) // Ensure the control exists
                {
                    if (isApplied())
                    {
                        btnApplyJob.Enabled = false;
                        btnApplyJob.Text = "Applied";
                    }
                    else
                    {
                        btnApplyJob.Enabled = true;
                        btnApplyJob.Text = "Apply Now";
                    }
                }
            }
        }

        bool isApplied()// Add any additional logic for data binding here if needed
        {
            con = new SqlConnection(str);
            string query = @"SELECT * FROM AppliedJobs WHERE  UserId = @UserId and JobID = @JobID";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.Parameters.AddWithValue("@JobID", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            if (dt1.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected string GetImageUrl(object url)
        {
            if (url == null || string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                // Log the issue (optional)
                System.Diagnostics.Debug.WriteLine("CompanyImage is null or empty.");
                return ResolveUrl("~/Images/No_Image.png");
            }
            return ResolveUrl($"~/Admin/{url.ToString()}");
        }
    }
}
