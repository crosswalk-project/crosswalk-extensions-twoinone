using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    class TabletMonitor : MonitorInterface
    {
        public delegate void TabletMode(bool isTablet);
        public TabletMode TabletModeDelegate;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CONVERTABLESLATEMODE = 0x2003;

        private System.Threading.Timer _timer = null;
        private bool _isTablet = false;

        Emulator _emulator;

        public TabletMonitor(Emulator emulator)
        {
            _emulator = emulator;
            emulator.TabletMonitorDelegate = onEmulatorTabletMonitorChange;

            _isTablet = GetSystemMetrics(SM_CONVERTABLESLATEMODE) == 0;
        }

        private void onEmulatorTabletMonitorChange(bool isTablet)
        {
            if (isTablet != _isTablet)
            {
                _isTablet = isTablet;
                TabletModeDelegate(_isTablet);
            }
        }

        public bool IsTablet
        {
            get
            {
                bool tm = GetSystemMetrics(SM_CONVERTABLESLATEMODE) == 0;
                if (_isTablet != tm)
                {
                    _isTablet = tm;
                    TabletModeDelegate(_isTablet);
                }
                return _isTablet;
            }
        }

        public void start()
        {
            _timer = new System.Threading.Timer(timerCb, null, 500, System.Threading.Timeout.Infinite);
        }

        public void stop()
        {
            _timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
            _timer.Dispose();
            _timer = null;
        }

        public bool isRunning()
        {
            return _timer == null;
        }

        private void timerCb(object state)
        {
            _isTablet = this.IsTablet;
        }
    }
}
