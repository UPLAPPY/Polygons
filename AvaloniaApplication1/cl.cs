using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Security.Cryptography;
using Avalonia.Controls.Shapes;
using System.Threading;

namespace AvaloniaApplication1
{
    public class CustomControl : UserControl
    {
        private int cx, cy, counter = 0;
        List<Shape> shapes = new List<Shape>();
        List<Avalonia.Point> borders = new List<Avalonia.Point>();
        private string _shape = "Triangle";
        private bool _menupressed = false;

        Pen pen = new Pen(Brushes.Green, 2, lineCap: PenLineCap.Square);
        Brush brush = new SolidColorBrush(Colors.Green);
        public override void Render(DrawingContext drawingContext)
        {
            borders.Clear();
            foreach (Shape s in shapes)
            {
                s.Draw(drawingContext, pen, brush);
                s.is_vertex = false;
            }

            if (shapes.Count < 3)
            {
                return;
            }

            for (int i = 0; i < shapes.Count - 1; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    Shape p1 = shapes[i];
                    Shape p2 = shapes[j];
                    bool allAbove = true;
                    bool allBelow = true;
                    if (p1.y == p2.y)
                    {
                        for (int z = 0; z < shapes.Count; z++)
                        {
                            if (z == i || z == j)
                            {
                                continue;
                            }

                            Shape point = shapes[z];

                            if (p1.y > point.y)
                            {
                                allBelow = false;
                            }
                            else if (p1.y < point.y)
                            {
                                allAbove = false;
                            }
                            else
                            {
                                allAbove = false;
                                allBelow = false;
                            }
                        }
                    }

                    if (p1.x == p2.x)
                    {
                        for (int z = 0; z < shapes.Count; z++)
                        {
                            if (z == i || z == j)
                            {
                                continue;
                            }

                            Shape point = shapes[z];

                            if (p1.x > point.x)
                            {
                                allBelow = false;
                            }
                            else if (p1.x < point.x)
                            {
                                allAbove = false;
                            }
                            else
                            {
                                allAbove = false;
                                allBelow = false;
                            }
                        }
                    }

                    else
                    {
                        double k = ((double)p2.y - p1.y) / (p2.x - p1.x);
                        double b = p1.y - k * p1.x;

                        for (int z = 0; z < shapes.Count; z++)
                        {
                            if (z == i || z == j)
                            {
                                continue;
                            }

                            Shape point = shapes[z];
                            double y2 = k * point.x + b;

                            if (y2 > point.y)
                            {
                                allBelow = false;
                            }
                            else if (y2 < point.y)
                            {
                                allAbove = false;
                            }
                            else
                            {
                                allAbove = false;
                                allBelow = false;
                            }
                        }
                    }
                    

                    if (allAbove || allBelow)
                    {
                        p1.is_vertex = true;
                        p2.is_vertex = true;
                        borders.Add(new Avalonia.Point(p1.x, p1.y));
                        borders.Add(new Avalonia.Point(p2.x, p2.y));
                    }
                }
            }

            for (int i = 0; i < borders.Count; i += 2)
            {
                drawingContext.DrawLine(pen, borders[i], borders[i + 1]);
            }
        }

        public void LeftClick(int X, int Y)
        {
            this.cx = X; 
            this.cy = Y;

            foreach (Shape s in shapes)
            {
                if (s.isInside(cx, cy))
                {
                    counter++;
                    s.is_moving = true;
                    s.DX = cx - s.x;
                    s.DY = cy - s.y;
                }
            }
            if (counter == 0)
            {
                switch (_shape)
                {
                    case "Triangle":
                        shapes.Add(new Triangle(cx, cy));
                        break;
                    case "Square":
                        shapes.Add(new Square(cx, cy));
                        break;
                    case "Circle":
                        shapes.Add(new Circle(cx, cy));
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

            InvalidateVisual();
            counter = 0;
        }

        public void RightClick(int X, int Y)
        {
            shapes.Reverse();
            int delete_index = -1;
            foreach (Shape s in shapes)
            {
                if (s.isInside(X, Y))
                {
                    delete_index = shapes.IndexOf(s);
                    break;
                }
            }
            if (delete_index != -1)
            {
                shapes.RemoveAt(delete_index);
            }
            shapes.Reverse();

            InvalidateVisual();
        }

        public void Drag(int X, int Y)
        {
            foreach (Shape s in shapes)
            {
                if (s.is_moving)
                {
                    s.x = X - s.DX;
                    s.y = Y - s.DY;
                }
            }

            InvalidateVisual();
        }

        public void Drop() 
        {
            foreach (Shape shape in shapes)
            {
                shape.is_moving = false;
            }
        }
        
        public void SetShape(string menuShape)
        {
            _shape = menuShape;
        }
    }
}
