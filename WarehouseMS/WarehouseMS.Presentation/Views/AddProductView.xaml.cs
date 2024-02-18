using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WarehouseMS.Presentation.Views;

public partial class AddProductView : UserControl
{
    public AddProductView()
    {
        InitializeComponent();
    }

    private void RTB_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            InsertText(RTB, "\r\n");
            e.Handled = true;
        }
    }

    public static void InsertText(RichTextBox rtb, string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            rtb?.BeginChange();
            if (!string.IsNullOrEmpty(rtb.Selection.Text))
            {
                rtb.Selection.Text = string.Empty;
            }
            var tp = rtb.CaretPosition.GetPositionAtOffset(0, LogicalDirection.Forward);
            rtb.CaretPosition.InsertTextInRun(content);
            rtb.CaretPosition = tp;
            rtb.EndChange();
            Keyboard.Focus(rtb);
        }
    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!char.IsDigit(e.Text, e.Text.Length - 1))
        {
            e.Handled = true;
        }
    }

}