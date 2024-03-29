﻿namespace Wpf.Models;

public struct Point2D
{
    public double X { get; set; }
    public double Y { get; set; }

    public Point2D(double x = 0.0, double y = 0.0)
        => (X, Y) = (x, y);
}