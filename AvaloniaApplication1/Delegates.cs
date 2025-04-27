using System;
using Avalonia.Media;

namespace AvaloniaApplication1
{
    public delegate void RadiusChangedHandler(object? sender, RadiusEventArgs e);

    public class RadiusEventArgs : EventArgs
    {
        public int _r { get; set; }
        public RadiusEventArgs(int r = 30)
        {
            _r = r;
        }
    }

    public delegate void ColorChangedHandler(object? sender, ColorEventArgs e);
    public class ColorEventArgs : EventArgs
    {
        public Color Color { get; set; }

        public ColorEventArgs(Color color)
        {
            Color = color;
        }
    }
}
