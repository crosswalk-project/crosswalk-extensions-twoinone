using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    public interface MonitorInterface
    {
        void start();
        void stop();
        bool isRunning();
    }
}
