namespace PocketDashboard
{
    partial class Directionsform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Directionsform));
            this.lblDirectionStatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btPrev = new System.Windows.Forms.Button();
            this.do_action = new System.Windows.Forms.Timer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btResume = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.btZoom = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDirectionStatus
            // 
            this.lblDirectionStatus.BackColor = System.Drawing.Color.Black;
            this.lblDirectionStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDirectionStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDirectionStatus.ForeColor = System.Drawing.Color.White;
            this.lblDirectionStatus.Location = new System.Drawing.Point(0, 0);
            this.lblDirectionStatus.Name = "lblDirectionStatus";
            this.lblDirectionStatus.Size = new System.Drawing.Size(240, 42);
            this.lblDirectionStatus.Text = "loading...";
            this.lblDirectionStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 235);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 227);
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // btPrev
            // 
            this.btPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btPrev.Dock = System.Windows.Forms.DockStyle.Left;
            this.btPrev.ForeColor = System.Drawing.Color.White;
            this.btPrev.Location = new System.Drawing.Point(46, 0);
            this.btPrev.Name = "btPrev";
            this.btPrev.Size = new System.Drawing.Size(45, 37);
            this.btPrev.TabIndex = 0;
            this.btPrev.Text = "<";
            this.btPrev.Click += new System.EventHandler(this.btPrev_Click);
            // 
            // do_action
            // 
            this.do_action.Tick += new System.EventHandler(this.do_action_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.btResume);
            this.panel2.Controls.Add(this.btNext);
            this.panel2.Controls.Add(this.btZoom);
            this.panel2.Controls.Add(this.btPrev);
            this.panel2.Controls.Add(this.btClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 283);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 37);
            // 
            // btResume
            // 
            this.btResume.BackColor = System.Drawing.Color.Green;
            this.btResume.Dock = System.Windows.Forms.DockStyle.Left;
            this.btResume.ForeColor = System.Drawing.Color.White;
            this.btResume.Location = new System.Drawing.Point(171, 0);
            this.btResume.Name = "btResume";
            this.btResume.Size = new System.Drawing.Size(68, 37);
            this.btResume.TabIndex = 3;
            this.btResume.Text = "Manual";
            this.btResume.Click += new System.EventHandler(this.btResume_Click);
            // 
            // btNext
            // 
            this.btNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btNext.Dock = System.Windows.Forms.DockStyle.Left;
            this.btNext.ForeColor = System.Drawing.Color.White;
            this.btNext.Location = new System.Drawing.Point(126, 0);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(45, 37);
            this.btNext.TabIndex = 2;
            this.btNext.Text = ">";
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btClose.ForeColor = System.Drawing.Color.White;
            this.btClose.Location = new System.Drawing.Point(0, 0);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(46, 37);
            this.btClose.TabIndex = 1;
            this.btClose.Text = "Close";
            this.btClose.Click += new System.EventHandler(this.btBack_Click);
            // 
            // btZoom
            // 
            this.btZoom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.btZoom.ForeColor = System.Drawing.Color.White;
            this.btZoom.Location = new System.Drawing.Point(91, 0);
            this.btZoom.Name = "btZoom";
            this.btZoom.Size = new System.Drawing.Size(35, 37);
            this.btZoom.TabIndex = 4;
            this.btZoom.Text = "+";
            this.btZoom.Click += new System.EventHandler(this.btZoom_Click);
            // 
            // Directionsform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDirectionStatus);
            this.ForeColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Directionsform";
            this.Text = "Directions";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Directionsform_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDirectionStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btPrev;
        private System.Windows.Forms.Timer do_action;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btResume;
        private System.Windows.Forms.Button btZoom;
    }
}