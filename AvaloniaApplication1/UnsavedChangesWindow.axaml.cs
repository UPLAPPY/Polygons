using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaApplication1;

public partial class UnsavedChangesWindow : Window
{
    public UnsavedChangesWindow()
    {
        InitializeComponent();
    }

    private void SaveButton_Pressed(object sender, RoutedEventArgs e)
    {
        Close("Save");
    }

    private void NotSaveButton_Pressed(object sender, RoutedEventArgs e)
    {
        Close("DontSave");
    }

    private void CancelButton_Pressed(object sender, RoutedEventArgs e)
    {
        Close("null");
    }
}