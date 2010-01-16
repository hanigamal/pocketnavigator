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
        public Directionsform(Mainform arg_papa,string directionfile)
        {
            papa = arg_papa;
            
            direction_file = directionfile;
            InitializeComponent();
            HandleNewPositionNotification = HandleNewPosition;
        }
        public void HandleNewPosition(GpsPosition pos)
        {
            if (pos.LatitudeValid && pos.LongitudeValid)
            {
                this.CurrentLocation = new LatLng(pos.Latitude, pos.Longitude);
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
            }
            catch(Exception)
            {
                this.lblDirectionStatus.Text="selected file can not be loaded!!";
                direction_file = null;
            }
            
        }
        private void do_action_Tick(object sender, EventArgs e)
        {
            if (m_DirectionInfo == null && direction_file != null)
            {
                LoadDirectionInfoFromFile();
            }
            if (m_DirectionInfo == null)
            {
                return;
            }
            PixelPoint myloc = new PixelPoint();
            Size currentImageSize = new Size();
            this.lblDirectionStatus.Text = m_DirectionInfo.GetDirectionDiscriptionFromLocation(CurrentLocation);
            this.pictureBox1.Image = m_DirectionInfo.GetMap(CurrentLocation, 0, ref myloc, ref currentImageSize);
            pictureBox1.Width = currentImageSize.Width;
            pictureBox1.Height = currentImageSize.Height;
            
            if ((this.pictureBox1.Left + myloc.x > panel1.Width) || (this.pictureBox1.Left + myloc.x < 0))
            {
                this.pictureBox1.Left = (int)(panel1.Width / 2 - myloc.x);
            }
            if ((this.pictureBox1.Top + myloc.y > panel1.Height) || (this.pictureBox1.Top + myloc.y < 0))
            {
                this.pictureBox1.Top = (int)(panel1.Height / 2 - myloc.y);
            }
            
        }

        private void Directionsform_Load(object sender, EventArgs e)
        {
            do_action.Enabled = true;
            papa.CurrentClient = this;
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
    }
}