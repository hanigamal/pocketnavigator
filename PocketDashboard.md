# Pocketdashboard Introduction #

Pocketdashboard is mobile app that consumes the XML exported by desktop app, to display turn by turn navigation on windows mobile phone (offline)

Additionally it has another screen to display speed and lat, lag , which can be used as dashboard of your bike.

## How it Uses GPS? ##
You can see class GPS as a wrapper, around these native APIs
```
     #region PInvokes to gpsapi.dll
        [DllImport("gpsapi.dll")]
        static extern IntPtr GPSOpenDevice(IntPtr hNewLocationData, IntPtr hDeviceStateChange, string szDeviceName, int dwFlags);

        [DllImport("gpsapi.dll")]
        static extern int  GPSCloseDevice(IntPtr hGPSDevice);

        [DllImport("gpsapi.dll")]
        static extern int  GPSGetPosition(IntPtr hGPSDevice, IntPtr pGPSPosition, int dwMaximumAge, int dwFlags);

        [DllImport("gpsapi.dll")]
        static extern int  GPSGetDeviceState(IntPtr pGPSDevice);
        #endregion

        #region PInvokes to coredll.dll
        [DllImport("coredll.dll")]
        static extern IntPtr CreateEvent(IntPtr lpEventAttributes, int bManualReset, int bInitialState, StringBuilder lpName);

        [DllImport("coredll.dll")]
        static extern int CloseHandle(IntPtr hObject);

        const int waitFailed = -1;
        [DllImport("coredll.dll")]
        static extern int WaitForMultipleObjects(int nCount, IntPtr lpHandles, int fWaitAll, int dwMilliseconds);

        const int eventSet = 3;
        [DllImport("coredll.dll")]
        static extern int EventModify(IntPtr hHandle, int dwFunc);
        
#endregion
```