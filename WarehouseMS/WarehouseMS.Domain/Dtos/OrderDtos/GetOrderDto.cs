using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Dtos.OrderDtos;

public class GetOrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int TrackingId { get; set; }
    public int OrderStatusId { get; set; }
    public DateTime Date { get; set; }
}