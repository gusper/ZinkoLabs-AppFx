using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TestApp.Resources;
using ZinkoLabs.AppFx.StartupNotifications;

namespace TestApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        StartupNotificationsManager _notificationsManager;
        EulaPresenter _eulaPresenter;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            InitializeStartupNotifications();
        }

        private void InitializeStartupNotifications()
        {
            var disclaimerText = "Your disclaimer text goes here.";
            _eulaPresenter = new EulaPresenter("Test App", 103, disclaimerText);
            _notificationsManager = new StartupNotificationsManager("Test App",
                new StartupNotification(202, "What's new?", "We've added:\n• New feature\n• Another feature\n• Minor bug fixes"));

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_eulaPresenter.PresentEulaToUser())
            {
                DisableAllControls();
            }
            else
            {
                _notificationsManager.ShowNotifications();
            }
        }

        private void DisableAllControls()
        {
            EulaRejectedText.Visibility = System.Windows.Visibility.Visible;
            TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
            ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
            ApplicationBar.IsVisible = false;
        }
    }
}