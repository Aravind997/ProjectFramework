using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProjectFramework.Web.Models
{
    public class AdminViewModel : ViewModelBase
    {
        private BLL.SettingsBLL SettingsBLLObj = new BLL.SettingsBLL();
        public string EnableMobAuth { get; set; }
        public bool IsChecked { get; set; }
        public AdminViewModel()
        {
            LoadSettings();
        }
        private void LoadSettings()
        {
            EnableMobAuth = SettingsBLLObj.GetValue("EnableMobAuth");
            if (EnableMobAuth.ToLower() == "true")
            {
                IsChecked = true;
            }
            else
            {
                IsChecked = false;
            }
        }
        public bool UpdateSettings()
        {
            try
            {
                SettingsBLLObj.SetValue("AppName", AppName);
                SettingsBLLObj.SetValue("MainHeading", MainHeading);
                SettingsBLLObj.SetValue("MainDesc", MainDesc);
                SettingsBLLObj.SetValue("EnableMobAuth", EnableMobAuth);
                LoadSettings();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
