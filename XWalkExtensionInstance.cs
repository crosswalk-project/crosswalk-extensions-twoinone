// Copyright (c) 2015 Intel Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using System;

namespace xwalk
{
    public class XWalkExtensionInstance
    {
        private KeyboardMonitor _keyboardMonitor;

        public XWalkExtensionInstance(dynamic native)
        {
            native_ = native;
            _keyboardMonitor = new KeyboardMonitor();
            _keyboardMonitor.MonitorKeyboardDelegate = onMonitorKeyboard;
            _keyboardMonitor.startMonitor();
        }

        public void HandleMessage(String message)
        {
            native_.PostMessageToJS(message);
        }

        public void HandleSyncMessage(String message)
        {
            String result = message;
            if (message == "haveKeyboard()")
            {
                result = _keyboardMonitor.HaveKeyboard ? "true" : "false";
            }
            native_.SendSyncReply(result);
        }

        private void onMonitorKeyboard(bool haveKeyboard)
        {
            native_.PostMessageToJS(haveKeyboard ? "true" : "false");
        }

        private dynamic native_;
    }
}
