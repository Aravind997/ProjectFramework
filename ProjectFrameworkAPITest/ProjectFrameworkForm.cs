using Newtonsoft.Json;
using ProjectFrameworkAPITest.Properties;
using ProjectFrameworkCommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectFrameworkAPITest
{
    public partial class ProjectFrameworkForm : Form
    {
        AuthInfo AuthInfo; 
        public ProjectFrameworkForm()
        {
            InitializeComponent();
            AuthInfo = new AuthInfo();
        }

        
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void SetResponseData(string Response)
        {
            textBoxResponseData.Text = Response;
        }

        private async void buttonLogin_ClickAsync(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(textBoxURL.Text);
                HttpResponseMessage response = await client.GetAsync("api/Auth/ValidateUser?UserID=" + textBoxUserID.Text + "&Password=" + textBoxPassword.Text);
                string ResponseData = "";
                if (response.IsSuccessStatusCode)
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    SetResponseData(ResponseData);
                    AuthInfo = JsonConvert.DeserializeObject<AuthInfo>(ResponseData);
                    labelAuthTokenValue.Text = AuthInfo.AuthenticationToken;
                }
                else
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    if(string.IsNullOrEmpty(ResponseData))
                    {
                        SetResponseData("No Error Response Data");
                    }
                    else
                    {
                        SetResponseData(ResponseData);
                    }
                    
                }

            }
        }

        private async void buttonGet_ClickAsync(object sender, EventArgs e)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(textBoxURL.Text);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", labelAuthTokenValue.Text);

                HttpResponseMessage response = await client.GetAsync("api/Settings/GetSettingsInfo");
                string ResponseData = "";
                if (response.IsSuccessStatusCode)
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    
                    AppSettings Settings = JsonConvert.DeserializeObject<AppSettings>(ResponseData);
                    textBoxAppName.Text = Settings.AppName;
                    textBoxMainHeading.Text = Settings.MainHeading;
                    textBoxDescription.Text = Settings.MainDesc;

                    SetResponseData(ResponseData);
                    
                }
                else
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    SetResponseData(ResponseData);
                }
                SetResponseData(ResponseData);
            }
        }

        private async void buttonSet_ClickAsync(object sender, EventArgs e)
        {
            AppSettings Settings = new AppSettings();
            Settings.AppName= textBoxAppName.Text;
            Settings.MainHeading= textBoxMainHeading.Text;
            Settings.MainDesc= textBoxDescription.Text;

            string Paramters = "api/Settings/UpdateSettingsInfo";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(textBoxURL.Text);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", labelAuthTokenValue.Text);
                var serializedItem = JsonConvert.SerializeObject(Settings);

                HttpResponseMessage response = await client.PostAsync(Paramters, new StringContent(serializedItem, Encoding.UTF8, "application/json"));
                string ResponseData = "";
                if (response.IsSuccessStatusCode)
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    SetResponseData(ResponseData);
                }
                else
                {
                    ResponseData = await response.Content.ReadAsStringAsync();
                    SetResponseData(ResponseData);
                }
            }
        }

    }
}
