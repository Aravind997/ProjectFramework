using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFramework.Web.Models
{
    public class ViewModelBase 
    {
        private BLL.SettingsBLL SettingsBLLObj = new BLL.SettingsBLL();
        public string AppName { get; set; }
        public string MainHeading { get; set; }
        public string MainDesc { get; set; }
        public string StatusString { get; set; }

        public bool IsAdmin { get; set; }
        public ViewModelBase()
        {
            //Load it form Database later
            AppName = SettingsBLLObj.GetValue("AppName");
            MainHeading = SettingsBLLObj.GetValue("MainHeading");
            MainDesc = SettingsBLLObj.GetValue("MainDesc");
                        
        }
    }
}
