using System.Windows.Controls;
using System.Windows.Input;

namespace WarehouseMS.Presentation.Views;

public partial class ProductsView : UserControl
{
    public ProductsView()
    {
        InitializeComponent();
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (sender is ScrollViewer scrollViewer)
        {
            double deltaY = -e.Delta;

            if (e.Delta > 0)
            {
                scrollViewer.LineUp();
            }
            else if (e.Delta < 0)
            {
                scrollViewer.LineDown();
            }

            e.Handled = true;
        }
    }
}