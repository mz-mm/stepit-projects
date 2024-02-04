using System.Windows;
using System.Windows.Input;

namespace WarehouseMS.Presentation.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
    }

    private void Minimize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;

    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        App.Current.Shutdown();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }
}
