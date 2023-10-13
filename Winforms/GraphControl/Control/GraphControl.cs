#nullable enable
using GraphControl.Control.Core.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using GraphControl.Control.Core.Geometry;
using GraphControl.Control.Core.Text;
using Axis = GraphControl.Control.Core.Axis.Axis;
using Series = GraphControl.Control.Core.Series.Series;

namespace GraphControl.Control;

public partial class GraphicControl : PictureBox
{
    private readonly List<Color> _usedColors = new();
    private static readonly Random Random = new();
    private readonly SimpleGraphic _graphic;
    private GraphFont _font;
    private bool _isMouseDown;
    private int _mouseXPrev;
    private int _mouseYPrev;
    private readonly double[] _multipliers = { 1, 2, 5, 10 };
    private readonly Axis _horizontalAxis;
    private readonly Axis _verticalAxis;
    private readonly Size _textSize;
        
    // Legend settings
    private const int LegendLineLength = 60;
    private static readonly GraphFont LegendFont = new("Times New Roman", 10, Color.Black);
    private const int LegendRecordHeight = 8;

    public IList<Series> Series { get; }
    public bool ShowLegend { get; set; }
    public bool ShowGrid { get; set; }

    public delegate void GraphControlHandler(double x, double y);
    public event GraphControlHandler? Notify; 

    public GraphicControl()
    {
        InitializeComponent();

        _graphic = new SimpleGraphic();

        _horizontalAxis = new Axis("X");
        _verticalAxis = new Axis("Y");

        _font = new GraphFont("Times New Roman", 14, Color.Black);
        _textSize = _font.GetTextSize(_horizontalAxis.Name);

        Series = new List<Series>();
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        _graphic.UpdateGraphics(e.Graphics);
        _graphic.ClearColor(Color.White);
            
        GenerateTicks();
        
        if (ShowGrid) DrawGrid();
        
        DrawBorder();
        DrawAxesTicks();
            
        _graphic.Viewport(_textSize.Height, 0, Width - _textSize.Height, Height - _textSize.Height);

        foreach (var series in Series)
        {
            series.Color ??= GenerateUniqueColor();

            switch (series.ChartType)
            {
                case SeriesChartType.Line:
                    _graphic.DrawLines(series.Points.ToArray(), series.Color!.Value);
                    break;
                    
                case SeriesChartType.Spline:
                    _graphic.DrawCurve(series.Points.ToArray(), series.Color!.Value);
                    break;
            }
        }

        _graphic.ClearViewport();
            
        if (ShowLegend) DrawLegend();
            
        _graphic.Viewport(_textSize.Height, 0, Width - _textSize.Height, Height - _textSize.Height);
        _graphic.ClearViewport();
    }

    private Color GenerateUniqueColor()
    {
        bool isGenerated = false;
        var color = Color.Black;
            
        while (!isGenerated)
        {
            var rgb = new byte[3];
            Random.NextBytes(rgb);
            color = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]);

