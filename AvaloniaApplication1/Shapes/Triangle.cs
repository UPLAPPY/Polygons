using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;


namespace AvaloniaApplication1.Shapes
{
    class Triangle : Shape
    {
        public Triangle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc)
        {
            Pen pen = new Pen(Brushes.Black, 5, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Yellow);

            Point down_left_point = new Point(x - (r / Math.Sqrt(3)), y - (r / 3));
            Point up_point = new Point(x, y + (2 * r / 3));
            Point down_right_point = new Point(x + (r / Math.Sqrt(3)), y - (r / 3));


            var geometry = new StreamGeometry();
            using (var geometryContext = geometry.Open())
            {
                geometryContext.BeginFigure(down_right_point, true);
                geometryContext.LineTo(up_point, true);
                geometryContext.LineTo(down_left_point, true);
                geometryContext.EndFigure(true);
            }

            dc.DrawGeometry(brush, pen, geometry);
        }
    }
}
