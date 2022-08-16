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
        public async Task<AuthInfo> ValidateUser(string UserID, string Password)
        {
            AuthInfo Info = new AuthInfo();
            try
            {
                string Paramters = "api/Auth/ValidateUser?UserID=" + UserID + "&Password=" + Password;
                var requestTask = await AppServiceClient.GetAsync(Paramters);
                var response = Task.Run(() => requestTask);
                Task<string> ResponseData;
                if (response.Result.IsSuccessStatusCode)
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();

                    Info = JsonConvert.DeserializeObject<AuthInfo>(ResponseData.Result);

                }
                else
                {
                    ResponseData = response.Result.Content.ReadAsStringAsync();
                    Info.UserID = -1;
                    Info.AuthenticationToken = ResponseData.Result;

                }
                return Info;
            }
            catch (Exception Ex)
            {
                Info.UserID = -1;
                Info.AuthenticationToken = Ex.Message;
                return Info;
            }
        }
    }
}
