using Avalonia.Controls;
using Avalonia;
using System;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using System.Linq;


namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        private bool _menuClicked = false;
        private bool _saved = true;
        private string _filePath = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartClicked(object? sender, RoutedEventArgs e)
        {
            CustomControl cc = this.FindControl<CustomControl>("myCC");
            cc.StartDynamic();
        }

        public void StopClicked(object? sender, RoutedEventArgs e)
        {
            CustomControl cc = this.FindControl<CustomControl>("myCC");
            cc.StopDynamic();
        }

        public void Window_Closed(object? sender, EventArgs e)
        {
            CloseAll();
        }

        private void MenuClicked(object? sender, PointerPressedEventArgs e)
        {
            _menuClicked = true;
        }

        private async void ItemClicked(object? sender, RoutedEventArgs e)
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
                    Window1 radiusWindow = FindWindow<Window1>();
                    if (radiusWindow == null)
                    {
                        radiusWindow = new Window1(Shape.r);
                        radiusWindow.RC += cc.UpdateRadius;
                        radiusWindow.Show();
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
                    var colorWindow = FindWindow<ColorWindow>();
                    if (colorWindow == null)
                    {
                        var color = new ColorWindow();
                        color.CC += cc.UpdateColor;
                        color.Show();
                    }
                    else
                    {
                        if (colorWindow.WindowState == WindowState.Minimized)
                        {
                            colorWindow.WindowState = WindowState.Normal;
                        }
                        colorWindow.Show();
                        colorWindow.Activate();
                    }
                }
                else if (mes == "New")
                {
                    string _result = "NoResult";
                    if (!_saved)
                    {
                        _result = await AskToSave();

                        if (_result == "Save")
                        {
                            if (_filePath == null)
                            {
                                _filePath = await SelectSaveFile();
                            }
                            if (_filePath == null) { return; }
                            _saved = true;
                            cc.Save(_filePath);
                            cc.SetNewFile();
                            CloseLoad();
                        }
                        else if (_result == "DontSave")
                        {
                            cc.SetNewFile();
                            _filePath = null;
                            _saved = true;
                            CloseLoad();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        cc.SetNewFile();
                        _filePath = null;
                        _saved = true;
                        CloseLoad();
                    }
                }
                else if (mes == "Save")
                {
                    if (_filePath == null)
                    {
                        _filePath = await SelectSaveFile();
                    }
                    if (_filePath == null) { return; }
                    _saved = true;
                    cc.Save(_filePath);
                }
                else if (mes == "Save as")
                {
                    _filePath = await SelectSaveFile();
                    if (_filePath == null) { return; }
                    _saved = true;
                    cc.Save(_filePath);
                }
                else if (mes == "Open")
                {
                    string _result = "NoResult";
                    if (!_saved)
                    {
                        _result = await AskToSave();

                        if (_result == "Save")
                        {
                            if (_filePath == null)
                            {
                                _filePath = await SelectSaveFile();
                            }
                            if (_filePath == null) { return; }
                            _saved = true;
                            cc.Save(_filePath);

                            _filePath = await SelectOpenFile();
                            if (_filePath == null) { return; }
                            cc.Load(_filePath);
                            CloseLoad();
                        }
                        else if (_result == "DontSave")
                        {
                            _filePath = await SelectOpenFile();
                            if (_filePath == null) { return; }
                            cc.Load(_filePath);
                            CloseLoad();
                        }
                    }
                    else
                    {
                        _filePath = await SelectOpenFile();
                        if (_filePath == null) { return; }
                        cc.Load(_filePath);
                        CloseLoad();
                    }
                }
                else if (mes == "Exit")
                {
                    string _result = "NoResult";
                    if (!_saved)
                    {
                        _result = await AskToSave();

                        if (_result == "Save")
                        {
                            if (_filePath == null)
                            {
                                _filePath = await SelectSaveFile();
                            }
                            if (_filePath == null) { return; }
                            _saved = true;
                            cc.Save(_filePath);
                            CloseAll();
                        }
                        else if (_result == "DontSave")
                        {
                            CloseAll();
                        }
                    }
                    else
                    {
                        CloseAll();
                    }
                }
            }
            _menuClicked = false;
        }

        private void CloseLoad()
        {
            var radiusWindow = FindWindow<Window1>();
            if (radiusWindow != null)
            {
                radiusWindow.Close();
            }
            var colorWindow = FindWindow<ColorWindow>();
            if (colorWindow != null)
            {
                colorWindow.Close();
            }
        }

        private void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (_menuClicked)
            {
                _menuClicked = false;
                return;
            }

            CustomControl CC = this.Find<CustomControl>("myCC");
            if (e.GetCurrentPoint(CC).Properties.IsRightButtonPressed)
            {
                _saved = false;
                CC.RightClick(Convert.ToInt32(e.GetPosition(CC).X), Convert.ToInt32(e.GetPosition(CC).Y));
            }
            else
            {
                _saved = false;
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

        private async Task<string> AskToSave()
        {
            UnsavedChangesWindow unsavedChangesWindow = new UnsavedChangesWindow();
            var result = await unsavedChangesWindow.ShowDialog<string>(this);
            _menuClicked = false;
            return result;
        }

        private async Task<string> SelectSaveFile()
        {
            var topLevel = GetTopLevel(this);
            var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Save File"
            });

            if (file == null)
            {
                return null;
            }
            else
            {
                return file.Path.AbsolutePath.ToString();
            }
        }

        private async Task<string> SelectOpenFile()
        {
            var topLevel = GetTopLevel(this);
            var file = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open File"
            });

            if (file == null || file.Count == 0)
            {
                return null;
            }
            else
            {
                return file[0].Path.AbsolutePath.ToString();
            }
        }

        private void CloseAll()
        {
            var windows = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).Windows.ToList();
            foreach (var window in windows)
            {
                window.Close();
            }
        }
    }
}