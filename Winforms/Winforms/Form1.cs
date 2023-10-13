#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;

namespace Test;

public partial class Form1 : Form
{
    private readonly Random _random;
    private int _fileIndex = -1;
    private SeriesChartType _chartType = SeriesChartType.Line;
    private readonly Dictionary<int, BindingSource> _dataByFile;

    public BindingSource Files { get; }
    public BindingSource Data { get; }

    public Form1()
    {
        _random = new();
        _dataByFile = new();

        Files = new();
        Data = new();

        InitializeComponent();

        dataGridView1.DataSource = Data;
        FilesTable.DataSource = Files;
        DrawingRegimeBox.DataSource = new[] { SeriesChartType.Line, SeriesChartType.Spline };

        Chart.Series.Clear();
        Chart.Legends.Clear();

        Data.ListChanged += (_, _) =>
        {
            if (_fileIndex < Chart.Series.Count) Chart.Series[_fileIndex].Points.DataBind(Data, "X", "Y", "");
        };
    }

    private void AddButtonClick(object sender, EventArgs e)
    {
        if (_fileIndex < 0) return;

        _dataByFile[_fileIndex].Add(new Data { X = _random.Next(-5, 5), Y = _random.Next(-2, 6) });
        Data.DataSource = _dataByFile[_fileIndex].List;
        Data.ResetBindings(true);
    }

    private void DrawingRegimeSelectedValueChanged(object sender, EventArgs e)
    {
        var comboBox = sender as ComboBox;
        _chartType = (SeriesChartType)comboBox!.SelectedItem;
        if (_fileIndex < 0) return;
        Chart.Series[_fileIndex].ChartType = _chartType;
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

            var data = JsonConvert.DeserializeObject<List<Data>>(serialized);

            AddFileInternal(openFileDialog.FileName);

            _dataByFile[_fileIndex].DataSource = data;
            Data.DataSource = data;

            AddSeriesInternal(data);
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
        DrawingRegimeBox.SelectedItem = Chart.Series[_fileIndex].ChartType;
    }

    private void AddLocalSeries(object sender, EventArgs e)
    {
        AddFileInternal("localSeries" + (Chart.Series.Count + 1));
        AddSeriesInternal();
    }

    private void AddFileInternal(string filename)
    {
        Files.Add(Path.GetFileName(filename));
        _fileIndex = _dataByFile.Count;
        _dataByFile.Add(_fileIndex, new BindingSource());
    }

    private void AddSeriesInternal(IEnumerable? data = null)
    {
        var series = new Series
        {
            ChartType = _chartType
        };
        if (data != null) series.Points.DataBind(data, "X", "Y", "");
        Chart.Series.Add(series);
        Chart.Legends.Add(series.Name);
        Chart.Legends[_fileIndex].Enabled = true;
        Chart.Series[_fileIndex].LegendText = Files[_fileIndex].ToString();
    }
}

public class Data
{
    public double X { get; set; }
    public double Y { get; set; }
}