namespace VehicleSystem.Custom_Forms
{
    partial class FeedSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxDirection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFeelURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFeedName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbxSensitivity = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxSpeed = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxProcessor = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxDetector = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefineRegion = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxRobustChecking = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxDirection);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtFeelURL);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtFeedName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 99);
            this.panel1.TabIndex = 0;
            // 
            // cbxDirection
            // 
            this.cbxDirection.FormattingEnabled = true;
            this.cbxDirection.Items.AddRange(new object[] {
            "Entrance",
            "Exit"});
            this.cbxDirection.Location = new System.Drawing.Point(104, 68);
            this.cbxDirection.Name = "cbxDirection";
            this.cbxDirection.Size = new System.Drawing.Size(278, 21);
            this.cbxDirection.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Feed Direction";
            // 
            // txtFeelURL
            // 
            this.txtFeelURL.Location = new System.Drawing.Point(104, 42);
            this.txtFeelURL.Name = "txtFeelURL";
            this.txtFeelURL.Size = new System.Drawing.Size(278, 20);
            this.txtFeelURL.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Feed URL";
            // 
            // txtFeedName
            // 
            this.txtFeedName.Location = new System.Drawing.Point(104, 16);
            this.txtFeedName.Name = "txtFeedName";
            this.txtFeedName.Size = new System.Drawing.Size(278, 20);
            this.txtFeedName.TabIndex = 3;
            this.txtFeedName.Text = "C:\\Users\\Matthew\\Desktop\\Front Edited.wmv";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Feed Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Feed Settings";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbxRobustChecking);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cbxSensitivity);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.cbxSpeed);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cbxProcessor);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.cbxDetector);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(13, 117);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(387, 154);
            this.panel2.TabIndex = 1;
            // 
            // cbxSensitivity
            // 
            this.cbxSensitivity.FormattingEnabled = true;
            this.cbxSensitivity.Items.AddRange(new object[] {
            "Low",
            "Medium",
            "High"});
            this.cbxSensitivity.Location = new System.Drawing.Point(115, 100);
            this.cbxSensitivity.Name = "cbxSensitivity";
            this.cbxSensitivity.Size = new System.Drawing.Size(267, 21);
            this.cbxSensitivity.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Detection Sensitivity";
            // 
            // cbxSpeed
            // 
            this.cbxSpeed.FormattingEnabled = true;
            this.cbxSpeed.Items.AddRange(new object[] {
            "Slow",
            "Medium",
            "Fast"});
            this.cbxSpeed.Location = new System.Drawing.Point(115, 73);
            this.cbxSpeed.Name = "cbxSpeed";
            this.cbxSpeed.Size = new System.Drawing.Size(267, 21);
            this.cbxSpeed.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Detection Speed";
            // 
            // cbxProcessor
            // 
            this.cbxProcessor.FormattingEnabled = true;
            this.cbxProcessor.Items.AddRange(new object[] {
            "Motion Area Highlighting",
            "Motion Grid Area Highlighting",
            "Motion Border Highlighting"});
            this.cbxProcessor.Location = new System.Drawing.Point(115, 46);
            this.cbxProcessor.Name = "cbxProcessor";
            this.cbxProcessor.Size = new System.Drawing.Size(267, 21);
            this.cbxProcessor.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Motion Processor";
            // 
            // cbxDetector
            // 
            this.cbxDetector.FormattingEnabled = true;
            this.cbxDetector.Items.AddRange(new object[] {
            "Two Frames Difference",
            "Background Modelling"});
            this.cbxDetector.Location = new System.Drawing.Point(115, 19);
            this.cbxDetector.Name = "cbxDetector";
            this.cbxDetector.Size = new System.Drawing.Size(267, 21);
            this.cbxDetector.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Motion Detector";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Detection Settings";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(325, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDefineRegion
            // 
            this.btnDefineRegion.Location = new System.Drawing.Point(12, 277);
            this.btnDefineRegion.Name = "btnDefineRegion";
            this.btnDefineRegion.Size = new System.Drawing.Size(98, 23);
            this.btnDefineRegion.TabIndex = 4;
            this.btnDefineRegion.Text = "Define Region";
            this.btnDefineRegion.UseVisualStyleBackColor = true;
            this.btnDefineRegion.Click += new System.EventHandler(this.btnDefineRegion_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 130);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Robust Checking";
            // 
            // cbxRobustChecking
            // 
            this.cbxRobustChecking.FormattingEnabled = true;
            this.cbxRobustChecking.Items.AddRange(new object[] {
            "Yes",
            "No"});
            this.cbxRobustChecking.Location = new System.Drawing.Point(115, 127);
            this.cbxRobustChecking.Name = "cbxRobustChecking";
            this.cbxRobustChecking.Size = new System.Drawing.Size(267, 21);
            this.cbxRobustChecking.TabIndex = 17;
            // 
            // FeedSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 312);
            this.Controls.Add(this.btnDefineRegion);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FeedSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FeedSettings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFeelURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFeedName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxDirection;
        private System.Windows.Forms.ComboBox cbxSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbxProcessor;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxDetector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxSensitivity;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefineRegion;
        private System.Windows.Forms.ComboBox cbxRobustChecking;
        private System.Windows.Forms.Label label10;
    }
}