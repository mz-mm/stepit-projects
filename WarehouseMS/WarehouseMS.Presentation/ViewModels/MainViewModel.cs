using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.Messages;
using WarehouseMS.Domain.Services;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Messages;

namespace WarehouseMS.Presentation.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly IMessenger _messenger;
    private readonly INavigationService _navigationService;

    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => Set(ref _currentView, value);
    }

    public MainViewModel(IMessenger messenger, INavigationService navigationService)
    {
        _messenger = messenger;
        _navigationService = navigationService;

        CurrentView = ServiceLocator.GetService<LoginViewModel>();

        _messenger.Register<NavigationMessage>(this, message => { CurrentView = message.ViewModelType; });

        _messenger.Register<UserLoginMessage>(this, message =>
        {
            if (message.User is null)
            {
                _navigationService.NavigateTo<LoginViewModel>();
                return;
            }

            _navigationService.NavigateTo<HomeViewModel>();
        });
    }
}