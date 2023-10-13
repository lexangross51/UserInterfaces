namespace GraphControl.Control.Core.Projection;

public static class Converter
{
    public static void FromWorldToProjection(
        double x, double y,
        Projection projection, Viewport.Viewport viewport,
        out double xRes, out double yRes)
    {
        if (x < viewport.X0)
        {
            xRes = projection.HCenter - projection.Dh;
        }
        else if (x > viewport.Width + viewport.X0)
        {
            xRes = projection.HCenter + projection.Dh;
        }
        else
        {
            double coefficient = (x - viewport.X0) / viewport.Width;
            xRes = projection.HCenter + (2 * coefficient - 1) * projection.Dh;
        }

        if (y < 0.0)
        {
            yRes = projection.VCenter + projection.Dv;
        }
        else if (y > viewport.Height + viewport.Y0)
        {
            yRes = projection.VCenter - projection.Dv;
        }
        else
        {
            double coefficient = (y - viewport.Y0) / viewport.Height;
            yRes = projection.VCenter - (2 * coefficient - 1) * projection.Dv;
        }
    }

    public static void FromProjectionToWorld(
        double x, double y,
        Projection projection, Viewport.Viewport viewport,
        out double xRes, out double yRes)
    {
        var dx = x - (projection.HCenter - projection.Dh);
        var dy = y - (projection.VCenter - projection.Dv);

        var coefficient = dx / projection.Width;
        xRes = coefficient * viewport.Width;

        coefficient = dy / projection.Height;
        yRes = viewport.Height * (1.0 - coefficient);
    }
}