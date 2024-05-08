using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
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
                HttpCookie orderCookie = Request.Cookies["Order"];
                if (orderCookie == null)
                {
                    Response.Redirect("Home.aspx");
                    return;
                }

                string reservationId = orderCookie.Value;

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
                            lblroomtype.Text = "Room Type: " + reader["RoomID"].ToString();
                            lblcheckin.Text = "Check-in Date: " + ((DateTime)reader["CheckIndate"]).ToString("dd-MM-yyyy");
                            lblcheckout.Text = "Check-out Date: " + ((DateTime)reader["CheckOutdate"]).ToString("dd-MM-yyyy");
                            lbltotalprice.Text = "Total Price: " + reader["TotalPrice"].ToString();
                            ReservedRoomNumber.Text = "Reserved Room Number: " + reader["Checkinout"].ToString();
                        }

                        reader.Close();
                    }
                }

                var stripeToken = Request.Form["stripeToken"];
                if (!string.IsNullOrEmpty(stripeToken))
                {
                    ProcessStripePayment(stripeToken);
                }

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
                            UpdateDatabaseWithPaymentDetails(approvalToken, "Paypal");
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
        private void ProcessStripePayment(string stripeToken)
        {
            ConfirmBooking confirmBookingInstance = new ConfirmBooking();
            string StripeSecretKey = ConfigurationManager.AppSettings["StripeSecretKey"];
            StripeConfiguration.SetApiKey(StripeSecretKey);

            var stripeChargeService = new ChargeService();
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = 999,
                Currency = "myr",
                Description = "Charge for Hotel booking",
                Source = stripeToken,
            };

            try
            {
                var charge = stripeChargeService.Create(chargeOptions);
                System.Diagnostics.Debug.WriteLine("Payment succeeded with charge ID: " + charge.Id);

                confirmBookingInstance.UpdateDatabaseWithPaymentDetails(charge.Id, "Stripe");

                Response.Redirect("PaymentSuccessful.aspx");
            }
            catch (StripeException ex)
            {
                System.Diagnostics.Debug.WriteLine("Payment failed with error: " + ex.Message);
                Response.Redirect("PaymentFail.aspx");
            }
        }



        public async static Task<string> createOrder()
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
                    Value = "100.00"
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
            string approvalUrl = await createOrder();
            Response.Redirect(approvalUrl);
        }
        private void UpdateDatabaseWithPaymentDetails(string paymentId, string paymentMethod)
        {
            string reservationId = Request.Cookies["Order"].Value;

            if (!string.IsNullOrEmpty(reservationId))
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                {
                    string query = "INSERT INTO Payment (PaymentID, ReservationID, Date, PaymentMethod) VALUES (@PaymentID, @ReservationID, @Date, @PaymentMethod)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PaymentID", paymentId);
                    cmd.Parameters.AddWithValue("@ReservationID", reservationId);
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}