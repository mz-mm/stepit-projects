using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WarehouseMS.Presentation.Models;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private ObservableCollection<ViewInfo> _views = new();

    public ObservableCollection<ViewInfo> Views
    {
        get => _views;
        set => Set(ref _views, value);
    }

    private ViewInfo _selectedView;

    public ViewInfo SelectedView
    {
        get => _selectedView;
        set => Set(ref _selectedView, value);
    }


    private ViewModelBase _currentView;
    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => Set(ref _currentView, value);
    }


    public HomeViewModel(ProductsViewModel productsViewModel, OrdersViewModel ordersViewModel)
    {
        Views.Add(new ViewInfo("Products", "Tag", productsViewModel));
        Views.Add(new ViewInfo("Orders", "Inbox", ordersViewModel));

        CurrentView = Views.First().View;
    }

    public RelayCommand NavigateSelectedViewCommand => new(() => { CurrentView = SelectedView.View; });
}