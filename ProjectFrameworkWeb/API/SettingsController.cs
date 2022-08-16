using ProjectFrameworkCommonLib;
using ProjectFrameworkWeb.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ProjectFrameworkWeb.API
{
    public class SettingsController : TokenCheckController
    {
        private SettingsBLL SettingsBLLObj = new SettingsBLL();
        
        [HttpGet]
        public HttpResponseMessage GetSettingsInfo()
        {
            try
            {
                AppSettings Settings = new AppSettings();
                Settings.AppName = SettingsBLLObj.GetValue("AppName");
                Settings.MainHeading = SettingsBLLObj.GetValue("MainHeading");
                Settings.MainDesc = SettingsBLLObj.GetValue("MainDesc");
                var message = Request.CreateResponse(HttpStatusCode.OK, Settings);
                message.Headers.Location = new Uri(Request.RequestUri.ToString());
                return message;
            }
            catch (Exception Ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
            }
            
        }
        [HttpGet]
        public HttpResponseMessage GetSettingsInfoEx(string Key)
        {
            try
            {
                string Value = SettingsBLLObj.GetValue(Key);
                var message = Request.CreateResponse(HttpStatusCode.OK, Value);
                message.Headers.Location = new Uri(Request.RequestUri.ToString());
                return message;
            }
            catch (Exception Ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
            }

        }
        [HttpPost]
        public HttpResponseMessage UpdateSettingsInfo(AppSettings Settings)
        {
            try
            {
                SettingsBLLObj.SetValue("AppName", Settings.AppName);
                SettingsBLLObj.SetValue("MainHeading", Settings.MainHeading);
                SettingsBLLObj.SetValue("MainDesc", Settings.MainDesc);
                var message = Request.CreateResponse(HttpStatusCode.OK, "Settings Updated Successfully");
                message.Headers.Location = new Uri(Request.RequestUri.ToString());
                return message;
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
            }

        }
    //    public HttpResponseMessage GetPortfolioInfo()
    //    {
    //        try
    //        {
    //            AppSettings Settings = new AppSettings();
    //            Settings.AppName = SettingsBLLObj.GetValue("FolioName");
    //            Settings.MainHeading = SettingsBLLObj.GetValue("FolioAmount");
    //            Settings.MainDesc = SettingsBLLObj.GetValue("Year");
    //            var message = Request.CreateResponse(HttpStatusCode.OK, Settings);
    //            message.Headers.Location = new Uri(Request.RequestUri.ToString());
    //            return message;
    //        }
    //        catch (Exception Ex)
    //        {

    //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
    //        }

    //    }
    //    [HttpGet]
    //    public HttpResponseMessage GetPortfolioInfoEx(string Key)
    //    {
    //        try
    //        {
    //            string Value = SettingsBLLObj.GetValue(Key);
    //            var message = Request.CreateResponse(HttpStatusCode.OK, Value);
    //            message.Headers.Location = new Uri(Request.RequestUri.ToString());
    //            return message;
    //        }
    //        catch (Exception Ex)
    //        {

    //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
    //        }

    //    }
    //    [HttpPost]
    //    public HttpResponseMessage UpdatePortfolioInfo(AppSettings Settings)
    //    {
    //        try
    //        {
    //            SettingsBLLObj.SetValue("FolioName", Settings.FolioName);
    //            SettingsBLLObj.SetValue("FolioAmount", Settings.FolioAmount);
    //            SettingsBLLObj.SetValue("Year", Settings.Year);
    //            var message = Request.CreateResponse(HttpStatusCode.OK, "Settings Updated Successfully");
    //            message.Headers.Location = new Uri(Request.RequestUri.ToString());
    //            return message;
    //        }
    //        catch (Exception Ex)
    //        {
    //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex.Message);
    //        }

    //    }


    //}
}