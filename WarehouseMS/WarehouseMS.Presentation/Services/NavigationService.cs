using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Services;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Messages;

namespace WarehouseMS.Presentation.Services;

public class NavigationService : INavigationService
{
    private readonly IMessenger _messenger;
    public NavigationService(IMessenger messenger)
    {
        _messenger = messenger;
    }
    public void NavigateTo<T>() where T : ViewModelBase
    {
        _messenger.Send(new NavigationMessage
            {
                ViewModelType = ServiceLocator.GetService<T>()
            });
    }
}