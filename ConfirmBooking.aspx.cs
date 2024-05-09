using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using Stripe;

namespace HotelManagement
{
    public partial class ConfirmBooking : System.Web.UI.Page
    {
        private static string PaypalclientId = System.Configuration.ConfigurationManager.AppSettings["PaypalClientID"];
        private static string PaypalclientSecret = System.Configuration.ConfigurationManager.AppSettings["PaypalSecretkey"];

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                /*HttpCookie orderCookie = Request.Cookies["Order"];
                 if (orderCookie == null)
                {
                    Response.Redirect("Home.aspx");
                    return;
                }*/

                string reservationId = "01853fb8-3ae6-4cdd-b6cc-d80e4ec0b043";

                if (!string.IsNullOrEmpty(reservationId))
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                    {
                        string query = "SELECT * FROM Reservation WHERE ReservationID = @ReservationID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            lblcustomername.Text = "Reservation ID: " + reader["ReservationID"].ToString();
                            lblroomtype.Text = reader["RoomID"].ToString();
                            lblcheckin.Text = "Check-in Date: " + ((DateTime)reader["CheckIndate"]).ToString("dd-MM-yyyy");
                            lblcheckout.Text = "Check-out Date: " + ((DateTime)reader["CheckOutdate"]).ToString("dd-MM-yyyy");
                            lbltotalprice.Text = reader["TotalPrice"].ToString();
                            ReservedRoomNumber.Text = "Reserved Room Number: " + reader["Checkinout"].ToString();
                        }

                        reader.Close();
                    }
                }
                string totaluse = GetTotalAmountFromDatabase();
                decimal paymentAmount = decimal.Parse(totaluse);

                if (Request.QueryString != null && Request.QueryString.Count != 0)
                {
                    string approvalToken = Request.QueryString["token"];
                    var response = await captureOrder(approvalToken);

                    if (response != null)
                    {
                        Order result = response.Result<Order>();
                        lblpaypal.Text = result.Status;
                        if (result.Status.ToString() == "COMPLETED")
                        {
                            UpdateDatabaseWithPaymentDetails(approvalToken, "Paypal", paymentAmount);
                            Response.Redirect("PaymentSuccesfull.aspx");
                        }
                        else
                        {
                            Response.Redirect("PaymentFail.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("PaymentFail.aspx");
                    }
                }

            }
            string stripeSecretKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.SetApiKey(stripeSecretKey);
            string totalAmount = GetTotalAmountFromDatabase();
            int amountInCents = (int)(Convert.ToDecimal(totalAmount) * 100);
            decimal paymentstripe = decimal.Parse(totalAmount);
            string stripeToken = Request.Form["stripeToken"];
            var stripeChargeService = new ChargeService();
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = amountInCents,
                Currency = "myr",
                Description = "Charge for HotelRoom",
                Source = stripeToken,
            };

            try
            {
                var charge = stripeChargeService.Create(chargeOptions);
                UpdateDatabaseWithPaymentDetails(stripeToken, "Stripe", paymentstripe);
                Response.Redirect("PaymentSuccesfull.aspx", false);
            }
            catch (StripeException ex)
            {
                System.Diagnostics.Debug.WriteLine("Payment failed with error: " + ex.Message);
            }

        }

        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlPaymentMethod.SelectedValue;

            if (selectedValue == "CreditCard")
            {
                pnlCreditCard.Visible = true;
                pnlPayPal.Visible = false;
            }
            else if (selectedValue == "PayPal")
            {
                pnlCreditCard.Visible = false;
                pnlPayPal.Visible = true;
            }
        }

        private string FormatAmountForPayPal(string totalAmount)
        {
            decimal amount = decimal.Parse(totalAmount);
            return amount.ToString("0.00");
        }

        private string GetTotalAmountFromDatabase()
        {
            if (Request == null || Request.Cookies["Order"] == null)
            {
                return "";
            }
            //string reservationId = Request.Cookies["Order"].Value;
            string reservationId = "01853fb8-3ae6-4cdd-b6cc-d80e4ec0b043";
            string totalAmount = "";

            if (!string.IsNullOrEmpty(reservationId))
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                {
                    string query = "SELECT TotalPrice FROM Reservation WHERE ReservationID = @ReservationID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                    conn.Open();
                    totalAmount = cmd.ExecuteScalar()?.ToString();
                }
            }

            return totalAmount;
        }

        public async static Task<PayPalHttp.HttpResponse> captureOrder(string token)
        {
            var request = new OrdersCaptureRequest(token);
            request.RequestBody(new OrderActionRequest());
            var environment = new SandboxEnvironment(PaypalclientId, PaypalclientSecret);
            var response = await (new PayPalHttpClient(environment).Execute(request));

            return response;
        }

        public static string GetApprovalUrl(Order result)
        {
            if (result.Links != null)
            {
                LinkDescription approvalLink = result.Links.Find(link => link.Rel.ToLower() == "approve");
                if (approvalLink != null)
                {
                    return approvalLink.Href;
                }
            }

            return "https://www.example.com";
        }

        protected async void BtnPaypal_Click(object sender, EventArgs e)
        {
            string totalAmount = GetTotalAmountFromDatabase();
            string formattedAmount = FormatAmountForPayPal(totalAmount);
            string approvalUrl = await createOrder(formattedAmount);
            Response.Redirect(approvalUrl);
        }
        public async static Task<string> createOrder(string formattedAmount)
        {
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
        {
            new PurchaseUnitRequest()
            {
                AmountWithBreakdown = new AmountWithBreakdown()
                {
                    CurrencyCode = "MYR",
                    Value = formattedAmount
                }
            }
        },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://localhost:44384/ConfirmBooking.aspx",
                    CancelUrl = "https://localhost:44384/ConfirmBooking.aspx"
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            var environment = new SandboxEnvironment(PaypalclientId, PaypalclientSecret);
            var response = await (new PayPalHttpClient(environment).Execute(request));
            Order result = response.Result<Order>();

            if (result != null && result.Status.ToLower() == "created")
            {
                return GetApprovalUrl(result);
            }
            else
            {
                return "PaymentFail.aspx";
            }
        }
        private void UpdateDatabaseWithPaymentDetails(string paymentId, string paymentMethod, decimal PaymentAmount)
        {
            string reservationId = Request.Cookies["Order"].Value;

            if (!string.IsNullOrEmpty(reservationId))
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                {
                    string query = "INSERT INTO Payment (PaymentID, ReservationID, Date, PaymentMethod,PaymentAmount) VALUES (@PaymentID, @ReservationID, @Date, @PaymentMethod,@PaymentAmount)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    cmd.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(PaymentAmount));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            //HttpCookie myCookie = new HttpCookie("Order");
            //myCookie.Expires = DateTime.Now.AddDays(-1d);
            //Response.Cookies.Add(myCookie);
        }
    }
}