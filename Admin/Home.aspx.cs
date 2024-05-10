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
          
                if (!IsPostBack)
                {
                    if (Session["Login"] == null)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        BindReservationList();
                    }
                }
            gvReservationList.RowDataBound += gvReservationList_RowDataBound;

        }
        protected void gvReservationList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btnCheckIn = (Button)e.Row.FindControl("btnCheckIn");
                Button btnCheckOut = (Button)e.Row.FindControl("btnCheckOut");

                DataRowView rowView = (DataRowView)e.Row.DataItem;
                int checkInOutStatus = Convert.ToInt32(rowView["Checkinout"]);

                // Check if the reservation is checked in
                if (checkInOutStatus == 1)
                {
                    btnCheckIn.CssClass = "btn disabled btn-danger";
                    btnCheckOut.CssClass = "btn btn-primary";
                }
                else
                {
                    btnCheckOut.CssClass = "btn disabled btn-danger";
                    btnCheckIn.CssClass = "btn btn-primary";
                }
            }
        }
        protected void BindReservationList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT r.ReservationID, c.Username AS CustomerName, ro.RoomType, r.CheckInDate, r.CheckOutDate, r.TotalPrice, r.Checkinout
                                FROM Reservation r
                                INNER JOIN Customer c ON r.CustomerID = c.CustomerID
                                INNER JOIN Room ro ON r.RoomID = ro.RoomId";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                try
                {
                    connection.Open();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        gvReservationList.DataSource = dataTable;
                        gvReservationList.DataBind();
                    }
                    else
                    {
                        Label1.Text = "No reservations found.";
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error: " + ex.Message;
                }
            }
        }
        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;

            string reservationId = gvReservationList.DataKeys[rowIndex].Value.ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Reservation SET Checkinout = 1 WHERE ReservationID = @ReservationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReservationID", reservationId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindReservationList();
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int rowIndex = row.RowIndex;

            string reservationId = gvReservationList.DataKeys[rowIndex].Value.ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Reservation SET Checkinout = 0 WHERE ReservationID = @ReservationID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReservationID", reservationId);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            BindReservationList();
        }
    }

}
