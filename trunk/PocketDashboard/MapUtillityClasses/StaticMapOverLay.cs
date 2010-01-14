using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace MapUtilities
{
    [Serializable]
    public class StaticMapOverLay
    {
        public double m_latitude;
        public double m_longitude;
        public int m_zoom;
        public double m_width;
        public double m_height;
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Image m_Image;
        MapsUtils util;
        public byte[] PictureByteArray
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                m_Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
            set
            {
                MemoryStream ms = new MemoryStream(value);
                Image returnImage = new Bitmap(ms); //Image.FromStream(ms);
                m_Image= returnImage;
            }
        }
        public StaticMapOverLay()
        {
            util = new MapsUtils();
        }
        public StaticMapOverLay(string map_key, double latitude, double longitude, int zoom, int width, int height)
        {
            this.m_latitude = latitude;
            this.m_longitude = longitude;
            this.m_zoom = zoom;
            this.m_width = width;
            this.m_height = height;
            this.util = new MapsUtils(map_key);
        }
        public Image GetImage()
        {
            return m_Image;
        }
        public int GetZoom()
        {
            return this.m_zoom;
        }
        public StaticMapOverLay(string map_key, LatLng loc1,LatLng loc2, int width, int height)
        {
            this.util = new MapsUtils(map_key);
            this.m_latitude = (loc1.Lat+loc2.Lat)/2;
            this.m_longitude = (loc1.Lng + loc2.Lng) / 2; ;
            this.m_zoom = util.GetSuiatbleZoomForPoints(loc1,loc2,width,height);
            this.m_width = width;
            this.m_height = height;
            
        }
        public StaticMapOverLay(string map_key, LatLng center, int width, int height)
        {
            this.util = new MapsUtils(map_key);
            this.m_latitude = center.Lat;
            this.m_longitude = center.Lng;
            this.m_zoom = 2;
            this.m_width = width;
            this.m_height = height;

        }
#if DESKTOP_VERSION
        public void LoadImageFromFile(string filename)
        {
            Image m = Image.FromFile(filename);
            this.m_Image = m;
        }
        public void LoadImageFromWeb()
        {
            WebRequest wr = WebRequest.Create(util.GetStaticMapUrl("" + this.m_latitude, "" + this.m_longitude, this.m_zoom, (int)this.m_width, (int)this.m_height));
            WebResponse resp = wr.GetResponse();
            m_Image = Image.FromStream(resp.GetResponseStream());
        }
#endif
        public Image GetImageAndDrawMyLocation(double latitude, double longitude)
        {
            PixelPoint mypoint = util.GetCordinateOnStaticImage(latitude, longitude, this.m_latitude, this.m_longitude, this.m_zoom, this.m_width, this.m_height);
            Bitmap bmp;
            if (this.m_Image != null)
            {
                bmp = new Bitmap(this.m_Image);
            }
            else
            {
                bmp = new Bitmap((int)this.m_width, (int)this.m_height);
            }

            Graphics g = Graphics.FromImage(bmp);
            //g.DrawEllipse(new Pen(Color.Blue, 5), (int)((float)mypoint.x - 3), (int)((float)mypoint.y) - 3, 7, 7);
            DrawMarker(g, new LatLng(latitude, latitude), Color.Blue);
            return bmp;
        }
        public PixelPoint GetPointOnMe(LatLng Loc)
        {
            PixelPoint mypoint = util.GetCordinateOnStaticImage(Loc.Lat, Loc.Lng, this.m_latitude, this.m_longitude, this.m_zoom, this.m_width, this.m_height);
            return mypoint;

        }
        public Image GetImageAndDrawMyLocationNPolyLine(double latitude, double longitude,List<LatLng> PolyLine)
        {
           // PixelPoint mypoint = GetPointOnMe(new LatLng(latitude, longitude));
            Bitmap bmp;
            if (this.m_Image != null)
            {
                bmp = new Bitmap(this.m_Image);

            }
            else
            {
                bmp = new Bitmap((int)this.m_width, (int)this.m_height);
            }

            Graphics g = Graphics.FromImage(bmp);
            DrawMarker(g, new LatLng(latitude, longitude), Color.Blue);

            DrawPolyLine(g, PolyLine);

            return bmp;
        }
        private void DrawPolyLine(Graphics g, List<LatLng> PolyLine)
        {
            Point[] Poly = new Point[PolyLine.Count];
            int counter = 0;
            foreach (LatLng loc in PolyLine)
            {
                PixelPoint pp = GetPointOnMe(loc);
                Point p = new Point((int)pp.x, (int)pp.y);
                Poly[counter] = p;
                counter++;
            }
#if DESKTOP_VERSION
            SolidBrush trnsBlueBrush = new SolidBrush(Color.FromArgb(120, 255, 0, 0));
            g.DrawLines(new Pen(trnsBlueBrush, 5), Poly);
#else
            g.DrawLines(new Pen(Color.Red), Poly);
#endif
            
            
            

        }
        private void DrawMarker(Graphics g,LatLng loc, Color c)
        {
            PixelPoint mypoint = GetPointOnMe(loc);
            Pen p = new Pen(c, 5);
            g.DrawEllipse(p, (int)((float)mypoint.x - 3), (int)((float)mypoint.y - 3), 7, 7);
        }
        public Image GetImageAndDrawMyLocationNPolyLineNTurn(double latitude, double longitude, List<LatLng> PolyLine,List<DirectionStep> steps,ref PixelPoint CurLoc)
        {
            PixelPoint mypoint = GetPointOnMe(new LatLng(latitude, longitude));
            CurLoc = mypoint;
            Bitmap bmp;
            if (this.m_Image != null)
            {
                bmp = new Bitmap(this.m_Image);

            }
            else
            {
                bmp = new Bitmap((int)this.m_width, (int)this.m_height);
            }

            Graphics g = Graphics.FromImage(bmp);
            DrawMarker(g, new LatLng(latitude, longitude), Color.Blue);

            DrawPolyLine(g, PolyLine);

            foreach (DirectionStep d in steps)
            {
                this.DrawMarker(g, d.step_Location, Color.Red);
            }
            
            return bmp;
        }
        /// <summary>
        /// finds whether the given lat lng can be mapped on this static image or not.
        /// </summary>
        /// <returns></returns>
        public bool IsLocationMapable(double latitude, double longitude)
        {
            PixelPoint mypoint = util.GetCordinateOnStaticImage(latitude, longitude, this.m_latitude, this.m_longitude, this.m_zoom, this.m_width, this.m_height);
            if (mypoint.x < this.m_width && mypoint.x >= 0 && mypoint.y >= 0 && mypoint.y < this.m_height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
