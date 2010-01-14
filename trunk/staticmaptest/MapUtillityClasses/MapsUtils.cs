using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;

namespace MapUtilities
{
       
    public class MapsUtils
    {
        string key;
        public MapsUtils(string key)
        {
            
        }
        public MapsUtils()
        {
            /*this is demo key*/
            key = "ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSsTL4WIgxhMZ0ZK_kHjwHeQuOD4xQJpBVbSrqNn69S6DOTv203MQ5ufA";
        }
        public string GetStaticMapUrl(string latitude, string longitude,int zoom,int width,int height)
        {
            string url = "http://maps.google.com/staticmap?center=" + latitude + "," + longitude + "&zoom=" + zoom + "&size=" + width + "x" + height + "&sensor=false&key=" + key;
            return url;
        }
        double toRad(double value)
        {

            return value * Math.PI / 180;
        }
        double toDeg(double value)
        {

            return value * 180 / Math.PI;
        }
        double toBearing(double value)
        {

            return (toDeg(value) + 360) % 360;
        }
        public double GetBearing(LatLng GLatLng1, LatLng GLatLng2) 
        {

            double lat1 = GLatLng1.Lat;
            double lon1 = GLatLng1.Lng;
            double lat2 = GLatLng2.Lat;
            double lon2 = GLatLng2.Lng;
            lat1 = toRad(lat1);
            lat2 = toRad(lat2);
            double dLon = toRad(lon2 - lon1);

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            return toBearing( Math.Atan2(y, x));
        }
        public double GetDistanceBetweenPoints(double latitudes1, double longitudes1, double latitudes2, double longitudes2)
        {
            double R = 6371; // km
            double dLat = toRad(latitudes2 - latitudes1);
            double dLon = toRad(longitudes1 - longitudes2);
            //latitudes1 = toRad(latitudes1);
            //latitudes2 = toRad(latitudes2);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(toRad(latitudes1)) * Math.Cos(toRad(latitudes2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;
            return Math.Abs( d);
        }
        public double GetDistanceBetweenPoints(LatLng p1, LatLng p2)
        {
            return GetDistanceBetweenPoints(p1.Lat, p1.Lng, p2.Lat, p2.Lng);

        }
        public double GetDistanceBetweenLineAndPointInUnknownUnit(double x1, double y1, double x2, double y2, double x0, double y0)
        {
            double distance = Math.Abs((x2 - x1) * (y1 - y0) - (x1 - x0) * (y2 - y1)) / Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1)*(y2 - y1));
            return distance;
        }
        public double GetDistanceBetweenLineAndPointInUnknownUnit(LatLng LinePoint1 ,LatLng LinePoint2,LatLng Point)
        {
            /**/
            
            PixelPoint pxLinePoint1 = FlatenMyLocation(LinePoint1);
            PixelPoint pxLinePoint2 = FlatenMyLocation(LinePoint2);
            PixelPoint pxPoint = FlatenMyLocation(Point);
            return GetDistanceBetweenLineAndPointInUnknownUnit(pxLinePoint1.x, pxLinePoint1.y, pxLinePoint2.x, pxLinePoint2.y, pxPoint.x, pxPoint.y);
   
        }
        PixelPoint FlatenMyLocation(LatLng loc)
        {
            double target_y = LatToY(loc.Lat );
            double target_x = LongToX(loc.Lng);
            return new PixelPoint(target_x, target_y);
        }
        double LatToY(double lat)
        {
            double offset = 268435456; 
            double radius = offset / Math.PI; 
            return (offset - radius * Math.Log((1 + Math.Sin(lat * Math.PI / 180)) / (1 - Math.Sin(lat * Math.PI / 180))) / 2);
        }
        double LongToX(double lon)
        {
            double offset = 268435456; 
            double radius = offset / Math.PI;
            return (offset + radius * lon * Math.PI / 180);    
        }
        public bool isPixelWithInRect(PixelPoint p, double width, double height)
        {
            if ((p.x >= 0 && p.x < width) && (p.y >= 0 && p.y < height))
            {
                return true;
            }
            return false;
        }
        public int GetSuiatbleZoomForPoints(LatLng P1,LatLng P2, double width, double height)
        {
            LatLng center = new LatLng((P1.Lat + P2.Lat) / 2, (P1.Lng + P2.Lng) / 2);
            int zoom = 16;
            for (zoom = 16; zoom > 2; zoom--)
            {
                PixelPoint px1 = GetCordinateOnStaticImage(P1, center, zoom, width, height);
                PixelPoint px2 = GetCordinateOnStaticImage(P2, center, zoom, width, height);
                if (isPixelWithInRect(px1, width, height) && isPixelWithInRect(px2, width, height))
                {
                    return zoom;
                }
            }
            return zoom;
        }
        public PixelPoint GetCordinateOnStaticImage(LatLng Position, LatLng Center, int zoom, double width, double height)
        {
            return GetCordinateOnStaticImage(Position.Lat, Position.Lng, Center.Lat, Center.Lng, zoom, width, height);
        }
        public PixelPoint GetCordinateOnStaticImage(double latitude, double longitude, double centerLat, double centerLang, int zoom,double width,double height)
        {

               long val = 1 << ((21 - zoom));
               double target_y = LatToY(latitude); 
               double target_x = LongToX(longitude);
               double delta_x = (((target_x - LongToX(centerLang))) / (val));
               double delta_y  =(((target_y - LatToY(centerLat)) )/ (val));
               double marker_x = (width/2) + delta_x;
               double marker_y = (height/2) + delta_y;
               PixelPoint p = new PixelPoint(marker_x,marker_y);
               return p;
        }
        
    }
}
