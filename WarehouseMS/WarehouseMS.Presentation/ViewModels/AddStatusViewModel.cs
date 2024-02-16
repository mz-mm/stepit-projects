using GalaSoft.MvvmLight;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddStatusViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public AddStatusViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public RelayCommand NavigatProductCommand() => new(() => _navigationService.HomeNavigateTo<ProductsViewModel>());
}