using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Speech;
using MapUtilities;


namespace StaticMapTest
{
    public partial class Form1 : Form
    {
        MapsUtils util = new MapsUtils();
        string key = "ABQIAAAAzr2EBOXUKnm_jVnk0OJI7xSsTL4WIgxhMZ0ZK_kHjwHeQuOD4xQJpBVbSrqNn69S6DOTv203MQ5ufA";
        //StaticMapOverLay static_img;
        DirectionInfo m_DirectionInfo=null;
        string prev_timer_lat="";
        string prev_timer_lng="";
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //    string lat= this.webBrowser1.Document.GetElementById("Lat").InnerText;
        //    string lng = this.webBrowser1.Document.GetElementById("Lang").InnerText;
        //    string str_zoom = this.webBrowser1.Document.GetElementById("Zoom").InnerText;
        //    int zoom = int.Parse(str_zoom);

        //    static_img = new GoogleMapStaticImage(this.key, double.Parse(lat), double.Parse(lng), zoom, 400, 400);
        //    static_img.LoadImageFromWeb();
        //    this.pictureBox1.Image = static_img.GetImage();
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            string MyPath=System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            webBrowser1.Navigate(new Uri("file://" + MyPath + "\\ClickMap.htm"));
        }

        private void btPutMarker_Click(object sender, EventArgs e)
        {
            //double lat = double.Parse(this.webBrowser1.Document.GetElementById("Lat").InnerText);
            //double lng =  double.Parse(this.webBrowser1.Document.GetElementById("Lang").InnerText);
            //string str_zoom = this.webBrowser1.Document.GetElementById("Zoom").InnerText;
            //if (static_img.IsLocationMapale(lat, lng))
            //{
            //    this.pictureBox1.Image = static_img.GetImageAndDrawMyLocation(lat, lng);
            //}
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string MyPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            webBrowser1.Navigate(new Uri("file://" + MyPath + "\\ClickMap.htm"));

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (webBrowser1.Url.ToString().IndexOf("DirectionsTest.html") >= 0)
            {
                //setDirections("5995 Pacific Mesa Ct, San Diego, CA 92121", "4550 La Jolla Village Dr, San Diego, CA 92122", "en_US");
                webBrowser1.Navigate("javascript:setDirections(\"" + txtFrom.Text + "\",\"" + txtTo.Text + "\", \"en_US\");");
            }
            else
            {
                MessageBox.Show("Click Load Direction first");
            }
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            //if (webBrowser1.Url.ToString().IndexOf("ClickMap.htm") >= 0)
            //{
            //    this.webBrowser1.Document.GetElementById("Lat").c
            //}
        }

        void map_Click(object sender, HtmlElementEventArgs e)
        {
            LoadOfflineMap();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string MyPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            webBrowser1.Navigate(new Uri("file://" + MyPath + "\\DirectionsTest.html"));
        }

        private void btExtractDirectionInfo_Click(object sender, EventArgs e)
        {
            HtmlElement DirectionsSteps= webBrowser1.Document.GetElementById("tDirections");
            if (DirectionsSteps == null)
            {
                MessageBox.Show("oops direction is not populated yet!!");
                return;
            }
            SerializableList<DirectionStep> Steps = new SerializableList<DirectionStep>();
            string description = "";
            double lat=0;
            double lng = 0;
            int PolyLineIndex = 0;
            foreach (HtmlElement InnerElement in DirectionsSteps.All)
            {
                if (InnerElement.GetAttribute("className") == "dStepDesc")
                {
                    description = InnerElement.InnerText;
                }
                if (InnerElement.GetAttribute("className") == "dLat")
                {
                    lat = double.Parse( InnerElement.InnerText);
                }
                if (InnerElement.GetAttribute("className") == "dLag")
                {
                    lng = double.Parse(InnerElement.InnerText);
                   
                }
                if (InnerElement.GetAttribute("className") == "dPolyLineIndex")
                {
                    PolyLineIndex = int.Parse(InnerElement.InnerText);
                    DirectionStep ds = new DirectionStep(new LatLng(lat, lng), description, PolyLineIndex);
                    Steps.Add(ds);
                }

            }
            SerializableList<LatLng> Points = new SerializableList<LatLng>();
            HtmlElement PathPoints = webBrowser1.Document.GetElementById("Points");
            if (PathPoints == null)
            {
                MessageBox.Show("oops direction is not populated yet!!");
                return;
            }

            foreach (HtmlElement InnerElement in PathPoints.All)
            {
                if (InnerElement.GetAttribute("className") == "pLat")
                {
                    lat = double.Parse(InnerElement.InnerText);
                }
                if (InnerElement.GetAttribute("className") == "pLng")
                {
                    lng = double.Parse(InnerElement.InnerText);

                    Points.Add(new LatLng(lat, lng));
                    
                }

            }
            m_DirectionInfo = new DirectionInfo(key ,Points, Steps);
            //m_DirectionInfo.
            label2.Text = "direction info loaded...";
        }
        private void LoadOfflineMap()
        {
            if (m_DirectionInfo == null)
            {
                MessageBox.Show("direction info not loaded");
                return;
            }
            try
            {
                PixelPoint myloc=new PixelPoint();
                double lat = double.Parse(this.webBrowser1.Document.GetElementById("Lat").InnerText);
                double lng = double.Parse(this.webBrowser1.Document.GetElementById("Lang").InnerText);
                string str_zoom = this.webBrowser1.Document.GetElementById("Zoom").InnerText;
                this.pictureBox1.Image = this.m_DirectionInfo.GetMap(new LatLng(lat, lng), 0,ref myloc);
                this.lblDirectionInfo.Text = this.m_DirectionInfo.GetDirectionDiscriptionFromLocation(new LatLng(lat, lng));
                if ((this.pictureBox1.Left + myloc.x > panel2.Width)|| (this.pictureBox1.Left + myloc.x<0))
                {
                    this.pictureBox1.Left = (int)(panel2.Width / 2 - myloc.x);
                }
                if ((this.pictureBox1.Top + myloc.y > panel2.Height) || (this.pictureBox1.Top + myloc.y < 0))
                {
                    this.pictureBox1.Top = (int)(panel2.Height / 2 - myloc.y);
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                this.timer1.Enabled = this.checkBox1.Enabled = false;
                //MessageBox.Show("Go to Simple map mode and click a point to indicate your current location");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LoadOfflineMap();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = checkBox1.Enabled;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (label2.Text != "direction info loaded...")
                {
                    return;
                }
                if( (prev_timer_lat!= this.webBrowser1.Document.GetElementById("Lat").InnerText) 
                   &&(prev_timer_lng!= this.webBrowser1.Document.GetElementById("Lang").InnerText) )
                {

                   double lat = double.Parse(this.webBrowser1.Document.GetElementById("Lat").InnerText);
                   double lng = double.Parse(this.webBrowser1.Document.GetElementById("Lang").InnerText); 
                   LoadOfflineMap();
                   prev_timer_lat=this.webBrowser1.Document.GetElementById("Lat").InnerText;
                   prev_timer_lng=this.webBrowser1.Document.GetElementById("Lang").InnerText;
                }
            }
            catch(Exception)
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (m_DirectionInfo != null)
            {
                webBrowser1.Navigate("javascript://map.setCenter(new GLatLng(" + m_DirectionInfo.GetLatLngStringOfStartPoint() + "), 15);");
            }
        }


    }
}