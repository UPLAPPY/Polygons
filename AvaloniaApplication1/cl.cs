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
        private double cx, cy;
        public override void Render(DrawingContext drawingContext)
        {
            Square square = new Square(100, 100);
            square.Draw(drawingContext);

            Circle circle = new Circle(300, 100);
            circle.Draw(drawingContext);

            Triangle triangle = new Triangle(500, 100);
            triangle.Draw(drawingContext);
        }

        public void Click(double X, double Y)
        {
            cx = X;
            cy = Y;
        }

    }
}
