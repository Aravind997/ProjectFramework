using Newtonsoft.Json;
using ProjectFrameworkCommonLib;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ProjectFrameworkMob.Services
{
    public partial class AppApiService
    {
        public async Task<AppSettings> GetSettingsInfo()
        {
            AppSettings Settings = new AppSettings();
            try
            {

                var requestTask = await AppServiceClient.GetAsync("api/Settings/GetSettingsInfo");
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Settings = JsonConvert.DeserializeObject<AppSettings>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);
                }
                return Settings;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        public async Task<bool> UpdateSettingsInfo(AppSettings Settings)
        {
            bool bResult = false;
            try
            {
                var serializedItem = JsonConvert.SerializeObject(Settings);
                string Paramters = "api/Settings/UpdateSettingsInfo";
                var requestTask = await AppServiceClient.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    bResult = true;

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    throw new Exception(ResponseData.Result);
                }
                return bResult;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }

    public async Task<AppSettings> GetPortfolioInfo()
    {
        AppSettings Settings = new AppSettings();
        try
        {

            var requestTask = await AppServiceClient.GetAsync("api/Settings/GetPortfolioInfo");
            var response = Task.Run(() => requestTask);
            Task<string> ResponseData;
            if (response.Result.IsSuccessStatusCode)
            {
                ResponseData = response.Result.Content.ReadAsStringAsync();

                Settings = JsonConvert.DeserializeObject<AppSettings>(ResponseData.Result);

            }
            else
            {
                ResponseData = response.Result.Content.ReadAsStringAsync();
                throw new Exception(ResponseData.Result);
            }
            return Settings;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    public async Task<bool> UpdatePortfolioInfo(AppSettings Settings)
    {
        bool bResult = false;
        try
        {
            var serializedItem = JsonConvert.SerializeObject(Settings);
            string Paramters = "api/Settings/UpdatePortfolioInfo";
            var requestTask = await AppServiceClient.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            var response = Task.Run(() => requestTask);
            Task<string> ResponseData;
            if (response.Result.IsSuccessStatusCode)
            {
                bResult = true;

            }
            else
            {
                ResponseData = response.Result.Content.ReadAsStringAsync();
                throw new Exception(ResponseData.Result);
            }
            return bResult;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
}

