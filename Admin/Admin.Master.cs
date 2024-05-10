using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelManagement.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (!IsPostBack)
            {
                if (!IsLoginPage() && Session["Login"] == null)
                {
                    divusersetting.Visible = false;
                    divoffcanvas.Visible = false;
                    Response.Redirect("Login.aspx");
                }
                else if (Session["Login"] != null)
                {
                    string empId = Session["Login"].ToString();
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Hotel"].ConnectionString))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM Employee WHERE EmpID = @EmpID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@EmpID", empId);
                            int count = (int)command.ExecuteScalar();

                            if (count == 0)
                            {
                                divusersetting.Visible = false;
                                divoffcanvas.Visible = false;
                                Response.Redirect("Login.aspx");
                                
                            }
                            else
                            {
                                Response.Redirect("Home.aspx");
                                divusersetting.Visible = true;
                                divoffcanvas.Visible = true;
                            }
                        }
                    }
                }
            }*/
        }
        protected void Btnlog_Click(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                // Abandon the session
                Session.Abandon();
                // Optionally, redirect the user to another page after session is abandoned
                Response.Redirect("Login.aspx");
            }
        }
        //check if the page is login or not
        private bool IsLoginPage()
        {
            return Request.Url.AbsolutePath.EndsWith("Login.aspx", StringComparison.OrdinalIgnoreCase);
        }


    }

}
