using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Presentation.ViewModels;

namespace WarehouseMS.Presentation.Models;

public class OrderWrapper : INotifyPropertyChanged
{
    public GetOrdersWithProductAndUserDto Order { get; set; }
    public int Id => Order.Id;
    public int UserId => Order.UserId;
    public int ProductId => Order.ProductId;
    public int TrackingId => Order.TrackingId;
    public string FirstName => Order.FirstName;
    public string LastName => Order.LastName;
    public double Total => Order.Total;
    public int OrderStatusId => Order.OrderStatusId;
    public string OrderStatus => Order.OrderStatus;
    public List<string> OrderStatuses => Order.OrderStatuses;
    public List<GetProductDto> Products => Order.Products;
    public int ItemsCount => Order.ItemsCount;
    public DateTime Date => Order.Date;

    private bool _isChecked;

    public bool IsChecked
    {
        get => _isChecked;
        set
        {
            _isChecked = value;
            OnPropertyChanged(nameof(IsChecked));
        }
    }

    private OrdersViewModel _ordersViewModel;

    public OrderWrapper(GetOrdersWithProductAndUserDto order, OrdersViewModel ordersViewModel)
    {
        Order = order;
        _ordersViewModel = ordersViewModel;
    }

    private void OrderWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OrderStatus))
        {
            _ordersViewModel.UpdateUpdateStatusVisibility();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}