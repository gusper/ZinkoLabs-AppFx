using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.StartupNotifications
{
    public class StartupNotificationsManager
    {
        IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
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
            {
                return;
            }

            if (_notification.ID > LoadLastShownId())
            {
                MessageBox.Show(_notification.Message, _notification.Title, MessageBoxButton.OK);
                SaveLastShownId(_notification.ID);
            }

            _alreadyBeenShown = true;
        }

        private int LoadLastShownId()
        {
            if (_settings.Contains(_lastShownNotificationId))
            {
                return (int)_settings[_lastShownNotificationId];
            }

            return -1;
        }

        private void SaveLastShownId(int notificationId)
        {
            _settings[_lastShownNotificationId] = notificationId;
        }
    }
}
