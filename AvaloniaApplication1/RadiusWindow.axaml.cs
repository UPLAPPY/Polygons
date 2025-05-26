using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace AvaloniaApplication1;

public partial class Window1 : Window
{
    public Window1(int radius)
    {
        InitializeComponent();
        slider.Value = radius;
    }

    public event RadiusChangedHandler RC;

    private void SliderValueChanged(object sender, RoutedEventArgs e)
    {
        
        if (RC != null)
        {
            RC(this, new RadiusEventArgs(Convert.ToInt32(slider.Value)));
        }
    }

    public void UpdateRadius(int r)
    {
        slider.Value = r;
    }
}