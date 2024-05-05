using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using HotelManagement;

namespace HotelManagement.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString; 
            string username = txtloginuser.Text.Trim();
            string password = txtloginpassword.Text.Trim();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Password, Salt, Role, EmpID FROM Employee WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPasswordDb = reader["Password"].ToString();
                            string salt = reader["Salt"].ToString();
                            string role = reader["Role"].ToString();
                            string EmpID = reader["EmpID"].ToString();
                            string hashedPasswordInput = AutoGenerator.GenerateHashedPassword(password, salt);
                            if (hashedPasswordDb == hashedPasswordInput)
                            {
                                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    Session["Login"] = EmpID;
                                    Response.Redirect("Home.aspx");
                                }
                                else
                                {
                                    lblerror.Text = "You are not authorized to access this page";
                                    lblerror.CssClass = "text-danger";
                                }
                            }
                            else
                            {
                                lblerror.Text = "Invalid username or password";
                                lblerror.CssClass = "text-danger";
                            }
                        }

                    }
                }
            }
        }
    }
}
