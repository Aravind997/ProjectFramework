using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectFrameworkMob.Views;
using Xamarin.Essentials;
using ProjectFrameworkCommonLib;
using ProjectFrameworkMob.Utils;
using ProjectFrameworkMob.Services;

namespace ProjectFrameworkMob
{
    public partial class App : Application
    {

        private const string AppEncryptionKey = "KtsInfotechPalaKeralaIndia";
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        public static string LocalDevelepmentURL = "http://192.168.43.154:50001/";
        public static string TestHostingURL = "http://testaspnet.virtualtutor.co.in/";
        public static string ProductionURL = "http://testaspnet.virtualtutor.co.in/";
        public static string AzureTestURL = "http://testaspnet.virtualtutor.co.in/";
        //Local
        //public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? LocalDevelepmentURL : LocalDevelepmentURL;
        //customer
        public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ?TestHostingURL : TestHostingURL ;
        //Dev
       // public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? ProductionURL : ProductionURL;
        // Azure Test
       // public static string AzureBackendUrl = DeviceInfo.Platform == DevicePlatform.Android ? AzureTestURL : AzureTestURL;

        public static AppSettingsManager SettingsManagerObj = null;

        public static AppApiService ApiServiceObj = null;

        public static MainPage MainPageObj { get; set; }

        public static PageSensex PageSensexObj { get; set; }

        public App()
        {
            try
            {
                InitializeComponent();

                //Se the Encryption Key AppEncryptionKey . This key value should be same in both Mobile and MobAPI to communicate 
                PFCrypt.Key = AppEncryptionKey;

                if (App.SettingsManagerObj == null)
                {
                    App.SettingsManagerObj = new AppSettingsManager();
                }

                if (ApiServiceObj == null)
                {
                    ApiServiceObj = new AppApiService();
                }

                App.SettingsManagerObj.LoadSettings();
                if (!string.IsNullOrEmpty(SettingsManagerObj.AuthenticationToken))
                {
                    ApiServiceObj.SetUserCredentials(SettingsManagerObj.UserID, SettingsManagerObj.EMail, SettingsManagerObj.AuthenticationToken);
                }
                LaunchForms();
            }
            catch(Exception Ex)
            {
                MainPageObj = new MainPage(Ex.Message);
                MainPage = new NavigationPage(MainPageObj);
            }
        }

        private void LaunchForms()
        {
            MainPageObj = new MainPage();
            if(string.IsNullOrEmpty(App.SettingsManagerObj.AuthenticationToken))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(MainPageObj);
            }
            
        }
        public void RelaunchMasterForm()
        {
            MainPage = new NavigationPage(MainPageObj);
        }

        public void RelaunchLoginForm()
        {
            MainPage = new NavigationPage(new LoginPage());
        }
        protected override void OnStart()
        {
        }

       
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
