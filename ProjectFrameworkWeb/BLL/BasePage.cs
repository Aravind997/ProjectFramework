using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectFrameworkWeb.BLL
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckCredentials();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void CheckCredentials()
        {
            if (string.IsNullOrEmpty(Session["UserName"] as string))
            {
                //Redirect to Login Page if Session is null & Expires
                //While you develop solutions comment this portion Response.Redirect("Login")
                //so that you can go directly to the Web Page
                Response.Redirect("~/Login");
            }
        }
     }
}