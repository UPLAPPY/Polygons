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
        protected double x, y;
        protected static int r;
        bool in_moving = false;

        public Shape(double x, double y)
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

        public abstract bool isInside(int xp, int yp);
    }
}
