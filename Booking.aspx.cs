using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class Booking : System.Web.UI.Page
    {
        private static string PaypalclientId = System.Configuration.ConfigurationManager.AppSettings["PaypalClientID"];
        private static string PaypalclientSecret = System.Configuration.ConfigurationManager.AppSettings["PaypalSecretkey"];
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ddlroom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnbook_Click(object sender, EventArgs e)
        {

        }
    }
}