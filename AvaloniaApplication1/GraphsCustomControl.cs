using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Windows;
using Avalonia.Media.TextFormatting;

namespace AvaloniaApplication1
{
    public class GraphCustomControl : UserControl
    {
        public override void Render(DrawingContext drawingContext)
        {
            DrawArrow(drawingContext, new Point(15, 502), new Point(15, 50), new SolidColorBrush(Colors.White));
            DrawArrow(drawingContext, new Point(14, 502), new Point(700, 502), new SolidColorBrush(Colors.White));
            GraphContent(drawingContext, Graph());
        }

        private Shape PolygonMaker()
        {
            Random rnd = new Random();
            switch (rnd.Next(0, 2))
            {
                case 0:
                    Triangle triangle = new Triangle(rnd.Next(0, 1000), rnd.Next(0, 1000));
                    return triangle;
                case 1:
                    Square square = new Square(rnd.Next(0, 1000), rnd.Next(0, 1000));
                    return square;
                case 2:
                    Circle circle = new Circle(rnd.Next(0, 1000), rnd.Next(0, 1000));
                    return circle;
            }
            return null;
        }
        private int FindCos(Shape A, Shape B, List<Shape> randomPolygons)
        {
            double cos = 2;
            Shape point = null;
            foreach (Shape polygon in randomPolygons)
            {
                if (point == null)
                {
                    point = polygon;
                }

                if (polygon != A && polygon != B)
                {
                    double AC = Math.Sqrt(Math.Pow(A.x - polygon.x, 2) + Math.Pow(A.y - polygon.y, 2));
                    double AB = Math.Sqrt(Math.Pow((B.x - A.x), 2) + Math.Pow((B.y - A.y), 2));
                    double nowCos = ((polygon.x - A.x) * (B.x - A.x) + (polygon.y - A.y) * (B.y - A.y)) / (AB * AC);
                    if (nowCos < cos)
                    {
                        cos = nowCos;
                        point = polygon;
                    }
                    else if (nowCos == cos)
                    {
                        double Current = Math.Pow(point.x - A.x, 2) + Math.Pow(point.y - A.y, 2);
                        double New = Math.Pow(polygon.x - A.x, 2) + Math.Pow(polygon.y - A.y, 2);

                        if (New > Current)
                        {
                            point = polygon;
                        }
                    }
                }
            }
            return randomPolygons.IndexOf(point);
        }

        private int GraphFindA(List<Shape> randomPolygons)
        {
            Shape A = null;
            foreach (Shape polygon in randomPolygons)
            {
                if (A is null)
                {
                    A = polygon;
                }
                else
                {
                    if (polygon.y > A.y)
                    {
                        A = polygon;
                    }
                    else if (polygon.y == A.y)
                    {
                        if (polygon.x > A.x)
                        {
                            A = polygon;
                        }
                    }
                }
            }
            return randomPolygons.IndexOf(A);
        }
        private void GraphJarvis(List<Shape> randomPolygons)
        {
            int A = GraphFindA(randomPolygons);
            int B = 0;

            Shape tempShape = new Triangle(randomPolygons[A].x + 100, randomPolygons[A].y);
            tempShape.x += 100;
            int C = FindCos(randomPolygons[A], tempShape, randomPolygons);
            randomPolygons[A].is_vertex = true;
            randomPolygons[C].is_vertex = true;
            B = A;
            A = C;

            int index = 1;
            while (index < randomPolygons.Count)
            {
                C = FindCos(randomPolygons[A], randomPolygons[B], randomPolygons);
                randomPolygons[A].is_vertex = true;
                randomPolygons[C].is_vertex = true;
                B = A;
                A = C;
                index++;
            }
        }

