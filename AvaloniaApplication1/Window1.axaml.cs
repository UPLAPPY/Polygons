using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
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
}