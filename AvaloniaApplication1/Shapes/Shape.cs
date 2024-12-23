using Avalonia.Media;
using Avalonia;
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

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int R{
            get => r;
        }
        static Shape()
        {
            r = 52;
        }

        public abstract void Draw(DrawingContext dc);
    }
}
