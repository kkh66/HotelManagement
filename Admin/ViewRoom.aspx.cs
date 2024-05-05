using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Admin
{
    public partial class ViewRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRoomList();
            }
        }
        private void BindRoomList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Room";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                rptRooms.DataSource = dataTable;
                rptRooms.DataBind();
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            int roomId = Convert.ToInt32(hfEditRoomId.Value);

            // 获取编辑后的房间信息
            //string roomType = /* 获取房间类型 */;
            //int capacity = /* 获取容量 */;
            //string description = /* 获取描述 */;
            //decimal pricePerNight = /* 获取每晚价格 */;
            //int roomNumber = /* 获取房间号 */;

            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Room SET RoomType = @RoomType, Capacity = @Capacity, Description = @Description, PricePerNight = @PricePerNight, RoomNumber = @RoomNumber WHERE RoomId = @RoomId";
                SqlCommand command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@RoomType", roomType);
                //command.Parameters.AddWithValue("@Capacity", capacity);
                //command.Parameters.AddWithValue("@Description", description);
                //command.Parameters.AddWithValue("@PricePerNight", pricePerNight);
                //command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                //command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindRoomList();
        }
        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            int roomId = Convert.ToInt32(hfDeleteRoomId.Value);

            string connectionString = ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Room WHERE RoomId = @RoomId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomId", roomId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            BindRoomList();
        }
    }
}