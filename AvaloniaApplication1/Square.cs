using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaApplication1
{
    class Square : Shape
    {
        public Square(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc)
        {
            Pen pen = new Pen(Brushes.Black, 2, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Green);
            double delta = R * Math.Sqrt(2) / 2;
            dc.DrawRectangle(brush, pen, new Rect(new Point(x - delta, y - delta), new Size(2*delta, 2*delta)));
        }

        public override bool isInside(int xp, int yp)
        {
            double delta = R * Math.Sqrt(2) / 2;
            if ((Math.Abs(x - xp) <= delta) & (Math.Abs(y - yp) <= delta)){
                return true;
            }
            return false;
        }
    }
}
