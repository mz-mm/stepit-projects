using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.EventArgs;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Services;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Messages;

namespace WarehouseMS.Presentation.ViewModels;

public class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentView;

    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set
        {
            Set(ref _currentView, value);
        }
    }

    public MainViewModel(IAuthService authService, IMessenger messenger, INavigationService navigationService)
    {
        _messenger = messenger;
        _navigationService = navigationService;

        CurrentView = ServiceLocator.GetService<LoginViewModel>();

        _messenger.Register<NavigationMessage>(this, message =>
        {
            CurrentView = message.ViewModelType;
        });

        authService.SubscribeUserLoggedIn(AuthService_UserLoggedIn);
    }

    private void AuthService_UserLoggedIn(object? sender, UserLoggedInEventArgs e)
    {
        if (e.User != null)
        {
            _navigationService.NavigateTo<HomeViewModel>();
        }
        else
        {
            _navigationService.NavigateTo<LoginViewModel>();
        }
    }

}