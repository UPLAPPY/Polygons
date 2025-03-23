using Avalonia.Controls;
using Avalonia;
using System;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using Avalonia.Input;
using Avalonia.Rendering.Composition;
using Tmds.DBus.Protocol;
using Avalonia.Controls.Shapes;

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
                    var window = new Window1();
                    window.RC += cc.UpdateRadius;
                    window.Show();
                }
                
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