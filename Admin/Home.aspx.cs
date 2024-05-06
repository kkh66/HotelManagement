using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindReservationList();
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

            checkinout = (checkinout == 0) ? 1 : 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Reservation SET Checkinout = @Checkinout WHERE ReservationID = @ReservationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Checkinout", checkinout);
                command.Parameters.AddWithValue("@ReservationID", reservationId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindReservationList();
        }
    }

}
