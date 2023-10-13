#nullable enable
using System;
using System.Windows.Forms;

namespace GraphControl.Control;

public partial class DrawingSettingsWindow : Form
{
    public event EventHandler? ShowGridCheckBoxChanged;
    public event EventHandler? ShowLegendCheckBoxChanged;
    
    public DrawingSettingsWindow()
    {
        InitializeComponent();
    }

    private void ShowGridCheckBoxCheckedChanged(object sender, EventArgs e)
    {
        ShowGridCheckBoxChanged?.Invoke(this, EventArgs.Empty);
    }

    private void ShowLegendCheckBoxCheckedChanged(object sender, EventArgs e)
    {
        ShowLegendCheckBoxChanged?.Invoke(this, EventArgs.Empty);
    }
}