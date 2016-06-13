using System;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Threading;

class RegistryTest
{
    static int _tabletMode = 0;

    static void Main()
    {
        Timer timer = new Timer(timerCb, null, 0, 500);

        // Poor man's mainloop
        int tick = 0;
        while (true)
        {
            Thread.Sleep(500);
            Console.Write(".");
            tick++;
            if (tick % 10 == 0)
            {
                Console.WriteLine("");
            }
        }
    }

    private static void timerCb(object state)
    {
        int tm = readKey();
        if (tm != _tabletMode) {
            _tabletMode = tm;
            Console.WriteLine("");
            Console.WriteLine("TabletMode: " + readKey());
        }
    }

    static int readKey()
    {
        object value = Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ImmersiveShell", "TabletMode", 0);
        return Int32.Parse(value.ToString());
    }
}
