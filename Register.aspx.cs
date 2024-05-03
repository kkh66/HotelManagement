using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BitArmory.Turnstile;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

namespace HotelManagement
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Get the user's IP address
        public string GetIp()
        {
            return this.Request.UserHostAddress;
        }
        //Hashing the password
        public string Hasing(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }
        protected async void btnSubmit_Click(object sender, EventArgs e)
        {
            string secret = ConfigurationManager.AppSettings["secretKey"];
            string clientIp = GetIp();
            string browserChallengeToken = null;
            string usrname = txtRegUserName.Text.Trim();
            string email = txtRegMail.Text.Trim();
            string password = txtRegPassword.Text.Trim();
            string confirmpassword = txtConfirmPassword.Text.Trim();
            string Gender = DdlGender.Text;
            string Age = RegDateofBirth.Text.Trim();
            Guid guid = Guid.NewGuid();
            string CusId = guid.ToString();
            string Register = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString).ConnectionString;
            string insertquery = "INSERT INTO Customer (CustomerID, Username, Password, Email, Gender, DateOfBirth) VALUES (@CustomerID, @Username, @Password, @Email, @Gender, @DateOfBirth)";

            Regex passregex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$");
            Regex emailregex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (Request.Form["cf-turnstile-response"] != null)
            {
                browserChallengeToken = Request.Form["cf-turnstile-response"];

                if (!string.IsNullOrEmpty(browserChallengeToken))
                {
                    var turnstileApi = new TurnstileService();
                    var verifyResult = await turnstileApi.VerifyAsync(browserChallengeToken, secret, clientIp);

                    if (!verifyResult.IsSuccess)
                    {
                        lblRegError.Text = "There is a problem with the captcha result.";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(usrname))
                        {
                            checkname.Text = "Please enter your name";
                            txtRegUserName.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                        }
                        else if (usrname.Length < 8)
                        {
                            checkname.Text = "Username must be at least 8 characters long";
                            txtRegUserName.CssClass = "form-control is-invalid  animate__animated animate__headShake";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(password))
                            {
                                txtRegUserName.CssClass = "form-control is-valid";
                                checkregpassword.Text = "Please Enter Your Password";
                                txtRegPassword.CssClass = "form-control is-invalid animate__animated animate__headShake";
                            }
                            else if (!passregex.IsMatch(password))
                            {
                                txtRegUserName.CssClass = "form-control is-valid";
                                checkregpassword.Text = "Should have at least one lower case, one upper case, one number, one special character and minimum 8 characters.";
                                txtRegPassword.CssClass = "form-control is-invalid animate__animated animate__headShake";
                                txtRegPassword.Text = "";
                            }
                            else if (password != confirmpassword)
                            {
                                txtRegUserName.CssClass = "form-control is-valid";
                                txtRegPassword.CssClass = "form-control is-valid";
                                checkregpassword.Text = "Password and Confirm Password should be same";
                                txtConfirmPassword.CssClass = "form-control is-invalid animate__animated animate__headShake";
                                txtConfirmPassword.Text = "";
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(email))
                                {
                                    txtRegUserName.CssClass = "form-control is-valid";
                                    txtRegPassword.CssClass = "form-control is-valid";
                                    checkRegmail.Text = "Please Enter Your Email";
                                    txtRegMail.CssClass = "form-control is-invalid animate__animated animate__headShake";
                                }
                                else if (!emailregex.IsMatch(email))
                                {
                                    txtRegUserName.CssClass = "form-control is-valid";
                                    txtRegPassword.CssClass = "form-control is-valid";
                                    checkRegmail.Text = "Your email format have problem";
                                    txtRegMail.CssClass = "form-control is-invalid animate__animated animate__headShake";
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(Gender) || Gender == "--Selected Gender--")
                                    {
                                        txtRegUserName.CssClass = "form-control is-valid";
                                        txtRegPassword.CssClass = "form-control is-valid";
                                        txtRegMail.CssClass = "form-control is-valid";
                                        CheckRegGen.Text = "<br/> Please choose the Gender";
                                        DdlGender.CssClass = "selectpicker is-invalid animate__animated animate__headShake";
                                        DdlGender.CssClass = "selectpicker";
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(Age))
                                        {
                                            txtRegUserName.CssClass = "form-control is-valid";
                                            txtRegPassword.CssClass = "form-control is-valid";
                                            txtRegMail.CssClass = "form-control is-valid";
                                            checkRegDob.Text = "Please Select Your Date of Birth";
                                            RegDateofBirth.CssClass = "form-control is-invalid animate__animated animate__headShake";
                                        }
                                        else
                                        {
                                            try
                                            {
                                                string hPassword = Hasing(password, new SHA256CryptoServiceProvider());
                                                using (SqlConnection con = new SqlConnection(Register))
                                                {
                                                    con.Open();
                                                    SqlCommand cmd = new SqlCommand(insertquery, con);
                                                    cmd.Parameters.AddWithValue("@CustomerID", CusId);
                                                    cmd.Parameters.AddWithValue("@Username", usrname);
                                                    cmd.Parameters.AddWithValue("@Password", hPassword);
                                                    cmd.Parameters.AddWithValue("@Email", email);
                                                    cmd.Parameters.AddWithValue("@Gender", Gender);
                                                    cmd.Parameters.AddWithValue("@DateOfBirth", Age);
                                                    cmd.ExecuteNonQuery();
                                                    con.Close();
                                                }
                                                MailMessage mail = new MailMessage();
                                                mail.To.Add(email);
                                                mail.From = new MailAddress(ConfigurationManager.AppSettings["email"]);
                                                mail.Subject = "Registration Successful";
                                                string Body = "Hi " + usrname + ",<br/><br/>You have successfully registered with us.<br/><br/>Thank you for registering with us.<br/><br/>Regards,<br/>KNN Hotel Management";
                                                mail.Body = Body;
                                                mail.IsBodyHtml = true;
                                                SmtpClient smtp = new SmtpClient();
                                                smtp.Host = ConfigurationManager.AppSettings["Host"];
                                                smtp.Port = 587;
                                                smtp.UseDefaultCredentials = false;
                                                smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["email"], ConfigurationManager.AppSettings["password"]);
                                                smtp.EnableSsl = true;
                                                smtp.Send(mail);
                                                Response.AddHeader("REFRESH", "3;URL=Home.aspx");
                                            }
                                            catch
                                            {
                                                lblRegError.Text = "Error in the database connection or when sending email";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblRegError.Text = "The client response must not be null or empty.";
                }
            }
            else
            {
                lblRegError.Text = "The 'token' key is not present in the form data.";
            }
        }
    }
}