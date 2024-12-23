using Avalonia.Controls;
using Avalonia;

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
            CC.Click(e.GetPosition(CC).X, e.GetPosition(CC).Y);
        }
    }
}