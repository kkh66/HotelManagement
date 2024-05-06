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
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
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
        //hashing function
        public string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        //generate OTP
        protected string GenerateOTP()
        {
            string characters = "1234567890";
            string otp = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
        protected async void Loginbtn_Click(object sender, EventArgs e)
        {

            /*string username = txtLoginUserName.Text;
            string password = txtLoginPassword.Text;
            string clientIp = GetIp();
            string script = "window.onload = function() {{ myModal.show(); }};";

            if (Request.Form["cf-turnstile-response"] != null)
            {
                browserChallengeToken = Request.Form["cf-turnstile-response"];

                if (!string.IsNullOrEmpty(browserChallengeToken))
                {
                    var turnstileApi = new TurnstileService();
                    var verifyResult = await turnstileApi.VerifyAsync(browserChallengeToken, secret, clientIp);

                    if (!verifyResult.IsSuccess)
                    {
                        lblLoginerror.Text = "There is a problem with the captcha result.";
                        lblLoginerror.CssClass = "text-danger";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
                    }
                    else
                    {

                        if (username == null && password == null)
                        {
                            lblLoginerror.Text = "Please enter username and password";
                            lblLoginerror.CssClass = "text-danger";
                            txtLoginUserName.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                            checklopass.Text = "Please enter password";
                            txtLoginPassword.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
                        }
                        else if (password == null)
                        {
                            checklopass.Text = "Please enter password";
                            txtLoginPassword.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
                        }
                        else
                        {
                            string login = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString).ConnectionString;
                            string loginquery = "select * from Customer where Username = @user AND password = @pass";
                            SqlConnection con = new SqlConnection(login);
                            SqlCommand cmd = new SqlCommand(loginquery, con);
                            cmd.Parameters.AddWithValue("@user", username);
                            cmd.Parameters.AddWithValue("@pass", ComputeHash(password, new SHA256CryptoServiceProvider()));
                            con.Open();
                            SqlDataReader dr = cmd.ExecuteReader();
                            //check if user exists
                            if (dr.HasRows)
                            {
                                if (checremenber.Checked)
                                {
                                    //30 days cookie
                                    HttpCookie cookie = new HttpCookie("Username");
                                    cookie.Value = username;
                                    Response.Cookies.Add(cookie);
                                    cookie.Expires = DateTime.Now.AddDays(30);
                                    Response.Cookies.Add(cookie);
                                    Response.Redirect("Home.aspx");
                                }
                                else
                                {
                                    HttpCookie cookie = new HttpCookie("Logincookie");
                                    cookie.Expires = DateTime.Now.AddMinutes(-1);
                                    Response.Cookies.Add(cookie);
                                }

                            }
                            else
                            {
                                lblLoginerror.Text = "Invalid username or password";
                                lblLoginerror.CssClass = "text-danger";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
                            }
                        }

                    }
                }
                else
                {
                    lblLoginerror.Text = " The client response must not be null or empty.";
                    lblLoginerror.CssClass = "text-danger";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
                }
            }
            else
            {
                lblLoginerror.Text = "The 'token' key is not present in the form data.";
                lblLoginerror.CssClass = "text-danger";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
            }*/
        }

    }
}