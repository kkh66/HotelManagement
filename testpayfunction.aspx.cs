using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stripe.Checkout;
using System.Reflection.Emit;


namespace HotelManagement
{
    public partial class testpayfunction : System.Web.UI.Page
    {
        private static readonly string StripePublishableKey = "pk_test_51OrP6QJMhsB8qHbvjZwtZTq8vcXf5w3N1u6FsYiRK8o7FWQfrEhvOV4E0LTrhtIwKgxSoreFUskmLdi9gUHB3z2l003HweZzGM"; // 替换为你的 Stripe 公钥  
        private static readonly string StripeSecretKey = "sk_test_51OrP6QJMhsB8qHbv9Dc9kcanfCTJ9QqLr9PIM3lcyMoi34htNiImxuGQ5oH7rN28xN9JOPJpNjhkiOwFqovaqjPC00lcn11ys8";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            var cardNumber = txtCardNumber.Text;
            var expiryMonth = txtExpiryMonth.Text;
            var expiryYear = txtExpiryYear.Text;
            var cvc = txtCvc.Text;

            // 创建 token  
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = cardNumber,
                    ExpMonth = expiryMonth,
                    ExpYear = expiryYear,
                    Cvc = cvc,
                }
            };

            var tokenService = new TokenService();
            Token token;

            try
            {
                token = tokenService.Create(tokenOptions);
            }
            catch (StripeException ex)
            {
                // 处理创建 token 时发生的异常  
                Response.Write("Token creation failed: " + ex.Message);
                return;
            }

            // 使用 token 创建收费  
            var chargeService = new ChargeService();
            var chargeOptions = new ChargeCreateOptions
            {
                Amount = 999, // 以分为单位的金额  
                Currency = "usd",
                Description = "Charge for test@example.com",
                Source = token.Id, // 使用上面创建的 token 的 ID  
            };

            Charge charge;

            try
            {
                charge = chargeService.Create(chargeOptions);
            }
            catch (StripeException ex)
            {
                // 处理创建收费时发生的异常  
                Response.Write("Charge creation failed: " + ex.Message);
                return;
            }

            if (charge.Paid)
            {
                // 支付成功，处理后续逻辑  
                Response.Write("Payment successful!");
            }
            else
            {
                // 支付失败，处理错误  
                Response.Write("Payment failed: " + charge.Status);
            }
        }
    }
}