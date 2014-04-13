using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ZinkoLabs.Common.UI.StartupNotifications
{
    public class StartupNotification
    {
        private int id;
        private string title;
        private string message;

        public int Id { get { return this.id; } }
        public string Title { get { return this.title; } }
        public string Message { get { return this.message; } }

        public StartupNotification(int id, string title, string message)
        {
            this.id = id;
            this.title = title;
            this.message = message;
        }
    }
}
