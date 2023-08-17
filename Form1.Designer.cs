namespace EVEBitmapViewer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbModel = new System.Windows.Forms.ToolStripComboBox();
            this.tbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.txtBridgeDetected = new System.Windows.Forms.ToolStripTextBox();
            this.txtEveLabel = new System.Windows.Forms.ToolStripLabel();
            this.txtEveID = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cbScale = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cbModel,
            this.tbConnect,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.txtBridgeDetected,
            this.txtEveLabel,
            this.txtEveID,
            this.toolStripLabel3,
            this.cbScale});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(814, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 24);
            this.toolStripLabel1.Text = "Model EVE-";
            // 
            // cbModel
            // 
            this.cbModel.DropDownHeight = 200;
            this.cbModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModel.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModel.IntegralHeight = false;
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(125, 27);
            this.cbModel.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // tbConnect
            // 
            this.tbConnect.AutoSize = false;
            this.tbConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbConnect.Checked = true;
            this.tbConnect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tbConnect.Image")));
            this.tbConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbConnect.Name = "tbConnect";
            this.tbConnect.Size = new System.Drawing.Size(100, 22);
            this.tbConnect.Text = "xxx";
            this.tbConnect.Click += new System.EventHandler(this.tbConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(121, 24);
            this.toolStripLabel2.Text = "USB Bridge Detected :";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // txtBridgeDetected
            // 
            this.txtBridgeDetected.Name = "txtBridgeDetected";
            this.txtBridgeDetected.ReadOnly = true;
            this.txtBridgeDetected.Size = new System.Drawing.Size(35, 27);
            this.txtBridgeDetected.Text = "No";
            // 
            // txtEveLabel
            // 
            this.txtEveLabel.Name = "txtEveLabel";
            this.txtEveLabel.Size = new System.Drawing.Size(79, 24);
            this.txtEveLabel.Text = "EVE Detected:";
            // 
            // txtEveID
            // 
            this.txtEveID.AutoSize = false;
            this.txtEveID.BackColor = System.Drawing.SystemColors.Control;
            this.txtEveID.Name = "txtEveID";
            this.txtEveID.Size = new System.Drawing.Size(98, 24);
            this.txtEveID.Text = "EVE Detected: No";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(34, 24);
            this.toolStripLabel3.Text = "Scale";
            // 
            // cbScale
            // 
            this.cbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScale.Items.AddRange(new object[] {
            "Proportional",
            "None",
            "Stretch"});
            this.cbScale.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.cbScale.Name = "cbScale";
            this.cbScale.Size = new System.Drawing.Size(121, 23);
            this.cbScale.SelectedIndexChanged += new System.EventHandler(this.cbScale_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(814, 477);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(814, 477);
            this.label1.TabIndex = 2;
            this.label1.Text = "Drag and drop an image file here.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 504);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "EVE Bitmap Viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton tbConnect;
        private System.Windows.Forms.ToolStripTextBox txtBridgeDetected;
        private System.Windows.Forms.ToolStripLabel txtEveID;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbScale;
        private System.Windows.Forms.ToolStripLabel txtEveLabel;
    }
}

