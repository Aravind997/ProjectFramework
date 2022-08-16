using ProjectFrameworkCommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProjectFrameworkMob.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage(string Message="")
        {
            InitializeComponent();
            FillSettings();
        }
        private void FillSettings()
        {
            lblAppName.Text = App.SettingsManagerObj.Settings.AppName;
            lblMainTitle.Text= App.SettingsManagerObj.Settings.MainHeading;
            lblMainDesc.Text = App.SettingsManagerObj.Settings.MainDesc;

        }

        private async void GetSettings_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Getting Settings from Server..";

                AppSettings Settings = await App.ApiServiceObj.GetSettingsInfo();

                lblAppName.Text = Settings.AppName;
                lblMainTitle.Text = Settings.MainHeading;
                lblMainDesc.Text = Settings.MainDesc;

                lblStatus.Text = "Success..";

            }
            catch(Exception Ex)
            {
                lblStatus.Text = "Failed..| "+Ex.Message;
            }

        }

        private async void UpdateSettings_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Updating Settings to Server..";

                App.SettingsManagerObj.Settings.AppName=lblAppName.Text  ;
                App.SettingsManagerObj.Settings.MainHeading=lblMainTitle.Text;
                App.SettingsManagerObj.Settings.MainDesc=lblMainDesc.Text;

                bool bResult = await App.ApiServiceObj.UpdateSettingsInfo(App.SettingsManagerObj.Settings);

                if(bResult)
                {
                    App.SettingsManagerObj.SaveSettings();
                    lblStatus.Text = "Success..";
                }
                else
                {
                    lblStatus.Text = "Failed to Update..";
                }
                

            }
            catch (Exception Ex)
            {
                lblStatus.Text = "Failed..| " + Ex.Message;
            }

            
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            lblStatus.Text = "Logout from Application ...";

            App.SettingsManagerObj.UserID = -1;
            App.SettingsManagerObj.AuthenticationToken = "";
            App.SettingsManagerObj.EMail = "";
            App.SettingsManagerObj.SaveSettings();

            await Task.Delay(500);

            ((App)(App.Current)).RelaunchLoginForm();

        }

        private async void ViewSensex_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageSensex());
        }
    }
}
