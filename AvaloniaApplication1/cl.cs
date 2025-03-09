using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using Avalonia;

namespace AvaloniaApplication1
{
    public class CustomControl : UserControl
    {
        private int cx, cy;
        private List<Shape> _shapes = new List<Shape>();
        private List<Shape[]> _borders = new List<Shape[]>();
        private string _shape = "Triangle";

        private Pen pen = new Pen(Brushes.Green, 2, lineCap: PenLineCap.Square);
        private Brush brush = new SolidColorBrush(Colors.Green);

        public override void Render(DrawingContext drawingContext)
        {
            _borders.Clear();
            foreach (Shape s in _shapes)
            {
                s.Draw(drawingContext, pen, brush);
                s.is_vertex = false;
            }

            if (_shapes.Count < 3)
            {
                return;
            }

            Jarvis();

            foreach (Shape[] line in _borders)
            {
                drawingContext.DrawLine(pen, new Point(line[0].x, line[0].y), new Point(line[1].x, line[1].y));
            }
            RemoveInside();
        }

        private void Jarvis()
        {
            int constA = FindA();
            int A = constA;

            int B = 0;
            int C = 0;

            Shape tempShape = new Triangle(_shapes[A].x + 100, _shapes[A].y);
            C = FindCos(_shapes[A], tempShape);

            _borders.Add([_shapes[A], _shapes[C]]);
            _shapes[A].is_vertex = true;
            _shapes[C].is_vertex = true;
            B = A;
            A = C;

            int index = 1;
            while (index < _shapes.Count)
            {
                C = FindCos(_shapes[A], _shapes[B]);
                _shapes[C].is_vertex = true;
                _borders.Add([_shapes[A], _shapes[C]]);

                B = A;
                A = C;
                index++;
            }
        }

        private int FindA()
        {
            Shape A = null;
            foreach (Shape shape in _shapes)
            {
                if (A is null)
                {
                    A = shape;
                }
                else
                {
                    if (shape.y > A.y)
                    {
                        A = shape;
                    }
                    else if (shape.y == A.y)
                    {
                        if (shape.x > A.x)
                        {
                            A = shape;
                        }
                    }
                }
            }
            return _shapes.IndexOf(A);
        }

        private int FindCos(Shape A, Shape B)
        {
            double maxCos = 2;
            Shape point = null;

            foreach (Shape s in _shapes)
            {
                if (point is null)
                {
                    point = s;
                }
                if (s != A && s != B)
                {
                    double AC = Math.Sqrt(Math.Pow((A.x - s.x), 2) + Math.Pow((A.y - s.y), 2));
                    double AB = Math.Sqrt(Math.Pow((B.x - A.x), 2) + Math.Pow((B.y - A.y), 2));
                    double nowCos = ((s.x - A.x) * (B.x - A.x) + (s.y - A.y) * (B.y - A.y)) / (AB * AC);

                    if (nowCos < maxCos)
                    {
                        maxCos = nowCos;
                        point = s;
                    }
                    else if (nowCos == maxCos)
                    {
                        double distCurrent = Math.Pow((point.x - A.x), 2) + Math.Pow((point.y - A.y), 2);
                        double distNew = Math.Pow((s.x - A.x), 2) + Math.Pow((s.y - A.y), 2);

                        if (distNew > distCurrent)
                        {
                            point = s;
                        }
                    }
                }
            }
            return _shapes.IndexOf(point);
        }

        
        
        
        public void LeftClick(int x, int y)
        {
            this.cx = x;
            this.cy = y;
            bool outsideShape = false;
            foreach (Shape s in _shapes)
            {
                if (s.isInside(cx, cy))
                {
                    s.is_moving = true;
                    s.DX = cx - s.x;
                    s.DY = cy - s.y;
                    outsideShape = true;
                }
            }
            if (!outsideShape)
            {
                bool inside = InsideShell(x, y);
                if (inside)
                {
                    foreach (Shape s in _shapes)
                    {
                        s.is_moving = true;
                        s.DX = cx - s.x; 
                        s.DY = cy - s.y;
                    }
                }
                else
                {
                    switch (_shape)
                    {
                        case "Triangle":
                            _shapes.Add(new Triangle(cx, cy));
                            break;
                        case "Square":
                            _shapes.Add(new Square(cx, cy));
                            break;
                        case "Circle":
                            _shapes.Add(new Circle(cx, cy));
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            InvalidateVisual();
        }
        public bool InsideShell(int x, int y)
        {
            bool inside = false;
            foreach (Shape[] line in _borders)
            {
                Point p1 = new Point(line[0].x, line[0].y);
                Point p2 = new Point(line[1].x, line[1].y);

                if (y >= Math.Min(p1.Y, p2.Y))
                {
                    if (y <= Math.Max(p1.Y, p2.Y))
                    {
                        if (x <= Math.Max(p1.X, p2.X))
                        {
                            double interX = (y - p1.Y) * (p2.X - p1.X) / (p2.Y - p1.Y) + p1.X;

                            if (Convert.ToInt32(p1.X) == Convert.ToInt32(p2.X) || x <= interX)
                            {
                                inside = !inside;
                            }
                        }
                    }
                }
            }
            return inside;
        }




        public void RightClick(int x, int y)
        {
            _shapes.Reverse();
            int delete_index = -1;
            foreach (Shape s in _shapes)
            {
                if (s.isInside(x, y))
                {
                    delete_index = _shapes.IndexOf(s);
                    break;
                }
            }
            if (delete_index != -1)
            {
                _shapes.RemoveAt(delete_index);
            }
            _shapes.Reverse();

            InvalidateVisual();
        }



        public void Drag(int x, int y)
        {
            foreach (Shape s in _shapes)
            {
                if (s.is_moving)
                {
                    s.x = x - s.DX;
                    s.y = y - s.DY;
                }
            }

            InvalidateVisual();
        }

        public void Drop()
        {
            foreach (Shape shape in _shapes)
            {
                shape.is_moving = false;
            }
        }

        public void SetShape(string menuShape)
        {
            _shape = menuShape;
        }

        private void RemoveInside()
        {
            List<Shape> remove = new List<Shape>();
            foreach (Shape polygon in _shapes)
            {
                if (!polygon.is_vertex)
                {
                    remove.Add(polygon);
                }
            }

            foreach (Shape polygon in remove)
            {
                _shapes.Remove(polygon);
            }
        }
    }
}