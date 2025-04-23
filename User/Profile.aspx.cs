using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmploymentAgency.User
{
    public partial class Profile : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter sda;
        string str = ConfigurationManager.ConnectionStrings["EmploymentAgencyConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                ShowProfile();
            }
        }

        private void ShowProfile()
        {
            con = new SqlConnection(str);   
            string query = "SELECT UserID, Username, CAST(Name AS NVARCHAR(MAX)) AS Name, Email, Address, PhoneNumber, City, Resume FROM [User] WHERE Username=@username";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", Session["user"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                dlProfile.DataSource = dt;
                dlProfile.DataBind();
            }
            else
            {
                Response.Write("<script>alert(Please do login again with your latest username');</script>");
            }
            
        }

        protected void dlProfile_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "EditUserProfile")
            {
                Response.Redirect("ResumeBuild.aspx?id=" + e.CommandArgument.ToString());
            }
        }
    }
}