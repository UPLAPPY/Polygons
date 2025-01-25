using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using System.Diagnostics.Metrics;

namespace AvaloniaApplication1
{
    public class CustomControl : UserControl
    {
        private int cx, cy, dx, dy, counter = 0;
        List<Shape> shapes = new List<Shape>();
        public override void Render(DrawingContext drawingContext)
        {
            foreach (Shape s in shapes)
            {
                s.Draw(drawingContext);
            }
        }
        
        public void RightClick(int X, int Y)
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
                Square s = new Square(cx, cy);
                shapes.Add(s);
            }

            InvalidateVisual();
            counter = 0;
        }

        public void LeftClick(int X, int Y)
        {
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
    }
}
