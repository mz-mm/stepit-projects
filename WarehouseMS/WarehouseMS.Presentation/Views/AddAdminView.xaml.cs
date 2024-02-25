using System.Windows;
using System.Windows.Controls;

namespace WarehouseMS.Presentation.Views;

public partial class AddAdminView : UserControl
{
    public AddAdminView()
    {
        InitializeComponent();
    }

    private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
    {
        PasswordPlaceholderLabel.Visibility = Visibility.Collapsed;
    }

    private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (PasswordBox.SecurePassword.Length == 0)
        {
            PasswordPlaceholderLabel.Visibility = Visibility.Visible;
        }
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            ((dynamic)DataContext).Password = ((PasswordBox)sender).Password;
        }

        PasswordPlaceholderLabel.Visibility =
            ((PasswordBox)sender).Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
    }

    private void ConfirmPasswordBox_GotFocus(object sender, RoutedEventArgs e)
    {
        ConfirmPasswordPlaceholderLabel.Visibility = Visibility.Collapsed;
    }

    private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
    {
        if (ConfirmPasswordBox.SecurePassword.Length == 0)
        {
            ConfirmPasswordPlaceholderLabel.Visibility = Visibility.Visible;
        }
    }

    private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null)
        {
            ((dynamic)DataContext).ConfirmPassword = ((PasswordBox)sender).Password;
        }

        ConfirmPasswordPlaceholderLabel.Visibility =
            ((PasswordBox)sender).Password.Length > 0 ? Visibility.Collapsed : Visibility.Visible;
    }
}