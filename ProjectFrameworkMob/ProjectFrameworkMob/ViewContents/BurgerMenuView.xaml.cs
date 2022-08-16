using ProjectFrameworkMob.Views;
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
    public partial class BurgerMenuView : ContentView
    {
        public BurgerMenuView()
        {
            InitializeComponent();
        }

        private void tapHome_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(App.MainPageObj);
        }

        private void tapAboutApp_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AboutPage());
        }
    }
}