using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace AvaloniaApplication1
{
    public delegate void RadiusChangedHandler(object? sender, RadiusEventArgs e);

    public class RadiusEventArgs : EventArgs
    {
        public int _r { get; set; }
        public RadiusEventArgs(int r = 30)
        {
            _r = r;
        }
    }
}
