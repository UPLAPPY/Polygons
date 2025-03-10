using Avalonia.Controls;
using Avalonia;
using System;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Rendering.Composition;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        bool _menuClicked = false;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MenuClicked(object? sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string shape = menuItem.Header.ToString();
                _menuClicked = true;
                CustomControl cc = this.FindControl<CustomControl>("myCC");
                
                cc.SetShape(shape);
            }
        }

        private void AlgMenuClicked(object? sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                string alg = menuItem.Header.ToString();
                _menuClicked = true;
                CustomControl cc = this.FindControl<CustomControl>("myCC");

                cc.SetAlg(alg);
            }
        }


        private async void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            await Task.Delay(100);

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
    }
}