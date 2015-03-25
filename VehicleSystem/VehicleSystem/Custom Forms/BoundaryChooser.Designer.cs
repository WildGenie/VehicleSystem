namespace VehicleSystem.Custom_Forms
{
    partial class BoundaryChooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoundaryChooser));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cancelButton = new System.Windows.Forms.Button();
            this.btnBoundaryAccept = new System.Windows.Forms.Button();
            this.VideoPlayer = new AForge.Controls.VideoSourcePlayer();
            this.rectangleButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rectangleButton,
            this.toolStripSeparator1,
            this.clearButton,
            this.toolStripButton1});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(773, 25);
            this.toolStrip.TabIndex = 6;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(391, 513);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 13;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // btnBoundaryAccept
            // 
            this.btnBoundaryAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoundaryAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnBoundaryAccept.Location = new System.Drawing.Point(295, 513);
            this.btnBoundaryAccept.Name = "btnBoundaryAccept";
            this.btnBoundaryAccept.Size = new System.Drawing.Size(75, 23);
            this.btnBoundaryAccept.TabIndex = 12;
            this.btnBoundaryAccept.Text = "Accept";
            this.btnBoundaryAccept.UseVisualStyleBackColor = true;
            // 
            // VideoPlayer
            // 
            this.VideoPlayer.BackColor = System.Drawing.SystemColors.ControlLight;
            this.VideoPlayer.ForeColor = System.Drawing.Color.White;
            this.VideoPlayer.Location = new System.Drawing.Point(44, 41);
            this.VideoPlayer.Name = "VideoPlayer";
            this.VideoPlayer.Size = new System.Drawing.Size(681, 457);
            this.VideoPlayer.TabIndex = 11;
            this.VideoPlayer.VideoSource = null;
            // 
            // rectangleButton
            // 
            this.rectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("rectangleButton.Image")));
            this.rectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rectangleButton.Name = "rectangleButton";
            this.rectangleButton.RightToLeftAutoMirrorImage = true;
            this.rectangleButton.Size = new System.Drawing.Size(23, 22);
            this.rectangleButton.ToolTipText = "Draw rectangular region";
            // 
            // clearButton
            // 
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearButton.Image = ((System.Drawing.Image)(resources.GetObject("clearButton.Image")));
            this.clearButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(23, 22);
            this.clearButton.ToolTipText = "Clear all regions";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Visible = false;
            // 
            // BoundaryChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 547);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.btnBoundaryAccept);
            this.Controls.Add(this.VideoPlayer);
            this.Controls.Add(this.toolStrip);
            this.Name = "BoundaryChooser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoundaryChooser";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton rectangleButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button btnBoundaryAccept;
        private AForge.Controls.VideoSourcePlayer VideoPlayer;
    }
}