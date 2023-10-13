using System.ComponentModel;

namespace GraphControl.Control;

partial class DrawingSettingsWindow
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
        this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
        this.ShowLegendCheckBox = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // ShowGridCheckBox
        // 
        this.ShowGridCheckBox.Location = new System.Drawing.Point(12, 12);
        this.ShowGridCheckBox.Name = "ShowGridCheckBox";
        this.ShowGridCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.ShowGridCheckBox.Size = new System.Drawing.Size(77, 24);
        this.ShowGridCheckBox.TabIndex = 0;
        this.ShowGridCheckBox.Text = "Show grid";
        this.ShowGridCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.ShowGridCheckBox.UseVisualStyleBackColor = true;
        this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBoxCheckedChanged);
        // 
        // ShowLegendCheckBox
        // 
        this.ShowLegendCheckBox.Location = new System.Drawing.Point(12, 42);
        this.ShowLegendCheckBox.Name = "ShowLegendCheckBox";
        this.ShowLegendCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        this.ShowLegendCheckBox.Size = new System.Drawing.Size(104, 24);
        this.ShowLegendCheckBox.TabIndex = 1;
        this.ShowLegendCheckBox.Text = "Show legend";
        this.ShowLegendCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.ShowLegendCheckBox.UseVisualStyleBackColor = true;
        this.ShowLegendCheckBox.CheckedChanged += new System.EventHandler(this.ShowLegendCheckBoxCheckedChanged);
        // 
        // DrawingSettingsWindow
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(269, 77);
        this.Controls.Add(this.ShowLegendCheckBox);
        this.Controls.Add(this.ShowGridCheckBox);
        this.MaximumSize = new System.Drawing.Size(285, 116);
        this.MinimumSize = new System.Drawing.Size(285, 116);
        this.Name = "DrawingSettingsWindow";
        this.Text = "DrawingSettingsWindow";
        this.ResumeLayout(false);
    }

    public System.Windows.Forms.CheckBox ShowLegendCheckBox;

    public System.Windows.Forms.CheckBox ShowGridCheckBox;

    #endregion
}