            if (_usedColors.Contains(color)) continue;
            _usedColors.Add(color);
            isGenerated = true;
        }

        return color;
    }
        
    private void DrawLegend()
    {
        var color = Color.Black;
        var legendHeight = 8 + (2 + LegendRecordHeight) * Series.Count;
            
        var p1 = new Point(0, 0);
        var p2 = new Point(150, 0);
        var p3 = new Point(0, legendHeight);
        var p4 = new Point(150, legendHeight);
            
        _graphic.Viewport(Width - 160, 8, 100, legendHeight);
        _graphic.FillRectangle(0, 0, 150, legendHeight, Color.White);
        _graphic.DrawLinePixels(p1, p2, color);
        _graphic.DrawLinePixels(p1, p3, color);
        _graphic.DrawLinePixels(p3, p4, color);
        _graphic.DrawLinePixels(p2, p4, color);

        for (var i = 0; i < Series.Count; i++)
        {
            var seriesName = Series[i].Name;
            _graphic.DrawLinePixels(
                new Point(10, (i + 1) * 10), 
                new Point(10 + LegendLineLength, (i + 1) * 10), 
                Series[i].Color!.Value);
                
            _graphic.DrawTextPixels(seriesName, LegendFont, 10 + LegendLineLength, (i + 1) * 2 + i * LegendRecordHeight);   
        }
            
        _graphic.ClearViewport();
    }

    public void UpdateProjection()
    {
        if (Series[0].Points.Count == 0) return;

        var minX = Series.SelectMany(s => s.Points).Min(p => p.X);
        var maxX = Series.SelectMany(s => s.Points).Max(p => p.X);
        var minY = Series.SelectMany(s => s.Points).Min(p => p.Y);
        var maxY = Series.SelectMany(s => s.Points).Max(p => p.Y);

        if (Math.Abs(minX - maxX) < 1E-07 || Math.Abs(minY - maxY) < 1E-07)
        {
            _graphic.Projection.HCenter = minX;
            _graphic.Projection.VCenter = minY;
            return;
        }
            
        if (maxX - minX > _graphic.Projection.Width || maxY - minY > _graphic.Projection.Height)
        {
            _graphic.Ortho(minX, maxX, minY, maxY);
        }
    }
        
    private void OnMouseDown(object sender, MouseEventArgs e)
    {
        _isMouseDown = true;
        _mouseXPrev = e.X;
        _mouseYPrev = e.Y;
    }

    private void OnMouseUp(object sender, MouseEventArgs e)
    {
        _isMouseDown = false;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        _graphic.ConvertToProjection(e.X, e.Y, out var xCurrent, out var yCurrent);
            
        Notify?.Invoke(xCurrent, yCurrent);
            
        if (!_isMouseDown) return;

        _graphic.ConvertToProjection(_mouseXPrev, _mouseYPrev, out var xPrev, out var yPrev);
        _graphic.Translate(-xCurrent + xPrev, -yCurrent + yPrev);

        _mouseXPrev = e.X;
        _mouseYPrev = e.Y;

        Invalidate();
    }

    private void OnMouseWheel(object sender, MouseEventArgs e)
    {
        _graphic.ConvertToProjection(e.X, e.Y, out var xPivot, out var yPivot);
        _graphic.Scale(xPivot, yPivot, e.Delta);

        Invalidate();
    }

    private double CalculateStepAxis(double begin, double end, double measure)
    {
        var dH = end - begin;
        var hh = measure;

        var fontSize = _font.GetTextSize(Axis.TemplateCaption).Width * dH / hh;
        var ds = Math.Floor(dH / fontSize);

        var dStep = dH / ds;
        var dMul = Math.Pow(10, Math.Floor(Math.Log10(dStep)));

        int i;
        for (i = 0; i < _multipliers.Length - 1; ++i)
        {
            if (dMul * _multipliers[i] > dStep) break;
        }

        dStep = _multipliers[i] * dMul;
        return dStep;
    }

    private void GenerateTicks()
    {
        var proj = _graphic.Projection.GetProjection();

        var size = _font.GetTextSize(_horizontalAxis.Name);
        double step = CalculateStepAxis(proj[0], proj[1], Width - size.Height);
        _horizontalAxis.GenerateTicks(proj[0], proj[1], step);
            
        step = CalculateStepAxis(proj[2], proj[3], Height - size.Height);
        _verticalAxis.GenerateTicks(proj[2], proj[3], step);
    }
        
    private void DrawAxesTicks()
    {
        var pTick1 = new Vertex();
        var pTick2 = new Vertex();
        var color = Color.Black;

        var proj = _graphic.Projection.GetProjection();

        // Horizontal axis
        var size = _font.GetTextSize(_horizontalAxis.Name);
        _graphic.Viewport(size.Height, 0, Width - size.Height, Height);
            
        int textWidth = size.Width;
        int textHeight = _horizontalAxis.Name == ""
            ? _font.GetTextSize("0").Height
            : size.Height;
        var hRatio = (proj[1] - proj[0]) / (Width - size.Height);
        var vRatio = (proj[3] - proj[2]) / Height;

        var minDrawLetter = proj[0];
        var maxDrawLetter = proj[1] - textWidth * hRatio;
            
        const double dy = 3;
        foreach (var it in _horizontalAxis.Points)
        {
            pTick1.X = (float)it;
            pTick1.Y = (float)(proj[2] + (textHeight - dy) * vRatio);
            
            pTick2.X = (float)it;
            pTick2.Y = (float)(proj[2] + (textHeight + dy) * vRatio);
            
            _graphic.DrawLine(pTick1, pTick2, color);
                
            var msVal = it.ToString("G10", CultureInfo.InvariantCulture);
            var stringSize = _font.GetTextSize(msVal).Width;
            var stringPositionL = it - stringSize * 0.5 * hRatio;
            var stringPositionR = it + stringSize * 0.5 * hRatio;

            if (stringPositionL < minDrawLetter || stringPositionR > maxDrawLetter) continue;

            _graphic.DrawText(msVal, _font, stringPositionL, proj[2] + textWidth * vRatio);
        }
            
        if (textWidth != 0)
        {
            _graphic.DrawText(_horizontalAxis.Name, _font, proj[1] - textWidth * hRatio, proj[2] + textWidth * vRatio);
        }
        
        _graphic.ClearViewport();


        // Vertical axis
        size = _font.GetTextSize(_verticalAxis.Name);
        textWidth = size.Width;
        textHeight = _verticalAxis.Name == ""
            ? _font.GetTextSize("0").Height
            : size.Height;
        _graphic.Viewport(0, 0, Width, Height - size.Height);
        hRatio = (proj[1] - proj[0]) / Width;
        vRatio = (proj[3] - proj[2]) / (Height - size.Height);
            
        minDrawLetter = proj[2];
        maxDrawLetter = proj[3] - textWidth * vRatio;
            
        const double dx = 3.0;
        foreach (var it in _verticalAxis.Points)
        {
            pTick1.X = (float)(proj[0] + (textHeight + dx) * hRatio);
            pTick1.Y = (float)it;
            
            pTick2.X = (float)(proj[0] + (textHeight - dx) * hRatio);
            pTick2.Y = (float)it;
            
            _graphic.DrawLine(pTick1, pTick2, color);
                
            var msVal = it.ToString("G10", CultureInfo.InvariantCulture);
            var stringSize = _font.GetTextSize(msVal).Width;
            var stringPositionL = it - stringSize * 0.5 * vRatio;
            var stringPositionR = it + stringSize * 0.5 * vRatio;

            if (stringPositionL < minDrawLetter || stringPositionR > maxDrawLetter) continue;
            
            _graphic.DrawText(msVal, _font, proj[0], stringPositionR, true);
        }
        
        if (textWidth != 0)
        {
            _graphic.DrawText(_verticalAxis.Name, _font, proj[0], proj[3], true);
        }
        
        _graphic.ClearViewport();
    }

    private void DrawBorder()
    {
        var fontSize = _font.GetTextSize(_horizontalAxis.Name);
        var proj = _graphic.Projection.GetProjection();
        var color = Color.Black;

        var x1 = proj[0];
        var x2 = proj[1];
        var y1 = proj[2];
        var y2 = proj[3];
        var p1 = new Vertex(x1, y1);
        var p2 = new Vertex(x2, y1);
        var p3 = new Vertex(x1, y2);

        _graphic.Viewport(fontSize.Height, 0, Width - fontSize.Height, Height - fontSize.Height);
        _graphic.DrawLine(p1, p2, color);
        _graphic.DrawLine(p1, p3, color);
        _graphic.ClearViewport();
    }

    private void DrawGrid()
    {
        var size = _font.GetTextSize(_horizontalAxis.Name);
        var pTick1 = new Vertex();
        var pTick2 = new Vertex();

        var proj = _graphic.Projection.GetProjection();
        var color = Color.LightGray;

        _graphic.Viewport(size.Height, 0, Width - size.Height, Height - size.Height);

        foreach (var it in _horizontalAxis.Points)
        {
            pTick1.X = (float)it;
            pTick1.Y = (float)proj[2];

            pTick2.X = (float)it;
            pTick2.Y = (float)proj[3];

            _graphic.DrawLine(pTick1, pTick2, color);
        }

        foreach (var it in _verticalAxis.Points)
        {
            pTick1.X = (float)proj[0];
            pTick1.Y = (float)it;

            pTick2.X = (float)proj[1];
            pTick2.Y = (float)it;

            _graphic.DrawLine(pTick1, pTick2, color);
        }

        _graphic.ClearViewport();
    }

    private void OnResize(object sender, EventArgs e)
    {
        Invalidate();
    }
}