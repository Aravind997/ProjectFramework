using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectFrameworkMob.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string URL)
        {
            InitializeComponent();
            HeaderControl.ShowNavigationOption(true);

            if (!string.IsNullOrEmpty(URL))
            {
                WebViewNavigate.Source = URL;
            }
            
        }
    }
}