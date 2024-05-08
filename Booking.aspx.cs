using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class Booking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void ddlroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRoomId = Convert.ToInt32(ddlroom.SelectedValue);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
            {
                string query = "SELECT RoomType, Capacity, PricePerNight FROM Room WHERE RoomId = @RoomId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomId", selectedRoomId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Update the labels with the retrieved room details
                    lblroomtype.Text = reader["RoomType"].ToString();
                    lblroomcapacity.Text = reader["Capacity"].ToString();
                    lblpricepernight.Text = string.Format("RM {0:0.00}", reader["PricePerNight"]);
                }

                reader.Close();
            }
        }
        protected void btnbook_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcheckindate.Text))
            {
                lblcheckin.Text = "Please enter the check-in date.";
                return;
            }
            if (string.IsNullOrEmpty(txtcheckoutdate.Text))
            {
                lblcheckin.Text = "Please enter the check-in date.";
                return;
            }
            DateTime checkinDate = DateTime.ParseExact(txtcheckindate.Text, "d-M-yyyy", CultureInfo.InvariantCulture);
            DateTime checkoutDate = DateTime.ParseExact(txtcheckoutdate.Text, "d-M-yyyy", CultureInfo.InvariantCulture);

            TimeSpan timeDiff = checkoutDate - checkinDate;
            int daysDiff = timeDiff.Days;
            int selectedRoomId = Convert.ToInt32(ddlroom.SelectedValue);
            decimal pricePerNight = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
            {
                string query = "SELECT PricePerNight FROM Room WHERE RoomId = @RoomId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomId", selectedRoomId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    pricePerNight = Convert.ToDecimal(result);
                }
            }
            decimal totalPrice = daysDiff * pricePerNight;
            string reservationId = Guid.NewGuid().ToString();
            DateTime currentTime = DateTime.Now;
            DateTime reservationExpiry = currentTime.AddHours(24);
            //string customerId = Request.Cookies["customerId"]?.Value;
            string customerId = "c2902bb3-18c5-41b9-98c8-7a7e1e291211";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
            {
                string query = "INSERT INTO Reservation (ReservationID, RoomID, CustomerID, CheckIndate, CheckOutdate,  TotalPrice, Checkinout, ReservationExpiry) " +
                               "VALUES (@ReservationID, @RoomID, @CustomerID, @CheckIndate, @CheckOutdate, @TotalPrice, 0, @ReservationExpiry)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                cmd.Parameters.AddWithValue("@RoomID", selectedRoomId);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@CheckIndate", checkinDate);
                cmd.Parameters.AddWithValue("@CheckOutdate", checkoutDate);
                cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                cmd.Parameters.AddWithValue("@ReservationExpiry", reservationExpiry);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            HttpCookie cookie = new HttpCookie("Order");
            cookie.Value = reservationId;
            cookie.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Add(cookie);
            Response.Redirect("ConfirmBooking.aspx");

        }
    }
}