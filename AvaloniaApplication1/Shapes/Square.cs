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
    }
}
