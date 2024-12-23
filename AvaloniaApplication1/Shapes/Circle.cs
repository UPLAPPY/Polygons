using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace AvaloniaApplication1.Shapes
{
    class Circle : Shape
    {
        public Circle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc)
        {
            Pen pen = new Pen(Brushes.Black, 2, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Yellow);
            dc.DrawEllipse(brush, pen, new Point(x, y), r, r);
        }
    }
}
