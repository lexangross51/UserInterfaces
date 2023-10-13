using System;

namespace GraphControl.Control.Core.Projection
{
    public class Projection
    {
        private readonly double[] _ortho;
        public double Dh => Width / 2.0;
        public double Dv => Height / 2.0;

        public double Width { get; private set; }
        public double Height { get; private set; }
        public double HCenter { get; set; }
        public double VCenter { get; set; }

        public Projection()
        {
            _ortho = new double[4];

            Ortho(-1, 1, -1, 1);
        }

        public void Ortho(double left, double right, double bottom, double top)
        {
            _ortho[0] = left;
            _ortho[1] = right;
            _ortho[2] = bottom;
            _ortho[3] = top;

            Width = right - left;
            Height = top - bottom;

            HCenter = (left + right) / 2.0;
            VCenter = (bottom + top) / 2.0;
        }

        public void Move(double dx, double dy)
        {
            HCenter += dx;
            VCenter += dy;
        }

        public void Scale(double x, double y, double delta)
        {
            var scale = delta < 1.05 ? 1.05 : 1.0 / 1.05;
            var left = x + scale * (HCenter - Dh - x);
            var right = x + scale * (HCenter - Dh + 2.0 * Dh - x);
            var bottom = y + scale * (VCenter - Dv - y);
            var top = y + scale * (VCenter - Dv + 2.0 * Dv - y);
            var newCenterX = (left + right) / 2.0;
            var newCenterY = (bottom + top) / 2.0;
            var newDHorizontal = newCenterX - left;
            var newDVertical = newCenterY - bottom;

            if (!(Math.Abs(2 * newDHorizontal) >
                  Math.Max(Math.Abs(newCenterX - HCenter), Math.Abs(newCenterX + HCenter)) * 1E-05) ||
                !(Math.Abs(2 * newDVertical) >
                  Math.Max(Math.Abs(newCenterY - Dv), Math.Abs(newCenterY + Dv)) * 1E-05)) return;

            HCenter = newCenterX;
            VCenter = newCenterY;
            Width = 2.0 * newDHorizontal;
            Height = 2.0 * newDVertical;
        }

        public double[] GetProjection()
        {
            _ortho[0] = HCenter - Dh;
            _ortho[1] = HCenter + Dh;
            _ortho[2] = VCenter - Dv;
            _ortho[3] = VCenter + Dv;

            return _ortho;
        }
    }
}
