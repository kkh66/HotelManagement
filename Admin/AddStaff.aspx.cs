using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelManagement;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HotelManagement.Admin
{
    public partial class AddStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnaddstaff_Click(object sender, EventArgs e)
        {
            string userName = txtStaffUserName.Text.Trim();
            string name = txtStaffName.Text.Trim();
            string age = txtstaffage.Text.Trim();
            string password = txtstaffpass.Text.Trim();
            string gender = ddlStaffgen.SelectedValue;
            string role = DdlRole.SelectedValue;
            const int MAX_AGE = 100;
            int ageValue;
            Guid newGuid = Guid.NewGuid();
            string uuidString = newGuid.ToString();
            AutoGenerator autoGenerator = new AutoGenerator();
            bool hasEmptyFields = false;
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;

            bool usernameExists = CheckUsernameExists(connectionString, userName);
            if (usernameExists)
            {
  
                lblerror.Text = "username exist！";
                return;
            }

            // Check if any of the textboxes are null or empty
            if (string.IsNullOrEmpty(userName))
            {
                lblwarnuser.Text = "Username is required.";
                txtStaffUserName.CssClass += " is-invalid animate__animated animate__headShake";
                hasEmptyFields = true;
            }
            else
            {
                txtStaffUserName.CssClass += " is-valid";
            }

            if (string.IsNullOrEmpty(name))
            {
                txtStaffName.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnname.Text = "Name is required.";
                hasEmptyFields = true;
            }
            else
            {
                txtStaffName.CssClass += " is-valid";
            }

            if (string.IsNullOrEmpty(age))
            {
                txtstaffage.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnage.Text = "Age is required.";
                hasEmptyFields = true;
            }
            else if (!int.TryParse(age, out ageValue))
            {
                txtstaffage.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnage.Text = "Age must be a valid number.";
                hasEmptyFields = true;
            }
            else if (ageValue < 0)
            {
                txtstaffage.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnage.Text = "Age cannot be negative.";
                hasEmptyFields = true;
            }
            else if (ageValue > MAX_AGE)
            {
                txtstaffage.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnage.Text = "Age cannot exceed " + MAX_AGE;
                hasEmptyFields = true;
            }
            else
            {
                txtstaffage.CssClass += " is-valid";
            }

            if (string.IsNullOrEmpty(password))
            {
                txtstaffpass.CssClass += " is-invalid animate__animated animate__headShake";
                lblwarnpass.Text = "Password is required.";
                hasEmptyFields = true;
            }
            else if (!autoGenerator.IsValidPassword(password))
            {
                lblwarnpass.Text = "Invalid password. It must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 8 characters long.";
                return;
            }
            else
            {
                txtstaffpass.CssClass += " is-valid";
            }

            if (DdlRole.SelectedValue == "--Selected Role--")
            {
                lblWarnRole.Text = "Please Select a role.";
            }

            if (ddlStaffgen.SelectedValue == "--Selected Gender--")
            {
                lblWarnGen.Text = "Please Select a gender";
            }
            if (hasEmptyFields)
            {
                return;
            }


            string salt = AutoGenerator.GenerateSalt();
            string hashedPassword = AutoGenerator.GenerateHashedPassword(password, salt);

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
            {
                // Check if the username already exists in the database. If it does, display an error message. Otherwise, insert the new employee into the database.
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Employee (EmpID, Password, Name, Age, Gender, Role, Username, Salt) VALUES (@EmpID, @Password, @Name, @Age, @Gender, @Role, @Username, @Salt)", connection))
                {
                    command.Parameters.AddWithValue("@EmpID", uuidString);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Age", age);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@Role", role);
                    command.Parameters.AddWithValue("@Username", userName);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.ExecuteNonQuery();
                }

            }
        }
        private bool CheckUsernameExists(string connectionString, string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Employee WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

    }
}