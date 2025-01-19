using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaApplication1.Shapes
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
            dc.DrawRectangle(brush, pen, new Rect(new Point(x, y - r), new Size(2 * r, 2 * r)));
        }

        public override bool isInside(int xp, int yp)
        {
            //double xc = Convert.ToDouble(this.x);
            //double yc = Convert.ToDouble(this.y);

            double xl = this.x - r / 2;
            double xr = this.x + r / 2;
            double yv = this.y + r / 2;
            double yn = this.y - r / 2;

            if (( xp >= xl && xp <= xr) && ( yp <= yv && yp >= yn))
            {
                Console.WriteLine("yes");
                return true;
            }
            return false;
        }
    }
}
