using System;
using GalaSoft.MvvmLight;

namespace WarehouseMS.Presentation.Models;

public class ViewInfo : ObservableObject
{
    public string Name { get; set; }
    public string Icon { get; set; }

    public readonly Action NavigateTo;

    public ViewInfo(string name, string icon, Action navigateTo)
    {
        Name = name;
        NavigateTo = navigateTo;
        Icon = icon;
    }
}