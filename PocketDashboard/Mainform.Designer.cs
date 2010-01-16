namespace PocketDashboard
{
    partial class Mainform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.btDashboard = new System.Windows.Forms.Button();
            this.btDirections = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // btDashboard
            // 
            this.btDashboard.BackColor = System.Drawing.Color.DimGray;
            this.btDashboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btDashboard.ForeColor = System.Drawing.Color.White;
            this.btDashboard.Location = new System.Drawing.Point(0, 143);
            this.btDashboard.Name = "btDashboard";
            this.btDashboard.Size = new System.Drawing.Size(240, 59);
            this.btDashboard.TabIndex = 0;
            this.btDashboard.Text = "DashBoard";
            this.btDashboard.Click += new System.EventHandler(this.btDashboard_Click);
            // 
            // btDirections
            // 
            this.btDirections.BackColor = System.Drawing.Color.DimGray;
            this.btDirections.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btDirections.ForeColor = System.Drawing.Color.White;
            this.btDirections.Location = new System.Drawing.Point(0, 202);
            this.btDirections.Name = "btDirections";
            this.btDirections.Size = new System.Drawing.Size(240, 59);
            this.btDirections.TabIndex = 1;
            this.btDirections.Text = "Directions";
            this.btDirections.Click += new System.EventHandler(this.btDirections_Click);
            // 
            // btExit
            // 
            this.btExit.BackColor = System.Drawing.Color.DimGray;
            this.btExit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btExit.ForeColor = System.Drawing.Color.White;
            this.btExit.Location = new System.Drawing.Point(0, 261);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(240, 59);
            this.btExit.TabIndex = 2;
            this.btExit.Text = "Exit";
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 143);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btDashboard);
            this.Controls.Add(this.btDirections);
            this.Controls.Add(this.btExit);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Mainform";
            this.Text = "PocketDashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btDashboard;
        private System.Windows.Forms.Button btDirections;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}

