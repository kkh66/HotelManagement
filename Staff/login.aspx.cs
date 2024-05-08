using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Staff
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string staffname = txtuser.Text.Trim();
            string staffpassword = txtpassword.Text.Trim();
            bool hasEmptyFields = false;

            if (string.IsNullOrEmpty(staffname))
            {
                lbluser.Text = "Username is required";
                txtuser.CssClass += " is-invalid animate__animated animate__headShake";
                lbluser.Text = "username is required";
                lbluser.CssClass = "text-danger";
                txtuser.CssClass += "is-invalid animate__animated animate__headShake";
                hasEmptyFields = true;
            }

            if (string.IsNullOrEmpty(staffpassword))
            {
                txtpassword.CssClass += " is-invalid animate__animated animate__headShake";
                lblLogin.Text = "Password is required";
                lblLogin.Text = "Password is required";
                lblLogin.CssClass = "text-danger";
                txtpassword.CssClass += "is-invalid animate__animated animate__headShake";
                hasEmptyFields = true;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Password, Salt, Role, EmpID FROM Employee WHERE Username = @staffname";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@staffname", staffname);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPasswordDb = reader["Password"].ToString();
                            string salt = reader["Salt"].ToString();
                            string role = reader["Role"].ToString();
                            string EmpID = reader["EmpID"].ToString();
                            string hashedPasswordInput = AutoGenerator.GenerateHashedPassword(staffpassword, salt);
                            if (hashedPasswordDb == hashedPasswordInput)
                            {
                                if (role == "Staff")
                                {
                                    Response.Redirect("Home.aspx");
                                }
                            }
                            else
                            {
                                lblerror.Text = "The username or password might be error";
                                lblerror.CssClass = "text-danger";
                                txtuser.Text = "";
                                txtpassword.Text = "";
                                txtuser.CssClass += "is-invalid animate__animated animate__headShake";
                                txtpassword.CssClass += "is-invalid animate__animated animate__headShake";
                                txtuser.CssClass = "is-invalid animate__animated animate__headShake";
                                txtpassword.CssClass = "is-invalid animate__animated animate__headShake";
                            }
                        }
                    }
                }
            }

        }
    }
}