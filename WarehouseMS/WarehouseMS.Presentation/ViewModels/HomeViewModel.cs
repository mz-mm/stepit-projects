using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.Services;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Messages;
using WarehouseMS.Presentation.Models;

namespace WarehouseMS.Presentation.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private IMessenger _messenger;
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
        set
        {
            Set(ref _selectedView, value);
            CurrentView = SelectedView.View;
        }
    }

    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => Set(ref _currentView, value);
    }

    public HomeViewModel(INavigationService navigationService, IMessenger messenger, ProductsViewModel productsViewModel, OrdersViewModel ordersViewModel)
    {
        _messenger = messenger;

        Views.Add(new ViewInfo("Products", "Tag", productsViewModel));
        Views.Add(new ViewInfo("Orders", "Inbox", ordersViewModel));

        CurrentView = Views.First().View;

        _messenger.Register<HomeNavigationMessage>(this, message => { CurrentView = message.ViewModelType; });
    }
}