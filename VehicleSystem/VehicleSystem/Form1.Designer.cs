using System.ComponentModel;
using System.Windows.Forms;

namespace VehicleSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.btnAddFeed = new System.Windows.Forms.Button();
            this.lvResults = new System.Windows.Forms.ListView();
            this.colNumberPlate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsRegistered = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIsBlacklisted = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnlVideoFeed = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.statsDataGrid = new System.Windows.Forms.DataGridView();
            this.Statistic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statsDataGrid)).BeginInit();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddFeed
            // 
            this.btnAddFeed.Location = new System.Drawing.Point(9, 20);
            this.btnAddFeed.Name = "btnAddFeed";
            this.btnAddFeed.Size = new System.Drawing.Size(75, 23);
            this.btnAddFeed.TabIndex = 0;
            this.btnAddFeed.Text = "Add Feed";
            this.btnAddFeed.UseVisualStyleBackColor = true;
            this.btnAddFeed.Click += new System.EventHandler(this.btnAddFeed_Click);
            // 
            // lvResults
            // 
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colNumberPlate,
            this.colIsRegistered,
            this.colIsBlacklisted});
            this.lvResults.Location = new System.Drawing.Point(9, 49);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(315, 344);
            this.lvResults.TabIndex = 3;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.List;
            // 
            // colNumberPlate
            // 
            this.colNumberPlate.Text = "Number plate";
            this.colNumberPlate.Width = 100;
            // 
            // colIsRegistered
            // 
            this.colIsRegistered.Text = "IsRegistered?";
            this.colIsRegistered.Width = 90;
            // 
            // colIsBlacklisted
            // 
            this.colIsBlacklisted.Text = "IsBlacklisted?";
            this.colIsBlacklisted.Width = 90;
            // 
            // pnlVideoFeed
            // 
            this.pnlVideoFeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlVideoFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVideoFeed.Location = new System.Drawing.Point(0, 0);
            this.pnlVideoFeed.Margin = new System.Windows.Forms.Padding(0);
            this.pnlVideoFeed.Name = "pnlVideoFeed";
            this.pnlVideoFeed.Size = new System.Drawing.Size(866, 635);
            this.pnlVideoFeed.TabIndex = 4;
            // 
            // pnlControls
            // 
            this.pnlControls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControls.Controls.Add(this.statsDataGrid);
            this.pnlControls.Controls.Add(this.pnlStats);
            this.pnlControls.Controls.Add(this.lvResults);
            this.pnlControls.Controls.Add(this.label2);
            this.pnlControls.Controls.Add(this.btnAddFeed);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControls.Location = new System.Drawing.Point(866, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(329, 635);
            this.pnlControls.TabIndex = 7;
            // 
            // statsDataGrid
            // 
            this.statsDataGrid.AllowUserToAddRows = false;
            this.statsDataGrid.AllowUserToDeleteRows = false;
            this.statsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Statistic,
            this.Value});
            this.statsDataGrid.Location = new System.Drawing.Point(13, 425);
            this.statsDataGrid.Name = "statsDataGrid";
            this.statsDataGrid.ReadOnly = true;
            this.statsDataGrid.RowHeadersVisible = false;
            this.statsDataGrid.Size = new System.Drawing.Size(307, 201);
            this.statsDataGrid.TabIndex = 0;
            // 
            // Statistic
            // 
            this.Statistic.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Statistic.HeaderText = "Statistic";
            this.Statistic.Name = "Statistic";
            this.Statistic.ReadOnly = true;
            this.Statistic.Width = 69;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // pnlStats
            // 
            this.pnlStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStats.Controls.Add(this.label3);
            this.pnlStats.Location = new System.Drawing.Point(9, 399);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(315, 231);
            this.pnlStats.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "General Statistics";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "System Controls";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 635);
            this.Controls.Add(this.pnlVideoFeed);
            this.Controls.Add(this.pnlControls);
            this.Name = "Form1";
            this.Text = "Vehicle Detection System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statsDataGrid)).EndInit();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnAddFeed;
        private ListView lvResults;
        private ColumnHeader colNumberPlate;
        private ColumnHeader colIsRegistered;
        private ColumnHeader colIsBlacklisted;
        private FlowLayoutPanel pnlVideoFeed;
        private Panel pnlControls;
        private Panel pnlStats;
        private Label label2;
        private Label label3;
        private DataGridView statsDataGrid;
        private DataGridViewTextBoxColumn Statistic;
        private DataGridViewTextBoxColumn Value;
    }
}

