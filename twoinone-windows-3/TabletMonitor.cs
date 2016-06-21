using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    public abstract class TabletMonitor : MonitorInterface
    {
        public delegate void TabletMode(bool isTablet);
        public TabletMode TabletModeDelegate;

        public abstract bool IsTablet
        {
            get;
        }
        public abstract bool isRunning();
        public abstract void start();
        public abstract void stop();
    }
}
