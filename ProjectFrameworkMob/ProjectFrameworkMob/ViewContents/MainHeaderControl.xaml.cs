using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFrameworkMob.ViewContents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainHeaderControl : StackLayout
    {
        public bool DisplayNavigationOption { get; set; }
        public MainHeaderControl()
        {
            InitializeComponent();
            lblHeading.Text = App.SettingsManagerObj.Settings.AppName;
        }
        public void ShowMultiLingualOption(bool bShow)
        {
            BtnMutilaguage.IsVisible = bShow;
        }
        public void ShowBurgerMenuOption(bool bShow)
        {
            BtnBurgerMenu.IsVisible = bShow;
        }
        public void ShowNavigationOption(bool bShowNavigation)
        {
            DisplayNavigationOption = bShowNavigation;
            if(DisplayNavigationOption)
            {
                BtnMutilaguage.Source = "back_button.png";
            }
            else
            {
                BtnMutilaguage.Source = "translate.png";
            }
        }
        private void BtnBurgerMenu_Clicked(object sender, EventArgs e)
        {
            double dWidth;
            dWidth = MenuView.Width;
            if (dWidth <= 0)
            {
                dWidth = 300;
            }

            MenuGrid.IsVisible = true;
            MenuHeader.IsVisible = false;
            Action<double> callBack = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callBack, dWidth, 0, 16, 400, Easing.CubicInOut);

        }

        public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            CloseGrid();
        }
        public async void CloseGrid()
        {

            Action<double> callBack = input => MenuView.TranslationX = input;
            MenuView.Animate("anim", callBack, 0, MenuView.Width, 16, 400, Easing.CubicInOut);

            await Task.Delay(390);
            MenuGrid.IsVisible = false;
            MenuHeader.IsVisible = true;
        }



        private void BtnMutilaguage_Clicked(object sender, EventArgs e)
        {
            if (DisplayNavigationOption)
            {
                //Go Back
                Navigation.PopAsync();
            }
            else
            {
                //Display Multilanguage Option
            }
        }

    }
}