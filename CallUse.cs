using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace HotelManagement
{
    public class CallUse
    {
        public static bool passwordmatch(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }
        public static bool IsValidPassword(string password)
        {
            Regex regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            return regex.IsMatch(password);
        }
        public static bool IsValidemail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

    }
}