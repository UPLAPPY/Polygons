using Avalonia.Controls;
using Avalonia;
using System;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;


namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        bool _menuClicked = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuClicked(object? sender, PointerPressedEventArgs e)
        {
            _menuClicked = true;
        }

        private void ItemClicked(object? sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                _menuClicked = true;
                CustomControl cc = this.FindControl<CustomControl>("myCC");
                string mes = menuItem.Header.ToString();
                if (mes == "Triangle" || mes == "Circle" || mes == "Square")
                {
                    cc.SetShape(mes);
                }
                else if (mes == "Jarvis" || mes == "ByDef")
                {
                    cc.SetAlg(mes);
                }
                else if (mes == "Radius")
                {
                    var graphWindow = FindWindow<Window1>();
                    if (graphWindow == null)
                    {
                        graphWindow = new Window1(Shape.r);
                        graphWindow.RC += cc.UpdateRadius;
                        graphWindow.Show();
                    }
                    else
                    {
                        if (graphWindow.WindowState == WindowState.Minimized)
                        {
                            graphWindow.WindowState = WindowState.Normal;
                        }
                        graphWindow.Show();
                        graphWindow.Activate();
                    }
                }
                else if (mes == "Chart")
                {
                    var graphWindow = FindWindow<Graph>();
                    if (graphWindow == null)
                    {
                        graphWindow = new Graph();
                        graphWindow.Show();
                    }
                    else
                    {
                        if (graphWindow.WindowState == WindowState.Minimized)
                        {
                            graphWindow.WindowState = WindowState.Normal;
                        }
                        graphWindow.Show();
                        graphWindow.Activate();
                    }
                }
                else if (mes == "Color")
                {
                    var radiusWindow = FindWindow<ColorWindow>();
                    if (radiusWindow == null)
                    {
                        var color = new ColorWindow();
                        color.CC += cc.UpdateColor;
                        color.Show();
                    }
                    else
                    {
                        if (radiusWindow.WindowState == WindowState.Minimized)
                        {
                            radiusWindow.WindowState = WindowState.Normal;
                        }
                        radiusWindow.Show();
                        radiusWindow.Activate();
                    }
                }
            }
        }

        private async void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (_menuClicked)
            {
                _menuClicked = false;
                return;
            }

            CustomControl CC = this.Find<CustomControl>("myCC");
            if (e.GetCurrentPoint(CC).Properties.IsRightButtonPressed)
            {
                CC.RightClick(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
            }
            else
            {
                CC.LeftClick(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
            }
        }

        private void Window_PointerMoved(object? sender, PointerEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Drag(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
        }

        private void Window_PointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Drop();
        }

        private T FindWindow<T>() where T : Window
        {
            var windows = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).Windows;
            foreach (Window window in windows)
            {
                if (window is T typedWindow)
                {
                    return typedWindow;
                }
            }
            return null;
        }
    }
}