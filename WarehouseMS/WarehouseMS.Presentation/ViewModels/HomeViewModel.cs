using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Messages;
using WarehouseMS.Presentation.Models;

namespace WarehouseMS.Presentation.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private IMessenger _messenger;
    private INavigationService _navigationService;

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
            SelectedView.NavigateTo();
        }
    }

    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => Set(ref _currentView, value);
    }

    public HomeViewModel(IMessenger messenger, INavigationService navigationService)
    {
        _messenger = messenger;
        _navigationService = navigationService;

        _messenger.Register<HomeNavigationMessage>(this, message => { CurrentView = message.ViewModelType; });

        Views.Add(new ViewInfo("Products", "Tag", () => _navigationService.HomeNavigateTo<ProductsViewModel>()));
        Views.Add(new ViewInfo("Orders", "Inbox", () => _navigationService.HomeNavigateTo<OrdersViewModel>()));
        Views.Add(new ViewInfo("Users", "Users", () => _navigationService.HomeNavigateTo<ManageUsersViewModel>()));

        Views.First().NavigateTo();
    }
}