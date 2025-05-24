using Avalonia.Media;
using System;
using Avalonia;

namespace AvaloniaApplication1
{
    [Serializable]
    class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc)
        {
            dc.DrawEllipse(Brush, Pen, new Point(x, y), r, r);
        }

        public override bool isInside(int xp, int yp)
        {
            if (Math.Pow(x - xp, 2) + Math.Pow(y - yp, 2) <= Math.Pow(r, 2))
            {
                return true;
            }
            return false;
        }
    }
}
