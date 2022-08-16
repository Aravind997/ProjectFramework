using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectFramework.Web.BLL;
using ProjectFramework.Web.Models;
using ProjectFrameworkCommonLib;

namespace ProjectFramework.Web.Controllers
{
    public class HomeController : AppControllerBase
    {
        public HomeViewModel  HomeSettings = new HomeViewModel();
        public LoginViewModel LoginData = new LoginViewModel();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        private void SetAdminFlag()
        {
            string strUserID = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(strUserID))
            {
                HomeSettings.IsAdmin = false;
            }
            else
            {
                HomeSettings.IsAdmin = true;
            }
        }
        public IActionResult Index()
        {
            SetAdminFlag();
            return View(HomeSettings);
        }

        public IActionResult About()
        {
            SetAdminFlag();
            return View(HomeSettings);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            HttpContext.Session.SetString("UserID", "");
            HttpContext.Session.SetString("UserName", "");
            HttpContext.Session.SetString("Role", "");
            SetAdminFlag();
            return View(LoginData);
        }

        [HttpPost]
        public IActionResult Login(string UserName, string Password)
        {
            AuthInfo Info = LoginData.UserBLLObj.GetUserInfo(UserName, Password);

            if (Info.UserID>0)
            {
                UserRole Role = LoginData.UserBLLObj.GetUserRole(Info.UserID);
                HttpContext.Session.SetString("UserID", Info.UserID.ToString());
                HttpContext.Session.SetString("UserName", UserName);
                HttpContext.Session.SetString("Role", Role.ToString());
                LoginData.StatusString = "Login Success";
                //Redirect to settings View
                return RedirectToAction("Settings","Admin");

            }
            else
            {
                LoginData.StatusString = "Login Failed";
                return View(LoginData);
            }
            
        }
    }
}
