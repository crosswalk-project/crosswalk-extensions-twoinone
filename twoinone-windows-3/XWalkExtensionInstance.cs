// Copyright (c) 2015 Intel Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System;

namespace xwalk
{
    public class XWalkExtensionInstance
    {
        //private KeyboardMonitor _keyboardMonitor;
        private TabletMonitor _tabletMonitor;
        private Emulator _emulator;

        public XWalkExtensionInstance(dynamic native)
        {
            native_ = native;

            _emulator = new Emulator();

            //_keyboardMonitor = new KeyboardMonitor();
            //_keyboardMonitor.KeyboardModeMonitorDelegate = onMonitorKeyboard;
            //_keyboardMonitor.start();

            _tabletMonitor = TabletMonitorFactory.createMonitor(_emulator);
            _tabletMonitor.TabletModeDelegate = onMonitorTablet;
            _tabletMonitor.start();
        }

        public void HandleMessage(String message)
        {
            native_.PostMessageToJS(message);
        }

        public void HandleSyncMessage(String message)
        {
            String result = message;
            if (message == "queryKeyboard()")
            {
                //result = _keyboardMonitor.HaveKeyboard ? "true" : "false";
                result = "false";
            }
            else if (message == "queryTablet()")
            {
                result = _tabletMonitor.IsTablet ? "true" : "false";
            }
            else if (message.StartsWith("emulateSetIsTablet()"))
            {
                string payload = message.Substring("emulateSetIsTablet()".Length);
                result = _tabletMonitor.IsTablet ? "true" : "false";
                _emulator.IsTablet = (payload == "true");
            }
            native_.SendSyncReply(result);
        }

        private void onMonitorKeyboard(bool haveKeyboard)
        {
            string msg = "queryKeyboard()" + (haveKeyboard ? "true" : "false");
            System.Diagnostics.Debug.WriteLine(msg);
            native_.PostMessageToJS(msg);
        }

        private void onMonitorTablet(bool isTablet)
        {
            string msg = "queryTablet()" + (isTablet ? "true" : "false");
            System.Diagnostics.Debug.WriteLine(msg);
            native_.PostMessageToJS(msg);
        }

        private dynamic native_;
    }
}
