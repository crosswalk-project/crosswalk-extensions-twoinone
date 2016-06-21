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
        private TabletMonitor _monitor;

        TwoinoneTest(TabletMonitor monitor)
        {
            _monitor = monitor;
            _monitor.TabletModeDelegate = onTabletModeChanged;
        }

        void run()
        {
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
                    Console.WriteLine("Tablet mode " + _monitor.IsTablet);
                }
            }
        }

        void runEmulator(Emulator emulator)
        {
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
                    emulator.IsTablet = !_monitor.IsTablet;
                }
            }
        }

        private void onTabletModeChanged(bool isTablet)
        {
            Console.WriteLine("onTabletModeChanged: " + isTablet);
        }

        static void Main(string[] args)
        {
            Emulator emulator = new Emulator();
            TabletMonitor monitor = TabletMonitorFactory.createMonitor(emulator);
            TwoinoneTest test = new TwoinoneTest(monitor);

            if (args.Length > 0 && args[0] == "emulator")
            {
                test.runEmulator(emulator);
            }
            else
            {
                test.run();
            }
        }
    }
}
/*
        }

        static void run()
        {
            Emulator emulator = new Emulator();
            TabletMonitor monitor = new TabletMonitor(emulator);
            monitor.TabletModeDelegate = onTabletModeChanged;
            monitor.start();
            Console.WriteLine("Main: " + monitor.IsTablet);

        }

        static void runEmulator()
        {
            Emulator emulator = new Emulator();
            TabletMonitor monitor = new TabletMonitor(emulator);
            monitor.TabletModeDelegate = onTabletModeChanged;
            monitor.start();
            Console.WriteLine("Main: " + monitor.IsTablet);


        }
    }
}
*/