using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.StartupNotifications
{
    public class StartupNotificationsManager
    {
        IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;
        StartupNotification _notification;
        bool _alreadyBeenShown = false;
        string _appName;
        const string _lastShownNotificationId = "ZinkoLabs.AppFx.StartupNotifications.LastShownNotificationId";

        public StartupNotificationsManager(string appName, StartupNotification notification)
        {
            _appName = appName;
            _notification = notification;
        }

        public void ShowNotifications()
        {
            if (_alreadyBeenShown)
                return;

            if (_notification.ID > LoadLastShownId())
            {
                MessageBox.Show(_notification.Message, _notification.Title, MessageBoxButton.OK);
                SaveLastShownId(_notification.ID);
            }

            _alreadyBeenShown = true;
        }

        internal int LoadLastShownId()
        {
            if (_isoStore.Contains(_lastShownNotificationId))
                return (int)_isoStore[_lastShownNotificationId];

            return -1;
        }

        internal void SaveLastShownId(int notificationId)
        {
            _isoStore[_lastShownNotificationId] = notificationId;
        }
    }
}
