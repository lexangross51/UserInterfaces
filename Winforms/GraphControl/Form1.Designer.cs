using GraphControl.Control;

namespace GraphControl
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainGraphControl = new GraphControl.Control.GraphicControl();
            this.StatusBar = new System.Windows.Forms.StatusBar();
            this.XLabel = new System.Windows.Forms.StatusBarPanel();
            this.XPositionBox = new System.Windows.Forms.StatusBarPanel();
            this.YLabel = new System.Windows.Forms.StatusBarPanel();
            this.YPositionBox = new System.Windows.Forms.StatusBarPanel();
            this.FilesTable = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DrawingRegimeBox = new System.Windows.Forms.ComboBox();
            this.AddPoint = new System.Windows.Forms.Button();
            this.AddSeriesButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawingSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.XLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPositionBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YLabel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPositionBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGraphControl
            // 
            this.MainGraphControl.AutoSize = true;
            this.MainGraphControl.BackColor = System.Drawing.SystemColors.Control;
            this.MainGraphControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainGraphControl.Location = new System.Drawing.Point(12, 27);
            this.MainGraphControl.Name = "MainGraphControl";
            this.MainGraphControl.ShowLegend = false;
            this.MainGraphControl.Size = new System.Drawing.Size(554, 486);
            this.MainGraphControl.TabIndex = 0;
            // 
            // StatusBar
            // 
            this.StatusBar.Location = new System.Drawing.Point(0, 524);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] { this.XLabel, this.XPositionBox, this.YLabel, this.YPositionBox });
            this.StatusBar.ShowPanels = true;
            this.StatusBar.Size = new System.Drawing.Size(1060, 24);
            this.StatusBar.TabIndex = 1;
            this.StatusBar.Text = "statusBar1";
            // 
            // XLabel
            // 
            this.XLabel.Name = "XLabel";
            this.XLabel.Text = "X:";
            this.XLabel.Width = 15;
            // 
            // XPositionBox
            // 
            this.XPositionBox.Name = "XPositionBox";
            this.XPositionBox.Text = "0.0000";
            this.XPositionBox.Width = 80;
            // 
            // YLabel
            // 
            this.YLabel.Name = "YLabel";
            this.YLabel.Text = "Y:";
            this.YLabel.Width = 15;
            // 
            // YPositionBox
            // 
            this.YPositionBox.Name = "YPositionBox";
            this.YPositionBox.Text = "0.0000";
            // 
            // FilesTable
            // 
            this.FilesTable.FormattingEnabled = true;
            this.FilesTable.Location = new System.Drawing.Point(577, 27);
            this.FilesTable.Name = "FilesTable";
            this.FilesTable.Size = new System.Drawing.Size(208, 251);
            this.FilesTable.TabIndex = 2;
            this.FilesTable.SelectedIndexChanged += new System.EventHandler(this.FilesTableSelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(791, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(257, 161);
            this.dataGridView1.TabIndex = 3;
            // 
            // DrawingRegimeBox
            // 
            this.DrawingRegimeBox.FormattingEnabled = true;
            this.DrawingRegimeBox.Location = new System.Drawing.Point(791, 194);
            this.DrawingRegimeBox.Name = "DrawingRegimeBox";
            this.DrawingRegimeBox.Size = new System.Drawing.Size(98, 21);
            this.DrawingRegimeBox.TabIndex = 4;
            this.DrawingRegimeBox.SelectedValueChanged += new System.EventHandler(this.DrawingRegimeSelectedValueChanged);
            // 
            // AddPoint
            // 
            this.AddPoint.Location = new System.Drawing.Point(906, 194);
            this.AddPoint.Name = "AddPoint";
            this.AddPoint.Size = new System.Drawing.Size(64, 21);
            this.AddPoint.TabIndex = 5;
            this.AddPoint.Text = "Add point";
            this.AddPoint.UseVisualStyleBackColor = true;
            this.AddPoint.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // AddSeriesButton
            // 
            this.AddSeriesButton.Location = new System.Drawing.Point(906, 221);
            this.AddSeriesButton.Name = "AddSeriesButton";
            this.AddSeriesButton.Size = new System.Drawing.Size(64, 21);
            this.AddSeriesButton.TabIndex = 6;
            this.AddSeriesButton.Text = "Add series";
            this.AddSeriesButton.UseVisualStyleBackColor = true;
            this.AddSeriesButton.Click += new System.EventHandler(this.AddLocalSeries);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.settingsToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1060, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.loadToolStripMenuItem, this.saveToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItemClick);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.drawingSettingsToolStripMenuItem });
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // drawingSettingsToolStripMenuItem
            // 
            this.drawingSettingsToolStripMenuItem.Name = "drawingSettingsToolStripMenuItem";
            this.drawingSettingsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.drawingSettingsToolStripMenuItem.Text = "Drawing settings";
            this.drawingSettingsToolStripMenuItem.Click += new System.EventHandler(this.DrawingSettingsToolStripMenuItemClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 548);
            this.Controls.Add(this.AddSeriesButton);
            this.Controls.Add(this.AddPoint);
            this.Controls.Add(this.DrawingRegimeBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.FilesTable);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainGraphControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1076, 587);
            this.MinimumSize = new System.Drawing.Size(1076, 587);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.XLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XPositionBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YLabel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YPositionBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawingSettingsToolStripMenuItem;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;

        private System.Windows.Forms.ComboBox DrawingRegimeBox;
        private System.Windows.Forms.Button AddPoint;
        private System.Windows.Forms.Button AddSeriesButton;

        private System.Windows.Forms.ListBox FilesTable;
        private System.Windows.Forms.DataGridView dataGridView1;

        public System.Windows.Forms.StatusBarPanel XPositionBox;
        public System.Windows.Forms.StatusBarPanel YLabel;
        public System.Windows.Forms.StatusBarPanel YPositionBox;

        public System.Windows.Forms.StatusBarPanel XLabel;

        private System.Windows.Forms.StatusBar StatusBar;

        #endregion

        private GraphControl.Control.GraphicControl MainGraphControl;
    }
}

