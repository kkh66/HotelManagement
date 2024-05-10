using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie login = Request.Cookies["CustomerID"];
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
                                BindOrderHistory();
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
        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            string title = txtReviewTitle.Text;
            string comment = txtReviewComment.Text;
            string reviewId = Guid.NewGuid().ToString();
            HttpCookie login = Request.Cookies["CustomerID"];
            string CustomerID = login.Value.ToString();
            int roomId = Convert.ToInt32(hiddenRoomId.Value);

            SaveReviewToDatabase(reviewId, title, comment, roomId, CustomerID);

            txtReviewTitle.Text = string.Empty;
            txtReviewComment.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "closeReviewModal", "closeReviewModal();", true);
        }
        private void SaveReviewToDatabase(string reviewId, string title, string comment, int roomId, string CustomerID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string query = "INSERT INTO [dbo].[Review] (ReviewID, Title, Comment, RoomId, CustomerID) VALUES (@ReviewID, @Title, @Comment, @RoomId, @CustomerID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReviewID", reviewId);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Comment", comment);
                command.Parameters.AddWithValue("@RoomId", roomId);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void BindOrderHistory()
        {
            HttpCookie login = Request.Cookies["CustomerID"];
            string CustomerID = login.Value.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string query = @"
                SELECT 
                    R.ReservationID,
                    R.RoomID,
                    R.CheckIndate,
                    R.CheckOutdate,
                    R.TotalPrice,
                    Ro.RoomType
                FROM 
                    [dbo].[Reservation] R
                INNER JOIN 
                    [dbo].[Room] Ro ON R.RoomID = Ro.RoomId
                WHERE 
                    R.CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvOrderHistory.DataSource = dataTable;
                    gvOrderHistory.DataBind();

                    // 设置 DataKeys
                    gvOrderHistory.DataKeyNames = new string[] { "ReservationID" };
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void gvOrderHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowReview")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOrderHistory.Rows[index];
                string reservationID = gvOrderHistory.DataKeys[index].Value.ToString();
                ShowPaymentInfo(reservationID);
            }
        }

        private void ShowPaymentInfo(string reservationID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string query = @"
                SELECT 
                    PaymentMethod,
                    PaymentAmount,
                    PaymentStatus,
                    Reason
                FROM 
                    [dbo].[Payment]
                WHERE 
                    ReservationID = @ReservationID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ReservationID", reservationID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string paymentMethod = reader["PaymentMethod"].ToString();
                        decimal paymentAmount = Convert.ToDecimal(reader["PaymentAmount"]);
                        int paymentStatus = Convert.ToInt32(reader["PaymentStatus"]);
                        string reason = reader["Reason"].ToString();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showPaymentModal", "showPaymentInfo('" + paymentMethod + "', '" + paymentAmount + "', '" + paymentStatus + "', '" + reason + "');", true);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void btnuppass_Click(object sender, EventArgs e)
        {
            string oldPassword = txtoldpass.Text;
            string newPassword = txtnewpass.Text;
            string confirmPassword = txtconpass.Text;
            HttpCookie login = Request.Cookies["CustomerID"];
            string CustomerID = login.Value.ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string sqlQuery = "SELECT Salt, Password FROM Customer WHERE CustomerID = @CustomerID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string saltFromDB = reader["Salt"].ToString();
                        string hashedPasswordFromDB = reader["Password"].ToString();

                        string hashedOldPassword = AutoGenerator.GenerateHashedPassword(oldPassword, saltFromDB);
                        if (hashedOldPassword != hashedPasswordFromDB)
                        {
                            lbloldpass.Text = "Incorrect old password";
                            return;
                        }

                        if (newPassword != confirmPassword)
                        {
                            lblconpass.Text = "Passwords do not match";
                            return;
                        }

                        string newSalt = AutoGenerator.GenerateSalt();
                        string newHashedPassword = AutoGenerator.GenerateHashedPassword(newPassword, newSalt);
                        string updateSqlQuery = "UPDATE Customer SET Password = @Password, Salt = @Salt WHERE CustomerID = @CustomerID";
                        SqlCommand updateCommand = new SqlCommand(updateSqlQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Password", newHashedPassword);
                        updateCommand.Parameters.AddWithValue("@Salt", newSalt);
                        updateCommand.Parameters.AddWithValue("@CustomerID", CustomerID);
                        int rowsAffected = updateCommand.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
            }
        }


        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text;
            string newEmail = txtNewEmail.Text;
            HttpCookie login = Request.Cookies["CustomerID"];
            string CustomerID = login.Value.ToString();
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string query = "UPDATE Customer SET Username = @Username, Email = @Email WHERE CustomerID = @CustomerID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", newUsername);
                    command.Parameters.AddWithValue("@Email", newEmail);
                    command.Parameters.AddWithValue("@CustomerID", CustomerID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}