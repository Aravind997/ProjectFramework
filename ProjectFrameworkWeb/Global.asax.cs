using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Configuration;
using ProjectFrameworkWeb.BLL;
using ProjectFrameworkWeb.Utils;
using ProjectFrameworkCommonLib;

namespace ProjectFrameworkWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Set the database connection strings and other Info
            SetDBConnectionString();
            //Set the Cryptographic Key
            SetEncryptionDecryptionKey();

            //Set Authentication Token Options
            SetAuthenticationTokenOptions();
            //Set the Base Path to refer the relative path for image and other Document Storage

            BLLBase.WebApplicationPath = Server.MapPath("");

        }
        private void SetAuthenticationTokenOptions()
        {
            try
            {
                SettingsBLL SettingsBLLObj = new SettingsBLL();
                string Result = SettingsBLLObj.GetValue("EnableMobAuth");
                if (Result.ToLower() == "true")
                {
                    SettingsBLL.SetAuthToken(true);
                }
                else
                {
                    SettingsBLL.SetAuthToken(false);
                }
            }
            catch(Exception)
            {
                SettingsBLL.SetAuthToken(false);
            }
        }
        //Set the Root Path to place images relative to this folder
        private void SetEncryptionDecryptionKey()
        {
            string EncryptionDecryptionKey = ConfigurationManager.AppSettings["PFEncryptDecryptKey"];
            if(string.IsNullOrEmpty(EncryptionDecryptionKey))
            {
                EncryptionDecryptionKey = "KtsInfotechPalaKeralaIndia";
            }
            PFCrypt.Key = EncryptionDecryptionKey;
        }
        private void SetDBConnectionString()
        {
            string AppConnectionString = ConfigurationManager.AppSettings["PFConnStr"];
            DatabaseUtils.SetConnectionString(AppConnectionString);
            
        }
       
    }
}