using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinkoLabs.AppFx.EulaManagement
{
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

    public delegate void EulaAnsweredEventHandler(object sender, EulaAnsweredEventArgs e);
}
