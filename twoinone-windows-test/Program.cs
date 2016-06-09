using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    class Program
    {
        static void Main(string[] args)
        {
            TabletMonitor m = new TabletMonitor();
            m.MonitorTabletDelegate = onTabletModeChanged;
            m.start();
            Console.WriteLine("Main: " + m.IsTablet);

            Console.ReadLine();
        }

        private static void onTabletModeChanged(bool isTablet)
        {
            Console.WriteLine("onTabletModeChanged: " + isTablet);
        }
    }
}
