using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFrameworkWeb.AdminPages
{
    public partial class AdminSettings : BasePage
    {
        private SettingsBLL SettingsBLLObj = new SettingsBLL(); 
        protected new void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Load the Data
                TextBoxAppName.Text = SettingsBLLObj.GetValue("AppName");
                TextBoxMainHeading.Text = SettingsBLLObj.GetValue("MainHeading");
                TextBoxMainDesc.Text = SettingsBLLObj.GetValue("MainDesc");
                string Result= SettingsBLLObj.GetValue("EnableMobAuth");
                if(Result.ToLower()=="true")
                {
                    CheckEnableAPIAuth.Checked = true;
                }
                else
                {
                    CheckEnableAPIAuth.Checked = false;
                }

            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SettingsBLLObj.SetValue("AppName", TextBoxAppName.Text);
                SettingsBLLObj.SetValue("MainHeading", TextBoxMainHeading.Text);
                SettingsBLLObj.SetValue("MainDesc", TextBoxMainDesc.Text);
                SettingsBLLObj.SetValue("EnableMobAuth", CheckEnableAPIAuth.Checked.ToString());
                SettingsBLL.SetAuthToken(CheckEnableAPIAuth.Checked);
                LabelStatus.Text = "Settings Updated Successfully...";
            }
            catch(Exception Ex)
            {
                LabelStatus.Text = Ex.Message;
            }
        }

        protected void ViewSensex_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/PageSensex");
        }
    }
}