using GraphControl.Control.Core.Projection;
using System.Drawing;
using GraphControl.Control.Core.Geometry;
using GraphControl.Control.Core.Text;

namespace GraphControl.Control.Core.Graphics;

public class SimpleGraphic
{
    private System.Drawing.Graphics _graphics;
    private Viewport.Viewport _viewport;
    public Projection.Projection Projection { get; } = new();

    public void UpdateGraphics(System.Drawing.Graphics graphics)
    {
        _graphics = graphics;
    }

    public void ClearColor(Color color)
    {
        _graphics.Clear(color);
    }

    public void Viewport(int x0, int y0, int width, int height)
    {
        _viewport.X0 = x0;
        _viewport.Y0 = y0;
        _viewport.Width = width;
        _viewport.Height = height;

        var clipRect = new Rectangle(0, 0, width + x0, height + y0 + 1);
        _graphics.TranslateTransform(x0, y0);
        _graphics.SetClip(clipRect);
    }

    public void FillRectangle(int x0, int y0, int width, int height, Color color)
    {
        using var solidBrush = new SolidBrush(color);
        _graphics.FillRectangle(solidBrush, new Rectangle(x0, y0, width, height));
    }

    public void Ortho(double left, double right, double bottom, double top)
    {
        Projection.Ortho(left, right, bottom, top);
    }

    public void Translate(double dx, double dy)
    {
        Projection.Move(dx, dy);
    }

    public void Scale(double x, double y, double delta)
    {
        Projection.Scale(x, y, delta);
    }

    public void ConvertToProjection(double x, double y, out double resX, out double resY)
    {
        Converter.FromWorldToProjection(x, y, Projection, _viewport, out resX, out resY);
    }

    public void ClearViewport()
    {
        _graphics.ResetTransform();
    }

    public void DrawLine(Vertex p1, Vertex p2, Color color)
    {
        Converter.FromProjectionToWorld(p1.X, p1.Y, Projection, _viewport, out var resX1, out var resY1);
        Converter.FromProjectionToWorld(p2.X, p2.Y, Projection, _viewport, out var resX2, out var resY2);

        using var pen = new Pen(color);
        _graphics.DrawLine(pen, new PointF((float)resX1, (float)resY1), new PointF((float)resX2, (float)resY2));
    }
        
    public void DrawLines(Vertex[] points, Color color)
    {
        if (points.Length < 2) return;
            
        var converted = new PointF[points.Length];
            
        for (int i = 0; i < points.Length; i++)
        {
            Converter.FromProjectionToWorld(points[i].X, points[i].Y, Projection, _viewport, out var x, out var y);
            converted[i] = new PointF((float)x, (float)y);
        }

        using var pen = new Pen(color);
        _graphics.DrawLines(pen, converted);
    }

    public void DrawCurve(Vertex[] points, Color color)
    {
        if (points.Length < 2) return;
            
        var converted = new PointF[points.Length];
            
        for (int i = 0; i < points.Length; i++)
        {
            Converter.FromProjectionToWorld(points[i].X, points[i].Y, Projection, _viewport, out var x, out var y);
            converted[i] = new PointF((float)x, (float)y);
        }

        using var pen = new Pen(color);
        _graphics.DrawCurve(pen, converted);
    }
        
    public void DrawLinePixels(Point p1, Point p2, Color color)
    {
        using var pen = new Pen(color);
        _graphics.DrawLine(pen, p1, p2);
    }

    public void DrawText(string text, GraphFont font, double x, double y, bool isVertical = false)
    {
        Converter.FromProjectionToWorld(x, y, Projection, _viewport, out var resX, out var resY);
        var location = new PointF((float)resX, (float)resY);
        using var solidBrush = new SolidBrush(font.Color);
            
        if (isVertical)
        {
            _graphics.DrawString(text, font.SystemFont, solidBrush, location, new StringFormat {FormatFlags = StringFormatFlags.DirectionVertical});
        }
        else
        {
            _graphics.DrawString(text, font.SystemFont, solidBrush, location);
        }
    }
        
    public void DrawTextPixels(string text, GraphFont font, int x, int y)
    {
        using var solidBrush = new SolidBrush(font.Color);
        _graphics.DrawString(text, font.SystemFont, solidBrush, new Point(x, y));
    }
}