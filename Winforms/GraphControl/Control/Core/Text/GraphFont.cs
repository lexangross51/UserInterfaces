using System.Drawing;
using System.Windows.Forms;

namespace GraphControl.Control.Core.Text
{
    public struct GraphFont
    {
        private readonly Font _systemFont;
        public string FontFamily { get; set; }
        public int Size { get; set; }
        public Color Color { get; set; }
        public FontStyle Style { get; set; }
        public Font SystemFont => _systemFont;

        public GraphFont(string fontFamily, int size, Color color, FontStyle style = FontStyle.Regular)
        {
            FontFamily = fontFamily;
            Size = size;
            Color = color;
            Style = style;

            _systemFont = new Font(FontFamily, Size, Style);
        }

        public Size GetTextSize(string text)
        {
            return TextRenderer.MeasureText(text, _systemFont);
        }
    }
}