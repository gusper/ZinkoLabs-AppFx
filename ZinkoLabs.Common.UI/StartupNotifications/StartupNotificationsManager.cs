using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.StartupNotifications
{
    public class StartupNotificationsManager
    {
        IsolatedStorageSettings isoStore = IsolatedStorageSettings.ApplicationSettings;
        StartupNotification notification;
        private bool alreadyBeenShown = false;
        private string appName;
        private const string LastShownNotificationId = "ZinkoLabs.Common.UI.StartupNotifications.LastShownNotificationId";

        public StartupNotificationsManager(string appName, StartupNotification notification)
        {
            this.appName = appName;
            this.notification = notification;
        }

        public void ShowNotifications()
        {
            if (this.alreadyBeenShown)
                return;

            if (notification.Id > LoadLastShownId())
            {
                MessageBox.Show(notification.Message, notification.Title, MessageBoxButton.OK);
                SaveLastShownId(notification.Id);
            }

            this.alreadyBeenShown = true;
        }

        private int LoadLastShownId()
        {
            if (isoStore.Contains(LastShownNotificationId))
                return (int)isoStore[LastShownNotificationId];

            return -1;
        }

        private void SaveLastShownId(int notificationId)
        {
            isoStore[LastShownNotificationId] = notificationId;
        }
    }
}
