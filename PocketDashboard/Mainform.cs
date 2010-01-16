using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapUtilities;
using System.IO;
using System.Xml.Serialization;
using GPSUtils;

namespace PocketDashboard
{
    public partial class Mainform : Form
    {
        //DirectionInfo m_DirectionInfo;
        //int stepCounter = 0;
        public GPSUtils.Gps myGpsController;
        public INeedGps CurrentClient = null;
        public Mainform()
        {
            InitializeComponent();
            myGpsController = new Gps();
            myGpsController.Open();
            myGpsController.LocationChanged += new LocationChangedEventHandler(myGpsController_LocationChanged);
            
        }

        void myGpsController_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
        {
           
        }

        void myGpsController_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            if (CurrentClient != null)
            {
                CurrentClient.GetNewPosition(args.Position);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.myGpsController.Close();
            Application.Exit();
        }

        private void btDashboard_Click(object sender, EventArgs e)
        {
            Dashboard d = new Dashboard(this);
            CurrentClient = d;
            d.Show();
            //this.Hide();
        }

        private void btDirections_Click(object sender, EventArgs e)
        {
            DirectionSelect ds = new DirectionSelect(this);
            ds.Show();
            //this.Hide();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void menuItem1_Click(object sender, EventArgs e)
        //{

        //    string FileName;
        //    FileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\" + "testDirectionifo.xml";
        //    FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
        //    XmlSerializer serializer = new XmlSerializer(typeof(DirectionInfo));
        //    m_DirectionInfo = (DirectionInfo)serializer.Deserialize(fs);
        //    fs.Close();
        //    timer1.Enabled = true;
            
        //}

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (m_DirectionInfo == null)
        //    {
        //        return;
        //    }
        //    LatLng CurLoc=m_DirectionInfo.Points[stepCounter];
        //    PixelPoint myloc=new PixelPoint();
        //    this.label1.Text= m_DirectionInfo.GetDirectionDiscriptionFromLocation(CurLoc);
        //    this.pictureBox1.Image = m_DirectionInfo.GetMap(CurLoc, 0, ref myloc);
        //    if ((this.pictureBox1.Left + myloc.x > panel1.Width) || (this.pictureBox1.Left + myloc.x < 0))
        //    {
        //        this.pictureBox1.Left = (int)(panel1.Width / 2 - myloc.x);
        //    }
        //    if ((this.pictureBox1.Top + myloc.y > panel1.Height) || (this.pictureBox1.Top + myloc.y < 0))
        //    {
        //        this.pictureBox1.Top = (int)(panel1.Height / 2 - myloc.y);
        //    }
        //    stepCounter = (++stepCounter) % m_DirectionInfo.Points.Count;
        //}
    }
}