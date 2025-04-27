using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace AvaloniaApplication1;

public partial class Graph : Window
{
    public Graph()
    {
        InitializeComponent();
        var graphControl = new GraphCustomControl();
        Content = graphControl;
    }
}