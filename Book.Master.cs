using BitArmory.Turnstile;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class Book : System.Web.UI.MasterPage
    {
        string secret = ConfigurationManager.AppSettings["secretKey"];
        string browserChallengeToken = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["Username"];
            //check if user is logged in
            if (cookie != null && cookie.Value != null)
            {
                Loginlink.Visible = false;
                Registerlink.Visible = false;
                Profilelink.Visible = true;
            }
            else //if user is not logged in
            {
                //show login button
                Loginbtn.Visible = true;
                Registerlink.Visible = true;
                Profilelink.Visible = false;
            }
        }
        public string GetIp()
        {
            return this.Request.UserHostAddress;
        }

        protected async void Loginbtn_Click(object sender, EventArgs e)
        {

            string username = txtLoginUserName.Text.Trim();
            string password = txtLoginPassword.Text.Trim();
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            string clientIp = GetIp();
            string script = "var LoginModal = new bootstrap.Modal(document.getElementById('LoginModal')); LoginModal.show();";


            if (Request.Form["cf-turnstile-response"] != null)
            {
                browserChallengeToken = Request.Form["cf-turnstile-response"];

                if (!string.IsNullOrEmpty(browserChallengeToken))
                {
                    var turnstileApi = new TurnstileService();
                    var verifyResult = await turnstileApi.VerifyAsync(browserChallengeToken, secret, clientIp);

                    if (!verifyResult.IsSuccess)
                    {
                        lblerrorlogin.Text = "There is a problem with the captcha result.";
                        lblerrorlogin.CssClass = "text-danger";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginModal", script, true);
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(username))
                        {
                            lblLoginuser.Text = "Please enter Username";
                            txtLoginUserName.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginModal", script, true);
                        }

                        if (string.IsNullOrEmpty(password))
                        {
                            lblloginpass.Text = "Please enter password";
                            txtLoginPassword.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginModal", script, true);
                        }

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            string loginquery = "SELECT Salt, PasswordHash FROM Customer WHERE Username = @user";
                            using (SqlCommand cmd = new SqlCommand(loginquery, connection))
                            {
                                cmd.Parameters.AddWithValue("@user", username);
                                connection.Open();

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.Read())
                                    {
                                        string storedSalt = dr.GetString(dr.GetOrdinal("Salt"));
                                        string storedHash = dr.GetString(dr.GetOrdinal("PasswordHash"));

                                        string computedHash = AutoGenerator.GenerateHashedPassword(password, storedSalt);

                                        if (computedHash.Equals(storedHash))
                                        {
                                            if (checremenber.Checked)
                                            {
                                                HttpCookie cookie = new HttpCookie("Username");
                                                cookie.Value = username;
                                                cookie.Expires = DateTime.Now.AddDays(30);
                                                Response.Cookies.Add(cookie);
                                            }
                                            else
                                            {
                                                HttpCookie cookie = new HttpCookie("Logincookie");
                                                cookie.Expires = DateTime.Now.AddMinutes(-1);
                                                Response.Cookies.Add(cookie);
                                            }

                                            Response.Redirect("Home.aspx");
                                        }
                                        else
                                        {

                                            lblerrorlogin.Text = "Invalid username or password";
                                            lblerrorlogin.CssClass = "text-danger";
                                        }
                                    }
                                    else
                                    {

                                        lblerrorlogin.Text = "Invalid username or password";
                                        lblerrorlogin.CssClass = "text-danger";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblerrorlogin.Text = " The client response must not be null or empty.";
                    lblerrorlogin.CssClass = "text-danger";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LoginModal", script, true);
                }
            }

        }

    }
}