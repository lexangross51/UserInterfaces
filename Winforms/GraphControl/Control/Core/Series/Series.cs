using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using GraphControl.Control.Core.Geometry;

namespace GraphControl.Control.Core.Series;

public class Series
{
    public string Name { get; set; }
    public IList<Vertex> Points { get; private set; } = new List<Vertex>();
    public SeriesChartType ChartType { get; set; } = SeriesChartType.Line;
    public Color? Color { get; set; }
    
    public void DataBind(IList<Vertex> data)
    {
        Points = new List<Vertex>(data);
    }
}