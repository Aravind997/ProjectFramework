using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFrameworkWeb
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadPageSettings();
            }
            if(Session["UserID"]!=null && !string.IsNullOrEmpty(Session["UserID"].ToString()))
            {
                SetLoginText("Logout");
                if(Session["Role"].ToString()== UserRole.Admin.ToString())
                {
                    IDAdminSettings.Visible = true;
                }
                else
                {
                    //Show other settings based on your Business Role
                }
                
            }
            else
            {
                SetLoginText("Login");
                IDAdminSettings.Visible = false;
            }
        }

        public void LoadPageSettings()
        {
            SettingsBLL SettingsInfo = new SettingsBLL();
            LabelApplicationName.Text = SettingsInfo.GetValue("AppName");
        }
        public void SetLoginText(string LoginText)
        {
            LabelLogin.Text = LoginText;
        }

    }
}