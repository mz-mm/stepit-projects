using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IProductService _productService;
    private readonly IStatusService _statusService;

    private ObservableCollection<GetProductDto> _products = new();

    public ObservableCollection<GetProductDto> Products
    {
        get => _products;
        set => Set(ref _products, value);
    }

    private GetProductDto _selectedProducts = new();

    public GetProductDto SelectedProducts
    {
        get => _selectedProducts;
        set => Set(ref _selectedProducts, value);
    }

    private ObservableCollection<GetStatusDto> _statuses = new();

    public ObservableCollection<GetStatusDto> Statuses
    {
        get => _statuses;
        set => Set(ref _statuses, value);
    }

    private GetStatusDto _selectedStatus;

    public GetStatusDto SelectedStatus
    {
        get => _selectedStatus;
        set => Set(ref _selectedStatus, value);
    }

    public ProductsViewModel(IProductService productService, INavigationService navigationService,
        IStatusService statusService)
    {
        _productService = productService;
        _navigationService = navigationService;
        _statusService = statusService;
        InitilizeAsync();
    }

    private async void InitilizeAsync()
    {
        var products = await _productService.GetAllProductsAsync();
        Products = new ObservableCollection<GetProductDto>(products);

        var statuses = await _statusService.GetAllStatusAsync();
        Statuses = new ObservableCollection<GetStatusDto>(statuses);
    }

    public RelayCommand AddProductCommand =>
        new(() => { _navigationService.HomeNavigateTo<AddProductViewModel>(); });

    public RelayCommand AddStatusCommand =>
        new(() => { _navigationService.HomeNavigateTo<AddStatusViewModel>(); });

}