using System;
using System.Security.Cryptography;
using System.Text;

namespace ProjectFrameworkCommonLib
{
    /// <summary>
    /// Summary Common class to encrypt and all types of data
    /// </summary>
    /// 
    
    public class AuthInfo
    {
        public int          UserID { get; set; }
        public string AuthenticationToken { get; set; }
        public AuthInfo()
        {
            UserID = -1;
            AuthenticationToken = "";
        }
    }

    public class UserInfo
    {
        public string FirstName { get; set; } //First Name
        public string LastName { get; set; } //First Name
        public string EMail { get; set; } //E Mail
        public string PhoneNo { get; set; } //E Mail


    }

}
