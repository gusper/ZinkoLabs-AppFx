using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.StartupNotifications
{
    public class EulaPresenter
    {
        IsolatedStorageSettings _isoStore = IsolatedStorageSettings.ApplicationSettings;
        bool _alreadyAccepted;
        string _appName;
        int _appVersion;
        string _appEulaText;
        const string _eulaAcceptanceResult = "ZinkoLabs.AppFx.EulaPresentation.UserAcceptedEula";

        public EulaPresenter(string appName, int appVersion, string appEulaText)
        {
            _appName = appName;
            _appVersion = appVersion;
            _appEulaText = appEulaText;
            _alreadyAccepted = IsEulaAlreadyAccepted();
        }

        public bool PresentEulaToUser()
        {
            if (_alreadyAccepted)
                return true;

            var mbResult = MessageBox.Show(_appEulaText, string.Format("{0} Application Terms", _appName), MessageBoxButton.OKCancel);

            if (mbResult != MessageBoxResult.OK)
                return false;

            SaveAcceptedEulaState();
            _alreadyAccepted = true;
            return true;
        }

        internal bool IsEulaAlreadyAccepted()
        {
            if (_isoStore.Contains(_eulaAcceptanceResult))
                return (_appVersion <= (int)_isoStore[_eulaAcceptanceResult]);

            return false;
        }

        internal void SaveAcceptedEulaState()
        {
            _isoStore[_eulaAcceptanceResult] = _appVersion;
        }
    }
}
