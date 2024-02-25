using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Exceptions;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStatusViewService _statusViewService;
    private readonly IOrderProductRepository _orderProductRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IStatusViewService statusViewService,
        IOrderProductRepository orderProductRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _statusViewService = statusViewService;
        _orderProductRepository = orderProductRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetOrderDto>>(orders);
    }


    public async Task<IEnumerable<GetOrdersWithProductAndUserDto>> GetAllOrdersWithStatusAndProductsAsync()
    {
        var orders = await _orderRepository.GetAllWithProductsAndUserAsync();
        var orderEntities = _mapper.Map<IEnumerable<GetOrdersWithProductAndUserDto>>(orders);

        var allOrdersWithStatusAndProductsAsync = orderEntities.ToList();
        foreach (var orderEntity in allOrdersWithStatusAndProductsAsync)
        {
            orderEntity.ItemsCount = orderEntity.Products.Count;
            orderEntity.Total += orderEntity.Products.Sum(p => p.Price);

            var status = await _statusViewService.GetStatusByIdAsync(orderEntity.OrderStatusId);
            if (status?.Name != null) orderEntity.OrderStatuses.Add(status.Name);
        }

        return allOrdersWithStatusAndProductsAsync;
    }

    public async Task<IEnumerable<GetOrderDto>> GetUserOrdersAsync(int userId)
    {
        var userOrders = await _orderRepository.GetAllUserOrdersAsync(userId);
        return _mapper.Map<IEnumerable<GetOrderDto>>(userOrders);
    }

    public async Task<GetOrderDto?> GetUserOrderByIdAsync(int userId, int orderId)
    {
        var userOrder = await _orderRepository.GetUserOrderByIdAsync(userId, orderId);
        return _mapper.Map<GetOrderDto>(userOrder);
    }

    public async Task<GetOrderDto?> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return _mapper.Map<GetOrderDto>(order);
    }

    public async Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDetails)
    {
        var orderEntity = _mapper.Map<Order>(orderDetails);
        orderEntity.OrderStatusId = 1;

        var result = await _orderRepository.InsertAsync(orderEntity);

        foreach (var productId in orderDetails.ProductIds)
        {
            await _orderProductRepository.InsertOrderProductAsync(result.Id, productId);
        }

        return _mapper.Map<GetOrderDto>(result);
    }

    public async Task UpdateOrderStatusAsync(GetOrdersWithProductAndUserDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(orderDto.Id);
        if (order is null)
            throw new NotFoundException($"Order with ID {orderDto.Id} not found");

        var status = await _statusViewService.GetStatusByIdAsync(orderDto.OrderStatusId++);
        if (status is null)
            return;

        order.OrderStatusId = status.Id;

        await _orderRepository.UpdateAsync(order);
    }

    public async Task<bool> UpdateOrderAsync(CreateOrderDto order)
    {
        var orderEntity = _mapper.Map<Order>(order);
        return await _orderRepository.UpdateAsync(orderEntity);
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null)
            return false;

        return await _orderRepository.DeleteAsync(order);
    }
}