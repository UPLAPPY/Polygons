using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace AvaloniaApplication1
{
    public class CustomControl : UserControl
    {
        public override void Render(DrawingContext context)
        {
            Pen pen = new Pen(Brushes.Green, 1, lineCap: PenLineCap.Square);
            Brush brush = new SolidColorBrush(Colors.Black);

            context.DrawEllipse(brush, pen, new Point(100, 100), 10, 10);
            Console.WriteLine("Drawing");
        }
    }
}
