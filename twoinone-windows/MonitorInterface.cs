using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    interface MonitorInterface
    {
        void start();
        void stop();
        bool isRunning();
    }
}
