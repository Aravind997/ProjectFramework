using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectFrameworkCommonLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectFramework.Web.API
{
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        [Route("GetSettingsInfo")]
        [HttpGet]
        public ActionResult GetSettingsInfo()
        {
            try
            {
                AppSettings Settings = new AppSettings();
                Settings.AppName = "Project Framework Web";
                Settings.MainHeading = "ASP.NET Core Project Framework";
                Settings.MainDesc = "This Application can be used as a Template to develop future applications.";
                return Ok(Settings); ;
            }
            catch (Exception Ex)
            {
                return NotFound(Ex.Message);
            }

        }
        [Route("GetSettingsInfoEx")]
        [HttpGet]
        public ActionResult GetSettingsInfoEx(string Key)
        {
            try
            {
                string Value = "Key Value";
                
                return Ok(Value);
            }
            catch (Exception Ex)
            {
                return NotFound(Ex.Message);
            }

        }
        [Route("UpdateSettingsInfo")]
        [HttpPost]
        public ActionResult UpdateSettingsInfo(AppSettings Settings)
        {
            try
            {
                return Ok("Settings Updated Successfully");
            }
            catch (Exception Ex)
            {
                return NotFound(Ex.Message);
            }

        }
    }
}
