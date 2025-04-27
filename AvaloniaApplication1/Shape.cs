using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SkiaSharp;

namespace AvaloniaApplication1
{
    abstract class Shape
    {
        public int x, y;
        public static int r { get; set; }
        public bool is_moving = false;
        private int dx, dy;
        public bool is_vertex = false;
        static Pen pen;
        static Brush brush = new SolidColorBrush(Colors.Green);
        public static Pen Pen
        {
            get => pen;
            set => pen = value;
        }

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        } 
        public int DX
        {
            get { return dx; }
            set { dx = value; }
        }

        public int DY
        {
            get { return dy; }
            set { dy = value; }
        }

        static Shape()
        {
            pen = new Pen(brush, 3);
            r = 30;
        }

        public static Brush Brush
        {
            get => brush;
            set
            {
                brush = value;
                pen = new Pen(brush, 3);
            }
        }

        public abstract void Draw(DrawingContext dc);
        public abstract bool isInside(int xp, int yp);
    }
}
