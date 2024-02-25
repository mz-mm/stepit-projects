using System.Linq;
using GalaSoft.MvvmLight;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Presentation.Interfaces;
using WarehouseMS.Presentation.Models;
using WarehouseMS.Presentation.Services;

namespace WarehouseMS.Presentation.ViewModels;

public class OrdersViewModel : ViewModelBase
{
    private readonly IOrderService _orderService;
    private readonly INavigationService _navigationService;

    private bool _isUpdateStatusVisible;

    public bool IsUpdateStatusVisible
    {
        get => _isUpdateStatusVisible;
        set => Set(ref _isUpdateStatusVisible, value);
    }

    private ObservableItemCollection<OrderWrapper> _orderDetails = new();

    public ObservableItemCollection<OrderWrapper> OrderDetails
    {
        get => _orderDetails;
        set
        {
            if (_orderDetails != value)
            {
                Set(ref _orderDetails, value);
                UpdateUpdateStatusVisibility();
            }
        }
    }

    public ObservableItemCollection<OrderWrapper> _checkedOrders  = new();

    public ObservableItemCollection<OrderWrapper> CheckedOrders
    {
        get => _checkedOrders;
        set => Set(ref _checkedOrders, value);
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
        foreach (var order in orders)
        {
            OrderDetails.Add(new OrderWrapper(order, this));
        }
    }
    public void UpdateUpdateStatusVisibility()
    {
        CheckedOrders.Clear();

        if (CheckedOrders.Any())
        {
            foreach (var order in OrderDetails)
            {
                if (order.IsChecked)
                    CheckedOrders.Add(order);
            }
        }    

        IsUpdateStatusVisible = CheckedOrders.Any();
    }

    public RelayCommand UpdateSelectedStatusCommand => new(async () =>
    {
        foreach (var checkedOrder in CheckedOrders)
        {
            await _orderService.UpdateOrderStatusAsync(checkedOrder.Order);
        }
    });

    public RelayCommand CreateOrderCommand => new(() => _navigationService.HomeNavigateTo<AddOrderViewModel>());
}