using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class PaymentSuccesfull : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            lblshowtime.Text = "You will now be redirected in 5 seconds <br/>";
            Response.AppendHeader("Refresh", "5;url=Home.aspx");
        }
    }
}