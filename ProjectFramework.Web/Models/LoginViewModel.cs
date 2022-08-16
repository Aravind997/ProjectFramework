using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ProjectFramework.Web.Models
{
    public class LoginViewModel : ViewModelBase
    {
        public BLL.UserBLL UserBLLObj = new BLL.UserBLL();
        public LoginViewModel()
        {
            StatusString = "";
        }
    }
}
