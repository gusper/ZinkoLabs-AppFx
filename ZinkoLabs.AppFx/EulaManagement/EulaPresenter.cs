using Microsoft.Phone.Controls;
using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.EulaManagement
{
    public class EulaPresenter
    {
        IsolatedStorageSettings _settings = IsolatedStorageSettings.ApplicationSettings;
        bool _eulaResponseNeeded;
        bool _userAcceptedEula;
        string _appName;
        int _appVersion;
        string _eulaContent;
        CustomMessageBox _eulaMessageBox;
        const string _eulaAcceptanceResult = "ZinkoLabs.AppFx.EulaPresentation.UserAcceptedEula";

        public EulaPresenter(string appName, int appVersion, string eulaContent)
        {
            _appName = appName;
            _appVersion = appVersion;
            _eulaContent = eulaContent;
            _eulaResponseNeeded = !HasEulaAlreadyBeenAccepted();
        }

        public bool EulaResponseNeeded
        {
            get { return _eulaResponseNeeded; }
        }

        public bool UserAcceptedEula
        {
            get { return _userAcceptedEula; }
        }

        public event EulaAnsweredEventHandler Answered;

        public void PresentEulaToUser()
        {
            _eulaMessageBox = new CustomMessageBox() {
                Caption = string.Format("{0} App Terms", _appName),
                Message = _eulaContent,
                LeftButtonContent = "accept",
                RightButtonContent = "reject"
            };

            _eulaMessageBox.Dismissed += (s1, e1) => {
                bool result = (e1.Result == CustomMessageBoxResult.LeftButton);                
                if (result)
                {
                    _userAcceptedEula = true;
                    _eulaResponseNeeded = false;
                    SaveAcceptedEulaState();
                }

                Answered(this, new EulaAnsweredEventArgs(result));
            };

            _eulaMessageBox.Show();
        }

        private void SaveAcceptedEulaState()
        {
            _settings[_eulaAcceptanceResult] = _appVersion;
        }

        private bool HasEulaAlreadyBeenAccepted()
        {
            if (_settings.Contains(_eulaAcceptanceResult))
            {
                return (_appVersion <= (int)_settings[_eulaAcceptanceResult]);
            }

            return false;
        }
    }

    public delegate void EulaAnsweredEventHandler(object sender, EulaAnsweredEventArgs e);

    public class EulaAnsweredEventArgs
    {
        bool _eulaAccepted;

        public EulaAnsweredEventArgs(bool isAccepted)
        {
            _eulaAccepted = isAccepted;
        }

        public bool EulaAccepted
        {
            get { return _eulaAccepted; }
        }
    }
}
