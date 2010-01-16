using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GPSUtils;

namespace PocketDashboard
{
    public partial class Dashboard : Form, INeedGps
    {
        Mainform papa;
        NewPositionHandler HandleNewPositionNotification;
        public Dashboard(Mainform parent)
        {
            papa = parent;
            this.HandleNewPositionNotification = HandleNewPosition;
            InitializeComponent();
        }
        private void btBack_Click(object sender, EventArgs e)
        {
            papa.Show();
            papa.CurrentClient = null;
            this.Close();
        }
        public void HandleNewPosition(GpsPosition pos)
        {
            if (!pos.LatitudeValid || !pos.LongitudeValid)
            {
                this.lblLatLang.Text = "N/A";

            }
            else
            {
                this.lblLatLang.Text = "Lat:" + String.Format("{0:0.00000}", pos.Latitude) + "  Lng:" + String.Format("{0:0.00000}", pos.Longitude);
            }

            if (!pos.TimeValid)
            {
                this.lblTime.Text = "...";
            }
            else
            {
                this.lblTime.Text = pos.Time.ToString();
            }

            if (!pos.SpeedValid)
            {
                this.lblSpeed.Text = "...";
            }
            else
            {
                float speed = (float)(pos.Speed * 1.85200);
                this.lblSpeed.Text = String.Format("{0:0.0}", speed);
            }

            if (!pos.HeadingValid)
            {
                this.lblHeading.Text = "Heading\n...";
            }
            else
            {
                this.lblHeading.Text = "Heading\n" + pos.Heading + " deg";
            }
        }
        public void GetNewPosition(GpsPosition pos)
        {
            try
            {
                this.Invoke(this.HandleNewPositionNotification, new object[] { pos });
            }
            catch (Exception)
            {
            }
        }
    }
}