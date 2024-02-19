using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Dtos.StatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Messages;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class ProductsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IProductService _productService;
    private readonly IStatusViewService _statusViewService;
    private readonly IMessenger _messenger;

    public AddStatusViewModel AddStatusViewModel { get; set; }

    private IEnumerable<GetProductWithStatusDto> _allProducts;
    private ObservableCollection<GetProductWithStatusDto> _products = new();

    public ObservableCollection<GetProductWithStatusDto> Products
    {
        get => _products;
        set => Set(ref _products, value);
    }

    private GetProductWithStatusDto _selectedProducts = new();

    public GetProductWithStatusDto SelectedProducts
    {
        get => _selectedProducts;
        set => Set(ref _selectedProducts, value);
    }

    private ObservableCollection<GetStatusViewDto> _statusViews = new();

    public ObservableCollection<GetStatusViewDto> StatusViews
    {
        get => _statusViews;
        set => Set(ref _statusViews, value);
    }

    private GetStatusViewDto _selectedStatusView;

    public GetStatusViewDto SelectedStatusView
    {
        get => _selectedStatusView;
        set
        {
            Set(ref _selectedStatusView, value);

            Products = new ObservableCollection<GetProductWithStatusDto>(_allProducts.Where(product =>
                product.StatusName == _selectedStatusView.Name));
        }
    }

    public ProductsViewModel(IProductService productService, INavigationService navigationService,
        IStatusViewService statusViewService, AddStatusViewModel addStatusViewModel, IMessenger messenger)
    {
        _productService = productService;
        _navigationService = navigationService;
        _statusViewService = statusViewService;
        _messenger = messenger;

        _messenger.Register<AddStatusViewMessage>(this, message => { StatusViews.Add(message.StatusView); });

        AddStatusViewModel = addStatusViewModel;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        _allProducts = await _productService.GetAllProductsWithStatusAsync();
        Products = new ObservableCollection<GetProductWithStatusDto>(_allProducts);

        var statuses = await _statusViewService.GetAllStatusAsync();
        StatusViews = new ObservableCollection<GetStatusViewDto>(statuses);
    }


    public RelayCommand AllCommand =>
        new(() => { Products = new ObservableCollection<GetProductWithStatusDto>(_allProducts); });

    public RelayCommand AddProductCommand =>
        new(() => { _navigationService.HomeNavigateTo<AddProductViewModel>(); });

    public RelayCommand AddStatusCommand =>
        new(() => { AddStatusViewModel.IsOpen = true; });
}