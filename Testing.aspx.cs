using System;
using System.Collections.Generic;
using System.Linq;
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
                    Label1.Text = result.Status;
                    if (result.Status.ToLower() == "approved")
                    {
                        // 付款成功后的处理逻辑，如保存订单信息等
                    }
                }
                else
                {
                    // 处理空响应的错误
                }
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Label1.Text = "订单已创建。";
                var response = Task.Run(async () => await createOrder());
                Response.Redirect(response.Result);
            }
            catch (Exception ex)
            {
                // 记录异常信息或执行其他适当的错误处理操作
                Console.WriteLine("发生异常：" + ex.Message);
                // 如果需要，在页面上显示错误消息
                Label1.Text = "发生异常：" + ex.Message;
            }
        }
    }
}