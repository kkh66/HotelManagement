using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (!IsLoginPage() && Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else if (Session["Login"] != null)
            {
                string empId = Session["Login"].ToString();
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Employee WHERE EmpID = @EmpID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmpID", empId);
                        int count = (int)command.ExecuteScalar();

                        if (count == 0)
                        {
                            Response.Redirect("Login.aspx");
                        }
                    }
                }*/
        }
        //check if the page is login or not
        private bool IsLoginPage()
        {
            return Request.Url.AbsolutePath.EndsWith("Login.aspx", StringComparison.OrdinalIgnoreCase);
        }
    }
}