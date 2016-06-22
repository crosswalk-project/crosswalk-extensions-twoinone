// Copyright (c) 2016 Intel Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license
// that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    public abstract class TabletMonitor : MonitorInterface
    {
        public delegate void TabletMode(bool isTablet);
        public TabletMode TabletModeDelegate;

        public abstract bool IsTablet
        {
            get;
        }
        public abstract bool isRunning();
        public abstract void start();
        public abstract void stop();
    }
}
