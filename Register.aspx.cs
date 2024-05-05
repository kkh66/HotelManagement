using BitArmory.Turnstile;
using Stripe.Apps;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace HotelManagement
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetIp()
        {
            return this.Request.UserHostAddress;
        }

        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = txtcustomeruser.Text.Trim();
            string password = txtpassword.Text.Trim();
            string email = txtemail.Text.Trim();
            string browserChallengeToken = null;
            string secret = ConfigurationManager.AppSettings["secretKey"];
            string confirm = txtconfirmpass.Text.Trim();
            int gender = ddlgender.SelectedIndex;
            string dateofbirth = txtDateofBirth.Text.Trim();
            string clientIp = GetIp();
            Guid guid = Guid.NewGuid();
            string CusId = guid.ToString();
            string salt = AutoGenerator.GenerateSalt();
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            bool hasEmptyFields = false;
            bool usernameExists = CheckUsernameExists(connectionString, name);
            bool emailExists = CheckEmailExists(connectionString, email);


            if (Request.Form["cf-turnstile-response"] != null)
            {
                browserChallengeToken = Request.Form["cf-turnstile-response"];

                if (!string.IsNullOrEmpty(browserChallengeToken))
                {
                    var turnstileApi = new TurnstileService();
                    var verifyResult = await turnstileApi.VerifyAsync(browserChallengeToken, secret, clientIp);

                    if (!verifyResult.IsSuccess)
                    {
                        lblerror.Text = "There is a problem with the captcha result.";
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(name))
                        {
                            lblcususe.Text = "Username is required.";
                            txtcustomeruser.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtcustomeruser.CssClass += " is-valid";
                        }

                        if (string.IsNullOrEmpty(email))
                        {
                            labelmail.Text = "Email is required.";
                            txtemail.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (!IsValidEmail(email))
                        {
                            labelmail.Text = "Invalid email format.";
                            txtemail.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtemail.CssClass += " is-valid";
                        }

                        if (string.IsNullOrEmpty(dateofbirth))
                        {
                            lblDateofBirth.Text = "Date of birth is required.";
                            txtDateofBirth.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtDateofBirth.CssClass += " is-valid";
                        }

                        if (hasEmptyFields)
                        {
                            return;
                        }

                        if (usernameExists)
                        {
                            lblerror.Text = "Username already exists!";
                            return;
                        }

                        if (emailExists)
                        {
                            lblerror.Text = "Email already exists!";
                            return;
                        }
                    }
                }
                else
                {
                    lblerror.Text = "Please complete the captcha challenge.";
                }
            }



        }
        private bool CheckUsernameExists(string connectionString, string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        //check email exits or not
        private bool CheckEmailExists(string connectionString, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Customer WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        //check valid email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}