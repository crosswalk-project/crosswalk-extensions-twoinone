using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace xwalk
{
    class KeyboardMonitor
    {
        public KeyboardResponse MonitorKeyboardDelegate;
        public KeyboardResponse QueryKeyboardDelegate;
        public delegate void KeyboardResponse(bool haveKeyboard);

        private DeviceWatcher _dw = null;
        private Dictionary<string, bool> _keyboardDevices = new Dictionary<string, bool>();
        private bool _isQuery;

        public KeyboardMonitor()
        {
        }

        public void QueryKeyboard()
        {
            if (_dw != null)
            {
                // Already running. TODO figure out how to print this to JS console.
                return;
            }

            _dw = DeviceInformation.CreateWatcher();
            _dw.Added += onAdded;
            _dw.EnumerationCompleted += onEnumerationCompleted;
            _dw.Removed += onRemoved;
            _dw.Stopped += onStopped;
            _dw.Updated += onUpdated;

            _isQuery = true;
            _dw.Start();
        }

        public void MonitorKeyboard()
        {
            if (_dw != null)
            {
                // Already running. TODO figure out how to print this to JS console.
                return;
            }

            // TODO use background trigger instead
            // https://msdn.microsoft.com/en-us/library/windows/apps/mt187355.aspx#watch_devices_as_a_background_task

            _dw = DeviceInformation.CreateWatcher();
            _dw.Added += onAdded;
            _dw.EnumerationCompleted += onEnumerationCompleted;
            _dw.Removed += onRemoved;
            _dw.Stopped += onStopped;
            _dw.Updated += onUpdated;

            _dw.Start();
        }

        public bool HaveKeyboard
        {
            get
            {
                bool haveKeyboard = false;
                foreach (KeyValuePair<string, bool> pair in _keyboardDevices)
                {
                    if (pair.Value.ToString().ToLower() == "true")
                    {
                        haveKeyboard = true;
                    }
                }
                return haveKeyboard;
            }
        }

        public void Stop()
        {
            _dw.Stop();
        }

        public bool isRunning()
        {
            return _dw.Status == DeviceWatcherStatus.Started ||
                   _dw.Status == DeviceWatcherStatus.EnumerationCompleted;
        }

        private void onUpdated(DeviceWatcher dw, DeviceInformationUpdate diu)
        {
            // Update device if we recognise it as a keyboard
            if (_keyboardDevices.ContainsKey(GuidFromId(diu.Id)))
            {
                Object isEnabled;
                diu.Properties.TryGetValue("System.Devices.InterfaceEnabled", out isEnabled);
                bool enabled = isEnabled.ToString().ToLower() == "true";
                _keyboardDevices[GuidFromId(diu.Id)] = enabled;
                MonitorKeyboardDelegate(enabled);
            }
        }

        private void onStopped(DeviceWatcher dw, object args)
        {
            System.Diagnostics.Debug.WriteLine("onStopped");
            _dw = null;
            _keyboardDevices.Clear();
        }

        private void onRemoved(DeviceWatcher dw, DeviceInformationUpdate diu)
        {
            if (_keyboardDevices.ContainsKey(GuidFromId(diu.Id)))
            {
                Object isEnabled;
                diu.Properties.TryGetValue("System.Devices.InterfaceEnabled", out isEnabled);
                bool enabled = isEnabled.ToString().ToLower() == "true";
                _keyboardDevices.Remove(GuidFromId(diu.Id));

                // Only notify if removed device was enabled
                if (enabled)
                {
                    MonitorKeyboardDelegate(false);
                }
            }
        }

        private void onEnumerationCompleted(DeviceWatcher dw, object args)
        {
            System.Diagnostics.Debug.WriteLine("onEnumerationCompleted");

            if (_isQuery)
            {
                QueryKeyboardDelegate(this.HaveKeyboard);
                _dw.Stop();
            }
            else
            {
                MonitorKeyboardDelegate(this.HaveKeyboard);
            }
        }

        private void onAdded(DeviceWatcher dw, DeviceInformation di)
        {
            // Fuzzy matching. At some point we should rather query the registry for device info including class
            // See https://msdn.microsoft.com/en-us/library/windows/hardware/jj649944(v=vs.85).aspx
            if (di.Name.ToLower().Contains("keyboard"))
            {
                Object isEnabled;
                di.Properties.TryGetValue("System.Devices.InterfaceEnabled", out isEnabled);
                _keyboardDevices[GuidFromId(di.Id)] = di.IsEnabled;

                // Only notify if monitoring, otherwise this happens only in enumeration-completed
                if (!_isQuery)
                {
                    MonitorKeyboardDelegate(di.IsEnabled);
                }
            }
        }

        private String GuidFromId(String deviceId)
        {
            return deviceId.Substring(deviceId.LastIndexOf('{'));
        }
    }
}
