using System;
using System.Collections.Generic;
using System.Text;
using GPSUtils;

namespace PocketDashboard
{
    public interface INeedGps
    {
        void GetNewPosition(GpsPosition pos);
    }
    delegate void NewPositionHandler(GpsPosition pos);
}
