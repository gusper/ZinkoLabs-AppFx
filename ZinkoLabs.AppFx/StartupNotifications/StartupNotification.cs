using System;

namespace ZinkoLabs.AppFx.StartupNotifications
{
    public class StartupNotification
    {
        int _id;
        string _title;
        string _message;

        public int ID { get { return _id; } }
        public string Title { get { return _title; } }
        public string Message { get { return _message; } }

        public StartupNotification(int id, string title, string message)
        {
            _id = id;
            _title = title;
            _message = message;
        }
    }
}
