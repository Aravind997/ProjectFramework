using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectFrameworkCommonLib;
namespace ProjectFrameworkMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSensex : ContentPage
    {
        public PageSensex()
        {
            InitializeComponent();
            FillSettings();

        }
        private void FillSettings()
        {
            lblFolioName.Text = App.SettingsManagerObj.Settings.FolioName;
            lblFolioAmount.Text = App.SettingsManagerObj.Settings.FolioAmount;
            lblYear.Text = App.SettingsManagerObj.Settings.Year;

        }


        private  async void GetPortfolio_Clicked(object sender, EventArgs e)
        {
            {
                try
                {
                    lblDone.Text = "Getting Settings from Server..";

                    AppSettings Settings = await App.ApiServiceObj.GetPortfolioInfo();

                    lblFolioName.Text = Settings.FolioName;
                    lblFolioAmount.Text = Settings.FolioAmount;
                    lblYear.Text = Settings.Year;

                    lblDone.Text = "Success..";

                }
                catch (Exception Ex)
                {
                    lblDone.Text = "Failed..| " + Ex.Message;
                }

            }
        }

        private async void UpdatePortfolio_Clicked(object sender, EventArgs e)
        {
            {
                try
                {
                    lblDone.Text = "Updating Folio to Server..";

                    App.SettingsManagerObj.Settings.FolioName = lblFolioName.Text;
                    App.SettingsManagerObj.Settings.FolioAmount = lblFolioAmount.Text;
                    App.SettingsManagerObj.Settings.Year = lblYear.Text;

                    bool bResult = await App.ApiServiceObj.UpdatePortfolioInfo(App.SettingsManagerObj.Settings);

                    if (bResult)
                    {
                        App.SettingsManagerObj.SaveSettings();
                        lblDone.Text = "Update Done Successfully";
                    }
                    else
                    {
                        lblDone.Text = "Failed to Update..";
                    }


                }
                catch (Exception Ex)
                {
                    lblDone.Text = "Failed..| " + Ex.Message;
                }


            }
        }
    }
}