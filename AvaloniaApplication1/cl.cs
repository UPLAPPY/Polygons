using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using AvaloniaApplication1.Shapes;

namespace AvaloniaApplication1
{
    public class CustomControl : UserControl
    {
        private int cx, cy;
        public override void Render(DrawingContext drawingContext)
        {
            Square square = new Square(100, 100);
            square.Draw(drawingContext);

            Circle circle = new Circle(300, 100);
            circle.Draw(drawingContext);

            Triangle triangle = new Triangle(500, 100);
            triangle.Draw(drawingContext);
        }

        public void Click(int X, int Y)
        {
            this.cx = X; 
            this.cy = Y;

            //foreach (Shape s in l)
            //{
            //    if (s.isInside(cx, cy))
            //    {
                    
            //    }
            //}
        }

        public void Drag()
        {

        }

        public void Drop() 
        { 
            
        }
    }
}
