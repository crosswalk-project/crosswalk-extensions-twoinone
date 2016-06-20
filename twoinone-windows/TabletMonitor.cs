using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    public delegate void TabletMode(bool isTablet);

    interface TabletMonitor : MonitorInterface
    {
        bool IsTablet
        {
            get;
        }
    }
}
