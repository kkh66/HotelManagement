using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Staff
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindReservationList();
            ClientScriptManager cs = Page.ClientScript;
            foreach (GridViewRow row in gvReservationList.Rows)
            {
                Button btnCheckInOut = (Button)row.FindControl("btnCheckInOut");
                if (btnCheckInOut != null)
                {
                    cs.RegisterForEventValidation(btnCheckInOut.UniqueID);
                }
            }
        }
        private void BindReservationList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT r.ReservationID, c.Username AS CustomerName, rm.RoomType, r.CheckInDate, r.CheckOutDate, r.TotalPrice, r.ReservedRoomNumber, r.Pax
FROM Reservation r
JOIN Customer c ON r.CustomerID = c.CustomerID
JOIN Room rm ON r.RoomId = rm.RoomId";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                gvReservationList.DataSource = dataTable;
                gvReservationList.DataBind();
            }
        }
        protected void btnCheckInOut_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;

            // Check if the DataKeys collection has any keys
            if (gvReservationList.DataKeys.Count > row.RowIndex && row.RowIndex >= 0)
            {
                string reservationId = gvReservationList.DataKeys[row.RowIndex].Value.ToString();

                int checkinout = 0;
                string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Checkinout FROM Reservation WHERE ReservationID = @ReservationID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ReservationID", reservationId);

                    connection.Open();
                    checkinout = Convert.ToInt32(command.ExecuteScalar());
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Reservation SET Checkinout = @Checkinout WHERE ReservationID = @ReservationID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Checkinout", checkinout);
                    command.Parameters.AddWithValue("@ReservationID", reservationId);

                    connection.Open();

                    if (checkinout == 0)
                    {
                        // Check-in scenario
                        command.Parameters["@Checkinout"].Value = 1;
                        btn.Text = "Check-Out";
                    }
                    else
                    {
                        // Check-out scenario
                        command.Parameters["@Checkinout"].Value = 0;
                        btn.Text = "Check-In";
                    }

                    command.ExecuteNonQuery();
                }

                // Rebind the GridView after the update
                BindReservationList();
            }
        }
    }
}