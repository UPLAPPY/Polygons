using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1
{
    internal class SaveData
    {
        public List<Shape> SaveShapes { get; set; }
        public int SaveR { get; set; }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public SaveData()
        {
            SaveShapes = new List<Shape>();
            SaveR = 0;
            A = 0;
            R = 0;
            G = 0;
            B = 0;
        }

        public SaveData(List<Shape> shapes, int r, Color color)
        {
            SaveShapes = shapes;
            SaveR = r;
            A = color.A; 
            R = color.R;
            G = color.G;
            B = color.B;
        }
    }
}
