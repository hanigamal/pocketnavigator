namespace PocketDashboard
{
    partial class DirectionSelect
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
            this.lstDirectionItem = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDirectoryname = new System.Windows.Forms.Label();
            this.btCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstDirectionItem
            // 
            this.lstDirectionItem.BackColor = System.Drawing.Color.Black;
            this.lstDirectionItem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstDirectionItem.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lstDirectionItem.ForeColor = System.Drawing.Color.White;
            this.lstDirectionItem.Location = new System.Drawing.Point(0, 44);
            this.lstDirectionItem.Name = "lstDirectionItem";
            this.lstDirectionItem.Size = new System.Drawing.Size(240, 236);
            this.lstDirectionItem.TabIndex = 0;
            this.lstDirectionItem.SelectedIndexChanged += new System.EventHandler(this.lstDirectionItem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 20);
            this.label1.Text = "Select Direction Item";
            // 
            // lblDirectoryname
            // 
            this.lblDirectoryname.BackColor = System.Drawing.Color.Gray;
            this.lblDirectoryname.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDirectoryname.ForeColor = System.Drawing.Color.Black;
            this.lblDirectoryname.Location = new System.Drawing.Point(0, 280);
            this.lblDirectoryname.Name = "lblDirectoryname";
            this.lblDirectoryname.Size = new System.Drawing.Size(240, 20);
            // 
            // btCancel
            // 
            this.btCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCancel.ForeColor = System.Drawing.Color.White;
            this.btCancel.Location = new System.Drawing.Point(0, 300);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(240, 20);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            // 
            // DirectionSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.Controls.Add(this.lstDirectionItem);
            this.Controls.Add(this.lblDirectoryname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.ForeColor = System.Drawing.Color.White;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DirectionSelect";
            this.Text = "DirectionSelect";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DirectionSelect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstDirectionItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDirectoryname;
        private System.Windows.Forms.Button btCancel;
    }
}