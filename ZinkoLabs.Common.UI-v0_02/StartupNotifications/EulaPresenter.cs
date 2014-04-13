﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Microsoft.Phone.Controls;

namespace ZinkoLabs.Common.UI.StartupNotifications
{
    public class EulaPresenter
    {
        IsolatedStorageSettings isoStore = IsolatedStorageSettings.ApplicationSettings;
        private bool alreadyAccepted;
        private string appName;
        private int appVersion;
        private string appEulaText;
        private const string EulaAcceptanceResult = "ZinkoLabs.Common.UI.EulaPresentation.UserAcceptedEula";

        public EulaPresenter(string appName, int appVersion, string appEulaText)
        {
            this.appName = appName;
            this.appVersion = appVersion;
            this.appEulaText = appEulaText;
            this.alreadyAccepted = this.IsEulaAlreadyAccepted();
        }

        public bool PresentEulaToUser()
        {
            if (this.alreadyAccepted) {
                return true;
            }

            var mbResult = MessageBox.Show(this.appEulaText, string.Format("{0} Application Terms", this.appName), MessageBoxButton.OKCancel);

            if (mbResult != MessageBoxResult.OK) {
                return false;
            }

            SaveAcceptedEulaState();
            this.alreadyAccepted = true;
            return true;
        }

        private bool IsEulaAlreadyAccepted()
        {
            if (isoStore.Contains(EulaAcceptanceResult)) {
                return (this.appVersion <= (int)isoStore[EulaAcceptanceResult]);
            }

            return false;
        }

        private void SaveAcceptedEulaState()
        {
            isoStore[EulaAcceptanceResult] = this.appVersion;
        }
    }
}