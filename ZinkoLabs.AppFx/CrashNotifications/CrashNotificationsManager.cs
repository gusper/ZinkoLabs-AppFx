using Microsoft.Phone.Tasks;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace ZinkoLabs.AppFx.CrashNotifications
{
    public static class CrashNotificationsManager
    {
        const string _filename = "ZinkoLabs.AppFx.CrashNotificationData.txt";

        public static void ReportException(Exception ex, string extra)
        {
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    SafeDeleteFile(store);
                    using (TextWriter output = new StreamWriter(store.CreateFile(_filename)))
                    {
                        output.WriteLine(extra);
                        output.WriteLine(ex.Message);
                        output.WriteLine(ex.StackTrace);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void CheckForPreviousException(string appName, string supportEmailAddress)
        {
            try
            {
                string contents = null;
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (store.FileExists(_filename))
                    {
                        using (TextReader reader = new StreamReader(store.OpenFile(_filename, FileMode.Open, FileAccess.Read, FileShare.None)))
                        {
                            contents = reader.ReadToEnd();
                        }

                        SafeDeleteFile(store);
                    }
                }
                if (contents != null)
                {
                    if (MessageBox.Show("A problem occurred the last time you ran this application. Would you like to send an email to report it?", "Problem Report", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        var email = new EmailComposeTask();
                        email.To = supportEmailAddress;
                        email.Subject = string.Format("{0} Problem Report", appName);
                        email.Body = contents;
                        SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication());
                        email.Show();
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                SafeDeleteFile(IsolatedStorageFile.GetUserStoreForApplication());
            }
        }

        private static void SafeDeleteFile(IsolatedStorageFile store)
        {
            try
            {
                store.DeleteFile(_filename);
            }
            catch (Exception)
            {
            }
        }
    }
}
