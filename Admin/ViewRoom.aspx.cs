using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace HotelManagement.Admin
{
    public partial class ViewRoom : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoomList();
            }
        }
        protected void btnCalledit_Click(object sender, EventArgs e)
        {
            Button btnEdit = (Button)sender;
            string roomId = btnEdit.CommandArgument;

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT RoomType, Capacity, Description, PricePerNight, RoomNumber FROM Room WHERE RoomId = @RoomId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtEditRoomType.Text = reader["RoomType"].ToString();
                    txtCapacity.Text = reader["Capacity"].ToString();
                    txtDescription.Text = reader["Description"].ToString();
                    txtPricePerNight.Text = reader["PricePerNight"].ToString();
                    txtRoomNumber.Text = reader["RoomNumber"].ToString();

                    hfEditRoomId.Value = roomId;
                    string script = "var editModal = new bootstrap.Modal(document.getElementById('editModal')); editModal.show();";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowEditModal", script, true);
                }
                reader.Close();
            }
        }

        private void BindRoomList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Room";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptRooms.DataSource = dataTable;
                rptRooms.DataBind();
            }
        }
        //Edit Room Details Button
        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int roomId = Convert.ToInt32(hfEditRoomId.Value);
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string roomType = txtEditRoomType.Text.Trim();
            int capacity = Convert.ToInt32(txtCapacity.Text.Trim());
            string description = txtDescription.Text.Trim();
            decimal pricePerNight = Convert.ToDecimal(txtPricePerNight.Text.Trim());
            int roomNumber = Convert.ToInt32(txtRoomNumber.Text.Trim());

            if (string.IsNullOrEmpty(roomType))
            {
                lblRoomtype.Text = "Room Type is required";
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Room SET RoomType = @RoomType, Capacity = @Capacity, Description = @Description, PricePerNight = @PricePerNight, RoomNumber = @RoomNumber WHERE RoomId = @RoomId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomType", roomType);
                command.Parameters.AddWithValue("@Capacity", capacity);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@PricePerNight", pricePerNight);
                command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                command.Parameters.AddWithValue("@RoomId", roomId);
                connection.Open();
                command.ExecuteNonQuery();
            }

            BindRoomList();
        }
        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int roomId = Convert.ToInt32(hfDeleteRoomId.Value);

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Room WHERE RoomId = @RoomId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindRoomList();
        }


    }
}