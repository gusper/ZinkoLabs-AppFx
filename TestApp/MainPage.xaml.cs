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
using ZinkoLabs.AppFx.EulaManagement;

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
            var disclaimerText = "Your application terms goes here.";
            _eulaPresenter = new EulaPresenter("Test App", 104, disclaimerText);
            _notificationsManager = new StartupNotificationsManager("Test App",
                new StartupNotification(202, "What's new?", "We've added:\n• New feature\n• Another feature\n• Minor bug fixes"));

            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            EnsureEulaIsAccepted();
        }

        /// <summary>
        /// Handles showing the EULA to the user and transitioning states once
        /// it has gotten the user's response to the terms.
        /// </summary>
        private void EnsureEulaIsAccepted()
        {
            if (_eulaPresenter.EulaResponseNeeded)
            {
                _eulaPresenter.Answered += (s1, e1) => {
                    if (e1.EulaAccepted)
                    {
                        _notificationsManager.ShowNotifications();
                    }
                    else
                    {
                        RestrictFunctionalityAfterEulaRejection();
                    }
                };

                _eulaPresenter.PresentEulaToUser();
            }
        }

        /// <summary>
        /// Appropriately enables and disables functionality in response
        /// to the user rejecting the app's terms.
        /// </summary>
        private void RestrictFunctionalityAfterEulaRejection()
        {
            // Show your EULA rejection information
            EulaRejectedText.Visibility = System.Windows.Visibility.Visible;

            // Hide your app's actual functionality
            TitlePanel.Visibility = System.Windows.Visibility.Collapsed;
            ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}