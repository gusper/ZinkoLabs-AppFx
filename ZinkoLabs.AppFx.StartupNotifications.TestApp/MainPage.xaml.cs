using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using ZinkoLabs.AppFx.StartupNotifications;

namespace ZinkoLabs.AppFx.StartupNotifications.TestApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        //StartupNotificationsManager _notificationsManager;
        //EulaPresenter _eulaPresenter;

        public MainPage()
        {
            InitializeComponent();
            //InitializeStartupNotifications();
        }

        private void InitializeStartupNotifications()
        {
            //var disclaimerText = "Your disclaimer text goes here.";
            //_eulaPresenter = new EulaPresenter("Test App", 103, disclaimerText);
            //_notificationsManager = new StartupNotificationsManager("Test App",
            //    new StartupNotification(201, "What's new?", "We've added:\n• New feature\n• Another feature\n• Minor bug fixes"));

            //this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!_eulaPresenter.PresentEulaToUser())
            //{
            //    DisableAllControls();
            //}
            //else
            //{
            //    _notificationsManager.ShowNotifications();
            //}
        }

        private void DisableAllControls()
        {
            //EulaRejectedText.Visibility = System.Windows.Visibility.Visible;
            //TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
            //ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
            //ApplicationBar.IsVisible = false;
        }
    }
}