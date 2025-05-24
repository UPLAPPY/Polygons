using Avalonia.Media;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SkiaSharp;
using System.Text.Json.Serialization;

namespace AvaloniaApplication1
{
    [JsonDerivedType(typeof(Circle), "Circle")]
    [JsonDerivedType(typeof(Square), "Square")]
    [JsonDerivedType(typeof(Triangle), "Triangle")]
    abstract class Shape
    {
        [JsonInclude]
        public int x;
        [JsonInclude]
        public int y;
        [JsonInclude]
        public static Brush Brush = new SolidColorBrush(Colors.Green);
        [JsonInclude]
        public static int r;

        static Pen pen;
        public bool is_moving = false;
        private int dx, dy;
        public bool is_vertex = false;
        
        public static int R
        {
            get; 
            set;
        }
        
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
            pen = new Pen(Brush, 3);
            r = 30;
        }

        public abstract void Draw(DrawingContext dc);
        public abstract bool isInside(int xp, int yp);
    }
}
