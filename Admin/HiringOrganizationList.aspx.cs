using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Linq;
using System.Web.UI;

namespace EmploymentAgency.Admin
{
    public partial class HiringOrganizationList : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["TabStatus"] = "Not Approved";
                ShowHiringOrganization("Not Approved");
                CountHiringOrganizations();
            }
        }

        private void ShowHiringOrganization(string status)
        {
            string query = string.Empty;

            con = new SqlConnection(str);

            if (status == "Not Approved")
            {
                query = @"
                        SELECT ROW_NUMBER() OVER (ORDER BY OrganizationId) AS [S.No],
                               OrganizationId, Name, Position, NoOfEmployee, Email, PhoneNo,
                               PANNo, CreateDate, City,
                               CASE WHEN IsApproval = 0 THEN 'Not Approved' ELSE 'Approved' END AS Status
                        FROM HiringOrganizations
                        WHERE IsApproval = 0";
            }
            else if (status == "Approved")
            {
                query = @"
                        SELECT ROW_NUMBER() OVER (ORDER BY OrganizationId) AS [S.No],
                               OrganizationId, Name, Position, NoOfEmployee, Email, PhoneNo,
                               PANNo, CreateDate, City,
                               CASE WHEN IsApproval = 1 THEN 'Approved' ELSE 'Not Approved' END AS Status
                        FROM HiringOrganizations
                        WHERE IsApproval = 1";
            }

            cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (status == "Approved")
            {
                foreach (DataControlField column in GridView1.Columns)
                {
                    if (column.HeaderText == "Approve")
                    {
                        column.Visible = false;
                        break;
                    }
                }
            }
            else
            {
                foreach (DataControlField column in GridView1.Columns)
                {
                    if (column.HeaderText == "Approve")
                    {
                        column.Visible = true;
                        break;
                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            string status = ViewState["TabStatus"].ToString();
            ShowHiringOrganization(status);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int organizationId = Convert.ToInt32(e.CommandArgument);
                UpdateApprovalStatus(organizationId);

                string status = ViewState["TabStatus"].ToString();
                ShowHiringOrganization(status);
            }
        }

        private void CountHiringOrganizations()
        {
            DataTable dt = GetHiringOrganizationList();

            int approvedCount = dt.AsEnumerable().Count(row => Convert.ToInt32(row["IsApproval"]) == 1);
            int notApprovedCount = dt.AsEnumerable().Count(row => Convert.ToInt32(row["IsApproval"]) == 0);

            lblApprovedCount.Text = approvedCount.ToString();
            lblNotApprovedCount.Text = notApprovedCount.ToString();
        }

        private void UpdateApprovalStatus(int organizationId)
        {
            try
            {
                string query = "UPDATE HiringOrganizations SET IsApproval = 1 WHERE OrganizationId = @OrganizationId";
                con = new SqlConnection(str);
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OrganizationId", organizationId);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.Text = "Approved successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    lblMsg.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "hideMsg", "hideMessage();", true);
                    CountHiringOrganizations();
                    string status = ViewState["TabStatus"].ToString();
                    ShowHiringOrganization(status);
                }
                else
                {
                    lblMsg.Text = "Cannot approved right now, please try after sometime..!";
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

        private DataTable GetHiringOrganizationList()
        {
            DataTable dt = new DataTable();
            string query = @"
                  SELECT ROW_NUMBER() OVER (ORDER BY OrganizationId) AS [S.No],
                  OrganizationId, Name, Position, NoOfEmployee, Email, PhoneNo,
                  PANNo, CreateDate, City, IsApproval,
                  CASE WHEN IsApproval = 0 THEN 'Not Approved' ELSE 'Approved' END AS Status
                  FROM HiringOrganizations";

            try
            {
                con = new SqlConnection(str);
                cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return dt;
        }

        protected void btnNotApproved_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            ViewState["TabStatus"] = "Not Approved";
            ShowHiringOrganization("Not Approved");
            CountHiringOrganizations();
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            ViewState["TabStatus"] = "Approved";
            ShowHiringOrganization("Approved");
            CountHiringOrganizations();
        }

        protected void btnConfirmApprove_Click(object sender, EventArgs e)
        {
            if (int.TryParse(hdnOrganizationId.Value, out int orgId))
            {
                UpdateApprovalStatus(orgId);
            }
        }
    }
}