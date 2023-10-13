namespace GraphControl.Control.Core.Viewport
{
    public struct Viewport
    {
        public int X0 { get; set; }
        public int Y0 { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Viewport(int x0, int y0, int width, int height)
        {
            X0 = x0;
            Y0 = y0;
            Width = width;
            Height = height;
        }
    }
}
