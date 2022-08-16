using ProjectFrameworkCommonLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFrameworkMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Checking Login Credentials...";
                AuthInfo Info = await App.ApiServiceObj.ValidateUser(lblUsername.Text, lblPassword.Text);
                if (Info.UserID > 0)
                {
                    App.SettingsManagerObj.UserID = Info.UserID;
                    App.SettingsManagerObj.AuthenticationToken = Info.AuthenticationToken;
                    App.SettingsManagerObj.EMail = lblUsername.Text;
                    App.SettingsManagerObj.SaveSettings();
                    App.ApiServiceObj.SetUserCredentials(Info.UserID, lblUsername.Text, Info.AuthenticationToken);
                    lblStatus.Text = "Login Success..";
                    //Thread.delay
                    await Task.Delay(500);
                    
                    ((App)(App.Current)).RelaunchMasterForm();
                }
                else
                {
                    lblStatus.Text = "Login Failed..";
                }
            }
            catch(Exception Ex)
            {

            }
        }

        private async void ButtonSignup_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebViewPage(App.AzureBackendUrl+"/Login"));
        }

    }
}

