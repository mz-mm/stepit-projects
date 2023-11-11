using System.Windows.Controls;
using System.Windows.Input;

namespace Monefy.Views
{
    public partial class AddTransactionView : UserControl
    {
        public AddTransactionView()
        {
            InitializeComponent();
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.Text, "^[0-9]+$"))
            {
                e.Handled = true; // Cancel the input
            }
        }

        private void View_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid.Focus();
        }

    }
}
