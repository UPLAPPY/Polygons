using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.Shapes
{
    abstract class Shape
    {
        protected int x, y;
        protected static int r;
        protected string color;

        public Shape(int x, int y, string color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }
        static Shape()
        {
            r = 0;
        }

        public void Draw(DrawingContext dc)
        {

        }
    }
}
