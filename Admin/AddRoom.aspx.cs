using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            string roomid = txtroomid.Text.Trim();
            string roomtype = txtroomtype.Text.Trim();
            string capacity = txtcapacity.Text.Trim();
            string desciption = txtdescription.Text.Trim();
            string price = txtPricepernight.Text.Trim();

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Room (RoomId, RoomType, Capacity, Description, PricePerNight, RoomImage, Roomenviroment) " +
                               "VALUES (@RoomId, @RoomType, @Capacity, @Description, @PricePerNight, @RoomImage, @Roomenviroment)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomid);
                command.Parameters.AddWithValue("@RoomType", roomtype);
                command.Parameters.AddWithValue("@Capacity", Convert.ToInt32(capacity));
                command.Parameters.AddWithValue("@Description", desciption);
                command.Parameters.AddWithValue("@PricePerNight", Convert.ToDecimal(price));
                string roomImagePath = "/img/";
                string roomEnvironmentPath = "/img/";
                string roomImageFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileRoomImage.FileName);
                string roomEnvironmentFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(fileRoomEnvironment.FileName);
                fileRoomImage.SaveAs(Server.MapPath(roomImagePath + roomImageFileName));
                fileRoomEnvironment.SaveAs(Server.MapPath(roomEnvironmentPath + roomEnvironmentFileName));

                command.Parameters.AddWithValue("@RoomImage", roomImagePath + roomImageFileName);
                command.Parameters.AddWithValue("@Roomenviroment", roomEnvironmentPath + roomEnvironmentFileName);
                connection.Open();
                command.ExecuteNonQuery();
            }
            lbltext.Text = "Update succesfully";
            lbltext.CssClass = "text-success";
        }
    }
}