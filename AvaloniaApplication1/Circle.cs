using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace AvaloniaApplication1
{
    class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc, Pen pen, Brush brush)
        {
            dc.DrawEllipse(brush, pen, new Point(x, y), r, r);
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
