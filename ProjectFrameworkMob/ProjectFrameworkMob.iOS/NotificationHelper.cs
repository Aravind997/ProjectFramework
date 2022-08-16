using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using ProjectFrameworkMob.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationHelper))]
namespace ProjectFrameworkMob.iOS
{
    public class NotificationHelper : INotification
    {
        public void CreateNotification(string title, string message, string Data)
        {
            new NotificationDelegate().RegisterNotification(title, message, Data);
        }
    }
}