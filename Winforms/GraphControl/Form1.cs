#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GraphControl.Control;
using GraphControl.Control.Core.Geometry;
using Newtonsoft.Json;
using Series = GraphControl.Control.Core.Series.Series;

namespace GraphControl;

public partial class Form1 : Form
{
    private readonly Random _random;
    private int _fileIndex = -1;
    private SeriesChartType _chartType = SeriesChartType.Line;
    private readonly Dictionary<int, BindingSource> _dataByFile;
    private readonly DrawingSettingsWindow _settingsWindow;
    
    private BindingSource Files { get; }
    private BindingSource Data { get; }

    public Form1()
    {
        _random = new Random();
        _dataByFile = new Dictionary<int, BindingSource>();

        Files = new BindingSource();
        Data = new BindingSource();
            
        InitializeComponent();

        _settingsWindow = new DrawingSettingsWindow();
        _settingsWindow.ShowGridCheckBoxChanged += (_, _) =>
        {
            MainGraphControl.ShowGrid = _settingsWindow.ShowGridCheckBox.Checked; 
            MainGraphControl.Invalidate();
        };
        _settingsWindow.ShowLegendCheckBoxChanged += (_, _) =>
        {
            MainGraphControl.ShowLegend = _settingsWindow.ShowLegendCheckBox.Checked;
            MainGraphControl.Invalidate();
        };
        
        MainGraphControl.Notify += UpdateCursorPosition;

        dataGridView1.DataSource = Data;
        FilesTable.DataSource = Files;
        DrawingRegimeBox.DataSource = new[] { SeriesChartType.Line, SeriesChartType.Spline };

        MainGraphControl.Series.Clear();

        Data.ListChanged += (_, _) =>
        {
            if (_fileIndex < MainGraphControl.Series.Count)
            {
                var data = Data.DataSource as IList<Vertex>;
                if (data == null || data.Count == 0) return;

                MainGraphControl.Series[_fileIndex].DataBind(data);
                MainGraphControl.UpdateProjection();
            }
                
            MainGraphControl.Invalidate();
        };
    }
        
    private void UpdateCursorPosition(double x, double y)
    {
        XPositionBox.Text = x.ToString("F3");
        YPositionBox.Text = y.ToString("F3");
    }
    
    private void AddButtonClick(object sender, EventArgs e)
    {
        if (_fileIndex < 0) return;

        _dataByFile[_fileIndex].Add(new Vertex { X = _random.Next(-5, 5), Y = _random.Next(-2, 6) });
        var data = _dataByFile[_fileIndex].List;
        Data.DataSource = data;
        Data.ResetBindings(true);
    }

    private void DrawingRegimeSelectedValueChanged(object sender, EventArgs e)
    {
        var comboBox = sender as ComboBox;
        _chartType = (SeriesChartType)comboBox!.SelectedItem;
        if (_fileIndex < 0) return;
        MainGraphControl.Series[_fileIndex].ChartType = _chartType;
        MainGraphControl.Invalidate();
    }

    private void SaveToolStripMenuItemClick(object sender, EventArgs e)
    {
        using var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = @"Json files (*.json)|*.json";

        if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

        using var sw = new StreamWriter(saveFileDialog.FileName);
        sw.Write(JsonConvert.SerializeObject(Data.List));
    }

    private void LoadToolStripMenuItemClick(object sender, EventArgs e)
    {
        using var openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = @"Json files (*.json)|*.json";

        if (openFileDialog.ShowDialog() != DialogResult.OK) return;

        try
        {
            using var sr = new StreamReader(openFileDialog.FileName);
            var serialized = sr.ReadToEnd();

            var data = JsonConvert.DeserializeObject<List<Vertex>>(serialized);

            AddFileInternal(openFileDialog.FileName);
            AddSeriesInternal(data);

            _dataByFile[_fileIndex].DataSource = data;
            Data.DataSource = data;
        }
        catch
        {
            MessageBox.Show(@"Bad deserialization", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FilesTableSelectedIndexChanged(object sender, EventArgs e)
    {
        if (FilesTable.SelectedIndex < 0) return;

        _fileIndex = FilesTable.SelectedIndex;
        Data.DataSource = _dataByFile[_fileIndex].List;
        DrawingRegimeBox.SelectedItem = MainGraphControl.Series[_fileIndex].ChartType;
    }

    private void AddLocalSeries(object sender, EventArgs e)
    {
        AddFileInternal("localSeries" + (MainGraphControl.Series.Count + 1));
        AddSeriesInternal();
        
        MainGraphControl.Invalidate();
    }

    private void AddFileInternal(string filename)
    {
        Files.Add(Path.GetFileName(filename));
        _fileIndex = _dataByFile.Count;
        _dataByFile.Add(_fileIndex, new BindingSource());
    }

    private void AddSeriesInternal(IEnumerable<Vertex>? data = null)
    {
        var series = new Series
        {
            ChartType = _chartType,
        };
        if (data != null)
        {
            var dataArray = data.ToArray();
                
            foreach (var point in dataArray)
            {
                series.Points.Add(new Vertex(point.X, point.Y));
            }
        } 
            
        MainGraphControl.Series.Add(series);
        MainGraphControl.Series[_fileIndex].Name = Files[_fileIndex].ToString();
    }

    private void DrawingSettingsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_settingsWindow.ShowDialog() == DialogResult.OK)
        {
            _settingsWindow.Close();
        }
    }
}