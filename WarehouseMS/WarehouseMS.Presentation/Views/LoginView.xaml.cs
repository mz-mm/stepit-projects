using System.Windows;
using System.Windows.Controls;

namespace WarehouseMS.Presentation.Views;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
    {
        PlaceholderLabel.Visibility = Visibility.Collapsed;
    }

    private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (PasswordBox.SecurePassword.Length == 0)
        {
            PlaceholderLabel.Visibility = Visibility.Visible;
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }

        PlaceholderLabel.Visibility = PasswordBox.SecurePassword.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
    }
}
