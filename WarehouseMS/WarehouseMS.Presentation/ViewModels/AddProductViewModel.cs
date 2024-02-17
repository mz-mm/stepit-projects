using GalaSoft.MvvmLight;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddProductViewModel : ViewModelBase
{
    private INavigationService _navigationService;

    public AddProductViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public RelayCommand NavigatProductCommand => new(() => _navigationService.HomeNavigateTo<ProductsViewModel>());
}