using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmploymentAgency.User
{
    public partial class JobListing : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        public int jobCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        private void showJobList()
        {
            if (dt == null)
            {
                con = new SqlConnection(str);
                string query = @"
                        SELECT 
                            JobID, 
                            Title, 
                            Salary,
                            JobType,
                            CompanyName,
                            CompanyImage,   
                            City,
                            Address, 
                            CreateDate
                        FROM Jobs";

                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            lblJobCount.Text = JobCount(dt.Rows.Count);
        }

        private string JobCount(int count)
        {
            if (count > 1)
            {
                return "Total <b>" + count + "</b> Jobs Found";
            }
            else if (count == 1)
            {
                return "Total <b>" + count + "</b> Job Found";
            }
            else
            {
                return "No Jobs Found";
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCity.SelectedValue != "0")
            {
                con = new SqlConnection(str);
                string query = @"
                        SELECT 
                            JobID, 
                            Title, 
                            Salary,
                            JobType,
                            CompanyName,
                            CompanyImage,   
                            City,
                            Address, 
                            CreateDate
                        FROM Jobs WHERE City = '" + ddlCity.SelectedValue + "'";

                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
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


        public static string RelativeDate(DateTime theDate)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>();
            int minute = 60;
            int hour = minute * 60;
            int day = hour * 24;

            thresholds.Add(60, "{0} seconds ago");
            thresholds.Add(minute * 2, "a minute ago");
            thresholds.Add(45 * minute, "{0} minutes ago");
            thresholds.Add(120 * minute, "an hour ago");
            thresholds.Add(day, "{0} hours ago");
            thresholds.Add(2 * day, "yesterday");
            thresholds.Add(30 * day, "{0} days ago");
            thresholds.Add(365 * day, "a month ago");
            thresholds.Add(long.MaxValue, "{0} years ago");

            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;

            foreach (long threshold in thresholds.Keys)
            {
                if (since < threshold)
                {
                    TimeSpan t = DateTime.Now - theDate;
                    return string.Format(
                        thresholds[threshold],
                        t.Days > 365 ? t.Days / 365 :
                        (t.Days > 0 ? t.Days :
                        (t.Hours > 0 ? t.Hours :
                        (t.Minutes > 0 ? t.Minutes :
                        (t.Seconds > 0 ? t.Seconds : 0))))
                    );
                }
            }
            return "";
        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string JobType = SelectedCheckBox();
            if (!string.IsNullOrEmpty(JobType))
            {
                con = new SqlConnection(str);
                string query = @"
                            SELECT 
                                JobID, 
                                Title, 
                                Salary,
                                JobType,
                                CompanyName,
                                CompanyImage,   
                                City,
                                Address, 
                                CreateDate
                            FROM Jobs WHERE JobType IN (" + JobType + ")";

                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
        }

        private string SelectedCheckBox()
        {
            string JobType = string.Empty;
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected)
                {
                    JobType += "'" + CheckBoxList1.Items[i].Text + "',";
                }
            }
            return JobType.TrimEnd(',');
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue != "0")
            {
                string postedDate = selectedRadioButton();
                con = new SqlConnection(str);
                string query = @"
                                SELECT 
                                    JobID, 
                                    Title, 
                                    Salary,
                                    JobType,
                                    CompanyName,
                                    CompanyImage,   
                                    City,
                                    Address, 
                                    CreateDate
                                FROM Jobs 
                                WHERE Convert(DATE, CreateDate) " + postedDate;

                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        private string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;
            if (RadioButtonList1.SelectedValue == "1")
            {
                postedDate = "= Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                postedDate = " BETWEEN Convert(DATE,'" + DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + "') AND Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "3")
            {
                postedDate = " BETWEEN Convert(DATE,'" + DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd") + "') AND Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if (RadioButtonList1.SelectedValue == "4")
            {
                postedDate = " BETWEEN Convert(DATE,'" + DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd") + "') AND Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else
            {
                postedDate = " BETWEEN Convert(DATE,'" + DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd") + "') AND Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }

            return postedDate;
        }

        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCondition = false;
                string subquery = string.Empty;
                string JobType = SelectedCheckBox();
                string postDate = string.Empty;
                List<string> queryList = new List<string>();
                con = new SqlConnection(str);

                if (ddlCity.SelectedValue != "0")
                {
                    queryList.Add(" City ='" + ddlCity.SelectedValue + "' ");
                    isCondition = true;
                }

                if (!string.IsNullOrEmpty(JobType))
                {
                    queryList.Add(" JobType IN (" + JobType + ")");
                    isCondition = true;
                }

                if (RadioButtonList1.SelectedValue != "0")
                {
                    postDate = selectedRadioButton();
                    queryList.Add(" Convert(DATE, CreateDate)" + postDate);
                    isCondition = true;
                }

                string query;
                if (isCondition)
                {
                    subquery = string.Join(" AND ", queryList);
                    query = @"SELECT JobID, Title, Salary, JobType, CompanyName, CompanyImage, City, CreateDate FROM Jobs WHERE " + subquery;
                }
                else
                {
                    query = @"SELECT JobID, Title, Salary, JobType, CompanyName, CompanyImage, City, CreateDate FROM Jobs";
                }

                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
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

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCity.ClearSelection();
            CheckBoxList1.ClearSelection();
            RadioButtonList1.SelectedValue = "0";
            RBSelectedColorChange();
            showJobList();
        }

        private void RBSelectedColorChange()
        {
            if (RadioButtonList1.SelectedItem != null && RadioButtonList1.SelectedItem.Selected)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }
    }
}
