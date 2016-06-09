using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twoinone
{
    class TabletMontitor : MonitorInterface
    {
        public ModeResponse ModeDelegate;
        public delegate void ModeResponse(bool inSlateMode);


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);
        private const int SM_CONVERTABLESLATEMODE = 0x2003;

        private System.Threading.Timer _timer = null;
        private bool _inTabletMode = false;

        public TabletMontitor()
        {
        }

        public bool InTabletMode
        {
            get
            {
                bool tm = GetSystemMetrics(SM_CONVERTABLESLATEMODE) == 0;
                if (_inTabletMode != tm)
                {
                    _inTabletMode = tm;
                    ModeDelegate(_inTabletMode);
                }
                return _inTabletMode;
            }
        }

        public void startMonitor()
        {
            _timer = new System.Threading.Timer(timerCb, null, 500, System.Threading.Timeout.Infinite);
        }

        public void stopMonitor()
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
            _inTabletMode = this.InTabletMode;
        }
    }
}
