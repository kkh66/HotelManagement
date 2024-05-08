using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace HotelManagement
{
    public partial class ConfirmBooking : System.Web.UI.Page
    {
        private static string PaypalclientId = System.Configuration.ConfigurationManager.AppSettings["PaypalClientID"];
        private static string PaypalclientSecret = System.Configuration.ConfigurationManager.AppSettings["PaypalSecretkey"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString != null && Request.QueryString.Count != 0)
            {
                string approvalToken = Request.QueryString["token"];
                var response = Task.Run(async () => await captureOrder(approvalToken));

                if (response.Result != null)
                {
                    Order result = response.Result.Result<Order>();
                    lblpaypal.Text = result.Status;
                    if (result.Status.ToLower() == "approved")
                    {
                    }
                }
                else
                {

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
                            Value = "100.00" // 设置总价值为 100.00 MYR
                        }
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = "https://localhost:44384/Default.aspx",
                    CancelUrl = "https://localhost:44384/Default.aspx"
                }
            };

            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            var environment = new SandboxEnvironment(PaypalclientId, PaypalclientSecret);
            var response = await (new PayPalHttpClient(environment).Execute(request));
            Order result = response.Result<Order>();

            return GetApprovalUrl(result);
        }

        public async static Task<PayPalHttp.HttpResponse> captureOrder(string token)
        {
            var request = new OrdersCaptureRequest(token);
            request.RequestBody(new OrderActionRequest());
            var environment = new SandboxEnvironment(PaypalclientId, PaypalclientSecret);
            var response = await (new PayPalHttpClient(environment).Execute(request));
            Order result = response.Result<Order>();

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

        protected void BtnPaypal_Click(object sender, EventArgs e)
        {
            Console.WriteLine("BtnPaypal_Click 事件处理程序被触发");
            var response = Task.Run(async () => await createOrder());
            Response.Redirect(response.Result);
        }
    }
}