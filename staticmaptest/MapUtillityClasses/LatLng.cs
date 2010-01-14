using System;
using System.Collections.Generic;
using System.Text;

namespace MapUtilities
{
    public struct LatLng
    {
        public double Lat;
        public double Lng;
        public LatLng(double aLat, double aLng)
        {
            Lat = aLat;
            Lng = aLng;
        }
    }
}
