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
            bool usernameExists = CallUse.CheckUsernameExists(connectionString, name);
            bool emailExists = CallUse.CheckEmailExists(connectionString, email);


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
                        lblerror.CssClass = "text-danger";
                    }
                    else
                    {


                        if (emailExists)
                        {
                            lblmail.Text = "Email already exists!";
                            return;
                        }

                        if (string.IsNullOrEmpty(name))
                        {
                            lblcususe.Text = "Username is required.";
                            txtcustomeruser.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (name.Length < 8)
                        {
                            lblcususe.Text = "Username must be at least 8 characters.";
                            txtcustomeruser.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (usernameExists)
                        {
                            lblcususe.Text = "Username already exists!";
                            txtcustomeruser.CssClass += " is-invalid animate__animated animate__headShake";
                            return;
                        }
                        else
                        {
                            txtcustomeruser.CssClass += " is-valid";
                        }

                        if (string.IsNullOrEmpty(password))
                        {
                            lblpass.Text = "Password is required.";
                            txtpassword.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (!CallUse.IsValidPassword(password))
                        {
                            lblpass.Text = "Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number, and 1 special character.";
                            txtpassword.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtpassword.CssClass = "is-valid";
                        }

                        if (string.IsNullOrEmpty(confirm))
                        {
                            lblconfitmpass.Text = "Confirm Password is required.";
                            txtconfirmpass.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (!CallUse.IsValidPassword(confirm))
                        {
                            lblconfitmpass.Text = "Confirm Password must contain at least 8 characters, 1 uppercase, 1 lowercase, 1 number, and 1 special character.";
                            txtconfirmpass.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (!CallUse.passwordmatch(password, confirm))
                        {
                            lblconfitmpass.Text = "Password does not match.";
                            txtpassword.CssClass = txtpassword.CssClass.Replace("is-valid", "");
                            txtpassword.CssClass += " is-invalid animate__animated animate__headShake";
                            txtconfirmpass.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtconfirmpass.CssClass += " is-valid";
                        }


                        if (string.IsNullOrEmpty(email))
                        {
                            lblmail.Text = "Email is required.";
                            txtemail.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else if (!CallUse.IsValidemail(email))
                        {
                            lblmail.Text = "Invalid email format.";
                            txtemail.CssClass += " is-invalid animate__animated animate__headShake";
                            hasEmptyFields = true;
                        }
                        else
                        {
                            txtemail.CssClass += " is-valid";
                        }

                        if (hasEmptyFields)
                        {
                            return;
                        }
                        string hashedPassword = AutoGenerator.GenerateHashedPassword(password, salt);
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand command = new SqlCommand("INSERT INTO Customer (CustomerID, Username, Password, Email, Gender, DateOfBirth, Salt) VALUES (@CustomerID, @Username, @Password, @Email, @Gender, @DateOfBirth, @Salt)", connection))
                            {
                                command.Parameters.AddWithValue("@CustomerID", CusId);
                                command.Parameters.AddWithValue("@Username", name);
                                command.Parameters.AddWithValue("@Password", hashedPassword);
                                command.Parameters.AddWithValue("@Email", email);
                                command.Parameters.AddWithValue("@Gender", gender);
                                command.Parameters.AddWithValue("@DateOfBirth", dateofbirth);
                                command.Parameters.AddWithValue("@Salt", salt);
                                command.ExecuteNonQuery();
                            }

                        }
                        lblerror.Text = "Registration successful!";
                        btnSubmit.CssClass = "loader";
                        btnSubmit.Text = "Loading...";
                    }
                }
                else
                {
                    lblerror.Text = "Please complete the captcha challenge.";
                }
            }

        }

    }
}