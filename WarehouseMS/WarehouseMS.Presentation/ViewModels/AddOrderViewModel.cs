using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.OrderDtos;
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

    public bool _addUserVisibility;

    public bool AddUserVisibility
    {
        get => _addUserVisibility;
        set => Set(ref _addUserVisibility, value);
    }

    public bool _addCustomerBtnVisibility = true;

    public bool AddCustomerBtnVisibility
    {
        get => _addCustomerBtnVisibility;
        set => Set(ref _addCustomerBtnVisibility, value);
    }

    private double _total = 0.00;

    public double Total
    {
        get => _total;
        set => Set(ref _total, value);
    }

    private string _error;

    public string Error
    {
        get => _error;
        set => Set(ref _error, value);
    }

    private bool _errorVisibility;

    public bool ErrorVisibility
    {
        get => _errorVisibility;
        set => Set(ref _errorVisibility, value);
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


    public RelayCommand AddProductCommand => new(
        () =>
        {
            SelectedProducts.Add(SelectedProduct);
            Total = SelectedProducts.Sum(sp => sp.Price);
            AddProductVisibility = false;
            ErrorVisibility = false;
        },
        () => !SelectedProducts.Contains(SelectedProduct));

    public RelayCommand AddUserCommand => new(() => { AddUserVisibility = false; });

    public RelayCommand CancelProductAddCommand => new(() =>
    {
        AddProductVisibility = false;
        SelectedProduct = null;
    });

    public RelayCommand DiscardCommand => new(() => _navigationService.HomeNavigateTo<OrdersViewModel>());
    public RelayCommand CancelUserAddCommand => new(() => { AddUserVisibility = false; });
    public RelayCommand ShowAddProduct => new(() => { AddProductVisibility = true; });
    public RelayCommand ShowAddUser => new(() => { AddUserVisibility = true; });

    public RelayCommand SaveCommand => new(async () =>
    {
        try
        {
            ErrorVisibility = false;

            await _orderService.CreateOrderAsync(new CreateOrderDto
            {
                UserId = SelectedUser.Id,
                ProductIds = SelectedProducts.Select(x => x.Id)
            });

            _navigationService.HomeNavigateTo<OrdersViewModel>();
        }
        catch (Exception ex)
        {
            ErrorVisibility = true;
            Error = ex.Message;
        }
    });
}