using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    class TabletMonitorFactory
    {
        protected TabletMonitorFactory()
        {}

        public static TabletMonitor createMonitor(Emulator emulator)
        {
            Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();
            System.Diagnostics.Debug.WriteLine(deviceInfo.OperatingSystem.ToString());


            return null;
        }
    }
}
