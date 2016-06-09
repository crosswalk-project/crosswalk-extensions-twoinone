using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twoinone
{
    class Emulator
    {
        public delegate void KeyboardResponse(bool haveKeyboard);
        public KeyboardResponse KeyboardMonitorDelegate;

        public delegate void TabletResponse(bool inTabletMode);
        public TabletResponse TabletMonitorDelegate;

        private bool _haveKeyboard;
        private bool _inTabletMode;

        public bool HaveKeyboard
        {
            get
            {
                return _haveKeyboard;
            }
            set
            {
                if (value != _haveKeyboard)
                {
                    _haveKeyboard = value;
                    KeyboardMonitorDelegate(_haveKeyboard);
                }
            }
        }

        public bool InTabletMode
        {
            get
            {
                return _inTabletMode;
            }
            set
            {
                if (value != _inTabletMode)
                {
                    _inTabletMode = value;
                    TabletMonitorDelegate(_inTabletMode);
                }
            }
        }
    }
}
