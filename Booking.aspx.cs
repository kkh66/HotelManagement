using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
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
            if (!IsPostBack)
            {
                HttpCookie login = Request.Cookies["Customerid"];
                if (login != null && login.Value != null)
                {
                    string logincookie = login.Value.ToString();
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                    {
                        connection.Open();
                        string querylogin = "SELECT COUNT(*) FROM Customer WHERE CustomerID = @CustomerID";
                        using (SqlCommand command = new SqlCommand(querylogin, connection))
                        {
                            command.Parameters.AddWithValue("@CustomerID", logincookie);
                            int count = (int)command.ExecuteScalar();
                            if (count == 0)
                            {
                                Response.Redirect("401page.aspx");
                            }
                            else
                            {
                                HttpCookie cookie = Request.Cookies["Room"];
                                if (cookie != null && cookie.Value != null)
                                {
                                    string roomValue = cookie.Value;
                                    int roomId = int.Parse(roomValue);
                                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                                    {
                                        SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[Room] WHERE [RoomId] = @RoomId", conn);
                                        cmd.Parameters.AddWithValue("@RoomId", roomId);
                                        conn.Open();
                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {

                                                lblroomtype.Text = reader["RoomType"].ToString();
                                                lblroomcapacity.Text = reader["Capacity"].ToString();
                                                lblpricepernight.Text = string.Format("RM {0:0.00}", reader["PricePerNight"]);
                                                lbldecription.Text = reader["Description"].ToString();
                                                HttpCookie ddlroom = Request.Cookies["Room"];
                                                ddlroom.Value = roomId.ToString();
                                                Response.Cookies.Add(ddlroom);
                                            }
                                            else
                                            {
                                                Response.Redirect("401page.aspx");
                                            }

                                        }

                                        string queryreview = "SELECT r.Title, r.Comment, c.Username " + "FROM Review r " + "INNER JOIN Customer c ON r.CustomerID = c.CustomerID " + "WHERE r.RoomId = @RoomId";

                                        SqlCommand reviewcommand = new SqlCommand(queryreview, conn);
                                        reviewcommand.Parameters.AddWithValue("@RoomId", roomId);

                                        SqlDataReader reader1 = reviewcommand.ExecuteReader();
                                        rptReviews.DataSource = reader1;
                                        rptReviews.DataBind();
                                    }

                                }
                                else
                                {
                                    Response.Redirect("401page.aspx");
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("401page.aspx");
                }
            }
        }
        protected void ddlroom_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedRoomId = Convert.ToInt32(ddlroom.SelectedValue);
            HttpCookie getroomcookie = Request.Cookies["Room"];
            getroomcookie.Value = selectedRoomId.ToString();
            getroomcookie.Expires = DateTime.Now.AddMinutes(15);
            Response.Cookies.Set(getroomcookie);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
            {
                string query = "SELECT * FROM Room WHERE RoomId = @RoomId";
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
                    //img1.ImageUrl = reader["RoomImage"].ToString();
                    //img2.ImageUrl = reader["Roomenviroment"].ToString();

                }

                reader.Close();
                string query1 = "SELECT r.Title, r.Comment, c.Username " +
                    "FROM Review r " +
                    "INNER JOIN Customer c ON r.CustomerID = c.CustomerID " +
                    "WHERE r.RoomId = @RoomId";

                SqlCommand command = new SqlCommand(query1, conn);
                command.Parameters.AddWithValue("@RoomId", selectedRoomId);

                SqlDataReader reader1 = command.ExecuteReader();
                rptReviews.DataSource = reader1;
                rptReviews.DataBind();
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
            string customerId = Request.Cookies["customerId"]?.Value;
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
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
            Response.Redirect("ConfirmBooking.aspx");

        }
    }
}