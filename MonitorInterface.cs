using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twoinone
{
    interface MonitorInterface
    {
        void startMonitor();
        void stopMonitor();
        bool isRunning();
    }
}
