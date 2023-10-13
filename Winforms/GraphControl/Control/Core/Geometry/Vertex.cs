namespace GraphControl.Control.Core.Geometry;

public class Vertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex(double x = 0.0, double y = 0.0)
    {
        X = x;
        Y = y;
    }
}