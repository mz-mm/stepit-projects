using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
    }

    public async Task<IEnumerable<GetOrderDto>> GetUserOrdersAsync(int userId)
    {
        var userOrders = await _orderRepository.GetAllUserOrdersAsync(userId);
        return _mapper.Map<IEnumerable<GetOrderDto>>(userOrders);
    }

    public async Task<GetOrderDto?> GetUserOrderbyIdAsync(int userId, int orderId)
    {
        var userOrder = await _orderRepository.GetUserOrderByIdAsync(userId, orderId);
        return _mapper.Map<GetOrderDto>(userOrder);
    }

    public async Task<GetOrderDto?> GetOrderbyIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDetails)
    {
        var orderEntity = _mapper.Map<Order>(orderDetails);
        var result = await _orderRepository.InsertAsync(orderEntity);

        return _mapper.Map<GetOrderDto>(result);
    }
    
    public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
            throw new ArgumentNullException(nameof(order));

        order.OrderStatus = newStatus.ToString();

        var result = await _orderRepository.UpdateAsync(order);

        return result;
    }

    public async Task<bool> UpdateOrderAsync(CreateOrderDto order)
    {
        var orderEntity = _mapper.Map<Order>(order);
        var result = await _orderRepository.UpdateAsync(orderEntity);

        return result;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);

        if (order is null)
            return false;

        var result = await _orderRepository.DeleteAsync(order);
        return result;
    }
}