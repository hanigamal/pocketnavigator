using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PocketDashboard
{
    public partial class DirectionSelect : Form
    {
        Mainform papa;
        string MyDirectoryName = "";
        public DirectionSelect(Mainform arg_papa)
        {
            papa = arg_papa;
            MyDirectoryName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            InitializeComponent();
        }

        private void lstDirectionItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            Directionsform f = new Directionsform(papa,MyDirectoryName+"\\"+ lstDirectionItem.SelectedItem);
            f.Show();
            this.Close();
        }

        private void DirectionSelect_Load(object sender, EventArgs e)
        {
            
            this.lblDirectoryname.Text = MyDirectoryName;
            string[] files=Directory.GetFiles(MyDirectoryName, "*.xml");
            foreach (string file in files)
            {
                this.lstDirectionItem.Items.Add(Path.GetFileName(file));
            }

        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            papa.Show();
            this.Close();
        }
    }
}