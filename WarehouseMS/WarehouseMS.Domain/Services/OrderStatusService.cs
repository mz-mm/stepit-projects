using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderStatusDtos;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;
using WarehouseMS.Infrastructure.Repositories;

namespace WarehouseMS.Domain.Services;

public class OrderStatusService : IOrderStatusService
{

    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IMapper _mapper;

    public OrderStatusService(IOrderStatusRepository orderStatusRepository, IMapper mapper)
    {
        _orderStatusRepository = orderStatusRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetOrderStatusDto>> GetAllOrderStatusAsync()
    {
        var orderStatuses = await _orderStatusRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<GetOrderStatusDto>>(orderStatuses);
    }

    public async Task<GetOrderStatusDto?> GetOrderStatusByIdAsync(int orderStatusId)
    {
        var orderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);
        return _mapper.Map<GetOrderStatusDto>(orderStatus);
    }

    public async Task<GetOrderStatusDto> CreateOrderStatusAsync(CreateOrderStatusDto orderStatus)
    {
        var orderStatusEntity = _mapper.Map<OrderStatus>(orderStatus);
        var result = await _orderStatusRepository.InsertAsync(orderStatusEntity);

        return _mapper.Map<GetOrderStatusDto>(result);
    }

    public async Task<bool> UpdateOrderStatusAsync(CreateOrderStatusDto orderStatus)
    {
        var orderStatusEntity = _mapper.Map<OrderStatus>(orderStatus);
        var result = await _orderStatusRepository.UpdateAsync(orderStatusEntity);

        return result;
    }

    public async Task<bool> DeleteOrderStatusAsync(int orderStatusId)
    {
        var orderStatus = await _orderStatusRepository.GetByIdAsync(orderStatusId);

        if (orderStatus is null)
            return false;

        var result = await _orderStatusRepository.DeleteAsync(orderStatus);

        return result;
    }
}