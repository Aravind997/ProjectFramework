using ProjectFrameworkCommonLib;
using ProjectFramework.Web.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFramework.Web.BLL
{
    public class BLLBase
    {
        public static string WebApplicationPath { get; set; }
        public DatabaseUtils m_objDatabaseUtils = new DatabaseUtils();

        //Whether to encrypt OTP Password is a strategy which we need to decide later 
        //So Just implementing a passthrough function for now
        public static string Encrypt(string Password)
        {
            return PFCrypt.Encrypt(Password); ;
        }

        public static string Decrypt(string EncryptedString)
        {
            return PFCrypt.Decrypt(EncryptedString); ;
        }

        public static string GetAuthenticationToken(string EMail, string userId)
        {
            //Cobine the E Mail and User ID to create authentication token
            //Authentication  

            string TokenString = EMail + ":" + userId.ToString();
            string AuthToken = PFCrypt.Encrypt(TokenString);
            return AuthToken;
        }

    }
}