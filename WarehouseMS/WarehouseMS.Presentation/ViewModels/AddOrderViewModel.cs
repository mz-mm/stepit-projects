using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Domain.Dtos.UserDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class AddOrderViewModel : ViewModelBase
{
    private readonly IOrderService _orderService;
    private readonly INavigationService _navigationService;
    private readonly IUserService _userService;
    private readonly IProductService _productService;

    private int _userId;
    private List<int> _productIds { get; set; }

    private GetUserDto _selectedUser;

    public GetUserDto SelectedUser
    {
        get => _selectedUser;
        set => Set(ref _selectedUser, value);
    }

    private ObservableCollection<GetUserDto> _users = new();

    public ObservableCollection<GetUserDto> Users
    {
        get => _users;
        set => Set(ref _users, value);
    }

    private GetProductDto _selectedProduct = new();

    public GetProductDto SelectedProduct
    {
        get => _selectedProduct;
        set => Set(ref _selectedProduct, value);
    }

    private ObservableCollection<GetProductDto> _selectedProducts = new();

    public ObservableCollection<GetProductDto> SelectedProducts
    {
        get => _selectedProducts;
        set => Set(ref _selectedProducts, value);
    }

    private ObservableCollection<GetProductDto> _products = new();

    public ObservableCollection<GetProductDto> Products
    {
        get => _products;
        set => Set(ref _products, value);
    }

    public bool _addProductVisibility;

    public bool AddProductVisibility
    {
        get => _addProductVisibility;
        set => Set(ref _addProductVisibility, value);
    }

    private int _total;

    public int Total
    {
        get => _total;
        set => Set(ref _total, value);
    }

    public AddOrderViewModel(IOrderService orderService, IProductService productService, IUserService userService,
        INavigationService navigationService)
    {
        _orderService = orderService;
        _productService = productService;
        _userService = userService;
        _navigationService = navigationService;

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        var products = await _productService.GetAllProductsAsync();
        Products = new ObservableCollection<GetProductDto>(products);

        var users = await _userService.GetAllUsersAsync();
        Users = new ObservableCollection<GetUserDto>(users);
    }


    public RelayCommand AddProductCommand => new(() =>
    {
        if (!SelectedProducts.Contains(SelectedProduct))
        {
            SelectedProducts.Add(SelectedProduct);
            AddProductVisibility = false;
        }
    });

    public RelayCommand CancelProductAddCommand => new(() =>
    {
        AddProductVisibility = false;
        SelectedProduct = null;
    });

    public RelayCommand ShowAddProduct => new(() => { AddProductVisibility = true; });

    public RelayCommand SaveCommand => new(() => { });
}