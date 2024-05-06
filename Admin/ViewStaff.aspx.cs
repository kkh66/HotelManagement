using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelManagement;

namespace HotelManagement.Admin
{
    public partial class ViewStaff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStaffGrid();

            }

        }
        private void BindStaffGrid()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT EmpID, Username, Name, Role, Age, Gender FROM Employee";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                gvStaff.DataSource = dataTable;
                gvStaff.DataBind();
            }
        }
        protected void gvStaff_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                Button btnDelete = e.Row.FindControl("btnDelete") as Button;

                string empID = gvStaff.DataKeys[e.Row.RowIndex].Value.ToString();
                btnEdit.CommandArgument = empID;
                btnDelete.CommandArgument = empID;
            }
        }


        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string empID = hfEditEmpID.Value;
            string username = txtEditUsername.Text;
            string name = txtEditName.Text;
            string role = txtEditRole.Text;
            string age = txtEditAge.Text;
            string gender = txtEditGender.Text;
            string newPassword = txtEditPassword.Text;
            string salt = AutoGenerator.GenerateSalt();

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                if (!string.IsNullOrEmpty(newPassword))
                {
                    string hashedPassword = AutoGenerator.GenerateHashedPassword(newPassword,salt);
                    string updatePasswordQuery = "UPDATE Employee SET Password = @Password WHERE EmpID = @EmpID";
                    SqlCommand passwordCommand = new SqlCommand(updatePasswordQuery, connection);
                    passwordCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    passwordCommand.Parameters.AddWithValue("@EmpID", empID);
                    passwordCommand.ExecuteNonQuery();
                }

                string updateQuery = "UPDATE Employee SET Name = @Name, Role = @Role, Age = @Age, Gender = @Gender WHERE EmpID = @EmpID";
                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@EmpID", empID);
                command.ExecuteNonQuery();
            }

            BindStaffGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeEditModal();", true);
        }

        protected void btnDeleteConfirm_Click(object sender, EventArgs e)
        {
            string empID = hfDeleteEmpID.Value;

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Employee WHERE EmpID = @EmpID";
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                command.Parameters.AddWithValue("@EmpID", empID);
                command.ExecuteNonQuery();
            }

            BindStaffGrid();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeDeleteModal();", true);
        }

        protected void gvStaff_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}