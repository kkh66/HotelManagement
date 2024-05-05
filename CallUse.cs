using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HotelManagement
{
    public class CallUse
    {
        public static bool passwordmatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

    }
}