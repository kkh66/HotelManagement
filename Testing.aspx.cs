using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace HotelManagement
{
    public partial class Testing : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string otp = txtOtp.Text.Trim();
            string newPassword = txtpassword.Text.Trim();
            string confirmPassword = txtconfirm.Text.Trim();

            bool checkmatchpassword = CallUse.passwordmatch(newPassword, confirmPassword);
            // Check if the OTP is correct and not expired
            if (ValidateOTP(otp) && !IsOTPExpired())
            {
                // Check if the new password and confirm password match
                if (checkmatchpassword)
                {
                    // Generate a salt and hash the new password
                    string salt = AutoGenerator.GenerateSalt();
                    string hashedPassword = AutoGenerator.GenerateHashedPassword(newPassword, salt);

                    // Update the user's password in the database
                    UpdatePassword(txtusername.Text.Trim(), hashedPassword, salt);

                    // Display a success message
                    lblerror.Text = "Password reset successfully.";
                }
                else
                {
                    // Display an error message
                    lblerror.Text = "New password and confirm password do not match.";
                }
            }
            else
            {
                // Display an error message
                lblerror.Text = "Invalid or expired OTP.";
            }
        }

        protected void BtnSendOtp_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string username = txtusername.Text.Trim();
            string email = txtmail.Text.Trim();

            bool isValidUser = CallUse.CheckUsernameExists(connectionString, username);
            bool isValidemail = CallUse.CheckEmailExists(connectionString, email);

            if (isValidUser && isValidemail)
            {
                string otp = AutoGenerator.GenerateOTP();
                SendOTPEmail(email, otp);

                divUsername.Visible = false;
                divEmail.Visible = false;

                divOtp.Visible = true;
                divPassword.Visible = true;
                divConfirm.Visible = true;
                BtnSubmit.Visible = true;

                Session["OTPExpiration"] = DateTime.Now.AddMinutes(15);
            }
            else
            {
                lblerror.Text = "Invalid username or email.";
            }
        }
        private bool ValidateOTP(string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string username = txtusername.Text.Trim();

            return CallUse.CheckOTP(connectionString, username, otp);
        }
        private bool IsOTPExpired()
        {
            if (Session["OTPExpiration"] != null)
            {
                DateTime otpExpiration = (DateTime)Session["OTPExpiration"];
                return DateTime.Now > otpExpiration;
            }
            return true;
        }
        private void UpdatePassword(string username, string hashedPassword, string salt)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE Customer SET Password = @HashedPassword, Salt = @Salt WHERE Username = @Username";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HashedPassword", hashedPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@Username", username);

                    command.ExecuteNonQuery();
                }
            }
        }
        private void SendOTPEmail(string email, string otp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(email);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["email"]);
                mail.Subject = "Reset Password OTP";
                string Body = "Your OTP for resetting is " + otp
                + ". This code will expires in 15 minutes.";
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["password"]);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during email sending
                // You can log the exception or display an error message to the user
                lblerror.Text = "Error sending OTP email: " + ex.Message;
            }
        }
    }
}