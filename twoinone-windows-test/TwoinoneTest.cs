using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace xwalk
{
    class TwoinoneTest
    {
        static void Main(string[] args)
        {
            TabletMonitorFactory.createMonitor(null);

            if (args.Length > 0 && args[0] == "emulator")
            {
                runEmulator();
            }
            else
            {
                run();
            }
        }

        static void run()
        {
            Emulator emulator = new Emulator();
            TabletMonitor monitor = new TabletMonitor(emulator);
            monitor.TabletModeDelegate = onTabletModeChanged;
            monitor.start();
            Console.WriteLine("Main: " + monitor.IsTablet);

            // Fudge mainloop
            int tick = 0;
            while (true)
            {
                Thread.Sleep(500);
                Console.Write(".");
                tick++;
                if (tick % 10 == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Tablet mode " + monitor.IsTablet);
                }
            }
        }

        static void runEmulator()
        {
            Emulator emulator = new Emulator();
            TabletMonitor monitor = new TabletMonitor(emulator);
            monitor.TabletModeDelegate = onTabletModeChanged;
            monitor.start();
            Console.WriteLine("Main: " + monitor.IsTablet);

            bool isTabletEmulated = monitor.IsTablet;

            // Fudge mainloop
            int tick = 0;
            while (true)
            {
                Thread.Sleep(500);
                Console.Write(".");
                tick++;
                if (tick % 10 == 0)
                {
                    Console.WriteLine("");
                    isTabletEmulated = !isTabletEmulated;
                    emulator.IsTablet = isTabletEmulated;
                }
            }
        }

        private static void onTabletModeChanged(bool isTablet)
        {
            Console.WriteLine("onTabletModeChanged: " + isTablet);
        }
    }
}
