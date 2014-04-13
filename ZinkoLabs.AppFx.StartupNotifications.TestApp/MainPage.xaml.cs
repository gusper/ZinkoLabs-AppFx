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
        private StartupNotificationsManager notificationsManager;
        private EulaPresenter eulaPresenter;

        public MainPage()
        {
            InitializeComponent();
            InitializeStartupNotifications();
        }

        private void InitializeStartupNotifications()
        {
            string disclaimerText = "In no event shall Zinko Labs LLC be liable for any damages whatsoever including but not limited to direct, indirect, special, incidental or consequential damages, or injuries to an individual’s health resulting from use of this tool.\n\nTap the 'ok' button below to accept these terms. If you do not accept these terms, please close and uninstall the app.";
            this.eulaPresenter = new EulaPresenter("Calendar Calculator", 103, disclaimerText);
            notificationsManager = new StartupNotificationsManager("Calendar Calculator",
                new StartupNotification(201, "What's new?", "We've added:\n• Live Tile support\n• Notifications for when to take meds\n• Minor bug fixes\n\nOther recent notable changes:\n• Support for reminders to take meds\n• Mango support\n• Live Tiles support\n• Fast app switching\n• Better tombstoning support\n• Some bug fixes"));

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }
                
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!eulaPresenter.PresentEulaToUser()) {
                DisableAllControls();
            }
            else {
                notificationsManager.ShowNotifications();
            }
        }

        private void DisableAllControls()
        {
            EulaRejectedText.Visibility = System.Windows.Visibility.Visible;
            TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
            ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
            this.ApplicationBar.IsVisible = false;
        }
    }
}