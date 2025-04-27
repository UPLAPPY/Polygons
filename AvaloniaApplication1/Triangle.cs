using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;


namespace AvaloniaApplication1
{
    class Triangle : Shape
    {
        private Point point1, point2, point3;
        

        public Triangle(int x, int y) : base(x, y)
        {
        }

        public override void Draw(DrawingContext dc)
        {

            point1 = new Point(x, y - r);
            point2 = new Point(x + (r / 2 * Math.Sqrt(3)), y + r / 2);
            point3 = new Point(x - (r / 2 * Math.Sqrt(3)), y + r / 2);

            Point[] points =
            {
                point1,
                point2,
                point3,
                point1
            };

            PolylineGeometry geometry = new PolylineGeometry(points, true);

            dc.DrawGeometry(Brush, Pen, geometry);
        }

        public override bool isInside(int xp, int yp)
        {

            double triangleArea = CalculateArea(point1.X, point1.Y, point2.X, point2.Y, point3.X, point3.Y);
            double area1 = CalculateArea(xp, yp, point2.X, point2.Y, point3.X, point3.Y);
            double area2 = CalculateArea(point1.X, point1.Y, xp, yp, point3.X, point3.Y);
            double area3 = CalculateArea(point1.X, point1.Y, point2.X, point2.Y, xp, yp);

            return Math.Abs(area1 + area2 + area3 - triangleArea) < 0.1;
        }

        static double CalculateArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double a = Distance(x1, y1, x2, y2);
            double b = Distance(x2, y2, x3, y3);
            double c = Distance(x3, y3, x1, y1);
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }

        static double Distance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
    }
}
