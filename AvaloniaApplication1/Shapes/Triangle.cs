﻿using System;
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
            Pen pen = new Pen(Brushes.Black, 2, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Yellow);

            double delta_x = r / 2 * Math.Sqrt(3);
            double delta_y = r / 2;

            Point[] points = 
            {
                new Point(x, y - r),
                new Point(x + delta_x, y + delta_y),
                new Point(x - delta_x, y + delta_y),
                new Point(x, y - r)
            };

            PolylineGeometry geometry = new PolylineGeometry(points, true);

            dc.DrawGeometry(brush, pen, geometry);
        }
    }
}
