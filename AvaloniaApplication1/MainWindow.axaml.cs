using Avalonia.Controls;
using Avalonia;
using System;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            if (e.GetCurrentPoint(CC).Properties.IsRightButtonPressed)
            {
                CC.LeftClick(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
            }
            else
            {
                CC.RightClick(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
            }
        }

        private void Window_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Drag(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
        }

        private void Window_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
        {
            CustomControl CC = this.Find<CustomControl>("myCC");
            CC.Drop();
        }
    }
}