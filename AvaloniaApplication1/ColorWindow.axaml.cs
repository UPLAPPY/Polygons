using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace AvaloniaApplication1;

public partial class ColorWindow : Window
{
    private Color _lastColor;

    public ColorWindow()
    {
        InitializeComponent();
        _lastColor = Colors.Green;
    }

    public event ColorChangedHandler CC;
    private void OkButton_Pressed(object sender, RoutedEventArgs e)
    {
        _lastColor = ColorPicker.Color;

        if (CC != null)
        {
            CC(this, new ColorEventArgs(_lastColor));
        }

        Close();
    }

    private void DefaultButton_Pressed(object sender, RoutedEventArgs e)
    {
        ColorPicker.Color = _lastColor;
    }
}