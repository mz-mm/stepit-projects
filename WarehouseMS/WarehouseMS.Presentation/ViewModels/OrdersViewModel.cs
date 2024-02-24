using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class OrdersViewModel : ViewModelBase
{
    private readonly IOrderService _orderService;
    private readonly INavigationService _navigationService;

    private ObservableCollection<GetOrdersWithStatusAndProductAndUserDto> _orderDetails = new();

    public ObservableCollection<GetOrdersWithStatusAndProductAndUserDto> OrderDetails
    {
        get => _orderDetails;
        set => Set(ref _orderDetails, value);
    }

    public OrdersViewModel(IOrderService order, INavigationService navigationService)
    {
        _orderService = order;
        _navigationService = navigationService;

        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        var orders = await _orderService.GetAllOrdersWithStatusAndProductsAsync();
        OrderDetails = new ObservableCollection<GetOrdersWithStatusAndProductAndUserDto>(orders);
    }


    public RelayCommand CreateOrderCommand => new(() => _navigationService.HomeNavigateTo<AddOrderViewModel>());
}