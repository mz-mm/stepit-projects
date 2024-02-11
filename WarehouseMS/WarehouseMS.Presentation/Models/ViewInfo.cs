using GalaSoft.MvvmLight;

namespace WarehouseMS.Presentation.Models;

public class ViewInfo : ObservableObject
{
    public string Name { get; set; }
    public string Icon { get; set; }
    public ViewModelBase View { get; set; }

    public ViewInfo(string name, string icon, ViewModelBase view)
    {
        Name = name;
        View = view;
        Icon = icon;
    }
}