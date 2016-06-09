using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    class TabletMonitor : MonitorInterface
    {
        public ModeResponse MonitorTabletDelegate;
        public delegate void ModeResponse(bool isTablet);


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CONVERTABLESLATEMODE = 0x2003;

        private System.Threading.Timer _timer = null;
        private bool _isTablet = false;

        public TabletMonitor()
        {
        }

        public bool IsTablet
        {
            get
            {
                bool tm = GetSystemMetrics(SM_CONVERTABLESLATEMODE) == 0;
                if (_isTablet != tm)
                {
                    _isTablet = tm;
                    MonitorTabletDelegate(_isTablet);
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
