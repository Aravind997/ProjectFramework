using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectFramework.Web.Models
{
    public class HomeViewModel : ViewModelBase
    {
        public BLL.SettingsBLL SettingsBLLObj = new BLL.SettingsBLL();
        
        public HomeViewModel()
        {
            
        }
        
    }
}
