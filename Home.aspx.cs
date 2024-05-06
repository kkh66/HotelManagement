using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnluxury_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("Room");
            cookie.Value = "1";
            cookie.Expires = DateTime.Now.AddMinutes(15);
            Response.Cookies.Add(cookie);
        }
    }
}