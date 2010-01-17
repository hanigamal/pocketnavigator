using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapUtilities;
using System.Xml.Serialization;
using GPSUtils;
using System.IO;



namespace PocketDashboard
{
    public partial class Directionsform : Form,INeedGps
    {
        Mainform papa;
        string direction_file;
        DirectionInfo m_DirectionInfo;
        NewPositionHandler HandleNewPositionNotification;
        LatLng CurrentLocation = new LatLng(17.43197201734061, 78.426246643066415);
        Bitmap bmp = new Bitmap(400, 400);
        Graphics MyGraphics;
        bool UseGPSToLocate = true;
        int CurStep = 0;
        int zoom_instruction = 0;
        int down_x;
        int down_y;
        bool autoposition = true;
        public Directionsform(Mainform arg_papa,string directionfile)
        {
            papa = arg_papa;
            
            direction_file = directionfile;
            InitializeComponent();
            HandleNewPositionNotification = HandleNewPosition;
            MyGraphics = Graphics.FromImage(bmp);
        }
        public void HandleNewPosition(GpsPosition pos)
        {
            if (pos.LatitudeValid && pos.LongitudeValid)
            {
                if (UseGPSToLocate)
                {
                    this.CurrentLocation = new LatLng(pos.Latitude, pos.Longitude);
                }
            }
        }
        private void btBack_Click(object sender, EventArgs e)
        {
           
            papa.CurrentClient = null;
            this.do_action.Enabled = false;
            papa.Show();
            this.Close();
        }
        private void LoadDirectionInfoFromFile()
        {
            try
            {
                string FileName;
                FileName = direction_file;
                FileStream fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new XmlSerializer(typeof(DirectionInfo));
                m_DirectionInfo = (DirectionInfo)serializer.Deserialize(fs);
                fs.Close();
                DrawNewLocation();
            }
            catch(Exception)
            {
                this.lblDirectionStatus.Text="selected file can not be loaded!!";
                direction_file = null;
            }
            
        }
        private void DrawNewLocation()
        {
            try
            {
                PixelPoint myloc = new PixelPoint();
                Size currentImageSize = new Size();
                this.lblDirectionStatus.Text = m_DirectionInfo.GetDirectionDiscriptionFromLocation(CurrentLocation);
                m_DirectionInfo.DrawMap(MyGraphics, CurrentLocation, zoom_instruction, ref myloc, ref currentImageSize);
                this.pictureBox1.Image = bmp;
                
                pictureBox1.Width = currentImageSize.Width;
                pictureBox1.Height = currentImageSize.Height;
                if (autoposition)
                {
                    if ((this.pictureBox1.Left + myloc.x > panel1.Width) || (this.pictureBox1.Left + myloc.x < 0))
                    {
                        this.pictureBox1.Left = (int)(panel1.Width / 2 - myloc.x);
                    }
                    if ((this.pictureBox1.Top + myloc.y > panel1.Height) || (this.pictureBox1.Top + myloc.y < 0))
                    {
                        this.pictureBox1.Top = (int)(panel1.Height / 2 - myloc.y);
                    }
                }
                if (this.UseGPSToLocate)
                {
                    btResume.Text = "Manual";
                }
                else
                {
                    btResume.Text = "GPS";
                }
                if (zoom_instruction == 0)
                {
                    btZoom.Text = "-";
                }
                else
                {
                    btZoom.Text = "+";
                }
            }
            catch (Exception)
            {
                this.lblDirectionStatus.Text = "new location can not be drawn!!";
            }
        }
        private void do_action_Tick(object sender, EventArgs e)
        {
            if (m_DirectionInfo == null && direction_file != null)
            {
                LoadDirectionInfoFromFile();
                this.do_action.Interval = 1000;
            }
            if (m_DirectionInfo == null)
            {
                return;
            }
            DrawNewLocation();
            
        }

        private void Directionsform_Load(object sender, EventArgs e)
        {
           
            papa.CurrentClient = this;
            
            do_action.Enabled = true;
        }

        #region INeedGps Members

        public void GetNewPosition(GPSUtils.GpsPosition pos)
        {
            try
            {
                this.Invoke(this.HandleNewPositionNotification, new object[] { pos });
            }
            catch (Exception)
            {
            }
        }

        #endregion

        private void btPrev_Click(object sender, EventArgs e)
        {
            try
            {
                UseGPSToLocate = false;
                if (CurStep > 0)
                {
                    CurStep = (CurStep - 1) % m_DirectionInfo.Points.Count;
                }
                this.CurrentLocation = m_DirectionInfo.Points[CurStep];
                DrawNewLocation();
                
            }
            catch (Exception)
            {

            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            try
            {
                UseGPSToLocate = false;
                CurStep = (CurStep + 1) % m_DirectionInfo.Points.Count;
                this.CurrentLocation = m_DirectionInfo.Points[CurStep];
                DrawNewLocation();
            }
            catch (Exception)
            {

            }
        }

        private void btResume_Click(object sender, EventArgs e)
        {
            this.UseGPSToLocate = !UseGPSToLocate;
            if (this.UseGPSToLocate)
            {
                btResume.Text = "Manual";
                
            }
            else
            {
                btResume.Text = "GPS";
            }
        }

        private void btZoom_Click(object sender, EventArgs e)
        {
            if (zoom_instruction == 0)
            {
                zoom_instruction = 1;
                
            }
            else
            {
                zoom_instruction = 0;
            }
            DrawNewLocation();

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            down_x = e.X;
            down_y = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pictureBox1.Left = pictureBox1.Left + (e.X - down_x);
                pictureBox1.Top = pictureBox1.Top + (e.Y - down_y);
                this.autoposition = false;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.autoposition = !this.autoposition;
            DrawNewLocation();
        }
    }
}