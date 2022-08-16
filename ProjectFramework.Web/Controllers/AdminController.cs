using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectFramework.Web.Models;

namespace ProjectFramework.Web.Controllers
{
    public class AdminController : AppControllerBase
    {
        public AdminViewModel AdminSettings = new AdminViewModel();
        public IActionResult Index()
        {
            return View("Settings");
        }

        private void SetAdminFlag()
        {
            string strUserID = HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(strUserID))
            {
                AdminSettings.IsAdmin = false;
            }
            else
            {
                AdminSettings.IsAdmin = true;
            }
        }

        public IActionResult Settings()
        {
            SetAdminFlag();
            
            if(AdminSettings.IsAdmin)
            {
                return View(AdminSettings);
               
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
            
        }
        [HttpPost]
        public IActionResult Settings(string AppName, string MainHeading, string MainDesc,string EnableMobAuth)
        {
            AdminSettings.AppName = AppName;
            AdminSettings.MainHeading = MainHeading;
            AdminSettings.MainDesc = MainDesc;
            if(!string.IsNullOrEmpty(EnableMobAuth) && EnableMobAuth.ToLower()=="true")
            {
                AdminSettings.EnableMobAuth = "true";
            }
            else
            {
                AdminSettings.EnableMobAuth = "false";
            }
            if(AdminSettings.UpdateSettings())
            {
                AdminSettings.StatusString = "Settings Updated Successfully";
            }
            else
            {
                AdminSettings.StatusString = "Settings Updation Failed";
            }
            SetAdminFlag();
            return View(AdminSettings);
        }

    }
}