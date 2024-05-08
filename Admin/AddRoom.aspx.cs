using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Admin
{
    public partial class AddRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string roomid=txtroomid.Text.Trim();
            string roomtype=txtroomtype.Text.Trim();
            string capacity = txtcapacity.Text.Trim();
            string desciption=txtdescription.Text.Trim();
            string price=txtPricepernight.Text.Trim();

        }
    }
}