        private void GraphByDef(List<Shape> randomPolygons)
        {
            for (int i = 0; i < randomPolygons.Count - 1; i++)
            {
                for (int j = i + 1; j < randomPolygons.Count; j++)
                {
                    bool above = false;
                    bool below = false;
                    for (int d = 0; d < randomPolygons.Count; d++)
                    {
                        if (d != i && d != j)
                        {
                            if (randomPolygons[i].x == randomPolygons[j].x)
                            {
                                if (randomPolygons[d].x < randomPolygons[i].x)
                                {
                                    above = true;
                                }
                                else if (randomPolygons[d].x > randomPolygons[i].x)
                                {
                                    below = true;
                                }
                            }
                            else if (randomPolygons[i].y == randomPolygons[j].y)
                            {
                                if (randomPolygons[d].y < randomPolygons[i].y)
                                {
                                    above = true;
                                }
                                else if (randomPolygons[d].y > randomPolygons[i].y)
                                {
                                    below = true;
                                }
                            }
                            else
                            {
                                double k = ((double)randomPolygons[i].y - randomPolygons[j].y) /
                                           (randomPolygons[i].x - randomPolygons[j].x);
                                double b = randomPolygons[i].y - k * randomPolygons[i].x;
                                double yd = k * randomPolygons[d].x + b;
                                if (randomPolygons[d].y < yd)
                                {
                                    below = true;
                                }
                                else if (randomPolygons[d].y > yd)
                                {
                                    above = true;
                                }
                            }
                        }
                    }

                    if (above == !below)
                    {
                        randomPolygons[i].is_vertex = true;
                        randomPolygons[j].is_vertex = true;
                    }
                }
            }
        }

        private List<Point[]> Graph()
        {
            List<Shape> randomPolygons = new List<Shape>();
            List<int[]> counter = new List<int[]>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    randomPolygons.Add(PolygonMaker());
                }
                int timeJarvis = 0;
                int timeByDef = 0;
                DateTime tic = DateTime.Now;
                GraphJarvis(randomPolygons);
                timeJarvis = (int)(DateTime.Now - tic).TotalMilliseconds;
                tic = DateTime.Now;
                GraphByDef(randomPolygons);
                timeByDef = (int)(DateTime.Now - tic).TotalMilliseconds;
                counter.Add(new int[] { timeJarvis, timeByDef });
            }
            List<Point[]> graphLines = new List<Point[]>();
            int yJarvis = 500;
            int yByDef = 500;
            int x = 20;
            foreach (int[] point in counter)
            {
                graphLines.Add(new Point[] { new Point(x, yJarvis), new Point(x + 40, yJarvis - point[0]) });
                graphLines.Add(new Point[] { new Point(x, yByDef), new Point(x + 40, yByDef - point[1]) });
                yJarvis -= point[0];
                yByDef -= point[1];
                x += 40;
            }
            return graphLines;
        }
        private void DrawArrow(DrawingContext drawingContext, Point start, Point end, IBrush brush)
        {
            drawingContext.DrawLine(new Pen(brush, 3), start, end);

            double arrowLength = 10;
            double arrowAngle = Math.PI / 6;

            Vector direction = (end - start);
            direction = direction.Normalize();
            Vector perpendicular = new Vector(-direction.Y, direction.X);

            Point arrowPoint1 = end - direction * arrowLength + perpendicular * arrowLength * Math.Tan(arrowAngle);
            Point arrowPoint2 = end - direction * arrowLength - perpendicular * arrowLength * Math.Tan(arrowAngle);

            var arrowGeometry = new StreamGeometry();
            using (var ctx = arrowGeometry.Open())
            {
                ctx.BeginFigure(end, true);
                ctx.LineTo(arrowPoint1);
                ctx.LineTo(arrowPoint2);
                ctx.EndFigure(true);
            }

            drawingContext.DrawGeometry(brush, null, arrowGeometry);
        }
        private void GraphContent(DrawingContext drawingContext, List<Point[]> graphLines)
        {

            int index = 0;
            foreach (Point[] line in graphLines)
            {
                if (index % 2 == 0)
                {
                    drawingContext.DrawLine(new Pen(Brushes.Blue, 3, lineCap: PenLineCap.Square), new Point(line[0].X, line[0].Y), new Point(line[1].X, line[1].Y));
                }
                else
                {
                    drawingContext.DrawLine(new Pen(Brushes.Green, 3, lineCap: PenLineCap.Square), new Point(line[0].X, line[0].Y), new Point(line[1].X, line[1].Y));
                }

                index++;
            }
        }
    }
}
