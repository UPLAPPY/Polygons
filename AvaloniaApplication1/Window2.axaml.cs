using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication1;

public partial class Window2 : Window
{
    public Window2()
    {
        InitializeComponent();
        var graphControl = new GraphCustomControl();
        Content = graphControl;
    }
}