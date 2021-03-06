﻿// Copyright (c) 2016 Intel Corporation. All rights reserved.
// Use of this source code is governed by a BSD-style license
// that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xwalk
{
    public class TabletMonitorFactory
    {
        protected TabletMonitorFactory()
        {}

        public static TabletMonitor createMonitor(Emulator emulator)
        {
            object value = Microsoft.Win32.Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentMajorVersionNumber", 0);
            if (Int32.Parse(value.ToString()) >= 10)
            {
                return new Win10TabletMonitor(emulator);
            }

            return new Win8TabletMonitor(emulator);
        }
    }
}
