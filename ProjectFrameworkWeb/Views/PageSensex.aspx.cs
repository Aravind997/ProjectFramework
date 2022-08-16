using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFrameworkWeb.Views
{
    public partial class PageSensex : System.Web.UI.Page  
         {
        private SettingsBLL SettingsBLLObj = new SettingsBLL();
        protected  void Page_Load(object sender, EventArgs e)
        {
            {
                if (!IsPostBack)
                {
                    //Loading the Data
                    TextBoxFolioName.Text = SettingsBLLObj.GetValue("FolioName");
                    TextBoxFolioAmount.Text = SettingsBLLObj.GetValue("FolioAmount");
                    TextBoxYear.Text = SettingsBLLObj.GetValue("Year");
                    string Result = SettingsBLLObj.GetValue("EnableMobAuthen");
                    if (Result.ToLower() == "true")
                    {
                        CheckEnableAPIAuthen.Checked = true;
                    }
                    else
                    {
                        CheckEnableAPIAuthen.Checked = false;
                    }

                }
            }
        }
        protected  void UpdateFolio_Click(object sender, EventArgs e)
        {
            {
                {
                    try
                    {
                        SettingsBLLObj.SetValue("FolioName", TextBoxFolioName.Text);
                        SettingsBLLObj.SetValue("FolioAmount", TextBoxFolioAmount.Text);
                        SettingsBLLObj.SetValue("Year", TextBoxYear.Text);
                        SettingsBLLObj.SetValue("EnableMobAuthen", CheckEnableAPIAuthen.Checked.ToString());
                        SettingsBLL.SetAuthToken(CheckEnableAPIAuthen.Checked);
                        LabelDone.Text = "Settings Updated Successfully...";
                    }
                    catch (Exception Ex)
                    {
                        LabelDone.Text = Ex.Message;
                    }
                }
            }
        }

    }
}
