using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFrameworkWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadPageData();
            }
        }
        public void LoadPageData()
        {
            SettingsBLL SettingsInfo = new SettingsBLL();
            LabelMainHeading.Text = SettingsInfo.GetValue("MainHeading");
            LabelMainDesc.Text = SettingsInfo.GetValue("MainDesc");
        }
    }
}