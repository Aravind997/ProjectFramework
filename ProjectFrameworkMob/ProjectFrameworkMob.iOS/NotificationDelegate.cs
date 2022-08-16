using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UserNotifications;

namespace ProjectFrameworkMob.iOS
{
#if true //This is for Push notification when teh window is active
    [Register("NotificationService")]
    public class NotificationService : UNNotificationServiceExtension
    {
#region Computed Properties
        Action<UNNotificationContent> ContentHandler { get; set; }
        UNMutableNotificationContent BestAttemptContent { get; set; }
#endregion

#region Constructors
        protected NotificationService(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
#endregion

#region Override Methods
        public override void DidReceiveNotificationRequest(UNNotificationRequest request, Action<UNNotificationContent> contentHandler)
        {
            ContentHandler = contentHandler;
            BestAttemptContent = (UNMutableNotificationContent)request.Content.MutableCopy();

            // Modify the notification content here...
            BestAttemptContent.Title = $"{BestAttemptContent.Title}[modified]";

            ContentHandler(BestAttemptContent);
        }

        public override void TimeWillExpire()
        {
            // Called just before the extension will be terminated by the system.
            // Use this as an opportunity to deliver your "best attempt" at modified content, otherwise the original push payload will be used.

            ContentHandler(BestAttemptContent);
        }
#endregion
    }
#endif
    class NotificationDelegate : UNUserNotificationCenterDelegate
    {
        public NotificationDelegate()
        {

        }
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do something with the notification
            Console.WriteLine("Active Notification: {0}", notification);

            // Tell system to display the notification anyway or use
            // `None` to say we have handled the display locally.
            completionHandler(UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Sound);
        }
        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            // Take action based on Action ID
            switch (response.ActionIdentifier)
            {
                case "reply":
                    // Do something
                    break;
                default:
                    // Take action based on identifier
                    if (response.IsDefaultAction)
                    {
                        // Handle default action...
                    }
                    else if (response.IsDismissAction)
                    {
                        // Handle dismiss action
                    }
                    break;
            }

            // Inform caller it has been handled
            completionHandler();
        }
        public void RegisterNotification(string title, string body, string Data)
        {
            UNUserNotificationCenter center = UNUserNotificationCenter.Current;

            //creat a UNMutableNotificationContent which contains your notification content
            UNMutableNotificationContent notificationContent = new UNMutableNotificationContent();

            notificationContent.Title = title;
            notificationContent.Body = body;
            try
            {
                notificationContent.UserInfo.TryAdd(new NSString("info"), new NSString(Data));
            }
            catch(Exception)
            {

            }

            notificationContent.Sound = UNNotificationSound.Default;

            UNTimeIntervalNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(5, false);

            UNNotificationRequest request = UNNotificationRequest.FromIdentifier("Diapazon", notificationContent, trigger);


            center.AddNotificationRequest(request, (NSError obj) =>
            {

            });
        }
    }
}