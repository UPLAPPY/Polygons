using Avalonia.Controls;
using Avalonia;
using System;
using Avalonia.Interactivity;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;


namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {

        private bool _menuClicked = false;
        private bool _saved = true;
        private string _saveFilePath = null;
        private string _loadFilePath = null;

        public MainWindow()
        {
            InitializeComponent();
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
                else if (mes == "New")
                {
                    string _result = "NoResult";
                    if (!_saved)
                    {
                        _result = await AskToSave();

                        if (_result == "Save")
                        {
                            if (_saveFilePath == null)
                            {
                                _saveFilePath = await SelectFile();
                            }
                            _saved = true;
                            if (_saveFilePath == null) { return; }
                            cc.Save(_saveFilePath);
                            cc.SetNewFile();
                        }
                        else if (_result == "DontSave")
                        {
                            cc.SetNewFile();
                            _saveFilePath = null;
                            _saved = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        cc.SetNewFile();
                        _saveFilePath = null;
                        _saved = true;
                    }
                }
                else if (mes == "Save")
                {
                    if (_saveFilePath == null)
                    {
                        _saveFilePath = await SelectFile();
                    }
                    if (_saveFilePath == null) { return; }
                    _saved = true;
                    cc.Save(_saveFilePath);
                }
                else if (mes == "Save As")
                {
                    _saveFilePath = await SelectFile();
                    if (_saveFilePath == null) { return; }
                    _saved = true;
                    cc.Save(_saveFilePath);
                }
                else if (mes == "Open")
                {
                    string _result = "NoResult";
                    if (!_saved)
                    {
                        _result = await AskToSave();

                        if (_result == "Save")
                        {
                            if (_saveFilePath == null)
                            {
                                _saveFilePath = await SelectFile();
                            }
                            if (_saveFilePath == null) { return; }
                            _saved = true;
                            cc.Save(_saveFilePath);
                        }
                        else if (_result != "null")
                        {
                            _loadFilePath = await SelectFile();
                            if (_loadFilePath == null) { return; }
                            cc.Load(_loadFilePath);
                        }
                    }
                    else
                    {
                        _loadFilePath = await SelectFile();
                        if (_loadFilePath == null) { return; }
                        cc.Load(_loadFilePath);
                    }
                }
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
            return result;
        }

        private async Task<string> SelectFile()
        {
            var topLevel = GetTopLevel(this);
            CustomControl CC = this.Find<CustomControl>("myCC");

            var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Select File"
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
    }
}